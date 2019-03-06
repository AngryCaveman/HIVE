using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase;
using System.Data;
namespace Dal
{
    public partial class Mission
    {
        public Mission()
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
        public bool Add(Model.Mission model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Mission(");
            strSql.Append("spidername,status,userid,PRI)");
            strSql.Append(" values (");
            string values = "'" + model.spidername + "','" + model.status + "','" + model.userid + "','" + model.pri+  "')";
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
            strSql.Append("delete from Mission ");
            strSql.Append(" where spidername='" + spiderName +  "'");

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
            string SelectStr = "select * from Mission where spidername='" + spiderName +  "';";
            object[] Content = mshelper.SelectSQl(SelectStr);
            return Content;
        }

        /// <summary>
        ///查询正在运行的任务
        /// </summary>
        public DataTable SelectWorkingMission(string userid)
        {
            string SelectStr = "select spidername from Mission where status='working' and userid='"+userid+"';";
            DataTable Content = mshelper.SelectSQl_More(SelectStr);
            return Content;
        }
        /// <summary>
        /// 查询任务名和状态
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable SelectSpiderName(string userID)
        {
            string SelectStr = "select spidername,status from Mission where userid='" + userID + "';";
            DataTable content = mshelper.SelectSQl_More(SelectStr);
            return content;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        public bool UpdateStatus(string spiderName, string Status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Mission set ");
            strSql.Append("status='" + Status+"'");
            strSql.Append(" where spidername='" + spiderName + "'");
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

        /// <summary>
        /// 更新优先级
        /// </summary>
        /// <param name="spiderName"></param>
        /// <param name="PRI"></param>
        /// <returns></returns>
        public bool UpdatePRI(string spiderName, string PRI)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Mission set ");
            strSql.Append("PRI='" + PRI + "'");
            strSql.Append(" where spidername='" + spiderName + "'");
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
