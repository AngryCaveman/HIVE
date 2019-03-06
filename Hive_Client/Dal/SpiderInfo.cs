using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase;

namespace Dal
{
    public partial class SpiderInfo
    {
        public SpiderInfo()
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
        public bool Add(Model.SpiderInfo model)
        {
            string startUrl = model.start_urls;
            string loginUrl = model.login_url;
            string customData = model.customdata;
            string ruleData = model.ruledatas;
            if (startUrl != "")
            {
                startUrl = model.start_urls.Replace(@"'", @"\'");
            }
            if (loginUrl != null)
            {
                loginUrl= model.login_url.Replace(@"'", @"\'");
            }
            if (customData != "")
            {
                customData = model.customdata.Replace(@"'", @"\'");
            }
            if (ruleData != "")
            {
                ruleData = model.ruledatas.Replace(@"'", @"\'");
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SpiderInfo(");
            strSql.Append("spiderName,website,files_store,start_urls,allowed_domains,rules,class,loader,away,login_url,_account,customdata,cls,ruledatas,cookies)");
            strSql.Append(" values (");
            string values = "'" + model.spidername + "','" + model.website + "','" + model.file_store + "',\"" + startUrl + "\",'" + model.allowed_domains + "','" + model.rules + "','" + model._class + "','" + model.loader + "','" + model.away + "',\"" + loginUrl + "\",'" + model.account + "','" + customData + "','" + model.cls+ "','" + ruleData + "','" + model.cookies+ "')";
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
            strSql.Append("delete from SpiderInfo ");
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
        ///查询
        /// </summary>
        public object[] Select(string spiderName)
        {
            string SelectStr = "select * from SpiderInfo where spiderName='" + spiderName + "';";
            object[] Content = mshelper.SelectSQl(SelectStr);
            return Content;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.SpiderInfo model)
        {
            string startUrl = model.start_urls;
            string loginUrl = model.login_url;
            string customData = model.customdata;
            string ruleData = model.ruledatas;
            if (startUrl != "")
            {
                startUrl = model.start_urls.Replace(@"'", @"\'");
            }
            if (loginUrl != null)
            {
                loginUrl = model.login_url.Replace(@"'", @"\'");
            }
            if (customData != "")
            {
                customData = model.customdata.Replace(@"'", @"\'");
            }
            if (ruleData != "")
            {
                ruleData = model.ruledatas.Replace(@"'", @"\'");
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SpiderInfo set ");
            strSql.Append("website='" + model.website + "',files_store='" + model.file_store + "',start_urls=\"" + model.start_urls + "\",allowed_domains='" + model.allowed_domains + "',Rules='" + model.rules + "',class='" + model._class +
                "',loader='" + model.loader + "',away='" + model.away + "',login_url=\"" + model.login_url + "\",_account='" + model.account + "',customdata='" + customData + "',cls='" + model.cls + "',ruledatas='" + ruleData + "',cookies='" + model.cookies+"'");
            strSql.Append(" where spiderName='" + model.spidername + "'");
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
        /// 更新id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateId(string id,string spidername)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SpiderInfo set ");
            strSql.Append("missionId="+id);
            strSql.Append(" where spiderName='" +spidername + "'");
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
