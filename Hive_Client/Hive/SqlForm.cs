using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Hive
{
    public partial class SqlForm : Form
    {
        public SqlForm()
        {
            InitializeComponent();
        }

        private void btn_SqlSave_Click(object sender, EventArgs e)
        {
            //获取Configuration对象
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //写入<add>元素的Value
            config.AppSettings.Settings["Server"].Value = txt_SqlIp.Text;
            config.AppSettings.Settings["Port"].Value = txt_SqlPort.Text;
            config.AppSettings.Settings["Database"].Value = txt_DBName.Text;
            config.AppSettings.Settings["User"].Value = txt_SqlUsr.Text;
            config.AppSettings.Settings["Password"].Value = txt_SqlPwd.Text;
            config.AppSettings.Settings["SqlStr"].Value = MakeSqlStr();
            config.AppSettings.Settings["SqlType"].Value = comb_SqlType.SelectedItem.ToString();
            //一定要记得保存，写不带参数的config.Save()也可以
            config.Save(ConfigurationSaveMode.Modified);
            //刷新，否则程序读取的还是之前的值（可能已装入内存）
            ConfigurationManager.RefreshSection("appSettings");
            this.DialogResult = DialogResult.OK;
        }
        private string MakeSqlStr()
        {
            string SqlStr;
            SqlStr = "Server =" + txt_SqlIp.Text + "; Port =" + txt_SqlPort.Text + "; Database =" + txt_DBName.Text + "; User = " + txt_SqlUsr.Text + " ; Password =" + txt_SqlPwd.Text + "; Charset = utf8;";
            return SqlStr;
        }

        private void SqlForm_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;//使最大化窗口失效
            string content;
            content = ConfigurationManager.AppSettings["SqlStr"];
            if (content == "")
            {
                txt_SqlIp.Text = "";
                txt_SqlPort.Text = "";
                txt_DBName.Text = "";
                txt_SqlPwd.Text = "";
                txt_SqlUsr.Text = "";
                comb_SqlType.SelectedIndex = 0;
            }
            else
            {
                txt_SqlIp.Text = ConfigurationManager.AppSettings["Server"];
                txt_SqlPort.Text = ConfigurationManager.AppSettings["Port"];
                txt_DBName.Text = ConfigurationManager.AppSettings["Database"];
                txt_SqlPwd.Text = ConfigurationManager.AppSettings["User"];
                txt_SqlUsr.Text = ConfigurationManager.AppSettings["Password"];
                comb_SqlType.SelectedItem= ConfigurationManager.AppSettings["SqlType"];
            }
        }
    }
}
