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
    public delegate void GetCustomLinkHandler(string customLink);
    public partial class CustomLinkForm : Form
    {
        public event GetCustomLinkHandler getCustomLink;
        public CustomLinkForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (getCustomLink != null)
            {
                getCustomLink(txt_CustomLink.Text);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void CustomLinkForm_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;//使最大化窗口失效
        }
    }
}
