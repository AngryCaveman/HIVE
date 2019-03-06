using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class SpiderInfo
    {

        public SpiderInfo()
        {
            dal = new Dal.SpiderInfo();
        }
        Dal.SpiderInfo dal;
        #region  BasicMethod
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.SpiderInfo model)
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
        /// 更新
        /// </summary>
        public bool Update(Model.SpiderInfo model)
        {
            return dal.Update(model);
        }
        /// <summary>
        /// 更新id
        /// </summary>
        public bool UpdateId(string id ,string spidername)
        {
            return dal.UpdateId(id,spidername);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public object[] Select(string spiderName)
        {
            return dal.Select(spiderName);
        }

        #endregion  BasicMethod
    }
}
