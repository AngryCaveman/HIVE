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
using System.Collections;
using Newtonsoft.Json;
namespace Hive
{
    public delegate void SetNameHandler(string spiderName);//定义委托
    public partial class SettingForm : Form
    {
        #region=============属性===============
        public event SetNameHandler ChangeName;
        string  spiderName;
        bool CloseFormFlag=false;//判断是否关闭当前窗体
        BLL.SpiderInfo SI_Bll = new BLL.SpiderInfo();
        Model.SpiderInfo SI_Mod = new Model.SpiderInfo();
        BLL.HoneyInfo HI_Bll = new BLL.HoneyInfo();
        Model.HoneyInfo HI_Mod = new Model.HoneyInfo();
        ArrayList ClsRule_List=new ArrayList();//存储最终页面Cls和解析linkre
        ArrayList Cls_List = new ArrayList();//存储最终页面Cls
        ArrayList Cookies_List = new ArrayList();//存储所有cookies
        ArrayList Step_List = new ArrayList();//存储所有Step
        int CookiesCount=0;//Cookies的条数
        int _StepCount = 0;//步骤数
        #endregion
        #region=============初始化=============
        public SettingForm()
        {
            InitializeComponent();
            spiderName = null;
        }

        public SettingForm(string spn)
        {
            InitializeComponent();
            spiderName = spn;
        }
        #endregion
        #region===========载入窗体=============
        private void SettingForm_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;//使最大化窗口失效
            //
            if (spiderName != null)
            {
                //不允许修改名称
                txt_spidername.Enabled = false;
                //查询
                object[] Si_Obj=SI_Bll.Select(spiderName);
                //赋值界面
                txt_spidername.Text = spiderName;
                txt_website.Text = Convert.ToString(Si_Obj[1]).Trim();
                txt_filestore.Text = Convert.ToString(Si_Obj[2]).Trim();
                txt_starturls.Text = Convert.ToString(Si_Obj[3]).Trim();
                txt_domains.Text = Convert.ToString(Si_Obj[4]).Trim();
                comb_loader.SelectedItem = Convert.ToString(Si_Obj[7]).Trim();
                comb_away.SelectedItem = Convert.ToString(Si_Obj[8]).Trim();
                txt_loginurl.Text = Convert.ToString(Si_Obj[9]).Trim();
                //解析账户信息
                if (Convert.ToString(Si_Obj[10]).Trim()!="")
                {
                    Dictionary<string, string> aData = JsonConvert.DeserializeObject<Dictionary<string, string>>(Convert.ToString(Si_Obj[10]).Trim());
                    text_userlab.Text = aData.Keys.ToArray()[0];
                    text_pwdlab.Text = aData.Keys.ToArray()[1];
                    txt_usr.Text = aData.Values.ToArray()[0];
                    txt_pwd.Text = aData.Values.ToArray()[1];
                }
                //加载自定义数据信息，并显示
                string customdata= Convert.ToString(Si_Obj[11]).Trim();
               
                if (customdata != "")
                {
                    customdata = customdata.Substring(1, customdata.Length - 2);//去掉首尾的（）
                    foreach (string temp in customdata.Split(new string[] { "):(" }, StringSplitOptions.None))
                    {
                       
                        Tuple<string, string, string> data = AnalysisCls(temp);
                        string cls= data.Item1.Replace("\\",string.Empty).Replace("\"",string.Empty);
                        string method= data.Item2.Replace("\\", string.Empty).Replace("\"", string.Empty);
                        string linkre= data.Item3.Replace("\\", string.Empty).Replace("\"", string.Empty);
                        ListViewItem newlvitem = new ListViewItem();
                        newlvitem.Text = cls;     //添加一行的第一列
                        newlvitem.SubItems.Add(method);
                        newlvitem.SubItems.Add(linkre);
                        listV_cls.Items.Add(newlvitem);
                        MakeCls(cls, method, linkre);//重新生成ClsRule
                    }
                    customdata = Convert.ToString(Si_Obj[12]).Trim();
                  
                }
                //加载ruledatas
                string  stepdata = Convert.ToString(Si_Obj[13]).Trim();
                if (stepdata != "")
                {
                    stepdata= stepdata.Substring(1, stepdata.Length - 2);//去掉首尾的{}
                    Step_List = new ArrayList();
                  
                    foreach (string s in stepdata.Split(new string[] { "}#{" }, StringSplitOptions.None))
                    {
                        Dictionary<string, string> sdata = JsonConvert.DeserializeObject<Dictionary<string, string>>("{ " + s + "}");
                       
                        ListViewItem newlvitem = new ListViewItem();
                        newlvitem.Text = sdata["step"];     //添加一行的第一列
                        newlvitem.SubItems.Add(sdata["linkRe"]);
                        string flag = "否";
                        if (sdata["item"] != "")
                        {
                            flag = "是";
                            //需要报item重新构造一下
                            string item_temp = sdata["item"];
                            sdata["item"] = step_MakeCls(item_temp);
                        }
                        if (sdata["relevance"] != "")
                        {
                            string relevance_temp = sdata["relevance"];
                            sdata["relevance"] = step_MakeRelevance(relevance_temp);
                        }
                        newlvitem.SubItems.Add(flag);
                        listV_Step.Items.Add(newlvitem);
                        Step_List.Add(JsonConvert.SerializeObject(sdata));
                    }
                }
                //加载post信息
                //解析cookies：字典用#号分隔
                string cookiesdata = Convert.ToString(Si_Obj[14]).Trim();
                if (cookiesdata != "")
                {
                    cookiesdata = cookiesdata.Substring(1, cookiesdata.Length - 2);//去掉首尾的{}
                    Cookies_List = new ArrayList();
                    foreach (string temp in cookiesdata.Split(new string[] { "}#{" }, StringSplitOptions.None))
                    {
                        Cookies_List.Add("{ " + temp + "}");
                        CookiesCount += 1;
                        ListViewItem newlvitem = new ListViewItem();
                        newlvitem.Text = CookiesCount.ToString();     //添加一行的第一列
                        newlvitem.SubItems.Add(temp);
                        listV_cookies.Items.Add(newlvitem);
                    }
                }
                //从数据库查询上次的配置信息
                Object[] Hi_Obj = HI_Bll.Select(spiderName);
                txt_SqlIp.Text = Convert.ToString(Hi_Obj[1]).Trim(); 
                txt_SqlPort.Text = Convert.ToString(Hi_Obj[2]).Trim();
                txt_DBName.Text = Convert.ToString(Hi_Obj[5]).Trim();
                txt_SqlPwd.Text = Convert.ToString(Hi_Obj[4]).Trim();
                txt_SqlUsr.Text = Convert.ToString(Hi_Obj[3]).Trim();
                comb_SqlType.SelectedItem = Convert.ToString(Hi_Obj[6]).Trim();
            }
            else
            {
                //新建
                comb_loader.SelectedIndex = 0;
                comb_away.SelectedIndex = 0;

                //从配置文件加载上次的数据库信息
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
                    comb_SqlType.SelectedItem = ConfigurationManager.AppSettings["SqlType"];
                }
            }
            //默认存储位置
            txt_filestore.Text = "/home/ac/桌面/Hive/Hive/Honey_CSV/FILE/";
        }
        #endregion
        #region==============保存==============
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_save_Click(object sender, EventArgs e)
        {
            CloseFormFlag = true;
            BLL.Mission m_BLL = new BLL.Mission();
            string N = Convert.ToString(m_BLL.Select(txt_spidername.Text)[0]).Trim();
            string S = Convert.ToString(m_BLL.Select(txt_spidername.Text)[1]).Trim();
            if (S == "delete")
            {
                m_BLL.Delete(N);
            }
            if (N == "" || S=="delete" || spiderName != null)//进入条件：1,新建任务时，表中没有该任务；2，新建任务时，表中有该任务但是状态为废弃；3，修改任务；
            {

                #region 保存爬虫配置
                //获取信息
                SI_Mod.spidername = txt_spidername.Text;
                SI_Mod.rules = SI_Mod.spidername;
                SI_Mod._class = SI_Mod.spidername;
                SI_Mod.website = txt_website.Text;
                SI_Mod.start_urls = txt_starturls.Text;
                SI_Mod.allowed_domains = txt_domains.Text;
                SI_Mod.file_store = txt_filestore.Text;
               
                if (comb_loader.SelectedItem != null)
                {
                    SI_Mod.loader = comb_loader.SelectedItem.ToString();
                }
                if (comb_away.SelectedItem != null)
                {
                    SI_Mod.away = comb_away.SelectedItem.ToString();
                }
                
                SI_Mod.customdata = string.Join(":", (string[])ClsRule_List.ToArray(typeof(string)));
                SI_Mod.cls = string.Join(":", (string[])Cls_List.ToArray(typeof(string)));
                //在get模式下，post的配置将不存储
                if (comb_away.SelectedItem != null && comb_away.SelectedItem.ToString() == "post")
                {
                    //构造账户信息字典字符串
                    Dictionary<string, string> account = new Dictionary<string, string>();
                    account.Add(text_userlab.Text, txt_usr.Text);
                    account.Add(text_pwdlab.Text, txt_pwd.Text);
                    SI_Mod.account = JsonConvert.SerializeObject(account);
                    //登录url
                    SI_Mod.login_url = txt_loginurl.Text;
                    //cookies
                    if (Cookies_List.Count == 0)
                    { SI_Mod.cookies = ""; }
                    else
                    { SI_Mod.cookies = string.Join("#", (string[])Cookies_List.ToArray(typeof(string))); }
                }
                //rulesdata
                SI_Mod.ruledatas = string.Join("#", (string[])Step_List.ToArray(typeof(string)));
                #endregion
                #region 保存数据库信息
                //存入配置文件
                //获取Configuration对象
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //写入<add>元素的Value
                config.AppSettings.Settings["Server"].Value = txt_SqlIp.Text;
                config.AppSettings.Settings["Port"].Value = txt_SqlPort.Text;
                config.AppSettings.Settings["Database"].Value = txt_DBName.Text;
                config.AppSettings.Settings["User"].Value = txt_SqlUsr.Text;
                config.AppSettings.Settings["Password"].Value = txt_SqlPwd.Text;
                config.AppSettings.Settings["SqlStr"].Value = MakeSqlStr();
                if (comb_SqlType.SelectedItem != null)
                {
                    config.AppSettings.Settings["SqlType"].Value = comb_SqlType.SelectedItem.ToString();
                }
                
                //一定要记得保存，写不带参数的config.Save()也可以
                config.Save(ConfigurationSaveMode.Modified);
                //刷新，否则程序读取的还是之前的值（可能已装入内存）
                ConfigurationManager.RefreshSection("appSettings");

                //存入数据库
                HI_Mod.spidername = txt_spidername.Text;
                HI_Mod.server = txt_SqlIp.Text;
                HI_Mod.port = txt_SqlPort.Text;
                HI_Mod.dbname = txt_DBName.Text;
                HI_Mod.user = txt_SqlUsr.Text;
                HI_Mod.password = txt_SqlPwd.Text;
                if (comb_SqlType.SelectedItem != null)
                {
                    HI_Mod.sqltype = comb_SqlType.SelectedItem.ToString();
                }
                
                #endregion
                //存储
                if (spiderName != null)//更新
                {
                    if (SI_Bll.Update(SI_Mod))
                    { }
                    else
                    {
                        MessageBox.Show("配置文件更新失败，未保存到数据库！");
                        CloseFormFlag = false;
                    }
                }
                else//新增
                {
                    if (ChangeName != null)
                    {
                        ChangeName(txt_spidername.Text);//返回给主界面
                    }

                    if (SI_Bll.Add(SI_Mod))
                    {}
                    else
                    {
                        MessageBox.Show("配置文件更新失败，未添加到数据库！");
                        CloseFormFlag = false;
                    }
                }

                object[] c_temp = HI_Bll.Select(spiderName);
                if (Convert.ToString(c_temp[0]).Trim() == "")
                {
                    if (HI_Bll.Add(HI_Mod))
                    {}
                    else
                    {
                        MessageBox.Show("配置文件更新失败，未添加到数据库！");
                        CloseFormFlag = false;
                    }
                }
                else
                {
                    if (HI_Bll.Update(HI_Mod))
                    {}
                    else
                    {
                        MessageBox.Show("配置文件更新失败，未添加到数据库！");
                        CloseFormFlag = false;
                    }
                }
                if (CloseFormFlag)
                { this.DialogResult = DialogResult.OK; }
            }
           
            else
            {
                MessageBox.Show("该任务已经存在！" + txt_spidername.Text);
            }
        }
        #endregion
        #region=========添加，删除对象=========
        /// <summary>
        /// 添加对象按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_addobj_Click(object sender, EventArgs e)
        {
            RuleForm RF = new RuleForm();   
            RF.SetRule += new SetRuleHandler(setRule);
            DialogResult dr = RF.ShowDialog();
        }
        /// <summary>
        /// 获取解析对象的相关数据
        /// </summary>
        /// <param name="cls"></param>
        /// <param name="method"></param>
        /// <param name="linkre"></param>
        public void setRule(string cls, string method, string linkre)
        {
            //前台显示
            ListViewItem newlvitem = new ListViewItem();
            newlvitem.Text = cls;     //添加一行的第一列
            newlvitem.SubItems.Add(method);
            newlvitem.SubItems.Add(linkre);
            listV_cls.Items.Add(newlvitem);

            //模型赋值
            MakeCls(cls,method,linkre);
        }
        /// <summary>
        /// 删除对象按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_deleteobj_Click(object sender, EventArgs e)
        {
            if (listV_cls.SelectedItems.Count > 0)//有选中项
            {
                ClsRule_List.RemoveAt(listV_cls.SelectedItems[0].Index);
                Cls_List.RemoveAt(listV_cls.SelectedItems[0].Index);
                listV_cls.Items.RemoveAt(listV_cls.SelectedItems[0].Index);  
             }
        }
        #endregion
        #region============其他方法============
        private string step_MakeCls(string item_temp)
        {
            ArrayList step_ClsRule_List = new ArrayList();
            item_temp = item_temp.Substring(1, item_temp.Length - 2);//去掉首尾的（）
            foreach (string t in item_temp.Split(new string[] { "):(" }, StringSplitOptions.None))
            {

                Tuple<string, string, string> data = AnalysisCls(t);
                string cls = data.Item1.Replace("\\", string.Empty).Replace("\"", string.Empty);
                string method = data.Item2.Replace("\\", string.Empty).Replace("\"", string.Empty);
                string linkre = data.Item3.Replace("\\", string.Empty).Replace("\"", string.Empty);
                var tp = new Tuple<string, string, string>("\\\"" + cls + "\\\"", "\\\"" + method + "\\\"", "\\\"" + linkre + "\\\"");
                step_ClsRule_List.Add(tp.ToString());
            }
            return string.Join(":", (string[])step_ClsRule_List.ToArray(typeof(string)));
        }
        private Tuple<string, string, string, string> AnalysisRelevance(string dataStr)
        {
            //var data_temp = dataStr.Split();
            //var data_S = JsonConvert.DeserializeObject<Tuple<string,string, string>>("{("+dataStr+")}");

            string[] data = dataStr.Replace('(', ' ').Replace(')', ' ').Trim().Split(',');
            var tp = new Tuple<string, string, string, string>("解析错误", "", "", "");
            if (data.Length <= 4)
            {
                string ToStep = data[3].Split('t')[1];
                tp = new Tuple<string, string, string, string>(data[0], data[1], data[2], ToStep);
            }

            return tp;
        }
        private string step_MakeRelevance(string relevance_temp)
        {
            ArrayList Relevance_List = new ArrayList();
            relevance_temp = relevance_temp.Substring(1, relevance_temp.Length - 2);//去掉首尾的（）

            foreach (string t in relevance_temp.Split(new string[] { "):(" }, StringSplitOptions.None))
            {

                Tuple<string, string, string, string> data = AnalysisRelevance("(" + t + ")");
                string containtPath = data.Item1.Replace("\\", string.Empty).Replace("\"", string.Empty);
                string relevanceField = data.Item2.Replace("\\", string.Empty).Replace("\"", string.Empty);
                string relevanceXpath = data.Item3.Replace("\\", string.Empty).Replace("\"", string.Empty);
                string relevanceStep = data.Item4.Replace("\\", string.Empty).Replace("\"", string.Empty);
                var tp = new Tuple<string, string, string, string>("\\\"" + containtPath + "\\\"", "\\\"" + relevanceField + "\\\"", "\\\"" + relevanceXpath + "\\\"", "\\\"" + relevanceStep + "\\\"");
                Relevance_List.Add(tp.ToString());
            }
            return string.Join(":", (string[])Relevance_List.ToArray(typeof(string)));
        }
        private void MakeCls(string cls,string method,string linkre)
        {
            
            var tp=new Tuple<string,string, string>("\""+cls+"\"", "\"" + method + "\"", "\"" + linkre + "\"") ;
            ClsRule_List.Add(tp.ToString());
            Cls_List.Add(cls);
        }

        private Tuple<string, string, string> AnalysisCls(string dataStr)
        {
            string[] data=dataStr.Split(new Char[] { ',' },3);
            //var data_S = JsonConvert.DeserializeObject<Tuple<string,string, string>>("{("+dataStr+")}");
            var tp = new Tuple<string, string, string>(data[0],data[1],data[2]);
            return tp;
        }
        
        private string MakeSqlStr()
        {
            string SqlStr;
            SqlStr = "Server =" + txt_SqlIp.Text + "; Port =" + txt_SqlPort.Text + "; Database =" + txt_DBName.Text + "; User = " + txt_SqlUsr.Text + " ; Password =" + txt_SqlPwd.Text + "; Charset = utf8;";
            return SqlStr;
        }
        #endregion
        #region===========Cookies相关==========
        private void btn_addcookies_Click(object sender, EventArgs e)
        {
            CookiesForm CF = new CookiesForm();
            CF.getCookies += new GetCookiesHandler(setcookies);
            DialogResult dr = CF.ShowDialog();
        }

        private void setcookies(string cookies)
        {
            CookiesCount += 1;
            ListViewItem newlvitem = new ListViewItem();
            newlvitem.Text = CookiesCount.ToString();     //添加一行的第一列
            newlvitem.SubItems.Add(cookies);
            listV_cookies.Items.Add(newlvitem);
            string tp="{"+cookies+"}";
            Cookies_List.Add(tp);

        }


        private void btn_deletecookies_Click(object sender, EventArgs e)
        {
            if (listV_cookies.SelectedItems.Count > 0)//有选中项
            {
               
                try
                {
                    Cookies_List.RemoveAt(listV_cookies.SelectedItems[0].Index);
                    CookiesCount -= 1;
                    listV_cookies.Items.RemoveAt(listV_cookies.SelectedItems[0].Index);
                }
                catch { }
            }
        }
        #endregion
        #region============步骤相关============
        int stepindex;
        private void btn_AddStep_Click(object sender, EventArgs e)
        {
            StepSettingForm SSF = new StepSettingForm();
            SSF.GetStepContent += new GetStepContentHandler(getStepContent);
            DialogResult dr = SSF.ShowDialog();
            
        }
        private void getStepContent(string content,string step,string linkre,string follow)
        {
            _StepCount += 1;
            Step_List.Add(content);
            //界面显示
            ListViewItem newlvitem = new ListViewItem();
            newlvitem.Text = step;     //添加一行的第一列
            newlvitem.SubItems.Add(linkre);
            newlvitem.SubItems.Add(follow);
            listV_Step.Items.Add(newlvitem);
        }
        private void UpdateStepContent(string content, string step, string linkre, string flag)
        {
            Step_List[stepindex] = content;
            //界面显示
            listV_Step.SelectedItems[0].Text = step;
            listV_Step.SelectedItems[0].SubItems[1].Text = linkre;
            listV_Step.SelectedItems[0].SubItems[2].Text = flag;
        }
        private void btn_UpdateStep_Click(object sender, EventArgs e)
        {
            if (listV_Step.SelectedItems.Count > 0)//有选中项
            {
                stepindex=listV_Step.SelectedItems[0].Index;
                StepSettingForm SSF = new StepSettingForm(Step_List[stepindex].ToString());
                
                SSF.GetStepContent += new GetStepContentHandler(UpdateStepContent);
                DialogResult dr = SSF.ShowDialog();
            }
        }

        private void btn_DeleteStep_Click(object sender, EventArgs e)
        {
            if (listV_Step.SelectedItems.Count > 0)//有选中项
            {
                Step_List.RemoveAt(listV_Step.SelectedItems[0].Index);
                _StepCount -= 1;
                listV_Step.Items.RemoveAt(listV_Step.SelectedItems[0].Index);
            }
        }
        #endregion


    }
}
