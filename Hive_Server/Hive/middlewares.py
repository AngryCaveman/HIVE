# -*- coding: utf-8 -*-

# Define here the models for your spider middleware
#
# See documentation in:
# https://doc.scrapy.org/en/latest/topics/spider-middleware.html
from scrapy import signals
from scrapy.downloadermiddlewares.httpproxy import HttpProxyMiddleware
from scrapy.downloadermiddlewares.useragent import UserAgentMiddleware
from collections import defaultdict
from os.path import realpath,dirname
import os
import json
import random
import csv
import codecs
import datetime
import asyncio
from threading import Thread
from Hive.SQL import MissionInfo_BLL,Mission_BLL,CSV2Mysql,CSV2Sqlserver
from Hive.ConfigHandler import *
import pandas as pd
#------------NEW 统计每次访问的状态，更新至数据库---------------
async def do_some_write(fp,item):
    csvFile = open(fp, "a",encoding='utf-8')
    writer = csv.writer(csvFile)
    writer.writerow(item.values()) 
    csvFile.close()

async def listen_statu_mission(spider):
    m=Mission_BLL.Mission()
    mission_sta=m._select_Status(spider.name)[0][0]
    if mission_sta=='end':
        spider.crawler.engine.close_spider(spider, 'User order : Close %s'%spider.name)
        m._change_Status(spider.name,'complete')
    #elif mission_sta=='stop':
    #spider.crawler.engine.engine.pause()

#推入数据库
async def put_sql_data(sqlConfig,table_name,table_keys,items):
    print('LinkeRe:推数据中。。。。')
    sqltype=sqlConfig["sqltype"]
    if sqltype=='MySql':
        print('同步数据至MySql')
        cm=CSV2Mysql.CSV2Mysql(sqlConfig)
        cm.create_table(table_keys,table_name)
        cm.insert_MoreData(table_name,items)
    elif sqltype=='SqlServer':
        print('同步数据至SqlServer')
        cs=CSV2Sqlserver.CSV2Sqlserver(sqlConfig)
        cs.create_table(table_keys,table_name)
        cs.insert_MoreData(table_name,items)
        

class UpdatePagesMiddleware(object):
    '''
    更新completePages,failPages,runTime三个字段
    '''
    def __init__(self):
        self.failPages=0
        self.completePages=0
        self.mi=MissionInfo_BLL.MissionInfo()
        self.st=datetime.datetime.now()
        self.new_loop = asyncio.new_event_loop()
        
        self.sqlFlag=0
        t = Thread(target=self.start_loop, args=(self.new_loop,))
        t.setDaemon(True)    # 设置子线程为守护线程
        t.start()

    def start_loop(self,loop):
        asyncio.set_event_loop(loop)
        loop.run_forever()

    def process_response(self,request, response, spider):
        sta='failure'
        if response.status==403:
            spider.EI_BLL._update_exceptionInfo(spider.name,"访问链接%s失败，状态返回码：403"%(response.url))
        if response.status<400:
            self.completePages=self.completePages+1
            sta='success'
        else :
            self.failPages=self.failPages+1
            sta='failure'
        et= datetime.datetime.now()
        self.mi._update_Pages(self.completePages,self.failPages,et,spider.name)
        
        #将跳转过的连接存入本地
        try:
            item={}
            item={'MissionName':spider.name,'Url':response.url,'AddTime':datetime.datetime.now().strftime('%Y-%m-%d %H:%M:%S'),'Statu':sta}
            print('正在写入跳转连接：'+spider.name)
            dp=dirname(realpath(__file__))+'/Honey_CSV/'+spider.name
            store_file = dp+'/_LinkRe.csv'
            # 分批写入
            if  not os.path.exists(store_file):
                # 写入数据
                csvFile = open(store_file, "a",encoding='utf-8')
                writer = csv.writer(csvFile)
                print('写入键')
                writer.writerow(item.keys())
                writer.writerow(item.values())   
                csvFile.close()
            else:
                 asyncio.run_coroutine_threadsafe(do_some_write(store_file,item), self.new_loop)    
        except Exception as e:
            print ("保存跳转链接异常！",e)
 
        #每5s检查任务状态
        val=et-self.st
        if val.seconds>8:
            self.st=et   
            asyncio.run_coroutine_threadsafe(listen_statu_mission(spider), self.new_loop) 
            sqlConfig=get_Sql(spider.name)
            print(sqlConfig)
            #推数据至数据库中
            csv_data_all=pd.read_csv(store_file)#读取文件所有内容
            csv_data_rs=csv_data_all.loc[self.sqlFlag:]
            self.sqlFlag=self.sqlFlag+len(csv_data_rs)#更新截取位置
            data=dict(csv_data_rs)# 处理数据格式
            datas=list(map( dict, zip(*([(key, val) for val in data[key]] for key in data.keys()))))
            table_keys=list(data.keys())
            table_name=spider.name+'_LinkRe'
            asyncio.run_coroutine_threadsafe(put_sql_data(sqlConfig,table_name,table_keys,datas), self.new_loop)
        return response
        
        

