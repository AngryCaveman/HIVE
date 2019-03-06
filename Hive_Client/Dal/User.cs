using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataBase;

namespace Dal
{
    public partial class User
    {
        public User()
        {
            //dbhelper = new DBHelper();
            mshelper = new MSHelper();
        }
        DBHelper dbhelper;
        MSHelper mshelper;
        #region  BasicMethod
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into User(");
            strSql.Append("UserID,UserStatus)");
            strSql.Append(" values (");
            string values = "'" + model.userid+"','"+model.userstatus+ "')";
            strSql.Append(values);

            int rows = mshelper.InsertSQl(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string UserID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from User ");
            strSql.Append(" where UserID='" + UserID + "'");

            int rows = mshelper.DeleteSQL(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object[] Select(string userID)
        {
            string SelectStr = "select * from User where UserID='" + userID + "';";
            object[] Content = mshelper.SelectSQl(SelectStr);
            return Content;
        }

        public object[] SelectStatus(string userID)
        {
            string SelectStr = "select UserStatus from User where UserID='" + userID + "';";
            object[] Content = mshelper.SelectSQl(SelectStr);
            return Content;
        }
        public bool UpdateStatus(string userStatus,string userID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update user set ");
            strSql.Append("UserStatus='" + userStatus + "'");
            strSql.Append(" where UserID='" + userID+ "'");
            int rows = mshelper.UpdateSQl(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion  BasicMethod
    }
}
