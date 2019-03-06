using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class HoneyInfo
    {
        public HoneyInfo() { }
        #region====HoneyInfo=====
        #region====字段====
        private string spiderName;//爬虫名字
        private string Server;//服务器IP
        private string Port;//端口值
        private string User;//用户名
        private string Password;//密码
        private string DBName;//数据库名字
        private string SqlType;//数据库类型
        #endregion 
        #region====属性====
        public string spidername
        {
            set { spiderName = value; }
            get { return spiderName; }
        }
        public string server
        {
            set { Server = value; }
            get { return Server; }
        }
        public string port
        {
            set { Port = value; }
            get { return Port; }
        }
        public string user
        {
            set { User = value; }
            get { return User; }
        }
        public string password
        {
            set { Password = value; }
            get { return Password; }
        }
        public string dbname
        {
            set { DBName = value; }
            get { return DBName; }
        }
        public string sqltype
        {
            set { SqlType = value; }
            get { return SqlType; }
        }
        #endregion
        #endregion
    }
}
