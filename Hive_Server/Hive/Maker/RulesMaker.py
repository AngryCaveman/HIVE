import json
#name:文件名称
#RuleDatas：一个包含多个字典的列表，每个列表包含键值对二元组
def addRules(name,RuleDatas):
    #print('正在生成'+name+'的rules！')
    i=1
    countlist=[]
    AllRules = {}
    try:
        for data in RuleDatas:
            count="rule"+str(i)
            i=i+1
            countlist.append(count)
            AllRules[count]={'step':data['step'],'paging':data['paging'],'dynamic':data['dynamic'],
'linkRe':data['linkRe'],'item':data['item'],'follow':data['follow'],'cls':data['cls'],'relevance':data['relevance'],'customLink':data['customLink'],'dpField':data['dpField']}
        AllRules['count']=countlist
        jsondata = json.dumps(AllRules, ensure_ascii=False)
        #print(jsondata)
    except Exception as e:
        raise
    return jsondata
  


