from scrapy.linkextractors import LinkExtractor
from scrapy.spiders import Rule
from Hive.ConfigHandler import get_rules
from Hive.Maker.CommonMaker import *

#follow:是否继续跟进页面
#linkRe:更进页面，链接解析规则
#cls:解析当前页面内容的对象
#item:解析当前页面内容对象的解析规则
#paging:翻页信息,为False时表示不翻页
#dynamic:当前页面是否为动态页面
#relevance:关联字段，一个元组（包含规则，关联字段，解析规则，关联步骤）
class Rules(object):
    def __init__(self,paging,dynamic,linkRe,follow,item,cls,relevance,customLink,dpField):
        self.paging=paging
        self.dynamic=dynamic
        self.follow=follow
        self.linkRe=linkRe
        self.item=item
        self.cls=cls
        self.relevance=relevance
        self.customLink=customLink
        self.dpField=dpField

def createrules(name):
    #print('正在生成链接扩展规则！')
    finalRule={}
    try:
        allrule=get_rules(name)
        count=allrule["count"]
        rulelist=[]
        
        step=1
        for no in count:
            values=allrule[no]
            print(values)
            if values['item']:
                item=Analysis(values['item'])
                #print(item.items())
            else :
                item=''
            cls=list2dict(values['cls'])
            if values['paging']=="":
                paging=''
            else:
                paging=LinkExtractor(restrict_xpaths=values['paging'])
            if  values['relevance'] =="":
                relevance=''
            if  values['customLink']=="":
                relevance=''
            if  values['dpField']=="":
                relevance=''
            newrule=Rules(paging,values['dynamic'],values['linkRe'],values['follow'],item,cls,values['relevance'],values['customLink'],values['dpField'])
            if values['step']!=step:  
                step=step+1
                rulelist=[]
            rulelist.append(newrule)
            finalRule[step]=rulelist
    except Exception as e:
        raise ValueError(e) from e        
    return finalRule



