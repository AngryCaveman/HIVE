import json
import sys
import os
import time
from multiprocessing import Pool
from scrapy.utils.project import get_project_settings
from Hive.spiders.Bee import Bee
from Hive.ConfigHandler import get_common
from scrapy.crawler import  CrawlerProcess
from Hive.SQL import Mission_BLL,SpiderInfo_BLL,HoneyInfo_BLL
from Hive import ConfigHandler
from scrapy.crawler import CrawlerRunner
from twisted.internet import reactor
from scrapy.utils.log import (LogCounterHandler, configure_logging)
filepath="./ServerConfigs/ServerConfigs.json"
with open(filepath,'r',encoding='utf-8') as f:#解析配置文件
    config=json.loads(f.read())
max_Task=config['max_Task']#最大任务数
Pro_count=1#线程数
M_BLL=Mission_BLL.Mission()#任务表
PRI=["high","common","low"]#优先级
SI_BLL=SpiderInfo_BLL.SpiderInfo()#配置表
HI_BLL=HoneyInfo_BLL.HoneyInfo()#数据库信息       
#获取任务    
def _Get_Mission():
    for pri in PRI:
        Missions=M_BLL._select_Mission(max_Task,pri)
        print(pri)
        if Missions:
            print(Missions)
            return Missions
        elif not Missions and pri=="low":
            return "NoMissions"

#获取Sql配置
def _get_Sql(Mission):
    SqlData=HI_BLL._get_spiderInfo(Mission)
    return SqlData[0]#

#字符串转元祖
def _StrToTuble(to_tuble):
    t_datas=to_tuble[1:-1].split(",")
    data=(t_datas[0],t_datas[1].strip(),[t_datas[2]])
    return data

#数据转步骤
def _StrToStep(to_Step):
    result=[]
    StepData=json.loads(to_Step)
    StepData["step"]=int(StepData["step"])
    StepData["linkRe"]="xpath:"+StepData["linkRe"]
    if StepData["dynamic"]=="True":
        StepData["dynamic"]=True
    else:
        StepData["dynamic"]=False

    if StepData["follow"]=="True":
        StepData["follow"]=True
    else:
        StepData["follow"]=False

    if StepData["item"]=="None" or StepData["item"]=="":
        StepData["item"]=None
        result.append(StepData)
    else:
        items=[]
        cls=[]
        AllItem=StepData["item"].split(":")
        AllCls=StepData["cls"].split(":")
        StepData["item"]=None
        StepData["cls"]=""
        result.append(StepData)
        #新的步骤
        newStep={}
        newStep["step"]=StepData["step"]
        newStep["paging"]=StepData["paging"]
        newStep["dynamic"]=StepData["dynamic"]
        newStep["follow"]=False
        newStep["linkRe"]=""
        for item in AllItem:
            items.append(_StrToTuble(item))
        for cl in AllCls:
            clTemp=(cl,"")
            cls.append(clTemp)
        newStep["item"]=items
        newStep["cls"]=cls
        result.append(newStep)
    return result

#获取爬虫配置并解析
def _Analysis_SpiderConfig(mission):
    
    SpiderConfig=SI_BLL._get_spiderInfo(mission)[0]
    print(SpiderConfig)   
    #print (SpiderConfig)
    name = mission
    print(name)
    #爬虫通用配置
    contentdict = {}
    contentdict['spider'] = name
    contentdict['website'] = SpiderConfig[1]
    contentdict['FILES_STORE'] = SpiderConfig[2]
    contentdict['start_urls'] = SpiderConfig[3]
    contentdict['allowed_domains'] = SpiderConfig[4]
    contentdict['rules'] = name
    contentdict['class'] = name
    if SpiderConfig[7]=="去重":
        contentdict['loader'] = 'NewItemLoader'
    else:#不去重
        contentdict['loader'] = 'NoDbLoader'
    #设置get or post
    if SpiderConfig[7]=="get":
        contentdict['away']='get'
    else:#post
        contentdict['away']='get' 
        contentdict['login_url']=''
        contentdict['account']={}
        contentdict['cookies']={}
        #print("post 设置尚未添加！")
    print(contentdict)
    #爬虫解析对象配置
    #如果是图片,则为contentdict['IMAGES_STORE'],image_urls,images
    customdata = []
    cls = []
    cus_datas=SpiderConfig[11].split(':')
    cls_datas=SpiderConfig[12].split(':')
    for data in cus_datas:
        customdata.append(_StrToTuble(data))
    for c_data in cls_datas:
        clsTuble=(c_data,"")
        cls.append(clsTuble)
    print(customdata)
    print(cls)
    #解析流程配置
    RuleDatas=[]
    r_datas=SpiderConfig[13].split('#')
    for r_data in r_datas:
        #print(r_data)
        RuleDatas.extend(_StrToStep(r_data)) 
    print(RuleDatas)
    SqlData=_get_Sql(name)
    #生成配置文件
    
   
    print(SqlData)
    ConfigHandler.create_Configs(name,contentdict, customdata,cls,RuleDatas,SqlData)

#工作流程，线程调用
def Action_Flow(mission):
    pro_name=os.getpid()
    print(mission +" in process")
    print('process id：',pro_name)
    print(str(pro_name)+"===>>"+mission)
    #cus_set={"LOG_FILE":"/home/hive/桌面/Hive_Server/Hive/LOG/test02.log"}
    #配置文件生成完毕
    BeeName=mission#获取命令行参数，爬虫名，也为配置文件名
    custom_settings=get_common(BeeName)
    spider='Bee'
    project_settings=get_project_settings()
    settings=dict(project_settings.copy())
    #合并配置
    settings.update(custom_settings.get('settings'))
    runner =  CrawlerRunner(settings)
    configure_logging(runner.settings, True)
    d = runner.crawl(spider,**{'BeeName':BeeName})
    d.addBoth(lambda _: reactor.stop())
    #reactor.addSystemEventTrigger('before', 'shutdown', self.stop)
    reactor.addSystemEventTrigger('before', 'shutdown', runner.stop)
    reactor.run()  # blocking call
    #reactor.run() 
    return mission

def Action_CallBack(result):
    print("执行结果！",result)
    


if __name__=="__main__":
    Action_Flow("测试")

