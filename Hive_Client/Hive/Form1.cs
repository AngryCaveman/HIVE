using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using System.Configuration;
using System.Collections;
using System.Threading;
using System.Diagnostics;
namespace Hive
{
   
    public partial class MainForm : Form
    {
        #region========属性========
        string nametemp;
        string userId;
        BLL.Mission MI_BLL = new BLL.Mission();
        Model.Mission MI_Mod = new Model.Mission();
        BLL.MissionInfo MIF_BLL = new BLL.MissionInfo();
        BLL.SpiderInfo SI_BLL = new BLL.SpiderInfo();
        BLL.HoneyInfo HI_BLL = new BLL.HoneyInfo();
        BLL.User User_BLL = new BLL.User();
        BLL.ExceptionInfo EI_BLL = new BLL.ExceptionInfo();
        DataTable ExceptionDT;
        #endregion
        #region=======初始化=======
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion
        #region========载入========
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;//使最大化窗口失效
            //查看详情按钮暂时屏蔽
            btn_Details.Visible = false;
            //载入登录窗体
            LoginForm log = new LoginForm();
            log.GetUserID += new GetUserIdHandler(getUserId);
            DialogResult dr = log.ShowDialog();
            if (dr != DialogResult.OK)
            {
                this.Close();
            }
            //登录成功，加载当前ID任务信息
            toolStripStatusLabel1.Text = "当前使用ID："+userId;
            listBox1.Items.Add(DateTime.Now.ToString()+"：欢迎"+userId);
            //
            ExceptionDT = EI_BLL.SelectMore();
            GetMissionStatus();//根据任务状态显示结点
            GetMissionInfo();//刷新任务信息显示
            GetExceptionInfo();//刷新异常信息显示
        }
        #endregion
        #region====右键菜单功能====
        private void 重启任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = treeView1.SelectedNode.Text;

