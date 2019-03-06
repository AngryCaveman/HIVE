using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase;
using System.Data;

namespace Dal
{
    public partial class MissionInfo
    {
        public MissionInfo()
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
        public bool Add(Model.MissionInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into missioninfo(");
            strSql.Append("spiderName,failPages,endTime,startTime,completePages,threadCounts,failCount_getdata,failCount_savedata,failCount_linkre)");
            strSql.Append(" values (");
            string values = "'" + model.spidername + "'," + model.failpages+","+model.endtime+","+model.starttime+","+model.completepages+","+model.threadcounts+","+model.failcount_getdata+","+model.failcount_savedata+","+model.failcount_linkre + ")";
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
            strSql.Append("delete from missioninfo ");
            strSql.Append(" where spiderName='" + spiderName + "'");

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
        ///查询一条
        /// </summary>
        public object[] Select(string spiderName)
        {
            string SelectStr = "select * from missioninfo where spiderName='" + spiderName + "';";
            object[] Content = mshelper.SelectSQl(SelectStr);
            return Content;
        }

        public DataTable SelectAll()
        {
            string SelectStr = "select * from missioninfo;";
            DataTable content = mshelper.SelectSQl_More(SelectStr);
            return content;
        }

        #endregion  BasicMethod
    }
}
