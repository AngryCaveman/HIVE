namespace Hive
{
    partial class SqlForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lab_IP = new System.Windows.Forms.Label();
            this.txt_SqlIp = new System.Windows.Forms.TextBox();
            this.txt_SqlPort = new System.Windows.Forms.TextBox();
            this.lab_Port = new System.Windows.Forms.Label();
            this.txt_SqlUsr = new System.Windows.Forms.TextBox();
            this.lab_Usr = new System.Windows.Forms.Label();
            this.txt_SqlPwd = new System.Windows.Forms.TextBox();
            this.lab_Pwd = new System.Windows.Forms.Label();
            this.comb_SqlType = new System.Windows.Forms.ComboBox();
            this.lab_SqlType = new System.Windows.Forms.Label();
            this.btn_SqlSave = new System.Windows.Forms.Button();
            this.txt_DBName = new System.Windows.Forms.TextBox();
            this.lab_DBlName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lab_IP
            // 
            this.lab_IP.AutoSize = true;
            this.lab_IP.Location = new System.Drawing.Point(13, 19);
            this.lab_IP.Name = "lab_IP";
            this.lab_IP.Size = new System.Drawing.Size(47, 12);
            this.lab_IP.TabIndex = 0;
            this.lab_IP.Text = "ip地址:";
            // 
            // txt_SqlIp
            // 
            this.txt_SqlIp.Location = new System.Drawing.Point(75, 10);
            this.txt_SqlIp.Name = "txt_SqlIp";
            this.txt_SqlIp.Size = new System.Drawing.Size(100, 21);
            this.txt_SqlIp.TabIndex = 1;
            // 
            // txt_SqlPort
            // 
            this.txt_SqlPort.Location = new System.Drawing.Point(75, 61);
            this.txt_SqlPort.Name = "txt_SqlPort";
            this.txt_SqlPort.Size = new System.Drawing.Size(100, 21);
            this.txt_SqlPort.TabIndex = 3;
            // 
            // lab_Port
            // 
            this.lab_Port.AutoSize = true;
            this.lab_Port.Location = new System.Drawing.Point(13, 70);
            this.lab_Port.Name = "lab_Port";
            this.lab_Port.Size = new System.Drawing.Size(35, 12);
            this.lab_Port.TabIndex = 2;
            this.lab_Port.Text = "端口:";
            // 
            // txt_SqlUsr
            // 
            this.txt_SqlUsr.Location = new System.Drawing.Point(318, 10);
            this.txt_SqlUsr.Name = "txt_SqlUsr";
            this.txt_SqlUsr.Size = new System.Drawing.Size(100, 21);
            this.txt_SqlUsr.TabIndex = 5;
            // 
            // lab_Usr
            // 
            this.lab_Usr.AutoSize = true;
            this.lab_Usr.Location = new System.Drawing.Point(256, 19);
            this.lab_Usr.Name = "lab_Usr";
            this.lab_Usr.Size = new System.Drawing.Size(47, 12);
            this.lab_Usr.TabIndex = 4;
            this.lab_Usr.Text = "用户名:";
            // 
            // txt_SqlPwd
            // 
            this.txt_SqlPwd.Location = new System.Drawing.Point(318, 61);
            this.txt_SqlPwd.Name = "txt_SqlPwd";
            this.txt_SqlPwd.Size = new System.Drawing.Size(100, 21);
            this.txt_SqlPwd.TabIndex = 7;
            // 
            // lab_Pwd
            // 
            this.lab_Pwd.AutoSize = true;
            this.lab_Pwd.Location = new System.Drawing.Point(256, 70);
            this.lab_Pwd.Name = "lab_Pwd";
            this.lab_Pwd.Size = new System.Drawing.Size(35, 12);
            this.lab_Pwd.TabIndex = 6;
            this.lab_Pwd.Text = "密码:";
            // 
            // comb_SqlType
            // 
            this.comb_SqlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_SqlType.FormattingEnabled = true;
            this.comb_SqlType.Items.AddRange(new object[] {
            "MySql",
            "SqlServer",
            "Access"});
            this.comb_SqlType.Location = new System.Drawing.Point(92, 113);
            this.comb_SqlType.Name = "comb_SqlType";
            this.comb_SqlType.Size = new System.Drawing.Size(83, 20);
            this.comb_SqlType.TabIndex = 8;
            // 
            // lab_SqlType
            // 
            this.lab_SqlType.AutoSize = true;
            this.lab_SqlType.Location = new System.Drawing.Point(15, 121);
            this.lab_SqlType.Name = "lab_SqlType";
            this.lab_SqlType.Size = new System.Drawing.Size(71, 12);
            this.lab_SqlType.TabIndex = 9;
            this.lab_SqlType.Text = "数据库类型:";
            // 
            // btn_SqlSave
            // 
            this.btn_SqlSave.Location = new System.Drawing.Point(371, 112);
            this.btn_SqlSave.Name = "btn_SqlSave";
            this.btn_SqlSave.Size = new System.Drawing.Size(75, 23);
            this.btn_SqlSave.TabIndex = 10;
            this.btn_SqlSave.Text = "保存";
            this.btn_SqlSave.UseVisualStyleBackColor = true;
            this.btn_SqlSave.Click += new System.EventHandler(this.btn_SqlSave_Click);
            // 
            // txt_DBName
            // 
            this.txt_DBName.Location = new System.Drawing.Point(260, 114);
            this.txt_DBName.Name = "txt_DBName";
            this.txt_DBName.Size = new System.Drawing.Size(95, 21);
            this.txt_DBName.TabIndex = 12;
            // 
            // lab_DBlName
            // 
            this.lab_DBlName.AutoSize = true;
            this.lab_DBlName.Location = new System.Drawing.Point(196, 121);
            this.lab_DBlName.Name = "lab_DBlName";
            this.lab_DBlName.Size = new System.Drawing.Size(59, 12);
            this.lab_DBlName.TabIndex = 11;
            this.lab_DBlName.Text = "数据库名:";
            // 
            // SqlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 160);
            this.Controls.Add(this.txt_DBName);
            this.Controls.Add(this.lab_DBlName);
            this.Controls.Add(this.btn_SqlSave);
            this.Controls.Add(this.lab_SqlType);
            this.Controls.Add(this.comb_SqlType);
            this.Controls.Add(this.txt_SqlPwd);
            this.Controls.Add(this.lab_Pwd);
            this.Controls.Add(this.txt_SqlUsr);
            this.Controls.Add(this.lab_Usr);
            this.Controls.Add(this.txt_SqlPort);
            this.Controls.Add(this.lab_Port);
            this.Controls.Add(this.txt_SqlIp);
            this.Controls.Add(this.lab_IP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SqlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SqlForm";
            this.Load += new System.EventHandler(this.SqlForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lab_IP;
        private System.Windows.Forms.TextBox txt_SqlIp;
        private System.Windows.Forms.TextBox txt_SqlPort;
        private System.Windows.Forms.Label lab_Port;
        private System.Windows.Forms.TextBox txt_SqlUsr;
        private System.Windows.Forms.Label lab_Usr;
        private System.Windows.Forms.TextBox txt_SqlPwd;
        private System.Windows.Forms.Label lab_Pwd;
        private System.Windows.Forms.ComboBox comb_SqlType;
        private System.Windows.Forms.Label lab_SqlType;
        private System.Windows.Forms.Button btn_SqlSave;
        private System.Windows.Forms.TextBox txt_DBName;
        private System.Windows.Forms.Label lab_DBlName;
    }
}