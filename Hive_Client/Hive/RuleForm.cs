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
    public delegate void SetRuleHandler(string cls, string methods, string linkre);//委托
    public partial class RuleForm : Form
    {
        #region========初始化========
        public RuleForm()
        {
            InitializeComponent();
        }
        private void RuleForm_Load(object sender, EventArgs e)
        {
            comb_method.SelectedIndex = 0;
            this.MaximizeBox = false;//使最大化窗口失效
        }
        #endregion
        #region=========属性=========
        public event SetRuleHandler SetRule;//事件
        #endregion
        #region=========保存=========
        private void btn_saveRule_Click(object sender, EventArgs e)
        {
            if (SetRule != null) {
                SetRule(txt_obj.Text,comb_method.SelectedItem.ToString(), txt_linkre.Text);
            }

            //关闭窗口
            this.DialogResult = DialogResult.OK;
        }
        #endregion

        
    }
}