            //MI_BLL.Delete(name);
            string status = Convert.ToString(MI_BLL.Select(name)[1]).Trim();
            if (status != "complete")
            {
                MessageBox.Show("该任务重启失败：" + name);
            }
            else
            {
                if (MI_BLL.UpdateStatus(name, "wait"))//标致该任务废弃
                {
                }
                else
                {
                    MessageBox.Show("重启任务失败：" + name);
                }
                //SI_BLL.Delete(name);//任务配置删除
                listBox1.Items.Add(DateTime.Now.ToString() + "：" +"已重启任务：" + name);
            }
        }
        private void 添加任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingForm SF = new SettingForm();
            SF.ChangeName += new SetNameHandler(getSpiderName);
            DialogResult dr = SF.ShowDialog();
            
            //********
            if (dr == DialogResult.OK)
            { 

                //存入数据库
                MI_Mod.spidername = nametemp;
                MI_Mod.status = "new";
                MI_Mod.userid = userId;
                MI_Mod.pri = "common";//默认为普通优先级
                if (MI_BLL.Add(MI_Mod))
                {
                    TreeNode newTN = new TreeNode();
                    newTN.Text = nametemp;
                    newTN.Name = nametemp;
                    newTN.ImageIndex = 0;          //未选中时图标
                    newTN.SelectedImageIndex = 0; //选中时图标
                    treeView1.Nodes["New_Node"].Nodes.Add(newTN);
                }
                else { MessageBox.Show("任务添加至数据库失败"); }
                //更新SpiderInfo的id
                BLL.SpiderInfo SI_BLL = new BLL.SpiderInfo();
                string idtemp = Convert.ToString(MI_BLL.Select(nametemp)[4]).Trim();
                if (SI_BLL.UpdateId(idtemp, nametemp))
                { }
                else
                { MessageBox.Show("任务id添加至数据库失败"); }
            }
        }

        private void 查看信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string spn ;
            if ( (treeView1.SelectedNode.Name == "Wait_Node") || (treeView1.SelectedNode.Name == "Complete_Node") || (treeView1.SelectedNode.Name == "Working_Node"))
            {
                MessageBox.Show("所选中为根节点");
            }
            else
            {
                spn = treeView1.SelectedNode.Name;
                SettingForm SF = new SettingForm(spn);
                DialogResult dr = SF.ShowDialog();
            }
        }
        /// <summary>
        /// 双击结点查看信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if ((treeView1.SelectedNode.Name == "New_Node") || (treeView1.SelectedNode.Name == "Wait_Node") || (treeView1.SelectedNode.Name == "Complete_Node") || (treeView1.SelectedNode.Name == "Working_Node"))
            {
                MessageBox.Show("所选中为根节点");
            }
            else
            {
                string spn;
                spn = treeView1.SelectedNode.Name;
                SettingForm SF = new SettingForm(spn);
                DialogResult dr = SF.ShowDialog();
            }
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = treeView1.SelectedNode.Text;

            //MI_BLL.Delete(name);
            string status=Convert.ToString(MI_BLL.Select(name)[1]).Trim();
            if (status == "delete")
            {
                MessageBox.Show("该任务已经废止：" + name);
            }
            else
            {
                if (MI_BLL.UpdateStatus(name, "delete"))//标致该任务废弃
                {
                    if(SI_BLL.Delete(name))//删除任务配置
                    { }
                    else
                    { MessageBox.Show("删除基本配置失败！"); }
                    if (HI_BLL.Delete(name))//删除数据库配置
                    { }
                    else
                    { MessageBox.Show("删除数据库配置失败！"); }
                    if (EI_BLL.Delete(name))
                    { }
                    else
                    { MessageBox.Show("删除异常数据失败！"); }
                }
                else
                {
                    MessageBox.Show("废止任务失败：" + name);
                }
                //SI_BLL.Delete(name);//任务配置删除
                MessageBox.Show("已废弃任务：" + name);
            }
        }
        /// <summary>
        /// 启动任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 启动任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = treeView1.SelectedNode.Text;
            string status = Convert.ToString(MI_BLL.Select(name)[1]).Trim();
            if (status == "wait")
            {
                MessageBox.Show("该任务正在等待：" + name);
            }
            else
            {
                if (MI_BLL.UpdateStatus(name, "wait"))//标致该任务进人等待
                { MessageBox.Show("已启动任务：" + name); }
                else
                {
                    MessageBox.Show("启动任务失败：" + name);
                }
                
            }
        }
        /// <summary>
        /// 停止任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 停止任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = treeView1.SelectedNode.Text;
            string status = Convert.ToString(MI_BLL.Select(name)[1]).Trim();
            if (status == "working")
            {
                MessageBox.Show("正在停止：" + name);
                if (MI_BLL.UpdateStatus(name, "end"))//标致该任务进人等待
                { MessageBox.Show("已终止任务，需要几秒缓冲：" + name); }
                else
                {
                    MessageBox.Show("终止任务失败：" + name);
                }
            }
            else
            {
                MessageBox.Show("无法停止：" + name);
            }
        }
        /// <summary>
        /// 设置优先级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 设置优先ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = treeView1.SelectedNode.Text;
            SetPriForm SPF = new SetPriForm(name);
            DialogResult dr = SPF.ShowDialog();
        }
        #endregion
        #region==线程刷新任务信息==
        private delegate void flushInfoCallBack(DataTable data,DataTable spiderName);
        private void GetMissionInfo()
        {
            Thread MissionInfo_T = new Thread(ListenSqlServer);
            MissionInfo_T.IsBackground = true;
            MissionInfo_T.Start();
        }
        private void ListenSqlServer()
        {
            while (true)
            {
                DataTable data = MIF_BLL.SelectAll();
                DataTable spiderName = MI_BLL.SelectWorkingMission(userId);
                Thread.Sleep(1000);//等待1s
                MissionInfoUpDateListView(data, spiderName);
            }
        }
        private void MissionInfoUpDateListView(DataTable data, DataTable spiderName)
        {
            

            if (this.InvokeRequired)
            {
                flushInfoCallBack d = new flushInfoCallBack(
                   MissionInfoUpDateListView);
                this.Invoke(d, data,spiderName); 
                return;
            } 
            bool flag ;
            //显示
            //只保留正在运行的任务信息

            foreach (DataRow dr in data.Rows)
            {
                if (spiderName.Rows.Count > 0)
                {
                    if (spiderName.Select("spidername ='" + dr[0].ToString() + "'").Length < 1)//如果不是正在运行的任务，则不显示
                    {
                        //在此处可以做删除多余显示的操作
                        //1,判断显示中是否存在该dr[0].ToString()
                        //2,存在则删除
                        ListViewItem[] del_Items = listV_Details.Items.Find(dr[0].ToString(), false);
                        if (del_Items.Length >= 1)//存在
                        {
                            foreach (ListViewItem del_Item in del_Items)
                            {
                                listV_Details.Items.Remove(del_Item);
                            }
                        }
                        continue;
                    }
                    else
                    {
                        string timeResult = "刚刚启动";
                        //计算运行时间
                        if (dr[4].ToString() != "")
                        {
                            TimeSpan runTime = DateTime.Parse(dr[4].ToString()) - DateTime.Parse(dr[5].ToString());
                            timeResult = runTime.ToString();
                        }
                        flag = true;//默认为新增
                        ListViewItem informationlist = new ListViewItem();
                        ListViewItem[] temp;
                        if (listV_Details.Items.Count != 0)
                        {
                            temp = listV_Details.Items.Find(dr[0].ToString(), false);
                            if (temp.Length != 0)
                            {
                                flag = false;
                                informationlist = temp[0];//只能找到一个
                            }
                        }
                        if (flag)//新增
                        {
                            informationlist.Text = dr[0].ToString();
                            informationlist.Name = dr[0].ToString();
                            informationlist.SubItems.Add(dr[1].ToString());
                            informationlist.SubItems.Add(dr[2].ToString());
                            informationlist.SubItems.Add(dr[8].ToString());
                            informationlist.SubItems.Add(dr[6].ToString());
                            informationlist.SubItems.Add(dr[7].ToString());
                            informationlist.SubItems.Add(timeResult);
                            listV_Details.Items.Add(informationlist);
                        }
                        else//修改
                        {
                            informationlist.Text = dr[0].ToString();
                            informationlist.Name = dr[0].ToString();
                            informationlist.SubItems[1].Text = dr[1].ToString();
                            informationlist.SubItems[2].Text = dr[2].ToString();
                            informationlist.SubItems[3].Text = dr[8].ToString();
                            informationlist.SubItems[4].Text = dr[6].ToString();
                            informationlist.SubItems[5].Text = dr[7].ToString();
                            informationlist.SubItems[6].Text = timeResult;
                        }
                    }
                }
            }   
        }

        #endregion
        #region ==线程刷新任务列表==
        private delegate void flushStatusCallBack(DataTable data);
        private void GetMissionStatus()
        {
            Thread MissionInfo_T = new Thread(ListenSqlForStatus);
            MissionInfo_T.IsBackground = true;
            MissionInfo_T.Start();
        }
        private void ListenSqlForStatus()
        {
            while (true)
            {
                DataTable data = MI_BLL.SelectSpiderName(userId); ;
                Thread.Sleep(1000);//等待1s
                MissionStatusUpDateListView(data);
            }
        }
        private void MissionStatusUpDateListView(DataTable content)
        {
            if(this.InvokeRequired)
            {
                flushStatusCallBack d = new flushStatusCallBack(
                   MissionStatusUpDateListView);
                this.Invoke(d, content);
                return;
            }
            TreeNode index_flag =null;
            string name ;
            int index;
            string type ;
            TreeNode[] temp;
            bool flag;
            if (treeView1.SelectedNode != null)
            {
                index_flag = treeView1.SelectedNode;
            }
            //显示
            foreach (DataRow row in content.Rows)
            {
                flag = true;
                name = "";
                index = 0;
                type = "";
                TreeNode newTN = new TreeNode();
                temp = treeView1.Nodes.Find(row[0].ToString(), true);

                if (temp.Length != 0)
                {
                    newTN = temp[0];//只能找到一个
                    treeView1.Nodes.Remove(newTN);
                }


                //获取值
                if (row[1].ToString() == "new")
                {
                    name = row[0].ToString();
                    index = 0;
                    type = "New_Node";
                }
                else if (row[1].ToString() == "wait")
                {
                    name = row[0].ToString();
                    index = 3;
                    type = "Wait_Node";
                }
                else if (row[1].ToString() == "complete")
                {
                    name = row[0].ToString();
                    index = 1;
                    type = "Complete_Node";
                }
                else if (row[1].ToString() == "working")
                {
                    name = row[0].ToString();
                    index = 2;
                    type = "Working_Node";
                }
                else {
                    flag = false;
                }

                if (flag)
                {
                    newTN.Text = name;
                    newTN.Name = name;
                    newTN.ImageIndex = index;          //未选中时图标
                    newTN.SelectedImageIndex = index; //选中时图标
                    treeView1.Nodes[type].Nodes.Add(newTN);
                }

            }
            if (index_flag != null)
            {
                treeView1.SelectedNode= index_flag;
            }
        }
        #endregion
        #region==线程刷新异常信息==
        private delegate void flushExceptionCallBack(DataTable data,DataTable SpiderName);
        private void GetExceptionInfo()
        {
            Thread ExceptionInfo_T = new Thread(ListenSqlForException);
            ExceptionInfo_T.IsBackground = true;
            ExceptionInfo_T.Start();
        }
        private void ListenSqlForException()
        {
            while (true)
            {
                DataTable data = EI_BLL.SelectMore() ;
                DataTable spiderName = MI_BLL.SelectWorkingMission(userId);
                Thread.Sleep(1000);//等待1s
                ExceptionInfoUpDateListBox(data,spiderName);
            }
        }
        private void ExceptionInfoUpDateListBox(DataTable data, DataTable spiderName)
        {
            if (this.InvokeRequired)
            {
                flushExceptionCallBack d = new flushExceptionCallBack(
                   ExceptionInfoUpDateListBox);
                this.Invoke(d, data,spiderName);
                return;
            }
            foreach (DataRow dr in data.Rows)
            {
                if (spiderName.Rows.Count > 0)
                {
                    if (spiderName.Select("spidername ='" + dr[0].ToString() + "'").Length < 1)
                    {
                        //不是当前id下正在运行的任务，跳过
                    }
                    else
                    {
                        if (ExceptionDT.Select("spidername ='" + dr[0].ToString() + "'").Length > 0)
                        {
                            //根据数据插入的时间判断异常信息是否有更新，如果时间不一致，则该条数据显示，
                            if (dr[2].ToString() != ExceptionDT.Select("spidername ='" + dr[0].ToString() + "'")[0][2].ToString())
                            {
                                listBox1.Items.Add(DateTime.Now.ToString() + "：" + dr[0].ToString() + "异常" + dr[1].ToString());
                            }
                        }
                        else if (dr[1].ToString() != "")
                        {
                            listBox1.Items.Add(DateTime.Now.ToString() + "：" + dr[0].ToString() + "异常" + dr[1].ToString());
                        }
                    }
                }
            }
            //更新临时数据ExceptionDT
            ExceptionDT = data;
            //始终控制滚动条在最底部
            if (listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
        }
        #endregion
        #region======其他方法======
        public void getSpiderName(string name)
        {
            nametemp = name;
        }
        public void getUserId(string userid)
        {
            userId = userid;
        }
        #endregion
        #region==关闭窗口结束进程==
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //切换当前用户id状态：下线
            if (userId != null)
            {
                User_BLL.Change2Offline(userId);
            }
            //关闭窗口时结束该进程
            Process.GetCurrentProcess().Kill();
        }
        #endregion
        #region====右键菜单显示====
        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Parent == null)
            {
                contextMenuStrip1.Items[0].Enabled = true;
                contextMenuStrip1.Items[1].Enabled = false;
                contextMenuStrip1.Items[2].Enabled = false;
                contextMenuStrip1.Items[3].Enabled = false;
                contextMenuStrip1.Items[4].Enabled = false;
                contextMenuStrip1.Items[5].Enabled = false;
                contextMenuStrip1.Items[6].Enabled = false;
            }
            else
            {
                if (treeView1.SelectedNode.Parent.Name == "New_Node")
                {
                    contextMenuStrip1.Items[0].Enabled = true;
                    contextMenuStrip1.Items[1].Enabled = true;
                    contextMenuStrip1.Items[2].Enabled = true;
                    contextMenuStrip1.Items[3].Enabled = true;
                    contextMenuStrip1.Items[4].Enabled = true;
                    contextMenuStrip1.Items[5].Enabled = false;
                    contextMenuStrip1.Items[6].Enabled = false;
                }
                else if (treeView1.SelectedNode.Parent.Name == "Complete_Node")
                {
                    contextMenuStrip1.Items[0].Enabled = false;
                    contextMenuStrip1.Items[1].Enabled = false;
                    contextMenuStrip1.Items[2].Enabled = true;
                    contextMenuStrip1.Items[3].Enabled = true;
                    contextMenuStrip1.Items[4].Enabled = false;
                    contextMenuStrip1.Items[5].Enabled = false;
                    contextMenuStrip1.Items[6].Enabled = true;
                }
                else if (treeView1.SelectedNode.Parent.Name == "Wait_Node")
                {
                    contextMenuStrip1.Items[0].Enabled = false;
                    contextMenuStrip1.Items[1].Enabled = false;
                    contextMenuStrip1.Items[2].Enabled = false;
                    contextMenuStrip1.Items[3].Enabled = true;
                    contextMenuStrip1.Items[4].Enabled = true;
                    contextMenuStrip1.Items[5].Enabled = false;
                    contextMenuStrip1.Items[6].Enabled = false;
                }
                else if (treeView1.SelectedNode.Parent.Name == "Working_Node")
                {
                    contextMenuStrip1.Items[0].Enabled = false;
                    contextMenuStrip1.Items[1].Enabled = false;
                    contextMenuStrip1.Items[2].Enabled = false;
                    contextMenuStrip1.Items[3].Enabled = true;
                    contextMenuStrip1.Items[4].Enabled = false;
                    contextMenuStrip1.Items[5].Enabled = true;
                    contextMenuStrip1.Items[6].Enabled = false;
                }
            }
        }



        #endregion

        
    }
}
