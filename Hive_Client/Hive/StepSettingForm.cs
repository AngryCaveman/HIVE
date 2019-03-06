using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using Newtonsoft.Json;


namespace Hive
{
    public  delegate void GetStepContentHandler(string content,string step,string linkre,string follow);
    public partial class StepSettingForm : Form
    {
        #region========属性==========
        public event GetStepContentHandler GetStepContent;
        Dictionary<string, string> Content_Dic = new Dictionary<string, string>();//存储一步的所有内容
        ArrayList ClsRule_List = new ArrayList();//存储该步骤所有Cls和解析linkre
        ArrayList Cls_List = new ArrayList();//存储该步骤所有Cls
        ArrayList CustomLink_List = new ArrayList();//存储该步骤所有自定义跳转链接
        ArrayList Relevance_List = new ArrayList();//存储该步骤中所有关联关系
        int customLink_count = 0;//记录自定义链接个数
        int relevanceCount = 0;//记录该步骤关联关系个数
        string UpdateStep = "";//修改第几步
        int clsCount = 0;//对象个数
        #endregion
        #region========初始化========
        public StepSettingForm()
        {
            InitializeComponent();
        }
        public StepSettingForm(string s)
        {
            UpdateStep = s;
            InitializeComponent();
        }
        #endregion
        #region=========载入=========
        private void StepSettingForm_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;//使最大化窗口失效
            if (UpdateStep != "")//修改信息
            {
                string temp = "";
                Dictionary<string, string> contentData = MakeStep(UpdateStep);
                txt_step.Text = contentData["step"];
                txt_paging.Text = contentData["paging"];
                txt_linkre.Text = contentData["linkRe"];
                try
                {
                    if (contentData["dynamic"] == "False")
                    {
                        temp = "静态";
                    }
                    else { temp = "动态"; }
                    comb_dynamic.SelectedItem = temp;
                    if (contentData["follow"] == "True")
                    {
                        temp = "是";
                    }
                    else { temp = "否"; }
                    comb_Follow.SelectedItem = temp;
                }
                catch (Exception error) { }
                string clsrule_temp = contentData["item"];
                
               
                //解析规则显示
                if (clsrule_temp != "")
                {
                    clsrule_temp = clsrule_temp.Substring(1, clsrule_temp.Length - 2);//去掉首尾的（）
                    foreach (string t in clsrule_temp.Split(new string[] { "):(" }, StringSplitOptions.None))
                    {
                        
                        Tuple<string, string, string> data = AnalysisCls(t);
                        string cls = data.Item1.Replace("\\", string.Empty).Replace("\"", string.Empty);
                        string method = data.Item2.Replace("\\", string.Empty).Replace("\"", string.Empty);
                        string linkre = data.Item3.Replace("\\", string.Empty).Replace("\"", string.Empty);
                        ListViewItem newlvitem = new ListViewItem();
                        newlvitem.Text = cls;     //添加一行的第一列
                        newlvitem.SubItems.Add(method);
                        newlvitem.SubItems.Add(linkre);
                        listV_StepCls.Items.Add(newlvitem);

                        MakeCls(cls,method,linkre);
                    }
                }
                clsCount = Cls_List.Count;
                //自定义链接显示
                if (contentData["customLink"] != "")
                { 
                    CustomLink_List = new ArrayList(contentData["customLink"].Split('|'));
                    foreach (string t in CustomLink_List)
                    {
                        customLink_count += 1;
                        ListViewItem newlvitem = new ListViewItem();
                        newlvitem.Text = customLink_count.ToString();     //添加一行的第一列
                        newlvitem.SubItems.Add(t);
                        listV_CustomLink.Items.Add(newlvitem);

                    }
                }
                //关联关系显示
                if (contentData["relevance"] != "")
                {
                    string relevance_temp = contentData["relevance"];
                    relevance_temp = relevance_temp.Substring(1, relevance_temp.Length - 2);//去掉首尾的（）
                    
                    foreach (string t in relevance_temp.Split(new string[] { "):(" }, StringSplitOptions.None))
                    {
                        
                        Tuple<string, string, string, string> data = AnalysisRelevance("(" + t + ")");
                        relevanceCount += 1;
                        string containtPath=data.Item1.Replace("\\", string.Empty).Replace("\"", string.Empty);
                        string relevanceField = data.Item2.Replace("\\", string.Empty).Replace("\"", string.Empty);
                        string relevanceXpath = data.Item3.Replace("\\", string.Empty).Replace("\"", string.Empty);
                        string relevanceStep = data.Item4.Replace("\\", string.Empty).Replace("\"", string.Empty);
                        ListViewItem newlvitem = new ListViewItem();
                        newlvitem.Text = relevanceCount.ToString();     //添加一行的第一列
                        newlvitem.SubItems.Add(data.Item1);
                        newlvitem.SubItems.Add(data.Item2);
                        newlvitem.SubItems.Add(data.Item4);
                        listV_Relevance.Items.Add(newlvitem);
                        MakeRelevance(containtPath, relevanceField, relevanceXpath, relevanceStep);
                    }
                }
            }
            else
            {
                comb_dynamic.SelectedIndex = 1;
                comb_Follow.SelectedIndex = 0;
            }

        }
        #endregion
        #region=======其他方法=======
        private void MakeCls(string cls, string method, string linkre)
        {
            var tp = new Tuple<string, string, string>("\\\"" + cls + "\\\"", "\\\"" + method + "\\\"", "\\\"" + linkre + "\\\"");
            ClsRule_List.Add(tp.ToString());
            Cls_List.Add(cls);
        }
        private void MakeRelevance(string containtPath, string relevanceField, string relevanceXpath, string relevanceStep)
        {
            string nowStep = txt_step.Text;
            relevanceStep = txt_step.Text + "t" + relevanceStep;//构造最后一个字段

            var tp = new Tuple<string, string, string, string>("\\\"" + containtPath + "\\\"", "\\\"" + relevanceField + "\\\"", "\\\"" + relevanceXpath + "\\\"", "\\\"" + relevanceStep + "\\\"");
            Relevance_List.Add(tp.ToString());
        }
        private Dictionary<string, string> MakeStep(string stepContent)
        {
            Dictionary<string, string> jsonDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(stepContent);
            return jsonDict;
        }
        private Tuple<string, string, string> AnalysisCls(string dataStr)
        {
            string[] data = dataStr.Split(new Char[] { ',' }, 3);
            
            var tp = new Tuple<string, string, string>(data[0], data[1], data[2]);
            return tp;
        }
        private Tuple<string, string, string,string> AnalysisRelevance(string dataStr)
        {
            //var data_temp = dataStr.Split();
            //var data_S = JsonConvert.DeserializeObject<Tuple<string,string, string>>("{("+dataStr+")}");
            
            string[] data = dataStr.Replace('(', ' ').Replace(')', ' ').Trim().Split(',');
            var tp=  new Tuple<string, string, string, string>("解析错误", "", "", "");
            if (data.Length <= 4)
            {
                string ToStep = data[3].Split('t')[1];
                tp = new Tuple<string, string, string, string>(data[0], data[1], data[2], ToStep);
            }
            
            return tp;
        }

