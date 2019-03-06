using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class User
    {
        public User()
        {
            dal = new Dal.User();
        }
        Dal.User dal;
        #region  BasicMethod
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.User model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public object[] Select(string userID)
        {
            return dal.Select(userID);
        }
        /// <summary>
        /// 查询状态
        /// </summary>
        public object[] SelectStatus(string userID)
        {
            return dal.SelectStatus(userID);
        }

        /// <summary>
        /// 上线
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool Change2Online(string userID)
        {
            return dal.UpdateStatus("online",userID);
        }

        /// <summary>
        /// 下线
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool Change2Offline(string userID)
        {
            return dal.UpdateStatus("offline",userID);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string UserID)
        {
            return dal.Delete(UserID);
        }

        #endregion  BasicMethod
    }
}
