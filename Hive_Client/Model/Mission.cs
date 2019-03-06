using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Mission
    {
        public Mission() { }
        #region====Mission属性=====
        #region====字段====
        private string spiderName;//爬虫名字
        private string Status;//状态
        private string UserID;//用户ID
        private string PRI;//优先级
        #endregion 
        #region====属性====
        public string spidername
        {
            set { spiderName = value; }
            get { return spiderName; }
        }
        public string status
        {
            set { Status = value; }
            get { return Status; }
        }
        public string userid
        {
            set { UserID = value; }
            get { return UserID; }
        }
        public string pri
        {
            set { PRI = value; }
            get { return PRI; }
        }
        #endregion
        #endregion
    }
}
