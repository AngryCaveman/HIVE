import json

#创建Item配置文件
#classname：创建的Item名称
#ItemDatas：一个列表，包含键值对二元组
def addItem(classname,ItemDatas):
    #print('正在写入'+classname+'的item')
    jsondict={}
    try:
        for data in ItemDatas:
            jsondict[data[0]]=data[1]
        jsondata=json.dumps(jsondict,ensure_ascii=False)
    except Exception as e:
        raise
    #print(jsondata)
    return jsondata

