from os.path import realpath,dirname
from Hive.Maker import CommonMaker
from Hive.Maker import ItemMaker
from Hive.Maker import RulesMaker
from Hive.Maker import SqlMaker
from scrapy.linkextractors import LinkExtractor
from scrapy.spiders import Rule
import json
import os

#生成配置文件
def create_Configs(name,contentdict, customdata, cls,RuleDatas,SqlData):
    print('正在生成配置文件！',name)
    try:
        Alldata=[]
        i=0
        types=['common','item','rules','sql']
        Alldata.append(CommonMaker.addCommon(contentdict, customdata, name))
        Alldata.append(ItemMaker.addItem(name,cls))
        Alldata.append(RulesMaker.addRules(name, RuleDatas))
        Alldata.append(SqlMaker.addSql(name,SqlData))
        dp=dirname(realpath(__file__))+'/MissionConfigs/'+name
        if not os.path.exists(dp):
            #print('创建目录',dp)
            os.mkdir(dp)
        else:
            print(dp,'已经存在！')
        for i in range(4):
            fp=dp+'/'+types[i]+'.json'
            jsondata=Alldata[i]
            with open(fp, 'wb') as f:
                f.write(jsondata.encode('utf8'))
        print(name,'配置文件生成完毕！')
    except Exception as e:
        raise ValueError(e) from e


#name:配置文件名
def get_common(name):
    print('正在获取配置文件')
    try:
        path=dirname(realpath(__file__))+'/MissionConfigs/'+name+'/common.json'
        with open(path,'r',encoding='utf-8') as f:
            return json.loads(f.read())
    except Exception as e:
        raise ValueError(e) from e


def get_Item(name):
    print('正在获取'+name+'的Item')
    try:
        path=dirname(realpath(__file__))+'/MissionConfigs/'+name+'/item.json'
        with open(path,'r',encoding='utf-8') as f:
            return json.loads(f.read())
    except Exception as e:
        raise ValueError(e) from e

def get_rules(name):
    print('正在获取rules')
    try:
        path = dirname(realpath(__file__)) + '/MissionConfigs/' + name + '/rules.json'
        with open(path, 'r', encoding='utf-8') as f:
            return json.loads(f.read())
    except Exception as e:
        raise ValueError(e) from e

def get_cookies(name):
    print('正在获取cookies')
    try:
        path = dirname(realpath(__file__)) + '/MissionConfigs/' + name + '/cookies.json'
        with open(path, 'r', encoding='utf-8') as f:
            return json.loads(f.read())
    except Exception as e:
        raise ValueError(e) from e

def get_Sql(name):
    print('正在获取cookies')
    try:
        path = dirname(realpath(__file__)) + '/MissionConfigs/' + name + '/sql.json'
        with open(path, 'r', encoding='utf-8') as f:
            return json.loads(f.read())
    except Exception as e:
        raise ValueError(e) from e

#传入一个二元组的列表【（type,name）】
#type为配置文件类型,type全为小写
#name为配置文件名称
def delete_config(configdict):
    print('删除配置文件！')
    for data in configdict:
        path = dirname(realpath(__file__)) + '/MissionConfigs/'+data[1]+'/' +data[0]+ '.json'
        if os._exists(path):
            os.remove(path)
        else :
            print('no such file'+path)


