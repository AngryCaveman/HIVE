import scrapy
import random
import datetime
import logging
from lxml import etree
from Hive.ConfigHandler import *
from scrapy.http import Request,FormRequest,HtmlResponse
from Hive.loaders import *
from Hive.DealRules import *
from scrapy_splash import SplashRequest
from Hive.SQL import MissionInfo_BLL,ExceptionInfo_BLL

        
class Bee(scrapy.Spider):
    name = 'Bee'
    #初始化函数
    def __init__(self,BeeName,*args,**kwargs):
        print('正在初始化基类')
        
        #错误记录字段
        self.fC_getdata=0
        self.fC_linkre=0
        self.mi=MissionInfo_BLL.MissionInfo()
        self.EI_BLL=ExceptionInfo_BLL.ExceptionInfo()#异常信息
        #基本
        self.honeyName='NoName'#存储数据的文件名
        self.name=BeeName
        try:
            config=get_common(self.name)
        except Exception as e:
            self.EI_BLL._update_exceptionInfo(self.name,str(e))
        self.config=config
        self.start_urls=config.get('start_urls')
        self.allowed_domains=config.get('allowed_domains')
        #加载自定义配置
        #print(dict(get_common(BeeName).get('settings')))
        try:
            self.custom_settings=dict(get_common(BeeName).get('settings'))
        except Exception as e:
            self.EI_BLL._update_exceptionInfo(self.name,str(e))
        #step控制相关
        self.custom_way=self.parse
        try:
            self.rules=createrules(self.name)
        except Exception as e:
            self.EI_BLL._update_exceptionInfo(self.name,str(e))
        self.stepcount=len(self.rules) #记录步骤总数
        print('总步数：',self.stepcount)
        #翻页
        self.PageCount=config.get('PageCount')
        self.seen = set()#防止重复
        #模拟登陆相关
        self.away=config.get('away')#post or get 默认为get 提供配置
        self.cookieflag=1 #是否携带cookie,True:表示使用授权后的cookie访问需要登录查看的页面
        self.usecookies=False #是否启用cookies
        if(self.away=='post'):
            self.login_url=config.get('login_url')#提供配置
            if config.get('cookies'):
                self.usecookies=True
                self.cookies=config.get('cookies')
                #print (self.cookies)
            if config.get('account'):
                self.account=config.get('account')
        #判断第一个页面是否需要动态解析
        firstRules=self.rules[1]
        self.firstDynamic=False
        for firstRule in firstRules:
            if firstRule.dynamic:
                self.firstDynamic=True
        super(Bee,self).__init__(*args,**kwargs)
        
    @classmethod
    def from_crawler(cls, crawler, *args, **kwargs):
        spider = cls(*args, **kwargs)
        spider._set_crawler(crawler)
        return spider

    def start_requests(self):
        if(self.away=='get'):    
            yield from super().start_requests()
        if(self.away=='post'):
            if self.usecookies:
                yield from super().start_requests()
            else:
                if self.firstDynamic:
                    self.logger.info('splash post')
                    yield SplashRequest(self.login_url,args={'timeout':15},callback=self.login,dont_filter=True)
                else:
                    yield Request(self.login_url,callback=self.login,dont_filter=True)

    def make_requests_from_url(self, url):
        """ This method is deprecated. """
        #return Request(url, dont_filter=True,callback=self.content_parse_item)
        
        if self.usecookies:
            cks=random.choice(self.cookies)#此处应该随机选择cookie
            print('cks',cks)
            if self.firstDynamic:
                self.logger.info('cookies,splash')
                return SplashRequest(url,args={'timeout':15},callback=self.custom_way,dont_filter=True,cookies=cks,meta={'step':1})
            else:
                return Request(url,dont_filter=True,callback=self.custom_way,cookies=cks,meta={'step':1})
        else:
            if self.firstDynamic:
                self.logger.info('get,splash')
                return SplashRequest(url,args={'timeout':15},callback=self.custom_way,dont_filter=True,meta={'cookiejar':self.cookieflag,'step':1})
            else :
                return Request(url, dont_filter=True,callback=self.custom_way,meta={'cookiejar':self.cookieflag,'step':1})
    
    #默认解析函数
    def parse(self,response):
        print('默认解析函数！进入流程！')
        return self.Step_Controller(response)
        

    #目标页面解析函数
    def content_parse_item(self,response):
        try:
            print('解析页面内容！'+response.url)
            print('step',response.meta['step'])
            rule=response.meta['rule']
            if response.meta['step']==self.stepcount:
                item = self.config.get('item')
                cls=get_Item(item.get('class'))
                loader=item.get('loader')
                allItem=item.get('attrs').items()
            else :
                item=rule.item
                cls=rule.cls
                loader='DbReLoader'
                allItem=item.items()
            if item:
                self.honeyName='step_'+str(response.meta['step'])+'_result'
                loader = eval(loader)( cls,response=response)
                # 动态获取属性配置
                for key, value in allItem :
                    for extractor in value:
                        if extractor.get('method') == 'xpath':
                            loader.add_xpath(key, *extractor.get('args'), **{'re': extractor.get('re')})
                        if extractor.get('method') == 'css':
                            loader.add_css(key, *extractor.get('args'), **{'re': extractor.get('re')})
                        if extractor.get('method') == 'value':
                            loader.add_value(key, *extractor.get('args'), **{'re': extractor.get('re')})
                        if extractor.get('method') == 'attr':
                            loader.add_value(key, getattr(response, *extractor.get('args')))
                #下载出图片以外的文件
                if 'file_urls' in cls:
                    url=loader.item['file_urls']
                    url=response.urljoin(url)
                    loader._replace_value('file_urls',url)
                elif 'image_urls' in cls:#下载图片文件
                    url=loader.item['image_urls']
                    url=response.urljoin(url)
                    loader._replace_value('image_urls',url)  
                #添加固定字段值
                loader.item['SourceUrl']=response.url#来源地址
                #loader.item['SourceUrl']=[response.url.replace('https://www.worldscientific.com','')]
                #loader.item['AddTime']=datetime.datetime.now().strftime('%Y-%m-%d %H:%M:%S')#添加时间
            yield loader.load_item()
            #print(loader.load_item())
            #print(cls)
        except Exception as e:
            self.logger.info("解析内容失败！%s",e)
            self.fC_getdata=self.fC_getdata+1
            item={}#返回空值
            self.mi._update_dataFailCount(self.fC_getdata,self.name)
            yield item
         
    #------------------翻页--------------------
    #传入翻页规则
    def _Paging(self,rule,step,response):
        print('正在翻页',step)
        paging=rule.paging
        dynamic=rule.dynamic
        links = [lnk for lnk in paging.extract_links(response) if lnk not in self.seen]
        if links:
            for link in links:
                self.seen.add(link)
                url=response.urljoin(link.url)
                yield self._build_request(url,step,dynamic)
        else:
            self.logger.info("Paging error:No links")
            yield []

    #----------中间页面传参数据生成-----------
    def middle_parse_item(self,response):
        partItem={}
        try:
            rule=response.meta['rule']
            rel=rule.relevance[0]
            x_rule=rel[0]#包含规则
            self.logger.info('xpath 规则:%s'%x_rule)
            P_Htmls=response.selector.xpath(x_rule).extract()
            self.honeyName='step_'+str(response.meta['step'])+'_middle'
            #开始分块解析，形成对应关系
            item=rule.item
            cls=rule.cls
            allItem=item.items()
            for part in P_Htmls:
                html = etree.HTML(part)
                #示例'k': 'abstract', 'v': [{'method': 'xpath', 'args': ["//li//a[@title='Abstract']/@href"]}]
                for key,value in allItem:
                    for extractor in value:
                        if extractor.get('method') =='xpath':
                            print(extractor.get('args')[0])
                            temp_item=html.xpath(extractor.get('args')[0])
                            if (temp_item):#有数据
                                if(key=="SourceUrl"):#url关联字段,需要处理成正常的url才能与目标数据关联
                                    self.logger.info(temp_item[0])
                                    partItem[key]=str(response.urljoin(temp_item[0]))
                                else:
                                    partItem[key]=temp_item[0]#如果解析到多组数据只取第一个 
                yield partItem
        except Exception as e:
            self.logger.info("%s中间页面解析错误！%s"%(response.url,e))
            yield partItem
    #----------------流程控制------------------
    #构造请求
    def _build_request(self, url, step,dynamic):
        self.logger.info("查看url："+url)
        if url not in self.seen:#去重
            if dynamic:
                #print('splash step:',step)
                r = SplashRequest(url,args={'timeout':15},callback=self.Step_Controller,dont_filter=True)
            else:
               
                r = Request(url=url, callback=self.Step_Controller,dont_filter=True)
            r.meta.update(step=step)
            self.seen.add(url)
        else:
            r=None
        return r

    #中间页面解析函数
    def url_parse_item(self,response):
        try:
            step=response.meta['step']
            rule=response.meta['rule']
            hrefs=[]
            print('解析页面链接！',response.url)
            print('step:',step)
            reType=rule.linkRe.split(':')[0]
            linkRe=rule.linkRe.split(':')[1]
            if rule.customLink:#如果存在自定义链接，则不匹配解析规则
                hrefs=rule.customLink
            else:
                #支持正则和xpath两种方式
                if reType == 'xpath':
                    hrefs=response.selector.xpath(linkRe).extract()
                elif reType=='re':
                    hrefs=response.selector.re(linkRe).extract()
            print(hrefs)
        except Exception as e:
            self.logger.info ("解析跳转规则失败！%s",e)
            self.fC_linkre=self.fC_linkre+1
            self.mi._update_linkreFailCount(self.fC_linkre,self.name)
        self.logger.info ("查看hrefs"+str(hrefs))
        self.logger.info ("查看linkre"+str(linkRe))
        #追踪访问
        for url in hrefs:
            url =response.urljoin(url)
            try:
                yield  self._build_request(url,step+1,rule.dynamic)
            except Exception as e:
                self.logger.info ("跳转失败！%s",e)
                self.fC_linkre=self.fC_linkre+1
                self.mi._update_linkreFailCount(self.fC_linkre,self.name)
                item={}
                yield item
         
    #流程控制函数
    def Step_Controller(self,response):
        step=response.meta['step']
        _Rules=self.rules[step]

        for rule in _Rules:
            response.meta['rule']=rule
            if rule.paging !='':
                #print('paging',rule.paging)
                for request_or_item in self._Paging(rule,step,response):
                    yield request_or_item
            if rule.follow:
                for request_or_item in self.url_parse_item(response):
                    yield request_or_item
            else:
                if rule.relevance:#该页面存在关联字段
                    for request_or_item in self.middle_parse_item(response):
                        yield request_or_item
                else:
                    for request_or_item in self.content_parse_item(response):
                        yield request_or_item

    #-----------------模拟登陆---------------
    def login(self,response):
        #登录页面的解析函数，构造FormRequest对象提交表单
        print('正在模拟登陆:',response.url)
        fd1=self.account
        #-----------------新站点-------------------
        sel=response.xpath('//form//input[@type="hidden"]')
        #print (sel)
        fd2=dict(zip(sel.xpath('./@name').extract(),sel.xpath('./@value').extract()))
        fd={}
        fd.update(fd1)
        fd.update(fd2)
        print(fd)
        #Cookie1 = response.headers.getlist('Set-Cookie')
        #print('登陆前cookies：',Cookie1)
        #------------------------------------------
        yield  FormRequest.from_response(response,formdata=fd,callback=self.parse_login)

    def parse_login(self,response):
        #登录成功后，继续爬取start_urls中的页面
        flag=response.url
        Cookies = response.request.headers.getlist('Cookie')
        print('登陆后:',Cookies)
        #print(response.text)
 
        if Cookies:
            self.cookieflag=True
            #重置首页
            self.start_urls=[response.url]
            #print(self.start_urls)
            yield from super().start_requests()
            #yield self.start_requests()
       

    
    
