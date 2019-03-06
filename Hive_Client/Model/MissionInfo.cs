using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class MissionInfo
    {
        public MissionInfo() { }
        #region====MissionInfo=====
        #region====字段====
        private string spiderName;//爬虫名字
        private int failPages;//失败页面
        private DateTime startTime;//起始时间
        private DateTime endTime;//结束时间
        private int completePages;//完成页面数
        private int threadCounts;//线程数量
        private int failCount_getdata;//获取数据失败的页数
        private int failCount_savedata;//保存数据失败的页数
        private int failCount_linkre;//解析跳转链接失败数
        #endregion 
        #region====属性====
        public string spidername
        {
            set { spiderName = value; }
            get { return spiderName; }
        }
        public int failpages
        {
            set { failPages = value; }
            get { return failPages; }
        }
        public DateTime starttime
        {
            set { startTime = value; }
            get { return startTime; }
        }
        public DateTime endtime
        {
            set { endTime = value; }
            get { return endTime; }
        }
        public int completepages
        {
            set { completePages = value; }
            get { return completePages; }
        }
        public int threadcounts
        {
            set { threadCounts = value; }
            get { return threadCounts; }
        }
        public int failcount_getdata
        {
            set { failCount_getdata = value; }
            get { return failCount_getdata; }
        }
        public int failcount_savedata
        {
            set { failCount_savedata = value; }
            get { return failCount_savedata; }
        }
        public int failcount_linkre
        {
            set { failCount_linkre = value; }
            get { return failCount_linkre; }
        }
        #endregion
        #endregion
    }
}
