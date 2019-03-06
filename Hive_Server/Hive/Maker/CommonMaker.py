import  json
import os
from os.path import realpath,dirname
def list2dict(listname):
    dictname={}
    try:
        for list0 in listname:
            dictname[list0[0]]=list0[1]
    except Exception as e:
        raise
    return  dictname


# 生成页面解析规则
# customdata:为包含一个三元组（属性名，解析方法，匹配规则）的列表
def Analysis(customdata):
    #print('正在生成页面解析规则！')
    allattrs=[]
    try:
        for data in customdata:
            tupdic = {}
            tupdic['method']=data[1]
            tupdic['args']=data[2]
            tup0=(data[0],[tupdic])
            allattrs.append(tup0)
        attrdict=list2dict(allattrs)
    except Exception as e:
        raise
    return attrdict
    #print(jsoncontent)

#生成json数据
#contentdict：包含爬虫共有信息的字典，键值均为字符串
#customdata：包含解析页面规则的列表，如果不解析页面，给false
def createjson(contentdict,customdata):
    #print('正在生成json数据')
    #因为item，和其他数据格式的要求，重新解析重组contentdict
    commondata={}
    try:
        #基本信息提取
        commondata['spider']=contentdict['spider']
        commondata['allowed_domains'] =[ contentdict['allowed_domains']]
        commondata['start_urls'] = [contentdict['start_urls']]
        commondata['website'] = contentdict['website']
        commondata['away']=contentdict['away']
        if commondata['away']=='post':
            commondata['login_url']=contentdict['login_url']
            commondata['account']=contentdict['account']
            commondata['cookies']=contentdict['cookies']
        #设置信息提取，比如线程数，目前暂不考虑
        settingconfig={}
        logFp='./Hive/LOG/'+contentdict['spider']+'.log'
        settingconfig['LOG_FILE']=logFp
        if 'FILES_STORE' in contentdict:
            settingconfig['FILES_STORE']= contentdict['FILES_STORE']
        elif 'IMAGES_STORE' in contentdict:
            settingconfig['IMAGES_STORE']= contentdict['IMAGES_STORE']
        commondata['settings']=settingconfig
        #设置Item信息
        ItemConfig={}
        ItemConfig['class']=contentdict['class']
        ItemConfig['loader'] = contentdict['loader']
        #是否解析页面
        if customdata !='false':
            ItemConfig['attrs']=Analysis(customdata)
        #整合所有
        commondata['item']=ItemConfig
    except Exception as e:
        raise
    return  commondata

#name为生成的json文件夹名
def addCommon(contentdict,customdata,name):
    #print('正在创建'+name+'的common')
    try:
        content=createjson(contentdict,customdata)
        #print(content)
        jsoncontent=json.dumps(content,ensure_ascii=False)
        #print(jsoncontent)
    except Exception as e:
        raise
    return jsoncontent

