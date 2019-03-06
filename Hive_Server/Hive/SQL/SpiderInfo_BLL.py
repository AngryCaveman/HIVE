#从数据库三层架构上考虑，这应该是DAL层，为了保持简洁，不再封装一层了
import pymysql
import json
from os.path import realpath,dirname
class SpiderInfo:
    #初始化
    def __init__(self):
        path=dirname(realpath(__file__))+'/sqlconfig.json'
        with open(path,'r',encoding='utf-8') as f:
            configs=json.loads(f.read())
        self.usr=configs['usr']
        self.pwd=configs['pwd']
        self.ip=configs['ip'] 
        self.dbname=configs['dbname']

    # 打开数据库连接
    def connect(self):
        try:
            return pymysql.connect(self.ip,self.usr,self.pwd,self.dbname)
        except Exception as e:
            raise
    
    def select(self,sentence):
        try:
            db=self.connect()
            # 使用cursor()方法获取操作游标 
            cursor = db.cursor()
            # SQL 查询语句
            # 执行SQL语句
            cursor.execute(sentence)
            # 获取所有记录列表
            results = cursor.fetchall()
        except Exception as e:
            results=None
            raise
        finally:
            self.close(db,cursor)
        return results

    def _get_spiderInfo(self,spidername):
        try:
            print(spidername)
            sentence="SELECT * FROM spiderinfo WHERE spiderName='%s'" %(spidername)
            result=self.select(sentence)
        except Exception as e:
            result=None
            raise
        return result

    def close(self,db,cursor):
        # 关闭数据库连接
        try:
            cursor.close()
            db.close()
        except Exception as e:
            raise
if __name__=='__main__':
    SI_BLL=SpiderInfo()#配置表
    SpiderConfig=SI_BLL._get_spiderInfo('test02')[0]
    print(SpiderConfig)
