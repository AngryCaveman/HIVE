namespace Hive
{
    partial class CookiesForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Cookies = new System.Windows.Forms.TextBox();
            this.btn_AddCookies = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入cookies内容";
            // 
            // txt_Cookies
            // 
            this.txt_Cookies.Location = new System.Drawing.Point(9, 38);
            this.txt_Cookies.Multiline = true;
            this.txt_Cookies.Name = "txt_Cookies";
            this.txt_Cookies.Size = new System.Drawing.Size(288, 228);
            this.txt_Cookies.TabIndex = 1;
            // 
            // btn_AddCookies
            // 
            this.btn_AddCookies.Location = new System.Drawing.Point(207, 8);
            this.btn_AddCookies.Name = "btn_AddCookies";
            this.btn_AddCookies.Size = new System.Drawing.Size(75, 23);
            this.btn_AddCookies.TabIndex = 2;
            this.btn_AddCookies.Text = "确定";
            this.btn_AddCookies.UseVisualStyleBackColor = true;
            this.btn_AddCookies.Click += new System.EventHandler(this.btn_AddCookies_Click);
            // 
            // CookiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 278);
            this.Controls.Add(this.btn_AddCookies);
            this.Controls.Add(this.txt_Cookies);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CookiesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加Cookies";
            this.Load += new System.EventHandler(this.CookiesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Cookies;
        private System.Windows.Forms.Button btn_AddCookies;
    }
}