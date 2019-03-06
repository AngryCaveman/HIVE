import json
import pymssql
import csv
import sys
import codecs
import os
from os.path import realpath,dirname


class CSV2Sqlserver:
    def __init__(self,sqlConfigs):
        print("正在初始化CSV2Sqlserver!")
        self.server=sqlConfigs['server']
        self.port=sqlConfigs['port']
        self.user=sqlConfigs['user'] 
        self.password=sqlConfigs['password'] 
        self.DBname=sqlConfigs['DBname']
    
    def connect(self):
        #打开数据库链接
        #连接到Sqlserver，主要一定要加上local_infile=1参数，否则会报错的
        try:
            return pymssql.connect( host=self.server,port=int(self.port),user=self.user,password=self.password,database=self.DBname,charset='utf8')
        except Exception as e:
            raise

    def close(self,db,cursor):
        # 关闭数据库连接
        try:
            cursor.close()
            db.close()
        except Exception as e:
            raise

    def create_table(self,keys,table_name):
        try:
            db=self.connect()
            cursor = db.cursor()
            colum=''
            for key in keys:
                colum=colum+key+' VARCHAR(MAX),'
            colum=colum[:-1]
            create="IF (not EXISTS(SELECT * FROM sysobjects WHERE name='"+table_name+"')) CREATE TABLE "+table_name+" (ID int NOT NULL IDENTITY(1,1) ,AddTime datetime default(getDate()),"+colum+",PRIMARY KEY ([ID]));"
            print(create)
            cursor.execute(create)
            db.commit()
        except Exception as e:
            raise
        finally:
            self.close(db,cursor)

    def insert_MoreData(self,table,items):
        try:
            colum=''
            values=''
            for key in items[0].keys():
                colum=colum+key+','
                values=values+'%s,'
            values=values[:-1]
            colum=colum[:-1]
            temp="INSERT INTO "+table+"("+colum+") values("+values+")"
            print(temp)
            data=[]
            for item in items :
                data.append(tuple(item.values()))
            db=self.connect()
            cursor = db.cursor()
            #批量插入
            cursor.executemany(temp,data)
            #插入之后对表进行去重
            re_db=""#去重语句
            #cursor.execute()
            db.commit()
        except Exception as e:
            raise
        finally:
            self.close(db,cursor)


if __name__=="__main__":
    test=CSV2Sqlserver({"server": "192.168.1.224", "port": "9018", "user": "sa", "password": "123456", "DBname": "spider", "sqltype": "Sqlserver"},"测试01")
    keys=['url','field']
    test.create_table(keys,'测试01')
    item1={'url':'是1','field':'a'}
    item2={'url':'是2','field':'b'}
    item3={'url':'是3','field':'c'}
    item4={'url':'是1','field':'d'}
    items=[item1,item2,item3,item4]
    test.insert_MoreData('测试01',items)

