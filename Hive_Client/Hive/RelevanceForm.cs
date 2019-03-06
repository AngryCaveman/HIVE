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
    public delegate void GetRelevanceHandler(string containtPath,string relevanceField,string relevanceXpath,string relevanceStep);
    public partial class RelevanceForm : Form
    {
        public event GetRelevanceHandler getRelevance;
        public RelevanceForm()
        {
            InitializeComponent();
        }

        private void btn_addRelevance_Click(object sender, EventArgs e)
        {
            if (getRelevance != null)
            {
                getRelevance(txt_containtPath.Text,txt_relevanceField.Text,txt_relevanceXpath.Text,txt_relevanceStep.Text);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void RelevanceForm_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;//使最大化窗口失效
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txt_relevanceField.Text = "SourceUrl";
        }
    }
}
