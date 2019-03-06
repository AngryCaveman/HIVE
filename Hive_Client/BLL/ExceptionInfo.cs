using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace BLL
{
    public partial class ExceptionInfo
    {
        public ExceptionInfo()
        {
            dal = new Dal.ExceptionInfo();
        }
        Dal.ExceptionInfo dal;
        #region  BasicMethod
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.ExceptionInfo model)
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

   
        /// <summary>
        /// 获取表中所有内容
        /// </summary>
        /// <param name="spiderName"></param>
        /// <returns></returns>
        public DataTable SelectMore()
        {
            return dal.SelectMore();
        }

        #endregion  BasicMethod
    }
}
