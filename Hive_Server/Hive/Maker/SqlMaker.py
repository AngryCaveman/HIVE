import json
#name:文件名称
def addSql(name,SqlData):
    AllSql = {}
    #print('正在生成'+name+'的SQL！')
    #print(SqlData)
    try:
        if SqlData:
            AllSql={'server':SqlData[1],'port':SqlData[2],'user':SqlData[3],'password':SqlData[4],'DBname':SqlData[5],'sqltype':SqlData[6]}
        jsondata = json.dumps(AllSql, ensure_ascii=False)
    except Exception as e:
        raise
    #print(jsondata)
    return jsondata
  


