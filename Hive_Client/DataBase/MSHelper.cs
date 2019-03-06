using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataBase
{
    public class MSHelper
    {
        MySqlConnection connection;
        string connectionStr;
        //数据库处理类
        public MSHelper()
        {
            connectionStr = ConfigurationManager.ConnectionStrings["MySqlStr"].ConnectionString;
        }
        /// <summary>
        /// 
        ///建立连接
        /// </summary>
        public void SQLConnect()
        {
            connection = new MySqlConnection(connectionStr);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                
            }
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
            catch (Exception ex)
            { }
        }
        /// <summary>
        /// 查询数据库,只返回一条数据
        /// </summary>
        /// <param name="SelectCommand">查询语句</param>
        public object[] SelectSQl(string SelectCommand)
        {
            object[] Content = new object[20];

            SQLConnect();//连接数据库
            try
            {
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = SelectCommand;

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    reader.GetValues(Content);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                return Content;
            }
            CloseConnect();//关闭数据库
            return Content;
        }

        /// <summary>
        /// 查询数据库,返回多条数据
        /// </summary>
        /// <param name="SelectCommand">查询语句</param>
        public DataTable SelectSQl_More(string SelectCommand)
        {
            DataTable dt = null;
            try
            {
                SQLConnect();//连接数据库
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = SelectCommand;
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                dt = new DataTable();
                adapter.Fill(dt);
                CloseConnect();//关闭数据库
            }
            catch (Exception ex)
            {
            }
            return dt;
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
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = InsertCommand;
            try
            {
                aim = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            { aim = -1; }
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
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = DeleteCommand;
            int aim;
            try
            {
                aim = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            { aim = -1; }
            CloseConnect();//关闭数据库
            return aim;
        }
        /// <summary>
        /// 更新数据库，返回影响行数
        /// </summary>
        /// <param name="UpdateCommand">更新语句</param>
        /// <returns></returns>
        public int UpdateSQl(string UpdateCommand)
        {
            int aim;
            SQLConnect();//连接数据库
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = UpdateCommand;
            try
            {
                aim = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            { aim = -1; }
            CloseConnect();//关闭数据库
            return aim;
        }
    }
}
