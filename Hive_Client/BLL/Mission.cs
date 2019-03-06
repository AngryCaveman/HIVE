using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace BLL
{
    public partial class Mission
    {
        public Mission()
        {
            dal = new Dal.Mission();
        }
        Dal.Mission dal;
        #region  BasicMethod
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.Mission model)
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
        /// 更新任务状态
        /// </summary>
        public bool UpdateStatus(string spiderName, string Status)
        {
            return dal.UpdateStatus(spiderName,Status);
        }

        /// <summary>
        /// 更新任务优先级
        /// </summary>
        public bool UpdatePRI(string spiderName, string Status)
        {
            return dal.UpdatePRI(spiderName, Status);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public object[] Select(string spiderName)
        {
            return dal.Select(spiderName);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable SelectWorkingMission(string userid)
        {
            return dal.SelectWorkingMission(userid);
        }
        /// <summary>
        /// 得到当前ID所有任务和状态
        /// </summary>
        /// <param name="spiderName"></param>
        /// <returns></returns>
        public DataTable SelectSpiderName(string userId)
        {
            return dal.SelectSpiderName(userId);
        }

        #endregion  BasicMethod
    }
}
