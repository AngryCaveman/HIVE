using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hive
{
    public delegate void GetUserIdHandler(string userid);
   
    public partial class LoginForm : Form
    {
        public event GetUserIdHandler GetUserID;
        BLL.User UI_Bll = new BLL.User();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            string UserStatus;//用户状态
            string UserId = textBox1.Text;//用户ID
            UserStatus = Convert.ToString(UI_Bll.SelectStatus(UserId)[0]).Trim();
            if (UserStatus == "")
            {
                MessageBox.Show("不存在ID："+UserId);
            }
            else if (UserStatus == "offline")//登录成功
            {
                if (GetUserID != null)
                {
                    GetUserID(UserId);
                }
                this.DialogResult = DialogResult.OK;
                //上线
                UI_Bll.Change2Online(UserId);
            }
            else if(UserStatus == "online")
            {
                MessageBox.Show("用户ID"+UserId+"正在使用");
            }

           
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;//使最大化窗口失效
        }
    }
}
