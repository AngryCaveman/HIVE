#从数据库三层架构上考虑，这应该是DAL层，为了保持简介，不再封装一层了
import pymysql
import json
from os.path import realpath,dirname

class MissionInfo:
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
    
    #添加任务运行信息
    def _add_missionInfo(self,model):
        try:
            db=self.connect()
            # 使用cursor()方法获取操作游标 
            cursor = db.cursor()
            # SQL 插入语句
            sql = "INSERT INTO missioninfo(spiderName,completePages,failPages,threadCount,failCount_getdata,failCount_savedata,failCount_linkre,startTime,endTime,missionId) \
            VALUES ('%s', %d,%d,%d,%d,%d,%d,'%s','%s',%d)" % \
            (model.spidername, model.completepages,model.failpages,model.threadcount,\
            model.failcount_getdata,model.failcount_savedata,model.failcount_linre,model.starttime,model.endtime,model.missionId)      
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

    #更新失败链接数，成功连接数，结束时间
    def _update_Pages(self,cp,fp,et,spidername):
        try:
            sentence="UPDATE missioninfo SET completePages='%s',failPages='%s',endTime='%s' WHERE spiderName='%s'" %(cp,fp,et,spidername)
            results=self.update(sentence)
        except Exception as e:
            results=None
            raise
        return results

    #更新保存数据失败字段
    def _update_SaveData(self,sd_Num,spidername):
        try:
            sentence="UPDATE missioninfo SET failCount_savedata='%s' WHERE spiderName='%s'" %(sd_Num,spidername)
            results=self.update(sentence)
        except Exception as e:
            results=None
            raise
        return results

    #更新解析数据失败字段
    def _update_dataFailCount(self,getdata_Num,spidername):
        try:
            sentence="UPDATE missioninfo SET failCount_getdata='%s' WHERE spiderName='%s'" %(getdata_Num,spidername)
            results=self.update(sentence)
        except Exception as e:
            results=None
            raise
        return results

    #更新解析链接失败字段
    def _update_linkreFailCount(self,linkre_Num,spidername):
        try:
            sentence="UPDATE missioninfo SET failCount_linkre='%s' WHERE spiderName='%s'" %(linkre_Num,spidername)
            results=self.update(sentence)
        except Exception as e:
            results=None
            raise
        return results

    #更新任务运行信息
    def update(self,sentence):
        try:
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
    
    #删除一条数据
    def _delete(self,name): 
        try:
            db=self.connect()
            sentence="delete from missioninfo where spiderName='"+name+"'"
            # 使用cursor()方法获取操作游标 
            cursor = db.cursor() 
            # 执行SQL语句
            cursor.execute(sentence)
            # 提交修改
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


    def _get_missionInfo(self,spidername):
        try:
            #获取任务信息
            sentence="SELECT * FROM missioninfo WHERE spiderName='%s'" %(spidername)
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