        #endregion
        #region====添加，删除对象====
        private void btn_addStepCls_Click(object sender, EventArgs e)
        {
            RuleForm RF = new RuleForm();
            RF.SetRule += new SetRuleHandler(setRule);
            clsCount += 1;
            DialogResult dr = RF.ShowDialog();

        }
        public void setRule(string cls, string method, string linkre)
        {
            ListViewItem newlvitem = new ListViewItem();
            newlvitem.Text = cls;     //添加一行的第一列
            newlvitem.SubItems.Add(method);
            newlvitem.SubItems.Add(linkre);
            listV_StepCls.Items.Add(newlvitem);
            //模型赋值
            MakeCls(cls, method, linkre);
        }

        private void btn_deleteStepCls_Click(object sender, EventArgs e)
        {
            if (listV_StepCls.SelectedItems.Count > 0)//有选中项
            {
                ClsRule_List.RemoveAt(listV_StepCls.SelectedItems[0].Index);
                Cls_List.RemoveAt(listV_StepCls.SelectedItems[0].Index);
                listV_StepCls.Items.RemoveAt(listV_StepCls.SelectedItems[0].Index);
                clsCount -= 1;
             
            }
        }
        #endregion
        #region=========保存=========
        private void btn_SaveStep_Click(object sender, EventArgs e)
        {
            string temp;
            Content_Dic.Add("step", txt_step.Text);
            Content_Dic.Add("paging", txt_paging.Text);
            if (comb_dynamic.SelectedItem.ToString() == "静态")
            { temp = "False"; }
            else { temp = "True"; }
            Content_Dic.Add("dynamic", temp);
            Content_Dic.Add("linkRe", txt_linkre.Text);
            if (comb_Follow.SelectedItem.ToString() == "是")
            { temp = "True"; }
            else { temp = "False"; }
            Content_Dic.Add("follow", temp);
            //去重字段
            if (txt_dubField.Text == "")
            { txt_dubField.Text = ""; }
            Content_Dic.Add("dpField", txt_dubField.Text);
            //设置自定义链接
            if (CustomLink_List.Count == 0)
            {
                Content_Dic.Add("customLink", "");
            }
            else
            { Content_Dic.Add("customLink", string.Join("|", (string[])CustomLink_List.ToArray(typeof(string)))); }
            //关联字段
            if (Relevance_List.Count == 0)
            {
                Content_Dic.Add("relevance", "");
            }
            else { Content_Dic.Add("relevance", string.Join(":", (string[])Relevance_List.ToArray(typeof(string)))); }
            if (clsCount > 0 || UpdateStep !="")//需要解析页面
            {
                if (clsCount > 0)
                {
                    Content_Dic.Add("item", string.Join(":", (string[])ClsRule_List.ToArray(typeof(string))));
                    Content_Dic.Add("cls", string.Join(":", (string[])Cls_List.ToArray(typeof(string))));
                }
                else if (UpdateStep != "")
                {
                    string value = string.Join(":", (string[])ClsRule_List.ToArray(typeof(string)));
                    Content_Dic["item"] = value;
                    Content_Dic["cls"] = string.Join(":", (string[])Cls_List.ToArray(typeof(string)));
                }
                
            }
            else
            {
                Content_Dic.Add("item", "");
                Content_Dic.Add("cls", "");
            }
            string contentStr = JsonConvert.SerializeObject(Content_Dic);
            //返回
            if (GetStepContent != null)
            {
                string flag = "否";
                if (Content_Dic["item"] != "")
                {
                    flag = "是";
                }
                GetStepContent(contentStr,txt_step.Text,txt_linkre.Text,flag);
            }
            this.DialogResult = DialogResult.OK;

        }
        #endregion
        #region==添加自定义跳转链接==
        private void btn_addcustomLink_Click(object sender, EventArgs e)
        {
            CustomLinkForm CLF = new CustomLinkForm();
            CLF.getCustomLink+= new GetCustomLinkHandler(setcustomlink);
            DialogResult dr = CLF.ShowDialog();
        }
        private void setcustomlink(string customlink)
        {
            customLink_count += 1;
            ListViewItem newlvitem = new ListViewItem();
            newlvitem.Text = customLink_count.ToString();     //添加一行的第一列
            newlvitem.SubItems.Add(customlink);
            listV_CustomLink.Items.Add(newlvitem);
            CustomLink_List.Add(customlink);
        }
        private void btn_delcustomLink_Click(object sender, EventArgs e)
        {
            if (listV_CustomLink.SelectedItems.Count > 0)//有选中项
            {
               
                try
                {
                    CustomLink_List.RemoveAt(listV_CustomLink.SelectedItems[0].Index);
                    customLink_count -= 1;
                    listV_CustomLink.Items.RemoveAt(listV_CustomLink.SelectedItems[0].Index);
                }
                catch { }
            }
        }

