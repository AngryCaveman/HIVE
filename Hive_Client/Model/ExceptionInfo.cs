using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class ExceptionInfo
    {
        public ExceptionInfo() { }
        #region====ExceptionInfo=====
        #region====字段====
        private string SpiderName;//任务名称
        private string ExceptionContent;//异常内容
        #endregion 
        #region====属性====
        public string spidername
        {
            set { spidername = value; }
            get { return spidername; }
        }
        public string exceptioncontent
        {
            set { exceptioncontent = value; }
            get { return exceptioncontent; }
        }
        #endregion
        #endregion
    }
}
