class mission:
    def __init__(self,spidername,status,userid,PRI):
        self.spidername=spidername
        self.status=status
        self.userid=userid
        self.PRI=PRI

class missionInfo:
    def __init__(self,spidername,completepages,failpages,threadcount,failcount_getdata,failcount_savedata,failcount_linkre,starttime,endtime,missionId):
        self.spidername=spidername
        self.completepages=completepages
        self.failpages=failpages
        self.threadcount=threadcount
        self.failcount_getdata=failcount_getdata
        self.failcount_savedata=failcount_savedata
        self.failcount_linre=failcount_linkre
        self.starttime=starttime
        self.missionId=missionId
        self.endtime=endtime

class spiderInfo:
    def __init__(self,spidername,website,start_urls,allowed_domains,rules,_class,loader,away,login_url,account,customdata,cls,ruledatas,cookies):
        self.spidername=spidername
        self.website=website
        self.start_urls=start_urls
        self.allowed_domains=allowed_domains
        self.rules=rules
        self._class=_class
        self.loader=loader
        self.away=away
        self.login_url=login_url
        self.account=account
        self.customdata=customdata
        self.cls=cls
        self.ruledatas=ruledatas
        self.cookies=cookies

class exceptionInfo:
    def __init__(self,spidername,exceptioncontent):
        self.spidername=spidername
        self.exceptioncontent=exceptioncontent

  


