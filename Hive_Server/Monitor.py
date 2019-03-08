#coding=utf-8
import time
import os
import datetime
import sys


def get_pid(processinfo):
    processinfo=processinfo.strip('\n').split(' ')
    #去除空字符串
    while '' in processinfo:
        processinfo.remove('')
    #list 信息为 UID，PID，PPID
    pid=processinfo[1]#获取pid
    return pid

def Monitor():
    while True:
        processinfo=os.popen("ps -ef| grep 'python3 ./Server.py'|grep -v grep").readlines()#拿到进程信息
        #处理进程信息
        if len(processinfo)>2:
            ProIDs=[]
            for pinfo in processinfo:
                ProIDs.append(get_pid(pinfo))
        elif len(processinfo)==0:
                os.system('python3 ./Server.py')
        time.sleep(10)


if __name__=='__main__':
    
    #启动程序
    Monitor()

