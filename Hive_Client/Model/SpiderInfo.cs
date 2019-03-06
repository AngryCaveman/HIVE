using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class SpiderInfo
    {
        
        public SpiderInfo()
        {
          
        }
        #region====SpiderInfo=====
        #region====字段====
        private string spiderName;//爬虫名字
        private string Website;//站点名字
        private string file_Store;//文件存储位置
        private string start_Urls;//起始url
        private string allowed_Domains;//允许域名
        private string Rules;//规则名
        private string _Class;//类名
        private string Loader;//装载器
        private string Away;//访问方式
        private string login_Url;//登陆url
        private string Account;//账号
        private string customData;//解析规则，方式等
        private string Cls;//解析对象
        private string ruleDatas;//解析步骤集合
        private string Cookies;//cookies 集合
        #endregion 
        #region====属性====
        public string spidername
        {
            set { spiderName = value; }
            get { return spiderName; }
        }
        public string website
        {
            set { Website = value; }
            get { return Website; }
        }
        public string file_store
        {
            set { file_Store = value; }
            get { return file_Store; }
        }
        public string start_urls
        {
            set { start_Urls = value; }
            get { return start_Urls; }
        }
        public string allowed_domains
        {
            set { allowed_Domains = value; }
            get { return allowed_Domains; }
        }
        public string rules
        {
            set { Rules = value; }
            get { return Rules; }
        }
        public string _class
        {
            set { _Class = value; }
            get { return _Class; }
        }
        public string loader
        {
            set { Loader = value; }
            get { return Loader; }
        }
        public string away
        {
            set { Away = value; }
            get { return Away; }
        }
        public string login_url
        {
            set { login_Url = value; }
            get { return login_Url; }
        }
        public string account
        {
            set { Account = value; }
            get { return Account; }
        }

        public string customdata
        {
            set { customData = value; }
            get { return customData; }
        }
        public string cls
        {
            set { Cls = value; }
            get { return Cls; }
        }
        public string ruledatas
        {
            set { ruleDatas = value; }
            get { return ruleDatas; }
        }
        public string cookies
        {
            set { Cookies = value; }
            get { return Cookies; }
        }
        #endregion
        #endregion
    }
}
