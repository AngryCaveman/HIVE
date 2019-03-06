import sys
from scrapy.utils.project import get_project_settings
from Hive.spiders.Bee import Bee
from Hive.ConfigHandler import get_common
from scrapy.crawler import  CrawlerProcess

def run():
    print('程序启动！')
    BeeName=sys.argv[1]#获取命令行参数，爬虫名，也为配置文件名
    custom_settings=get_common(BeeName)
    #spider=custom_settings.get('spider','spiderbase')
    spider='Bee'
    project_settings=get_project_settings()
    settings=dict(project_settings.copy())
    #合并配置
    settings.update(custom_settings.get('settings'))
    process=CrawlerProcess(settings)
    process.crawl(spider,**{'BeeName':BeeName})
    process.start()


if __name__=='__main__':
    run()
