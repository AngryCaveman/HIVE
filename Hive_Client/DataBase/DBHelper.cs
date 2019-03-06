using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace DataBase
{
    public class DBHelper
    {
        //数据库处理类
        public DBHelper()
        {
            connectionStr = ConfigurationManager.ConnectionStrings["SqlServerStr"].ConnectionString;
        }
        SqlConnection connection;
        string connectionStr;
        /// <summary>
        /// 
        ///建立连接
        /// </summary>
        public void SQLConnect()
        {
            connection = new SqlConnection(connectionStr);
            try
            {
                connection.Open();
            }
            catch
            { }
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        public void CloseConnect()
        {
            try
            {
                connection.Close();
            }
            catch
            { }
        }
        /// <summary>
        /// 查询数据库,返回一个SqlDataReader
        /// </summary>
        /// <param name="SelectCommand">查询语句</param>
        public object[] SelectSQl(string SelectCommand)
        {
            object[] Content = new object[8];

            SQLConnect();//连接数据库
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = SelectCommand;

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                reader.GetValues(Content);
            }
            reader.Close();
            CloseConnect();//关闭数据库
            return Content;
        }
        /// <summary>
        /// 插入数据库，返回影响行数
        /// </summary>
        /// <param name="InsertCommand">插入语句</param>
        /// <returns></returns>
        public int InsertSQl(string InsertCommand)
        {
            SQLConnect();//连接数据库
            int aim;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = InsertCommand;
            try
            {
                aim = command.ExecuteNonQuery();
            }
            catch { aim = -1; }
            CloseConnect();//关闭数据库
            return aim;
        }
        /// <summary>
        /// 删除数据库，返回影响行数
        /// </summary>
        /// <param name="DeletCommand">删除语句</param>
        /// <returns></returns>
        public int DeleteSQL(string DeleteCommand)
        {
            SQLConnect();//连接数据库
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = DeleteCommand;
            int aim;
            try
            {
                aim = command.ExecuteNonQuery();
            }
            catch
            { aim = -1; }
            CloseConnect();//关闭数据库
            return aim;
        }
        /// <summary>
        /// 更新数据库，返回影响行数
        /// </summary>
        /// <param name="InsertCommand">更新语句</param>
        /// <returns></returns>
        public int UpdateSQl(string UpdateCommand)
        {
            int aim;
            SQLConnect();//连接数据库
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = UpdateCommand;
            try
            {
                aim = command.ExecuteNonQuery();
            }
            catch { aim = -1; }
            CloseConnect();//关闭数据库
            return aim;
        }
    }
}
