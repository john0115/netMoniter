namespace openVisio
{
    partial class NetMonitor
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetMonitor));
            this.netpanel = new System.Windows.Forms.Panel();
            this.MonitorStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MonitorStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.MonitorSetItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openfile = new System.Windows.Forms.ToolStripMenuItem();
            this.AddMonitor = new System.Windows.Forms.ToolStripMenuItem();
            this.StartMonitor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.AboutItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MonitorStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // netpanel
            // 
            this.netpanel.AutoScroll = true;
            this.netpanel.AutoScrollMinSize = new System.Drawing.Size(400, 380);
            this.netpanel.AutoSize = true;
            this.netpanel.ForeColor = System.Drawing.SystemColors.Control;
            this.netpanel.Location = new System.Drawing.Point(12, 33);
            this.netpanel.Name = "netpanel";
            this.netpanel.Size = new System.Drawing.Size(450, 450);
            this.netpanel.TabIndex = 1;
            this.netpanel.Paint += new System.Windows.Forms.PaintEventHandler(this.netpanel_Paint);
            // 
            // MonitorStrip1
            // 
            this.MonitorStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MonitorStripMenuItem2,
            this.MonitorSetItem});
            this.MonitorStrip1.Name = "MonitorStrip1";
            this.MonitorStrip1.Size = new System.Drawing.Size(101, 48);
            // 
            // MonitorStripMenuItem2
            // 
            this.MonitorStripMenuItem2.Name = "MonitorStripMenuItem2";
            this.MonitorStripMenuItem2.Size = new System.Drawing.Size(100, 22);
            this.MonitorStripMenuItem2.Text = "删除";
            this.MonitorStripMenuItem2.Click += new System.EventHandler(this.MonitorStripMenuItem2_Click);
            // 
            // MonitorSetItem
            // 
            this.MonitorSetItem.Name = "MonitorSetItem";
            this.MonitorSetItem.Size = new System.Drawing.Size(100, 22);
            this.MonitorSetItem.Text = "设置";
            this.MonitorSetItem.Click += new System.EventHandler(this.MonitorSetItem_Click);
            // 
            // openfile
            // 
            this.openfile.CheckOnClick = true;
            this.openfile.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.openfile.Name = "openfile";
            this.openfile.Size = new System.Drawing.Size(44, 21);
            this.openfile.Text = "打开";
            this.openfile.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // AddMonitor
            // 
            this.AddMonitor.CheckOnClick = true;
            this.AddMonitor.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.AddMonitor.Name = "AddMonitor";
            this.AddMonitor.Size = new System.Drawing.Size(80, 21);
            this.AddMonitor.Text = "添加监控点";
            this.AddMonitor.Click += new System.EventHandler(this.AddMonitor_Click);
            // 
            // StartMonitor
            // 
            this.StartMonitor.CheckOnClick = true;
            this.StartMonitor.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.StartMonitor.Name = "StartMonitor";
            this.StartMonitor.Size = new System.Drawing.Size(44, 21);
            this.StartMonitor.Text = "启动";
            this.StartMonitor.Click += new System.EventHandler(this.StartMonitor_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openfile,
            this.AddMonitor,
            this.StartMonitor,
            this.AboutItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(484, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // AboutItem
            // 
            this.AboutItem.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.AboutItem.Name = "AboutItem";
            this.AboutItem.Size = new System.Drawing.Size(44, 21);
            this.AboutItem.Text = "关于";
            this.AboutItem.Click += new System.EventHandler(this.AboutItem_Click);
            // 
            // NetMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(450, 470);
            this.AutoSize = true;
            
            this.ClientSize = new System.Drawing.Size(484, 562);
            this.Controls.Add(this.netpanel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "NetMonitor";
            this.Text = "网络监控";
            this.Load += new System.EventHandler(this.NetMonitor_Load);
            this.MonitorStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel netpanel;
        private System.Windows.Forms.ContextMenuStrip MonitorStrip1;
        private System.Windows.Forms.ToolStripMenuItem MonitorStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem openfile;
        private System.Windows.Forms.ToolStripMenuItem AddMonitor;
        private System.Windows.Forms.ToolStripMenuItem StartMonitor;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MonitorSetItem;
        private System.Windows.Forms.ToolStripMenuItem AboutItem;
        //private System.Windows.Forms.Button bt1;
    }
}

