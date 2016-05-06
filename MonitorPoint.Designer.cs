namespace openVisio
{
    partial class MonitorPoint
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
            this.IpText = new System.Windows.Forms.TextBox();
            this.IP = new System.Windows.Forms.Label();
            this.OK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // IpText
            // 
            this.IpText.Font = new System.Drawing.Font("宋体", 10F);
            this.IpText.Location = new System.Drawing.Point(59, 29);
            this.IpText.Name = "IpText";
            this.IpText.Size = new System.Drawing.Size(203, 23);
            this.IpText.TabIndex = 0;
            // 
            // IP
            // 
            this.IP.AutoSize = true;
            this.IP.Font = new System.Drawing.Font("宋体", 12F);
            this.IP.Location = new System.Drawing.Point(13, 31);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(24, 16);
            this.IP.TabIndex = 1;
            this.IP.Text = "IP";
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(294, 28);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(60, 23);
            this.OK.TabIndex = 2;
            this.OK.Text = "确认";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // MonitorPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 76);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.IP);
            this.Controls.Add(this.IpText);
            this.Name = "MonitorPoint";
            this.Text = "输入IP地址";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IpText;
        private System.Windows.Forms.Label IP;
        private System.Windows.Forms.Button OK;
    }
}