namespace Hive
{
    partial class RuleForm
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
            this.btn_saveRule = new System.Windows.Forms.Button();
            this.lab_obj = new System.Windows.Forms.Label();
            this.txt_obj = new System.Windows.Forms.TextBox();
            this.lab_method = new System.Windows.Forms.Label();
            this.txt_linkre = new System.Windows.Forms.TextBox();
            this.lab_linkre = new System.Windows.Forms.Label();
            this.comb_method = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btn_saveRule
            // 
            this.btn_saveRule.Location = new System.Drawing.Point(348, 110);
            this.btn_saveRule.Name = "btn_saveRule";
            this.btn_saveRule.Size = new System.Drawing.Size(75, 23);
            this.btn_saveRule.TabIndex = 0;
            this.btn_saveRule.Text = "保存";
            this.btn_saveRule.UseVisualStyleBackColor = true;
            this.btn_saveRule.Click += new System.EventHandler(this.btn_saveRule_Click);
            // 
            // lab_obj
            // 
            this.lab_obj.AutoSize = true;
            this.lab_obj.Location = new System.Drawing.Point(21, 29);
            this.lab_obj.Name = "lab_obj";
            this.lab_obj.Size = new System.Drawing.Size(65, 12);
            this.lab_obj.TabIndex = 1;
            this.lab_obj.Text = "对象名称：";
            // 
            // txt_obj
            // 
            this.txt_obj.Location = new System.Drawing.Point(81, 21);
            this.txt_obj.Name = "txt_obj";
            this.txt_obj.Size = new System.Drawing.Size(100, 21);
            this.txt_obj.TabIndex = 2;
            // 
            // lab_method
            // 
            this.lab_method.AutoSize = true;
            this.lab_method.Location = new System.Drawing.Point(263, 28);
            this.lab_method.Name = "lab_method";
            this.lab_method.Size = new System.Drawing.Size(65, 12);
            this.lab_method.TabIndex = 3;
            this.lab_method.Text = "解析方式：";
            // 
            // txt_linkre
            // 
            this.txt_linkre.Location = new System.Drawing.Point(81, 65);
            this.txt_linkre.Name = "txt_linkre";
            this.txt_linkre.Size = new System.Drawing.Size(342, 21);
            this.txt_linkre.TabIndex = 6;
            // 
            // lab_linkre
            // 
            this.lab_linkre.AutoSize = true;
            this.lab_linkre.Location = new System.Drawing.Point(21, 73);
            this.lab_linkre.Name = "lab_linkre";
            this.lab_linkre.Size = new System.Drawing.Size(65, 12);
            this.lab_linkre.TabIndex = 5;
            this.lab_linkre.Text = "解析规则：";
            // 
            // comb_method
            // 
            this.comb_method.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_method.FormattingEnabled = true;
            this.comb_method.Items.AddRange(new object[] {
            "xpath",
            "css"});
            this.comb_method.Location = new System.Drawing.Point(328, 21);
            this.comb_method.Name = "comb_method";
            this.comb_method.Size = new System.Drawing.Size(95, 20);
            this.comb_method.TabIndex = 7;
            // 
            // RuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 145);
            this.Controls.Add(this.comb_method);
            this.Controls.Add(this.txt_linkre);
            this.Controls.Add(this.lab_linkre);
            this.Controls.Add(this.lab_method);
            this.Controls.Add(this.txt_obj);
            this.Controls.Add(this.lab_obj);
            this.Controls.Add(this.btn_saveRule);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RuleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RuleForm";
            this.Load += new System.EventHandler(this.RuleForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_saveRule;
        private System.Windows.Forms.Label lab_obj;
        private System.Windows.Forms.TextBox txt_obj;
        private System.Windows.Forms.Label lab_method;
        private System.Windows.Forms.TextBox txt_linkre;
        private System.Windows.Forms.Label lab_linkre;
        private System.Windows.Forms.ComboBox comb_method;
    }
}