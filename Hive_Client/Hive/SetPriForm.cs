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
    public partial class SetPriForm : Form
    {
        BLL.Mission M_BLL = new BLL.Mission();
        string spiderName;
        public SetPriForm()
        {
            InitializeComponent();
        }
        public SetPriForm(string name)
        {
            InitializeComponent();
            spiderName = name;
        }

        private void btn_SavePRI_Click(object sender, EventArgs e)
        {
            string pri;
            if (radioB_Com.Checked)
            {
                pri = "common";
            }
            else if (radioB_High.Checked)
            {
                pri = "high";
            }
            else if (radioB_Low.Checked)
            {
                pri = "low";
            }
            else
            {
                pri = "common";
            }
            M_BLL.UpdatePRI(spiderName, pri);
            this.DialogResult = DialogResult.OK;
        }

        private void SetPriForm_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;//使最大化窗口失效
            string pri = Convert.ToString(M_BLL.Select(spiderName)[0]).Trim();
            if (pri == "common")
            {
                radioB_Com.Checked = true;
            }
            else if (pri == "high")
            {
                radioB_High.Checked = true;
            }
            else if (pri == "low")
            {
                radioB_Low.Checked = true;
            }

        }
    }
}
