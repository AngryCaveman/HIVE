namespace Hive
{
    partial class StepSettingForm
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
            this.lab_step = new System.Windows.Forms.Label();
            this.txt_step = new System.Windows.Forms.TextBox();
            this.txt_linkre = new System.Windows.Forms.TextBox();
            this.lab_linkre = new System.Windows.Forms.Label();
            this.txt_paging = new System.Windows.Forms.TextBox();
            this.lab_paging = new System.Windows.Forms.Label();
            this.lab_dynamic = new System.Windows.Forms.Label();
            this.comb_dynamic = new System.Windows.Forms.ComboBox();
            this.comb_Follow = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listV_StepCls = new System.Windows.Forms.ListView();
            this.stepCls = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.stepMethod = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.stepLinkre = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_addStepCls = new System.Windows.Forms.Button();
            this.btn_deleteStepCls = new System.Windows.Forms.Button();
            this.btn_SaveStep = new System.Windows.Forms.Button();
            this.tab_xpath = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lab_dubF = new System.Windows.Forms.Label();
            this.txt_dubField = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listV_CustomLink = new System.Windows.Forms.ListView();
            this.btn_addcustomLink = new System.Windows.Forms.Button();
            this.btn_delcustomLink = new System.Windows.Forms.Button();
            this.listV_Relevance = new System.Windows.Forms.ListView();
            this.btn_delRelevance = new System.Windows.Forms.Button();
            this.btn_addRelevance = new System.Windows.Forms.Button();
            this.CutomLInkID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CustomLinks = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.relevanceID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ContainXpath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.relevanceField = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ToStep = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tab_xpath.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lab_step
            // 
            this.lab_step.AutoSize = true;
            this.lab_step.Location = new System.Drawing.Point(13, 20);
            this.lab_step.Name = "lab_step";
            this.lab_step.Size = new System.Drawing.Size(35, 12);
            this.lab_step.TabIndex = 0;
            this.lab_step.Text = "步骤:";
            // 
            // txt_step
            // 
            this.txt_step.Location = new System.Drawing.Point(77, 11);
            this.txt_step.Name = "txt_step";
            this.txt_step.Size = new System.Drawing.Size(100, 21);
            this.txt_step.TabIndex = 1;
            // 
            // txt_linkre
            // 
            this.txt_linkre.Location = new System.Drawing.Point(340, 10);
            this.txt_linkre.Name = "txt_linkre";
            this.txt_linkre.Size = new System.Drawing.Size(100, 21);
            this.txt_linkre.TabIndex = 3;
            // 
            // lab_linkre
            // 
            this.lab_linkre.AutoSize = true;
            this.lab_linkre.Location = new System.Drawing.Point(265, 20);
            this.lab_linkre.Name = "lab_linkre";
            this.lab_linkre.Size = new System.Drawing.Size(59, 12);
            this.lab_linkre.TabIndex = 2;
            this.lab_linkre.Text = "跳转规则:";
            // 
            // txt_paging
            // 
            this.txt_paging.Location = new System.Drawing.Point(340, 52);
            this.txt_paging.Name = "txt_paging";
            this.txt_paging.Size = new System.Drawing.Size(100, 21);
            this.txt_paging.TabIndex = 5;
            // 
            // lab_paging
            // 
            this.lab_paging.AutoSize = true;
            this.lab_paging.Location = new System.Drawing.Point(265, 61);
            this.lab_paging.Name = "lab_paging";
            this.lab_paging.Size = new System.Drawing.Size(59, 12);
            this.lab_paging.TabIndex = 4;
            this.lab_paging.Text = "翻页规则:";
            // 
            // lab_dynamic
            // 
            this.lab_dynamic.AutoSize = true;
            this.lab_dynamic.Location = new System.Drawing.Point(12, 55);
            this.lab_dynamic.Name = "lab_dynamic";
            this.lab_dynamic.Size = new System.Drawing.Size(59, 12);
            this.lab_dynamic.TabIndex = 6;
            this.lab_dynamic.Text = "解析方式:";
            // 
            // comb_dynamic
            // 
            this.comb_dynamic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_dynamic.FormattingEnabled = true;
            this.comb_dynamic.Items.AddRange(new object[] {
            "动态",
            "静态"});
            this.comb_dynamic.Location = new System.Drawing.Point(77, 46);
            this.comb_dynamic.Name = "comb_dynamic";
            this.comb_dynamic.Size = new System.Drawing.Size(100, 20);
            this.comb_dynamic.TabIndex = 7;
            // 
            // comb_Follow
            // 
            this.comb_Follow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_Follow.FormattingEnabled = true;
            this.comb_Follow.Items.AddRange(new object[] {
            "是",
            "否"});
            this.comb_Follow.Location = new System.Drawing.Point(77, 93);
            this.comb_Follow.Name = "comb_Follow";
            this.comb_Follow.Size = new System.Drawing.Size(100, 20);
            this.comb_Follow.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "是否跟进:";
            // 
            // listV_StepCls
            // 
            this.listV_StepCls.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.stepCls,
            this.stepMethod,
            this.stepLinkre});
            this.listV_StepCls.FullRowSelect = true;
            this.listV_StepCls.GridLines = true;
            this.listV_StepCls.Location = new System.Drawing.Point(3, 3);
            this.listV_StepCls.MultiSelect = false;
            this.listV_StepCls.Name = "listV_StepCls";
            this.listV_StepCls.Size = new System.Drawing.Size(429, 302);
            this.listV_StepCls.TabIndex = 10;
            this.listV_StepCls.UseCompatibleStateImageBehavior = false;
            this.listV_StepCls.View = System.Windows.Forms.View.Details;
            // 
            // stepCls
            // 
            this.stepCls.Text = "对象名";
            // 
            // stepMethod
            // 
            this.stepMethod.Text = "解析方式";
            // 
            // stepLinkre
            // 
            this.stepLinkre.Text = "解析规则";
            this.stepLinkre.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.stepLinkre.Width = 300;
            // 
            // btn_addStepCls
            // 
            this.btn_addStepCls.Location = new System.Drawing.Point(121, 311);
            this.btn_addStepCls.Name = "btn_addStepCls";
            this.btn_addStepCls.Size = new System.Drawing.Size(75, 23);
            this.btn_addStepCls.TabIndex = 11;
            this.btn_addStepCls.Text = "添加对象";
            this.btn_addStepCls.UseVisualStyleBackColor = true;
            this.btn_addStepCls.Click += new System.EventHandler(this.btn_addStepCls_Click);
            // 
            // btn_deleteStepCls
            // 
            this.btn_deleteStepCls.Location = new System.Drawing.Point(219, 311);
            this.btn_deleteStepCls.Name = "btn_deleteStepCls";
            this.btn_deleteStepCls.Size = new System.Drawing.Size(75, 23);
            this.btn_deleteStepCls.TabIndex = 12;
            this.btn_deleteStepCls.Text = "删除对象";
            this.btn_deleteStepCls.UseVisualStyleBackColor = true;
            this.btn_deleteStepCls.Click += new System.EventHandler(this.btn_deleteStepCls_Click);
            // 
            // btn_SaveStep
            // 
            this.btn_SaveStep.Location = new System.Drawing.Point(340, 511);
            this.btn_SaveStep.Name = "btn_SaveStep";
            this.btn_SaveStep.Size = new System.Drawing.Size(85, 34);
            this.btn_SaveStep.TabIndex = 13;
            this.btn_SaveStep.Text = "保存";
            this.btn_SaveStep.UseVisualStyleBackColor = true;
            this.btn_SaveStep.Click += new System.EventHandler(this.btn_SaveStep_Click);
            // 
            // tab_xpath
            // 
            this.tab_xpath.Controls.Add(this.tabPage1);
            this.tab_xpath.Controls.Add(this.tabPage3);
            this.tab_xpath.Controls.Add(this.tabPage2);
            this.tab_xpath.Location = new System.Drawing.Point(6, 136);
            this.tab_xpath.Name = "tab_xpath";
            this.tab_xpath.SelectedIndex = 0;
            this.tab_xpath.Size = new System.Drawing.Size(443, 369);
            this.tab_xpath.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listV_StepCls);
            this.tabPage1.Controls.Add(this.btn_deleteStepCls);
            this.tabPage1.Controls.Add(this.btn_addStepCls);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(435, 343);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "解析对象";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btn_delRelevance);
            this.tabPage3.Controls.Add(this.btn_addRelevance);
            this.tabPage3.Controls.Add(this.listV_Relevance);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(435, 343);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "参数传递";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lab_dubF
            // 
            this.lab_dubF.AutoSize = true;
            this.lab_dubF.Location = new System.Drawing.Point(265, 101);
            this.lab_dubF.Name = "lab_dubF";
            this.lab_dubF.Size = new System.Drawing.Size(59, 12);
            this.lab_dubF.TabIndex = 15;
            this.lab_dubF.Text = "去重字段:";
            // 
            // txt_dubField
            // 
            this.txt_dubField.Location = new System.Drawing.Point(340, 92);
            this.txt_dubField.Name = "txt_dubField";
            this.txt_dubField.Size = new System.Drawing.Size(100, 21);
            this.txt_dubField.TabIndex = 16;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_delcustomLink);
            this.tabPage2.Controls.Add(this.btn_addcustomLink);
            this.tabPage2.Controls.Add(this.listV_CustomLink);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(435, 343);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "自定义跳转链接";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listV_CustomLink
            // 
            this.listV_CustomLink.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CutomLInkID,
            this.CustomLinks});
            this.listV_CustomLink.FullRowSelect = true;
            this.listV_CustomLink.GridLines = true;
            this.listV_CustomLink.Location = new System.Drawing.Point(3, 6);
            this.listV_CustomLink.MultiSelect = false;
            this.listV_CustomLink.Name = "listV_CustomLink";
            this.listV_CustomLink.Size = new System.Drawing.Size(432, 296);
            this.listV_CustomLink.TabIndex = 0;
            this.listV_CustomLink.UseCompatibleStateImageBehavior = false;
            this.listV_CustomLink.View = System.Windows.Forms.View.Details;
            // 
            // btn_addcustomLink
            // 
            this.btn_addcustomLink.Location = new System.Drawing.Point(119, 311);
            this.btn_addcustomLink.Name = "btn_addcustomLink";
            this.btn_addcustomLink.Size = new System.Drawing.Size(75, 23);
            this.btn_addcustomLink.TabIndex = 1;
            this.btn_addcustomLink.Text = "添加";
            this.btn_addcustomLink.UseVisualStyleBackColor = true;
            this.btn_addcustomLink.Click += new System.EventHandler(this.btn_addcustomLink_Click);
            // 
            // btn_delcustomLink
            // 
            this.btn_delcustomLink.Location = new System.Drawing.Point(218, 311);
            this.btn_delcustomLink.Name = "btn_delcustomLink";
            this.btn_delcustomLink.Size = new System.Drawing.Size(75, 23);
            this.btn_delcustomLink.TabIndex = 2;
            this.btn_delcustomLink.Text = "删除";
            this.btn_delcustomLink.UseVisualStyleBackColor = true;
            this.btn_delcustomLink.Click += new System.EventHandler(this.btn_delcustomLink_Click);
            // 
            // listV_Relevance
            // 
            this.listV_Relevance.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.relevanceID,
            this.ContainXpath,
            this.relevanceField,
            this.ToStep});
            this.listV_Relevance.FullRowSelect = true;
            this.listV_Relevance.GridLines = true;
            this.listV_Relevance.Location = new System.Drawing.Point(0, 5);
            this.listV_Relevance.MultiSelect = false;
            this.listV_Relevance.Name = "listV_Relevance";
            this.listV_Relevance.Size = new System.Drawing.Size(433, 298);
            this.listV_Relevance.TabIndex = 0;
            this.listV_Relevance.UseCompatibleStateImageBehavior = false;
            this.listV_Relevance.View = System.Windows.Forms.View.Details;
            // 
            // btn_delRelevance
            // 
            this.btn_delRelevance.Location = new System.Drawing.Point(218, 311);
            this.btn_delRelevance.Name = "btn_delRelevance";
            this.btn_delRelevance.Size = new System.Drawing.Size(75, 23);
            this.btn_delRelevance.TabIndex = 4;
            this.btn_delRelevance.Text = "删除";
            this.btn_delRelevance.UseVisualStyleBackColor = true;
            this.btn_delRelevance.Click += new System.EventHandler(this.btn_delRelevance_Click);
            // 
            // btn_addRelevance
            // 
            this.btn_addRelevance.Location = new System.Drawing.Point(119, 311);
            this.btn_addRelevance.Name = "btn_addRelevance";
            this.btn_addRelevance.Size = new System.Drawing.Size(75, 23);
            this.btn_addRelevance.TabIndex = 3;
            this.btn_addRelevance.Text = "添加";
            this.btn_addRelevance.UseVisualStyleBackColor = true;
            this.btn_addRelevance.Click += new System.EventHandler(this.btn_addRelevance_Click);
            // 
            // CutomLInkID
            // 
            this.CutomLInkID.Text = "序列号";
            // 
            // CustomLinks
            // 
            this.CustomLinks.Text = "自定义链接";
            this.CustomLinks.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CustomLinks.Width = 360;
            // 
            // relevanceID
            // 
            this.relevanceID.Text = "序列号";
            // 
            // ContainXpath
            // 
            this.ContainXpath.Text = "包含规则";
            this.ContainXpath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ContainXpath.Width = 210;
            // 
            // relevanceField
            // 
            this.relevanceField.Text = "关联字段";
            this.relevanceField.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.relevanceField.Width = 90;
            // 
            // ToStep
            // 
            this.ToStep.Text = "至步骤";
            this.ToStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // StepSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 561);
            this.Controls.Add(this.txt_dubField);
            this.Controls.Add(this.lab_dubF);
            this.Controls.Add(this.btn_SaveStep);
            this.Controls.Add(this.comb_Follow);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comb_dynamic);
            this.Controls.Add(this.lab_dynamic);
            this.Controls.Add(this.txt_paging);
            this.Controls.Add(this.lab_paging);
            this.Controls.Add(this.txt_linkre);
            this.Controls.Add(this.lab_linkre);
            this.Controls.Add(this.txt_step);
            this.Controls.Add(this.lab_step);
            this.Controls.Add(this.tab_xpath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StepSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StepSettingForm";
            this.Load += new System.EventHandler(this.StepSettingForm_Load);
            this.tab_xpath.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lab_step;
        private System.Windows.Forms.TextBox txt_step;
        private System.Windows.Forms.TextBox txt_linkre;
        private System.Windows.Forms.Label lab_linkre;
        private System.Windows.Forms.TextBox txt_paging;
        private System.Windows.Forms.Label lab_paging;
        private System.Windows.Forms.Label lab_dynamic;
        private System.Windows.Forms.ComboBox comb_dynamic;
        private System.Windows.Forms.ComboBox comb_Follow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listV_StepCls;
        private System.Windows.Forms.ColumnHeader stepCls;
        private System.Windows.Forms.ColumnHeader stepMethod;
        private System.Windows.Forms.ColumnHeader stepLinkre;
        private System.Windows.Forms.Button btn_addStepCls;
        private System.Windows.Forms.Button btn_deleteStepCls;
        private System.Windows.Forms.Button btn_SaveStep;
        private System.Windows.Forms.TabControl tab_xpath;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lab_dubF;
        private System.Windows.Forms.TextBox txt_dubField;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btn_delcustomLink;
        private System.Windows.Forms.Button btn_addcustomLink;
        private System.Windows.Forms.ListView listV_CustomLink;
        private System.Windows.Forms.Button btn_delRelevance;
        private System.Windows.Forms.Button btn_addRelevance;
        private System.Windows.Forms.ListView listV_Relevance;
        private System.Windows.Forms.ColumnHeader CutomLInkID;
        private System.Windows.Forms.ColumnHeader CustomLinks;
        private System.Windows.Forms.ColumnHeader relevanceID;
        private System.Windows.Forms.ColumnHeader ContainXpath;
        private System.Windows.Forms.ColumnHeader relevanceField;
        private System.Windows.Forms.ColumnHeader ToStep;
    }
}