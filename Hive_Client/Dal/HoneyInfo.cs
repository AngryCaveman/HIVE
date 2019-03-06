using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase;

namespace Dal
{
    public partial class HoneyInfo
    {
        public HoneyInfo()
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
        public bool Add(Model.HoneyInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into HoneyInfo(");
            strSql.Append("spidername,server,port,user,password,DBname,sqltype)");
            strSql.Append(" values (");
            string values = "'" + model.spidername + "','" + model.server + "','" + model.port + "','" + model.user + "','" + model.password + "','" + model.dbname + "','" + model.sqltype + "')";
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
        public bool Delete(string spiderName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from HoneyInfo ");
            strSql.Append(" where spidername='" + spiderName + "'");

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

        /// <summary>
        ///查询任务所有信息
        /// </summary>
        public object[] Select(string spiderName)
        {
            string SelectStr = "select * from HoneyInfo where spidername='" + spiderName + "';";
            object[] Content = mshelper.SelectSQl(SelectStr);
            return Content;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.HoneyInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update HoneyInfo set ");
            strSql.Append("server='" + model.server + "',port='" + model.port + "',DBname='" + model.dbname + "',user='" + model.user + "',password='" + model.password + "',sqltype='" +model.sqltype+"'");
            strSql.Append(" where spidername='" + model.spidername + "'");
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