        #endregion
        #region=====添加关联字段=====
        private void btn_addRelevance_Click(object sender, EventArgs e)
        {
            RelevanceForm RF = new RelevanceForm();
            RF.getRelevance += new GetRelevanceHandler(setRelevance);
            DialogResult dr = RF.ShowDialog();
        }
        private void setRelevance(string containtPath, string relevanceField, string relevanceXpath, string relevanceStep)
        {
            relevanceCount += 1;
            ListViewItem newlvitem = new ListViewItem();
            newlvitem.Text = relevanceCount.ToString();     //添加一行的第一列
            newlvitem.SubItems.Add(containtPath);
            newlvitem.SubItems.Add(relevanceField);
            newlvitem.SubItems.Add(relevanceStep);
            listV_Relevance.Items.Add(newlvitem);
            //添加进列表
            MakeRelevance(containtPath, relevanceField, relevanceXpath, relevanceStep);
        }

        private void btn_delRelevance_Click(object sender, EventArgs e)
        {
            if (listV_Relevance.SelectedItems.Count > 0)//有选中项
            {

                try
                {
                    Relevance_List.RemoveAt(listV_Relevance.SelectedItems[0].Index);
                    relevanceCount-= 1;
                    listV_Relevance.Items.RemoveAt(listV_Relevance.SelectedItems[0].Index);
                }
                catch { }
            }
        }
        #endregion
    }
}
