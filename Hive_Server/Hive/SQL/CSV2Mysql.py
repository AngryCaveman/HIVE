import json
import pymysql
import csv
import sys
import codecs
import os
import re
from os.path import realpath,dirname


class CSV2Mysql:
    def __init__(self,sqlConfigs):
        print("正在初始化CSV2Mysql!")
        self.server=sqlConfigs['server']
        self.port=sqlConfigs['port']
        self.user=sqlConfigs['user'] 
        self.password=sqlConfigs['password'] 
        self.DBname=sqlConfigs['DBname']
    
    def connect(self):
        #打开数据库链接
        #连接到mysql，主要一定要加上local_infile=1参数，否则会报错的
        try:
            return pymysql.connect( host=self.server,port=int(self.port),user=self.user,passwd=self.password,db=self.DBname,charset='utf8',local_infile=1)
        except Exception as e:
            raise

    def close(self,db,cursor):
        # 关闭数据库连接
        try:
            cursor.close()
            db.close()
        except Exception as e:
            raise

    def table_exists(self,con,table_name):        
        #这个函数用来判断表是否存在,没用上
        try:
            db=self.connect()
            cursor = db.cursor()
            sql = "show tables;"
            cursor.execute(sql)
            tables = [con.fetchall()]
            table_list = re.findall('(\'.*?\')',str(tables))
            table_list = [re.sub("'",'',each) for each in table_list]
            db.commit()
            if table_name in table_list:
                return 1        #存在返回1
            else:
                return 0        #不存在返回0
        except Exception as e:
            raise
        finally:
            self.close(db,cursor)

    def create_table(self,keys,table_name):
        try:
            db=self.connect()
            cursor = db.cursor()
            colum=''
            for key in keys:
                colum=colum+key+' longtext,'
            colum=colum[:-1]
            create = 'create table if not exists '+table_name+' '+'(ID INT  PRIMARY KEY AUTO_INCREMENT,add_Time datetime DEFAULT CURRENT_TIMESTAMP,'+colum+')'+' DEFAULT CHARSET=utf8'
            print(create)
            cursor.execute('set names utf8')
            cursor.execute('set character_set_connection=utf8')
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
    cm=CSV2Mysql({"server": "124.238.155.42", "port": "3306", "user": "spider_test", "password": "hY16noBc=", "DBname": "spider", "sqltype": "MySql"},"测试01")
    keys=['url','field']
    cm.create_table(keys,'test01')
    item1={'url':'A','field':'a'}
    item2={'url':'B','field':'b'}
    item3={'url':'C','field':'c'}
    item4={'url':'A','field':'d'}
    items=[item1,item2,item3,item4]
    cm.insert_MoreData('test01',items)







'''
    def _Load_Mysql(self):
        self.connect()
        #构造路径
        #_dp=dirname(realpath(__file__))+'/Honey_CSV/'+name+'/'
        _dp='../Honey_CSV/'+self.name+'/'
        for root,dirs,files in os.walk(_dp):
            print(files)
        for _fp in files:
            csv_filename =_dp+_fp #csv格式的文件路径
            table_name = self.name+"_"+_fp.replace(".csv","") #mysql表名

            _f=codecs.open(csv_filename,'r','utf-8')
            reader=_f.readline()
            datas=reader.split(',')
            print(datas)
            colum=''
            for data in datas:
                colum=colum+data+' longtext,'
            colum=colum[:-1]
            create = 'create table if not exists '+table_name+' '+'('+colum+')'+' DEFAULT CHARSET=utf8'
            #data = 'LOAD DATA LOCAL INFILE \'' + csv_filename + '\'REPLACE INTO TABLE ' + table_name + ' FIELDS TERMINATED BY \',\' ENCLOSED BY \'\"\' LINES TERMINATED BY \'\n\' IGNORE 1 LINES;'
            cursor = self.db.cursor()
            cursor.execute('set names utf8')
            cursor.execute('set character_set_connection=utf8')
            cursor.execute(create)
            #cursor.execute(data)
            self.db.commit()
            cursor.close()
        self.close()
'''
