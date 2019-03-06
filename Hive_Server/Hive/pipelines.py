# -*- coding: utf-8 -*-

# Define your item pipelines here
#
# Don't forget to add your pipeline to the ITEM_PIPELINES setting
# See: https://doc.scrapy.org/en/latest/topics/item-pipeline.html
import os
import csv
import codecs
import datetime
import asyncio
import shutil
import pandas as pd
import json
from Hive.DealRules import *
from Hive.ConfigHandler import *
from threading import Thread
from os.path import realpath,dirname
from Hive.SQL import Mission_BLL,MissionInfo_BLL,Model,CSV2Mysql,CSV2Sqlserver

#写入本地文件
async def do_some_write(fp,item,dpF):
    data=[]
    if dpF:
        R_csvFile = open(fp, "r",encoding='utf-8')
        reader = csv.DictReader(R_csvFile)
        data=[row[dpF] for row in  reader]#获取去重字段的所有值
        print('data of dpf:',data)
        R_csvFile.close()
    if not dpF or item[dpF] not in data:#如果值不存在，或者不需要去重
        print('Item of dpf:',item)
        csvFile = open(fp, "a",encoding='utf-8')
        writer = csv.writer(csvFile)
        writer.writerow(item.values()) 
        csvFile.close()

#推入数据库:dp为根目录,
async def put_sql_data(sqlConfig,result_step,dp,_ste,_ste_key):
    print('Result:推数据中。。。。')
    path=dp+'/dataFlag.json'
    table_name=dp.split('/')[-1]#拿到spider.name
    with open(path,'r',encoding='utf-8') as f:
        dataFlag=json.loads(f.read())#各个文件的推送标志位
    #推数据至数据库
    for oneResult in result_step:
        pushflag=True
        step_num=oneResult.split('_')[1]#步骤名
        step_flag=dataFlag[step_num]
        rs_fp=dp+'/'+oneResult#得到结果文件路径
        csv_data_all=pd.read_csv(rs_fp)#读取文件所有内容
        if step_flag < len(csv_data_all):#该文件未推送完毕
            csv_data_rs=csv_data_all.loc[step_flag:]
            for mdResult in _ste[step_num]:#得到需要关联的步骤
                md_key=_ste_key[step_num][_ste[step_num].index(mdResult)]#得到关联字段
                md_fp=dp+'/'+'step_'+mdResult+'_middle'+'.csv'#得到关联文件路径
                
                if os.path.exists(md_fp):
                    
                    csv_data_md=pd.read_csv(md_fp)#读取文件所有内容
                    csv_data_rs=pd.merge(csv_data_rs,csv_data_md,on=md_key)#按照关联字段合并数据
                else:#说明需要关联的文件尚未生成好，跳过此次同步
                    pushflag=False
                    break
            if pushflag:
                
                dataFlag[step_num]=step_flag+len(csv_data_rs)#记录推送位置
                data=dict(csv_data_rs)# 处理数据格式
                datas=list(map( dict, zip(*([(key, val) for val in data[key]] for key in data.keys()))))
                table_name=table_name+'_step_'+step_num+'_result'#表名
                print("##################datas#########################")
                #print('datas',datas)
                print('dataFlag',dataFlag)
                table_keys=list(data.keys())#键名
                #数据库交互
                sqltype=sqlConfig["sqltype"]
                if sqltype=='MySql':
                    print('同步数据至MySql')
                    cm=CSV2Mysql.CSV2Mysql(sqlConfig)
                    cm.create_table(table_keys,table_name)
                    cm.insert_MoreData(table_name,datas)
                elif sqltype=='SqlServer':
                    print('同步数据至SqlServer')
                    cs=CSV2Sqlserver.CSV2Sqlserver(sqlConfig)
                    cs.create_table(table_keys,table_name)
                    cs.insert_MoreData(table_name,datas)
                #更新标志位
                jsondata = json.dumps(dataFlag, ensure_ascii=False)
                with open(path, 'wb') as f:
                    f.write(jsondata.encode('utf8'))
        


class HivePipeline(object):
    def process_item(self, item, spider):
        return item

