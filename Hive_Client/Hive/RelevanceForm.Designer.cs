namespace Hive
{
    partial class RelevanceForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_addRelevance = new System.Windows.Forms.Button();
            this.txt_relevanceStep = new System.Windows.Forms.TextBox();
            this.lab_relevanceStep = new System.Windows.Forms.Label();
            this.txt_relevanceXpath = new System.Windows.Forms.TextBox();
            this.lab_relevanceXpath = new System.Windows.Forms.Label();
            this.txt_relevanceField = new System.Windows.Forms.TextBox();
            this.lab_relevanceField = new System.Windows.Forms.Label();
            this.txt_containtPath = new System.Windows.Forms.TextBox();
            this.lab_containPath = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btn_addRelevance);
            this.panel1.Controls.Add(this.txt_relevanceStep);
            this.panel1.Controls.Add(this.lab_relevanceStep);
            this.panel1.Controls.Add(this.txt_relevanceXpath);
            this.panel1.Controls.Add(this.lab_relevanceXpath);
            this.panel1.Controls.Add(this.txt_relevanceField);
            this.panel1.Controls.Add(this.lab_relevanceField);
            this.panel1.Controls.Add(this.txt_containtPath);
            this.panel1.Controls.Add(this.lab_containPath);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(501, 162);
            this.panel1.TabIndex = 1;
            // 
            // btn_addRelevance
            // 
            this.btn_addRelevance.Location = new System.Drawing.Point(403, 127);
            this.btn_addRelevance.Name = "btn_addRelevance";
            this.btn_addRelevance.Size = new System.Drawing.Size(75, 23);
            this.btn_addRelevance.TabIndex = 8;
            this.btn_addRelevance.Text = "确定";
            this.btn_addRelevance.UseVisualStyleBackColor = true;
            this.btn_addRelevance.Click += new System.EventHandler(this.btn_addRelevance_Click);
            // 
            // txt_relevanceStep
            // 
            this.txt_relevanceStep.Location = new System.Drawing.Point(354, 79);
            this.txt_relevanceStep.Name = "txt_relevanceStep";
            this.txt_relevanceStep.Size = new System.Drawing.Size(124, 21);
            this.txt_relevanceStep.TabIndex = 7;
            // 
            // lab_relevanceStep
            // 
            this.lab_relevanceStep.AutoSize = true;
            this.lab_relevanceStep.Location = new System.Drawing.Point(266, 82);
            this.lab_relevanceStep.Name = "lab_relevanceStep";
            this.lab_relevanceStep.Size = new System.Drawing.Size(65, 12);
            this.lab_relevanceStep.TabIndex = 6;
            this.lab_relevanceStep.Text = "关联步骤：";
            // 
            // txt_relevanceXpath
            // 
            this.txt_relevanceXpath.Location = new System.Drawing.Point(354, 28);
            this.txt_relevanceXpath.Name = "txt_relevanceXpath";
            this.txt_relevanceXpath.Size = new System.Drawing.Size(124, 21);
            this.txt_relevanceXpath.TabIndex = 5;
            // 
            // lab_relevanceXpath
            // 
            this.lab_relevanceXpath.AutoSize = true;
            this.lab_relevanceXpath.Location = new System.Drawing.Point(269, 35);
            this.lab_relevanceXpath.Name = "lab_relevanceXpath";
            this.lab_relevanceXpath.Size = new System.Drawing.Size(65, 12);
            this.lab_relevanceXpath.TabIndex = 4;
            this.lab_relevanceXpath.Text = "解析规则：";
            // 
            // txt_relevanceField
            // 
            this.txt_relevanceField.Location = new System.Drawing.Point(115, 80);
            this.txt_relevanceField.Name = "txt_relevanceField";
            this.txt_relevanceField.Size = new System.Drawing.Size(122, 21);
            this.txt_relevanceField.TabIndex = 3;
            // 
            // lab_relevanceField
            // 
            this.lab_relevanceField.AutoSize = true;
            this.lab_relevanceField.Location = new System.Drawing.Point(28, 88);
            this.lab_relevanceField.Name = "lab_relevanceField";
            this.lab_relevanceField.Size = new System.Drawing.Size(65, 12);
            this.lab_relevanceField.TabIndex = 2;
            this.lab_relevanceField.Text = "关联字段：";
            // 
            // txt_containtPath
            // 
            this.txt_containtPath.Location = new System.Drawing.Point(115, 28);
            this.txt_containtPath.Name = "txt_containtPath";
            this.txt_containtPath.Size = new System.Drawing.Size(122, 21);
            this.txt_containtPath.TabIndex = 1;
            // 
            // lab_containPath
            // 
            this.lab_containPath.AutoSize = true;
            this.lab_containPath.Location = new System.Drawing.Point(28, 35);
            this.lab_containPath.Name = "lab_containPath";
            this.lab_containPath.Size = new System.Drawing.Size(65, 12);
            this.lab_containPath.TabIndex = 0;
            this.lab_containPath.Text = "包含规则：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(140, 127);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "url关联";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // RelevanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 162);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RelevanceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数传递";
            this.Load += new System.EventHandler(this.RelevanceForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_relevanceStep;
        private System.Windows.Forms.Label lab_relevanceStep;
        private System.Windows.Forms.TextBox txt_relevanceXpath;
        private System.Windows.Forms.Label lab_relevanceXpath;
        private System.Windows.Forms.TextBox txt_relevanceField;
        private System.Windows.Forms.Label lab_relevanceField;
        private System.Windows.Forms.TextBox txt_containtPath;
        private System.Windows.Forms.Label lab_containPath;
        private System.Windows.Forms.Button btn_addRelevance;
        private System.Windows.Forms.Button button1;
    }
}