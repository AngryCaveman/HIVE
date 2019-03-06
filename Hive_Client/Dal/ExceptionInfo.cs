using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase;
using System.Data;
namespace Dal
{
    public partial class ExceptionInfo
    {
        public ExceptionInfo()
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
        public bool Add(Model.ExceptionInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into exceptionInfo(");
            strSql.Append("spiderName,exceptionContent)");
            strSql.Append(" values (");
            string values = "'" + model.spidername + "','" + model.exceptioncontent + "')";
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
        public bool Delete(string SpiderName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from exceptioninfo ");
            strSql.Append(" where spiderName='" + SpiderName + "'");

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

        public object[] Select(string SpiderName)
        {
            string SelectStr = "select * from exceptioninfo where spiderName='" + SpiderName + "';";
            object[] Content = mshelper.SelectSQl(SelectStr);
            return Content;
        }

        public DataTable SelectMore()
        {
            string SelectStr = "select * from exceptioninfo;";
            DataTable Content = mshelper.SelectSQl_More(SelectStr);
            return Content;
        }

        #endregion  BasicMethod
    }
}
