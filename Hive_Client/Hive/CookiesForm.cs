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
    public delegate void GetCookiesHandler(string cookies);
    public partial class CookiesForm : Form
    {
        public event GetCookiesHandler getCookies;
        public CookiesForm()
        {
            InitializeComponent();
        }
        
        private void btn_AddCookies_Click(object sender, EventArgs e)
        {
            if (getCookies !=null) {
                getCookies(txt_Cookies.Text);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void CookiesForm_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;//使最大化窗口失效
        }
    }
}
