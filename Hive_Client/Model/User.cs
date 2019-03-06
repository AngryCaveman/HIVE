using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class User
    {
        public User() { }
        #region====User属性=====
        #region====字段====
        private string UserID;//用户ID
        private string UserStaus;//用户ID
        #endregion 
        #region====属性====
        public string userid
        {
            set { userid = value; }
            get { return userid; }
        }
        public string userstatus
        {
            set { userstatus = value; }
            get { return userstatus; }
        }
        #endregion
        #endregion
    }
}
