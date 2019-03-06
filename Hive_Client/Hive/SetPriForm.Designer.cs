namespace Hive
{
    partial class SetPriForm
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
            this.radioB_High = new System.Windows.Forms.RadioButton();
            this.radioB_Com = new System.Windows.Forms.RadioButton();
            this.radioB_Low = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_SavePRI = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioB_High
            // 
            this.radioB_High.AutoSize = true;
            this.radioB_High.Location = new System.Drawing.Point(71, 45);
            this.radioB_High.Name = "radioB_High";
            this.radioB_High.Size = new System.Drawing.Size(47, 16);
            this.radioB_High.TabIndex = 0;
            this.radioB_High.TabStop = true;
            this.radioB_High.Text = "高级";
            this.radioB_High.UseVisualStyleBackColor = true;
            // 
            // radioB_Com
            // 
            this.radioB_Com.AutoSize = true;
            this.radioB_Com.Location = new System.Drawing.Point(179, 45);
            this.radioB_Com.Name = "radioB_Com";
            this.radioB_Com.Size = new System.Drawing.Size(47, 16);
            this.radioB_Com.TabIndex = 1;
            this.radioB_Com.TabStop = true;
            this.radioB_Com.Text = "普通";
            this.radioB_Com.UseVisualStyleBackColor = true;
            // 
            // radioB_Low
            // 
            this.radioB_Low.AutoSize = true;
            this.radioB_Low.Location = new System.Drawing.Point(287, 45);
            this.radioB_Low.Name = "radioB_Low";
            this.radioB_Low.Size = new System.Drawing.Size(47, 16);
            this.radioB_Low.TabIndex = 2;
            this.radioB_Low.TabStop = true;
            this.radioB_Low.Text = "低级";
            this.radioB_Low.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "优先级设置:";
            // 
            // btn_SavePRI
            // 
            this.btn_SavePRI.Location = new System.Drawing.Point(163, 87);
            this.btn_SavePRI.Name = "btn_SavePRI";
            this.btn_SavePRI.Size = new System.Drawing.Size(75, 23);
            this.btn_SavePRI.TabIndex = 4;
            this.btn_SavePRI.Text = "保存";
            this.btn_SavePRI.UseVisualStyleBackColor = true;
            this.btn_SavePRI.Click += new System.EventHandler(this.btn_SavePRI_Click);
            // 
            // SetPriForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 122);
            this.Controls.Add(this.btn_SavePRI);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioB_Low);
            this.Controls.Add(this.radioB_Com);
            this.Controls.Add(this.radioB_High);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SetPriForm";
            this.Text = "SetPriForm";
            this.Load += new System.EventHandler(this.SetPriForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioB_High;
        private System.Windows.Forms.RadioButton radioB_Com;
        private System.Windows.Forms.RadioButton radioB_Low;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_SavePRI;
    }
}