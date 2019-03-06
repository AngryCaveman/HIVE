namespace Hive
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("新建任务", 0, 0);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("已经完成", 1, 1);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("等待任务", 3, 3);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("正在进行", 2, 2);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeView1 = new Hive.TreeViewNF();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.启动任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置优先ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.菜单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listV_Details = new Hive.ListViewNF();
            this.SPN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.completePages = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.failPages = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.linkre = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GetData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.saveData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.runTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_Details = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.重启任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Location = new System.Drawing.Point(5, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 541);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "任务集";
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(6, 13);
            this.treeView1.Name = "treeView1";
            treeNode1.ImageIndex = 0;
            treeNode1.Name = "New_Node";
            treeNode1.SelectedImageIndex = 0;
            treeNode1.Text = "新建任务";
            treeNode2.ImageIndex = 1;
            treeNode2.Name = "Complete_Node";
            treeNode2.SelectedImageIndex = 1;
            treeNode2.Text = "已经完成";
            treeNode3.ImageIndex = 3;
            treeNode3.Name = "Wait_Node";
            treeNode3.SelectedImageIndex = 3;
            treeNode3.Text = "等待任务";
            treeNode4.ImageIndex = 2;
            treeNode4.Name = "Working_Node";
            treeNode4.SelectedImageIndex = 2;
            treeNode4.Text = "正在进行";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(214, 522);
            this.treeView1.TabIndex = 1;
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加任务ToolStripMenuItem,
            this.启动任务ToolStripMenuItem,
            this.删除任务ToolStripMenuItem,
            this.查看信息ToolStripMenuItem,
            this.设置优先ToolStripMenuItem,
            this.停止任务ToolStripMenuItem,
            this.重启任务ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 180);
            this.contextMenuStrip1.Opened += new System.EventHandler(this.contextMenuStrip1_Opened);
            // 
            // 添加任务ToolStripMenuItem
            // 
            this.添加任务ToolStripMenuItem.Name = "添加任务ToolStripMenuItem";
            this.添加任务ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.添加任务ToolStripMenuItem.Text = "添加任务";
            this.添加任务ToolStripMenuItem.Click += new System.EventHandler(this.添加任务ToolStripMenuItem_Click);
            // 
            // 启动任务ToolStripMenuItem
            // 
            this.启动任务ToolStripMenuItem.Name = "启动任务ToolStripMenuItem";
            this.启动任务ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.启动任务ToolStripMenuItem.Text = "启动任务";
            this.启动任务ToolStripMenuItem.Click += new System.EventHandler(this.启动任务ToolStripMenuItem_Click);
            // 
            // 删除任务ToolStripMenuItem
            // 
            this.删除任务ToolStripMenuItem.Name = "删除任务ToolStripMenuItem";
            this.删除任务ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除任务ToolStripMenuItem.Text = "删除任务";
            this.删除任务ToolStripMenuItem.Click += new System.EventHandler(this.删除任务ToolStripMenuItem_Click);
            // 
            // 查看信息ToolStripMenuItem
            // 
            this.查看信息ToolStripMenuItem.Name = "查看信息ToolStripMenuItem";
            this.查看信息ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.查看信息ToolStripMenuItem.Text = "查看信息";
            this.查看信息ToolStripMenuItem.Click += new System.EventHandler(this.查看信息ToolStripMenuItem_Click);
            // 
            // 设置优先ToolStripMenuItem
            // 
            this.设置优先ToolStripMenuItem.Name = "设置优先ToolStripMenuItem";
            this.设置优先ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.设置优先ToolStripMenuItem.Text = "设置优先";
            this.设置优先ToolStripMenuItem.Click += new System.EventHandler(this.设置优先ToolStripMenuItem_Click);
            // 
            // 停止任务ToolStripMenuItem
            // 
            this.停止任务ToolStripMenuItem.Name = "停止任务ToolStripMenuItem";
            this.停止任务ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.停止任务ToolStripMenuItem.Text = "停止任务";
            this.停止任务ToolStripMenuItem.Click += new System.EventHandler(this.停止任务ToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "new.png");
            this.imageList1.Images.SetKeyName(1, "complete.png");
            this.imageList1.Images.SetKeyName(2, "working.png");
            this.imageList1.Images.SetKeyName(3, "stop.png");
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listBox1);
            this.panel2.Location = new System.Drawing.Point(256, 387);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(605, 169);
            this.panel2.TabIndex = 2;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(3, 2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(599, 160);
            this.listBox1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.菜单ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(881, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 菜单ToolStripMenuItem
            // 
            this.菜单ToolStripMenuItem.Name = "菜单ToolStripMenuItem";
            this.菜单ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.菜单ToolStripMenuItem.Text = "菜单";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Location = new System.Drawing.Point(0, 28);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(881, 567);
            this.panel3.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.listV_Details);
            this.panel1.Controls.Add(this.btn_Details);
            this.panel1.Location = new System.Drawing.Point(256, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(607, 354);
            this.panel1.TabIndex = 1;
            // 
            // listV_Details
            // 
            this.listV_Details.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SPN,
            this.completePages,
            this.failPages,
            this.linkre,
            this.GetData,
            this.saveData,
            this.runTime});
            this.listV_Details.GridLines = true;
            this.listV_Details.Location = new System.Drawing.Point(1, 2);
            this.listV_Details.Name = "listV_Details";
            this.listV_Details.Size = new System.Drawing.Size(604, 299);
            this.listV_Details.TabIndex = 5;
            this.listV_Details.UseCompatibleStateImageBehavior = false;
            this.listV_Details.View = System.Windows.Forms.View.Details;
            // 
            // SPN
            // 
            this.SPN.Text = "任务名";
            // 
            // completePages
            // 
            this.completePages.Text = "完成页数";
            this.completePages.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.completePages.Width = 80;
            // 
            // failPages
            // 
            this.failPages.Text = "失败页数";
            this.failPages.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.failPages.Width = 80;
            // 
            // linkre
            // 
            this.linkre.Text = "失败链接数";
            this.linkre.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.linkre.Width = 80;
            // 
            // GetData
            // 
            this.GetData.Text = "获取数据失败数";
            this.GetData.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GetData.Width = 110;
            // 
            // saveData
            // 
            this.saveData.Text = "保存数据失败数";
            this.saveData.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.saveData.Width = 110;
            // 
            // runTime
            // 
            this.runTime.Text = "运行时长";
            this.runTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.runTime.Width = 80;
            // 
            // btn_Details
            // 
            this.btn_Details.Location = new System.Drawing.Point(279, 317);
            this.btn_Details.Name = "btn_Details";
            this.btn_Details.Size = new System.Drawing.Size(88, 23);
            this.btn_Details.TabIndex = 4;
            this.btn_Details.Text = "查看详情";
            this.btn_Details.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 600);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(881, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // 重启任务ToolStripMenuItem
            // 
            this.重启任务ToolStripMenuItem.Name = "重启任务ToolStripMenuItem";
            this.重启任务ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.重启任务ToolStripMenuItem.Text = "重启任务";
            this.重启任务ToolStripMenuItem.Click += new System.EventHandler(this.重启任务ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 622);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "蜂巢";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 菜单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private TreeViewNF treeView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ToolStripMenuItem 添加任务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除任务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看信息ToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem 启动任务ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Details;
        private ListViewNF listV_Details;
        private System.Windows.Forms.ColumnHeader SPN;
        private System.Windows.Forms.ColumnHeader completePages;
        private System.Windows.Forms.ColumnHeader failPages;
        private System.Windows.Forms.ColumnHeader linkre;
        private System.Windows.Forms.ColumnHeader GetData;
        private System.Windows.Forms.ColumnHeader saveData;
        private System.Windows.Forms.ColumnHeader runTime;
        private System.Windows.Forms.ToolStripMenuItem 设置优先ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止任务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem 重启任务ToolStripMenuItem;
    }
}

