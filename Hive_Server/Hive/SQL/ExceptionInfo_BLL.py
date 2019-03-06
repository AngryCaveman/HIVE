#从数据库三层架构上考虑，这应该是ExceptionInfo表的DAL层，为了保持简洁，不再封装一层了
import pymysql
import json
from os.path import realpath,dirname

class ExceptionInfo:
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

     #添加异常信息
    def _add_exceptionInfo(self,model):
        try:
            db=self.connect()
            cursor =db.cursor() 
            # SQL 插入语句
            sql = "INSERT INTO exceptioninfo(spiderName,exceptionContent) VALUES ('%s', '%s')" % \
            (model.spidername, model.exceptioncontent)
     
            # 执行sql语句
            cursor.execute(sql)
            # 执行sql语句
            db.commit()
        except Exception as e:
            # 发生错误时回滚
            db.rollback()
            raise
        finally:
            self.close(db,cursor)
    
    #修改异常信息
    def _update_exceptionInfo(self,spiderName,exceptionContent):
        try:
            sentence="UPDATE exceptioninfo SET exceptionContent='%s' WHERE spiderName='%s'" %(exceptionContent,spiderName)
            results=self.update(sentence)
        except Exception as e:
            results=None
            raise
        return results
 
    #获取指定任务的异常信息
    def _get_exceptionInfo(self,spidername):
        try:
            sentence="SELECT * FROM exceptioninfo WHERE spidername='%s'" %(spidername)
            result=self.select(sentence)
        except Exception as e:
            result=None
            raise
        return result

    #更新语句
    def update(self,sentence):
        try:
            # 使用cursor()方法获取操作游标 
            db=self.connect()
            cursor = db.cursor() 
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
        try:
            # 使用cursor()方法获取操作游标 
            db=self.connect()
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


    def close(self,db,cursor):
        # 关闭数据库连接
        try:
            cursor.close()
            db.close()
        except Exception as e:
            raise
if __name__=="__main__":
    m1=ExceptionInfo()
    print(m1._update_exceptionInfo('test','test'))
