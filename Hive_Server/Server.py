import json
import time
import logging
from ThreadPool import *
from Hive.SQL import Mission_BLL,SpiderInfo_BLL,HoneyInfo_BLL,ExceptionInfo_BLL,Model
from Hive import ConfigHandler


class ServerPool:
    def __init__(self,filepath):
        with open(filepath,'r',encoding='utf-8') as f:#解析配置文件
            self.config=json.loads(f.read())
        self.max_Task=self.config['max_Task']#最大任务数
        self.thread_count=self.config['threadCount']#线程数
        self.M_BLL=Mission_BLL.Mission()#任务表
        self.SI_BLL=SpiderInfo_BLL.SpiderInfo()#配置表
        self.HI_BLL=HoneyInfo_BLL.HoneyInfo()#数据库信息  
        self.EI_BLL=ExceptionInfo_BLL.ExceptionInfo()#异常信息
        self.Exception_Model=Model.exceptionInfo("","")#异常模型
        self.PRI=["high","common","low"]#优先级
        self.T_Pool = ThreadPool(self.thread_count,self.max_Task)#任务线程池

        #日志信息
        self.loger=logging.getLogger() 
        self.loger.setLevel(logging.DEBUG)
        hfile=logging.FileHandler("./Hive/LOG/ServerLogInfo.log")
        formatter = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
        hfile.setFormatter(formatter)
        self.loger.addHandler(hfile)
        self.loger.info("Server初始化！")
        
    #获取任务    
    def _Get_Mission(self):
        time.sleep(5)#不能频繁去链接断开数据库
        for pri in self.PRI:
            try:
                Missions=self.M_BLL._select_Mission(self.max_Task,pri)
            except Exception as e:
                self.loger.error('获取任务失败！异常：%s！'%e)
                time.sleep(5)#等待5s再发起访问
                Missions=None
            #print(pri)
            if Missions:
                self.loger.info(Missions)
                return Missions
            elif not Missions and pri=="low":
                return "NoMissions"
            else:#查询异常时
                return None

    #字符串转元祖
    def _StrToTuple(self,to_tuble):
        data=()
        try:
            data=tuple(eval(to_tuble))
            if len(data)==3:#解析item需要转换最后一位为列表
                data=(data[0],data[1],[data[2]])
        except Exception as e:
            self.loger.error('转换元组异常！异常：%s！数据：%s'%(e,to_tuble))
            raise ValueError('转换元组异常！异常：%s！数据：%s'%(e,to_tuble)) from e
        return data

    #数据转步骤
    def _StrToStep(self,to_Step):
        result=[]
        try:
            StepData=json.loads(to_Step)
            StepData["step"]=int(StepData["step"])
            StepData["linkRe"]="xpath:"+StepData["linkRe"]#此处使linkRe只支持xpath，后续修改此处可以支持正则表达式
            if StepData["dynamic"]=="True":
                StepData["dynamic"]=True
            else:
                StepData["dynamic"]=False

            if StepData["follow"]=="True":
                StepData["follow"]=True
            else:
                StepData["follow"]=False

            #关联字段
            if StepData["relevance"]=="" or  StepData["relevance"]=="None":
                StepData["relevance"]=""  
            else:
                rels=StepData["relevance"]#如果需要关联多步，以冒号分隔
                rels=rels[1:-1].split("):(")
                allRel=[]
                for rel in rels:
                    self.loger.info('数据：%s'%(rel))
                    allRel.append(self._StrToTuple('('+rel+')'))
                    #allRel.append(self._StrToTuple(rel))
                StepData["relevance"]=allRel
     
            #自定义中间链接
            if StepData["customLink"]=="" or  StepData["customLink"]=="None":
                StepData["customLink"]="" 
            else:
                StepData["customLink"]=StepData["customLink"].split('|')#拆分成数组
        
            #去重字段
            if StepData["dpField"]=="" or StepData["dpField"]=="None":
                StepData["dpField"]="" 

            #添加Item
            if StepData["item"]=="None" or StepData["item"]=="":
                StepData["item"]=""
                result.append(StepData)
            else:
                items=[]
                cls=[]
                AllItem=StepData["item"]
                AllItem=AllItem[1:-1].split("):(")
                AllCls=StepData["cls"].split(":")
                #StepData["relevance"]=None
                StepData["item"]=""
                StepData["cls"]=""
                result.append(StepData)
                #新的步骤
                newStep={}
                newStep["step"]=StepData["step"]
                newStep["paging"]=StepData["paging"]
                newStep["dynamic"]=StepData["dynamic"]
                newStep["relevance"]=StepData["relevance"]
                newStep["customLink"]=StepData["customLink"]
                newStep["dpField"]=StepData["dpField"]
                newStep["follow"]=False
                newStep["linkRe"]=""
                for item in AllItem:
                    items.append(self._StrToTuple('('+item+')'))
                for cl in AllCls:
                    clTemp=(cl,"")
                    cls.append(clTemp)
                newStep["item"]=items
                newStep["cls"]=cls
            
                result.append(newStep)
        except Exception as e:
            self.loger.error('转换步骤异常！异常：%s！数据：%s'%(e,to_Step))
            raise ValueError('转换步骤异常！异常：%s！数据：%s'%(e,to_Step)) from e
        return result

    def _get_Sql(self,Mission):
        try:
            SqlData=self.HI_BLL._get_spiderInfo(Mission)[0]
        except Exception as e:
            self.loger.error('获取%s的sql配置信息异常！异常：%s！'%(Mission,e))
            raise ValueError('获取%s的sql配置信息异常！异常：%s！'%(Mission,e)) from e
        return SqlData#

    def _get_cookieslist(self,Datas):
        CookiesList=[]  
        try:
            Contents=Datas[1:-1].split('}#{')  
            for content in Contents:
                CookiesList.append(dict(eval('{'+content+'}')))#[{},{}]
        except Exception as e:
            self.loger.error('解析成cookies列表异常！异常：%s！数据：%s'%(e,Datas))
            raise ValueError('解析成cookies列表异常！异常：%s！数据：%s'%(e,Datas)) from e
        return CookiesList

    #获取爬虫配置并解析
    def _Analysis_SpiderConfig(self,mission):
        try:
            SpiderConfig=self.SI_BLL._get_spiderInfo(mission)[0]
       
            #print (SpiderConfig)
            name = mission
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
                contentdict['loader'] = 'NOItemLoader'
            #设置get or post
            if SpiderConfig[7]=="get":
                contentdict['away']='get'
            else:#post
                contentdict['away']=SpiderConfig[8] 
                contentdict['login_url']=SpiderConfig[9]
                if SpiderConfig[10] :
                    contentdict['account']=dict(eval(SpiderConfig[10]))
                else:
                    contentdict['account']={}
                if SpiderConfig[14]:
                    contentdict['cookies']=self._get_cookieslist(SpiderConfig[14])
                else:
                    contentdict['cookies']=[]
            #爬虫解析对象配置
            #如果是图片,则为contentdict['IMAGES_STORE'],image_urls,images
            customdata = []
            cls = []
            cus_datas=SpiderConfig[11]
            cus_datas=cus_datas[1:-1].split("):(")
            cls_datas=SpiderConfig[12].split(':')
            for data in cus_datas:
                customdata.append(self._StrToTuple("("+data+")"))
            for c_data in cls_datas:
                clsTuble=(c_data,"")
                cls.append(clsTuble)
            #print(customdata)
            #print(cls)
            #解析流程配置
            RuleDatas=[]
            r_datas=SpiderConfig[13]
            r_datas=r_datas[1:-1].split("}#{")
            for r_data in r_datas:
                #print(r_data)
                RuleDatas.extend(self._StrToStep("{"+r_data+"}")) 
            #print(RuleDatas)
            SqlData=self._get_Sql(name)
            #生成配置文件
            ConfigHandler.create_Configs(name,contentdict, customdata,cls,RuleDatas,SqlData)
        except Exception as e:
            strlog='生成%s的配置文件异常！%s'%(mission,e)
            self.EI_BLL._update_exceptionInfo(mission,strlog)
            self.loger.error(strlog)
    
    
    #开使工作
    def _Working(self):
        while True:
            Missions=self._Get_Mission()
            if Missions =="NoMissions":
                self.loger.info("当前没有任务！")
            elif Missions != None:
                for mission in Missions:
                    self.T_Pool.put(self._Action_Flow,(mission[0],),self._Action_CallBack)#1，进入线程
                    try:
                        self.M_BLL._change_Status(mission[0],"working")
                    except Exception as e:
                        self.loger.error('修改任务状态失败！异常：%s！'%e)
        self.T_Pool.close()


    #工作流程，线程调用
    def _Action_Flow(self,thread_name,mission):
        self.loger.info(mission +"in thread")
        if self.EI_BLL._get_exceptionInfo(mission):#如果该任务已经存在，清空异常内容
            self.EI_BLL._update_exceptionInfo(mission,"")
        else:
            self.Exception_Model.spidername=mission
            self.EI_BLL._add_exceptionInfo(self.Exception_Model)#添加一条空的异常信息
        self._Analysis_SpiderConfig(mission)#1，生成任务配置文件
        p=os.popen('python3 Run.py '+mission)#2，执行任务

        self.loger.info(thread_name+"===>>"+mission)
        #print(p.read())
        return mission

    #工作流程，线程回调
    def _Action_CallBack(self,status, result):
        if status:
            self.loger.info("执行成功！"+result)

if __name__=="__main__":
    serverP=ServerPool("./ServerConfigs/ServerConfigs.json")
    #serverP._get_Mission()
    serverP._Working()
