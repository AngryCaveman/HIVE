using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace BLL
{
    public partial class MissionInfo
    {
        public MissionInfo()
        {
            dal = new Dal.MissionInfo();
        }
        Dal.MissionInfo dal;
        #region  BasicMethod
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.MissionInfo model)
        {
            return dal.Add(model);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string spiderName)
        {
            return dal.Delete(spiderName);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public object[] Select(string spiderName)
        {
            return dal.Select(spiderName);
        }

        public DataTable SelectAll()
        {
            return dal.SelectAll();
        }
        #endregion  BasicMethod
    }
}