class Pipeline_ToCSV(object):  
    def __init__(self):
        print("初始化Pipeline_ToCSV")
        self.failCount_savedata=0       
        self.MI=MissionInfo_BLL.MissionInfo()
        self.new_loop = asyncio.new_event_loop()
        self.lock_push=False
        #推数据库相关
        self.sTe={}#一个字典，包含数据需要关联的文件,如：{'4':['2','3']}，将step2,step3的数据关联到4
        self.sTe_key={}#一个字典，包含数据需要关联的文件的关联字段，需要结合self.sTe使用,如：{'4':['a','b']}，将step2,step3的数据关联到4，关联字段分别为a，b 
        
        #写数据时每一步对应的去重字段
        self.dpFields={}      
        t = Thread(target=self.start_loop, args=(self.new_loop,))
        t.setDaemon(True)    # 设置子线程为守护线程
        t.start()

    def _relevance(self,rules):#提取关联信息
        for key in rules.keys():
            rule=rules[key]
            for ru in rule:
                if ru.relevance:#如果存在关联字段
                    rels = ru.relevance
                    print(rels)
                    if rels:
                        for rel in rels:#rel:[包含规则，关联字段，解析归则，关联步骤]
                            e=rel[3].split('t')[1]
                            s=rel[3].split('t')[0]
                            k=rel[1]
                            print(e,s,k)
                            if e not in self.sTe.keys():
                                self.sTe[e] =[s]
                                self.sTe_key[e]=[k]
                            else:
                                if s not in self.sTe[e]:#去除重复的
                                    self.sTe[e].append(s)
                                    self.sTe_key[e].append(k)

    def get_DpField(self,rules):
        for key in rules.keys():
            rule=rules[key]
            for ru in rule:
                #print('-------------dpf',ru.dpField)
                if key not in self.dpFields.keys():
                    self.dpFields[key]=ru.dpField
                elif ru.dpField not in self.dpFields[key]:#去除重复的
                    self.dpFields[key]=ru.dpField


    def start_loop(self,loop):
        asyncio.set_event_loop(loop)
        loop.run_forever()

    def open_spider(self, spider):
        print('进入key')
        self._relevance(spider.rules)#创建最终结果关联信息
        self.get_DpField(spider.rules)#创建去重字段信息
        print('--------------------sTe',self.sTe)
        print('--------------------dpField',self.dpFields)
        #当任务被开启时，往missioninfo表中插入默认数据
        self.m=Mission_BLL.Mission()
        try:
            self.sqlConfig=get_Sql(spider.name)
        except Exception as e:
            spider.logger.info('%s异常,pipeline获取sqlconfig！%s'%(spider.name,e))
            spider.EI_BLL._update_exceptionInfo('%s异常,pipeline获取sqlconfig！%s'%(spider.name,e))
        self.cm=CSV2Mysql.CSV2Mysql(self.sqlConfig)
        self.cs=CSV2Sqlserver.CSV2Sqlserver(self.sqlConfig)

        _id=self.m._select_ID(spider.name)[0][0]
        self.st= datetime.datetime.now()
        MID=Model.missionInfo(spider.name,0,0,0,0,0,0,self.st,self.st,_id)
        
        flag=self.MI._get_missionInfo(spider.name)
        if  flag:#该信息已经存在了
            self.MI._delete(spider.name)#删除该条记录
        self.MI._add_missionInfo(MID)
        #创建数据存储文件夹
        dp=dirname(realpath(__file__))+'/Honey_CSV/'+spider.name
        if not os.path.exists(dp):
            print('创建存储目录',dp)
            os.mkdir(dp)
        else:
            print(dp,'存储目录已经存在！')

        #初始化包含关联标志位的json文件
        
        item_sTe=dict.fromkeys(spider.rules.keys(),0)
        jsondata = json.dumps(item_sTe, ensure_ascii=False)
        fp=dp+'/dataFlag.json'
        print('写入dataFlag.json',fp)
        with open(fp, 'wb') as f:
            f.write(jsondata.encode('utf8'))
    
    def process_item(self,item,spider):
        try:
            #写入本地CSV
            print('正在写入csv：'+spider.name)
            dp=dirname(realpath(__file__))+'/Honey_CSV/'+spider.name
            if not os.path.exists(dp):
                print('创建存储目录',dp)
                os.mkdir(dp)
            else:
                print(dp,'存储目录已经存在！')
            #csv文件的位置,无需事先创建
            store_file = dp+'/'+spider.honeyName+'.csv'
            if  not os.path.exists(store_file):
                # 写入数据
                csvFile = open(store_file, "a",encoding='utf-8')
                writer = csv.writer(csvFile)
                print('写入键')
                writer.writerow(item.keys())
                writer.writerow(item.values())  
            else: 
                #获得该步的去重字段
                dpF=self.dpFields[int(spider.honeyName.split('_')[1])]
                print('传入dpf',dpF)
                asyncio.run_coroutine_threadsafe(do_some_write(store_file,item,dpF), self.new_loop)#异步写入,最后一位为需要去重的字段
        except Exception as e:
            print ("保存解析数据至本地异常！",e)
            self.failCount_savedata=self.failCount_savedata+1
            self.MI._update_SaveData(self.failCount_savedata,spider.name)

        #判断时间间隔，大于5s，同步数据
        end_t=datetime.datetime.now()
        val=end_t-self.st
        if val.seconds>10 :
            #self.lockflga=False
            self.st=end_t#重置起始时间
            #middle_step=[]
            result_step=[]
            #查找路径下文件数
            for f_name in os.listdir(dp+'/'):
                #print(f_name)
                #找到需要关联的CSV文件
                #if 'middle' in f_name:
                    #middle_step.append(f_name.replace('.csv',''))
                #找到最终结果文件
                if 'result' in f_name:
                    result_step.append(f_name)
            if self.lock_push:#技术不成熟，暂不开放基于协程的异步io         
                asyncio.run_coroutine_threadsafe(put_sql_data(self.sqlConfig,result_step,dp,self.sTe,self.sTe_key), self.new_loop)#推入数据库   
            else:#同步阻塞io
                self.old_push_data(self.sqlConfig,result_step,dp,self.sTe,self.sTe_key)
        return item

    def close_spider(self,spider):
        try:
            #将csv文件移动至备份文件夹
            dp=dirname(realpath(__file__))+'/Honey_CSV/'+spider.name+'/'
            des=str(datetime.datetime.now())+'_bak'
            os.mkdir(dp+des)
            for f_name in os.listdir(dp):
                if '.csv' in f_name or '.json' in f_name:
                    shutil.move(dp+f_name,dp+des+'/')
            #停止异步循环
            self.new_loop.stop()
        except Exception as e:
            spider.logger.info('关闭%s异常！%s'%(spider.name,e))
            spider.EI_BLL._update_exceptionInfo(spider.name,str(e))
        #爬虫退出，修改数据库任务状态
        sta=self.m._select_Status(spider.name)[0][0]
        if(sta =="working"):#确认为正在运行的任务
            self.m._change_Status(spider.name,'complete')

    '''
    def push_data(self,sqlConfig,result_step,dp,_ste,_ste_key):
        print("开始推数据")
    '''


    def old_push_data(self,sqlConfig,result_step,dp,_ste,_ste_key):
        print('Result:推数据中。。。。')
        path=dp+'/dataFlag.json'
        table_name=dp.split('/')[-1]#拿到spider.name
        with open(path,'r',encoding='utf-8') as f:
            dataFlag=json.loads(f.read())#各个文件的推送标志位
        #推数据至数据库
        for oneResult in result_step:
            pushflag=True
            step_num=oneResult.split('_')[1]#步骤名
            step_flag=dataFlag[step_num]
            rs_fp=dp+'/'+oneResult#得到结果文件路径
            csv_data_all=pd.read_csv(rs_fp)#读取文件所有内容
            if step_flag < len(csv_data_all):#该文件未推送完毕
                csv_data_rs=csv_data_all.loc[step_flag:]
                if step_num in _ste.keys():#如果该步骤需要关联
                    for mdResult in _ste[step_num]:#得到需要关联的步骤
                        md_key=_ste_key[step_num][_ste[step_num].index(mdResult)]#得到关联字段
                        md_fp=dp+'/'+'step_'+mdResult+'_middle'+'.csv'#得到关联文件路径
                        try:
                            if os.path.exists(md_fp):
                                csv_data_md=pd.read_csv(md_fp)#读取文件所有内容
                                csv_data_md.pop('files')#删除多余的下载文件键
                                csv_data_temp=pd.merge(csv_data_rs,csv_data_md,on=md_key)#按照关联字段合并数据
                            else:#说明需要关联的文件尚未生成好，跳过此次同步
                                pushflag=False
                                break
                            if not csv_data_temp.empty:#如果不为空，说明关联正常，如果为空，说明关联失败，只推原数据，不能不推数据
                                csv_data_rs=csv_data_temp
                        except Exception as e:
                            print("合并异常%s"%(e))
                if pushflag:
                    dataFlag[step_num]=step_flag+len(csv_data_rs)#记录推送位置
                    csv_data_rs=csv_data_rs.astype(object).where(pd.notnull(csv_data_rs), None)#去除缺页项nan数据
                    #csv_data_rs.rename(columns={'files_x':'files'}, inplace=True)#替换下载文件的键名
                    data=dict(csv_data_rs)# 处理数据格式
                    datas=list(map( dict, zip(*([(key, val) for val in data[key]] for key in data.keys()))))
                    table_name=table_name+'_step_'+step_num+'_result'#表名
                    print("##################datas#########################")
                    #print('datas',datas)
                    print('dataFlag',dataFlag)
                    table_keys=list(data.keys())#键名
                    #数据库交互
                    sqltype=sqlConfig["sqltype"]
                    if sqltype=='MySql':
                        print('同步数据至MySql')
                        
                        self.cm.create_table(table_keys,table_name)
                        self.cm.insert_MoreData(table_name,datas)
                    elif sqltype=='SqlServer':
                        print('同步数据至SqlServer')
                        
                        self.cs.create_table(table_keys,table_name)
                        self.cs.insert_MoreData(table_name,datas)
                    #更新标志位
                    jsondata = json.dumps(dataFlag, ensure_ascii=False)
                    with open(path, 'wb') as f:
                        f.write(jsondata.encode('utf8'))
        
