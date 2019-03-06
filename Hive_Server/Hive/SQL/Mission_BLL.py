#从数据库三层架构上考虑，这应该是Mission表的DAL层，为了保持简洁，不再封装一层了
import pymysql
import json
from os.path import realpath,dirname

class Mission:
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
            return  pymysql.connect(self.ip,self.usr,self.pwd,self.dbname)
        except Exception as e:
            raise

    #改变任务状态
    def _change_Status(self,spidername,status):
        try:
            #改任务状态
            sentence= "UPDATE Mission SET status='%s' WHERE spidername='%s'"%(status,spidername)
            result=self.update(sentence)
            if result is not None:
                print(spidername,"已经更新任务状态")
        except Exception as e:
            raise

    #更新语句
    def update(self,sentence):
        try:
            # 使用cursor()方法获取操作游标 
            db=self.connect()
            cursor =db.cursor() 
            # 执行SQL语句
            cursor.execute(sentence)
            # 提交到数据库执行
            db.commit()
            result='ok'
        except Exception as e:         
            result=None
            # 发生错误时回滚
            db.rollback()
            raise
        finally:
            self.close(db,cursor)
        return result
    
    def select(self,sentence):
        # 使用cursor()方法获取操作游标 
        db=self.connect()
        cursor =db.cursor() 
        # SQL 查询语句
        try:
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

    #查找spidername的任务状态
    def _select_Status(self,spidername):
        try:
            sentence ="SELECT  status FROM mission where spidername='%s';"%(spidername)
            results=self.select(sentence)
        except Exception as e:
            results=None
            raise
        return results

    #查找spidername的任务id
    def _select_ID(self,spidername):
        try:
            sentence ="SELECT  id FROM mission where spidername='%s';"%(spidername)
            results=self.select(sentence)
        except Exception as e:
            results=None
            raise
        return results


    #查找num个状态为pri的任务
    def _select_Mission(self,num,pri):
        try:
            sentence ="SELECT  * FROM mission where PRI='"+pri+"' and status='wait' LIMIT "+str(num)+";"
            results=self.select(sentence)
        except Exception as e:
            results=None
            raise
        return results

    def close(self,db,cursor):
        # 关闭数据库连接
        try:
            cursor.close()
            db.close()
        except Exception as e:
            raise

if __name__=="__main__":
    m1=Mission()
    print(m1._select_ID("测试01")[0][0])