#----------------------NEW 随机Agent---------------------------
class RandomAgentMiddleware(UserAgentMiddleware):
    '''
    设置User-Agent
    '''
 
    def __init__(self, user_agent):
        self.user_agent = user_agent
 
    @classmethod
    def from_crawler(cls, crawler):
        return cls(
            user_agent=crawler.settings.get('USER_AGENTS_LIST')
        )
 
    def process_request(self, request, spider):
        agent = random.choice(self.user_agent)
        request.headers['User-Agent'] = agent

 
#----------------------NEW 随机代理IP---------------------------
class RandomHeaderMiddleware(HttpProxyMiddleware):
    def __init__(self,auth_encoding='latin-1',proxy_list_file=None):
        #处理随机代理IP
        if not proxy_list_file:
            #raise NotConfigured
            print('NotConfigured')
        self.auth_encoding=auth_encoding
        #分别用两个列表维护HTTP和HTTPS的代理
        self.proxies=defaultdict(list)
        #从json文件中读取代理服务器信息，填入self.proxies
        with open(proxy_list_file)as f:
            proxy_list=json.load(f)
            for proxy in proxy_list:
                scheme=proxy['proxy_scheme']
                url=proxy['proxy']
                self.proxies[scheme].append(self._get_proxy(url,scheme))

    @classmethod
    def from_crawler(cls, crawler):
        #从配置文件中读取用户验证信息的编码
        auth_encoding=crawler.settings.get('HTTPPROXY_AUTH_ENCODING','latain-1')
        #从配置文件中读取代理服务器列表文件（json）的路径
        proxy_list_file=crawler.settings.get('HTTPPROXY_PROXY_LIST_FILE')
        return cls(auth_encoding,proxy_list_file)

    def _set_proxy(self, request, scheme):
        #随机选择一个代理
        creds,proxy=random.choice(self.proxies[scheme])
        request.meta['proxy']=proxy
        print('代理IP：',proxy)
        #print(creds)
        if creds:
            request.headers['Proxy-Authorization']=b'Basic'+cred
   
    #访问异常时，更换代理ip重新访问
    def process_exception(request, exception, spider):
        #print(self.proxies)
        print('正在更换代理ip')
        self._set_proxy(request,'http')
        return response
    
    




class HiveSpiderMiddleware(object):
    # Not all methods need to be defined. If a method is not defined,
    # scrapy acts as if the spider middleware does not modify the
    # passed objects.

    @classmethod
    def from_crawler(cls, crawler):
        # This method is used by Scrapy to create your spiders.
        s = cls()
        crawler.signals.connect(s.spider_opened, signal=signals.spider_opened)
        return s

    def process_spider_input(self, response, spider):
        # Called for each response that goes through the spider
        # middleware and into the spider.

        # Should return None or raise an exception.
        return None

    def process_spider_output(self, response, result, spider):
        # Called with the results returned from the Spider, after
        # it has processed the response.

        # Must return an iterable of Request, dict or Item objects.
        for i in result:
            yield i

    def process_spider_exception(self, response, exception, spider):
        # Called when a spider or process_spider_input() method
        # (from other spider middleware) raises an exception.

        # Should return either None or an iterable of Response, dict
        # or Item objects.
        pass

    def process_start_requests(self, start_requests, spider):
        # Called with the start requests of the spider, and works
        # similarly to the process_spider_output() method, except
        # that it doesn’t have a response associated.

        # Must return only requests (not items).
        for r in start_requests:
            yield r

    def spider_opened(self, spider):
        spider.logger.info('Spider opened: %s' % spider.name)


class HiveDownloaderMiddleware(object):
    # Not all methods need to be defined. If a method is not defined,
    # scrapy acts as if the downloader middleware does not modify the
    # passed objects.

    @classmethod
    def from_crawler(cls, crawler):
        # This method is used by Scrapy to create your spiders.
        s = cls()
        crawler.signals.connect(s.spider_opened, signal=signals.spider_opened)
        return s

    def process_request(self, request, spider):
        # Called for each request that goes through the downloader
        # middleware.

        # Must either:
        # - return None: continue processing this request
        # - or return a Response object
        # - or return a Request object
        # - or raise IgnoreRequest: process_exception() methods of
        #   installed downloader middleware will be called
        return None

    def process_response(self, request, response, spider):
        # Called with the response returned from the downloader.

        # Must either;
        # - return a Response object
        # - return a Request object
        # - or raise IgnoreRequest
        return response

    def process_exception(self, request, exception, spider):
        # Called when a download handler or a process_request()
        # (from other downloader middleware) raises an exception.

        # Must either:
        # - return None: continue processing this exception
        # - return a Response object: stops process_exception() chain
        # - return a Request object: stops process_exception() chain
        pass

    def spider_opened(self, spider):
        spider.logger.info('Spider opened: %s' % spider.name)
