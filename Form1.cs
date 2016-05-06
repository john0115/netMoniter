using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
namespace openVisio
{
    [LicenseProvider(typeof(MonitorLicenseProvider))]
    public partial class NetMonitor : Form
    {
        private AxVisioViewer.AxViewer viewer;
        private int btnCount = 0 ;
        delegate void soundDel();
        License _license = null;
      //  private ArrayList btnlist = new ArrayList();        /// <summary>
        /// The Visio Viewer OM
        /// </summary>
        public AxVisioViewer.AxViewer Viewer
        {
            get
            {
                return this.viewer;
            }
        }

        public NetMonitor()
        {
            this.InitializeComponent();
            _license = LicenseManager.Validate(typeof(NetMonitor),this);//进行验证
        }
        public void Dispose()
        {

            if (_license != null)
            {

                _license.Dispose(); // 调用_License的Dispose方法，显示释放资源

            }

        }
        public void UpdateSize(object obj, EventArgs ea)
        {
            this.viewer.ClientSize = new Size(this.ClientSize.Width , this.ClientSize.Height);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            this.viewer.Load((string)sender);

        }
        private string filename;
        private void OpenFile_Click(object sender, EventArgs e)
        {
//Thread Ring = new Thread(Alarm);
           // Ring.IsBackground = true;
        //   Ring.Start();
           // temEle.BackColor = System.Drawing.Color.Crimson;
         //  Alarm();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "vsd文件(*.vsd)|*.vsd";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
             
                filename = fileDialog.FileName;

                netpanel.Controls.Clear();
                netpanel.Resize += new EventHandler(this.UpdateSize);
                this.viewer = new AxVisioViewer.AxViewer();

                this.viewer.BeginInit();
                netpanel.Controls.Add(this.viewer);
                this.viewer.EndInit();

                this.viewer.CreateControl();
                this.viewer.Location = new Point(0, 0);
                this.viewer.ToolbarVisible = false;//去掉各种bar
                this.viewer.ScrollbarsVisible = false;
                this.viewer.PageTabsVisible = false;
                this.viewer.PageVisible = false; 

                this.UpdateSize(null, null);
                
                


                //打开配置文件
                FileStream monitorPoints = new FileStream(filename+".set", FileMode.OpenOrCreate);
                StreamReader reader = new StreamReader(monitorPoints);
                string monitorPoint = reader.ReadLine();
                
                while (monitorPoint!=null && monitorPoint.Length > 0)
                {
                    string[] point;
                    btnCount++;
                   monitorPoint =  monitorPoint.Trim();
                    point = monitorPoint.Split(' ');
                    //name x y ip enable
                    if (point.Length > 3)
                    {

                        if (Int32.Parse(point[4]) > 0)
                        {
                            AddMonitorPoint(point[0], new Point(Int32.Parse(point[1]), Int32.Parse(point[2])), point[3]);

                        }
                    }
                    monitorPoint = reader.ReadLine();

                }
                //btnCount = 0;//重置监控点
                monitorPoints.Close();

                this.Form1_Load(filename, null);
               // btnCount = 0;//重置监控点
                // MessageBox.Show("已选择文件:" + file, "选择文件提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void netpanel_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private void AddMonitor_Click(object sender, EventArgs e)
        {
            btnCount++;
            string name = "lb" + this.btnCount;
            AddMonitorPoint(name,new Point(100, 100),"");
        }
        public void AddMonitorPoint(string name,Point location,string ip) 
        {
            if (this.filename != null)
            {
                Label lb = new Label();

                lb.Name = name;
                lb.AccessibleDescription = ip;
                lb.BackColor = System.Drawing.Color.Crimson;
                //lb.FlatAppearance.BorderSize = 0;
                lb.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                lb.ForeColor = System.Drawing.Color.Transparent;
                lb.Location = new System.Drawing.Point(location.X, location.Y);
                lb.Size = new System.Drawing.Size(20, 20);
                lb.TabIndex = 0;
                lb.ContextMenuStrip = this.MonitorStrip1;
                //lb.UseVisualStyleBackColor = false;

                //双击添加 IP地址
                lb.DoubleClick += new EventHandler(this.RecordIp_DoubleClick);
                // bt1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.RecordIp_DoubleClick);

                lb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseDown);
                lb.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseMove);
                lb.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseUp);
                this.netpanel.Controls.Add(lb);
                lb.BringToFront();
            }
            else
            {
                MessageBox.Show("请先载入vsd文件");
            }
        }

        private void RecordIp_DoubleClick(object sender, EventArgs e)
        {
            
            Label lb = (Label)sender;
            string ip = lb.AccessibleDescription.ToString();
            if (ip.Length > 0 && ip != null)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo("ping", ip + " -t");
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = false;
                Process p = Process.Start(startInfo);

                p.WaitForExit();
                p.Close();
            }
            else
            {
                MessageBox.Show("未设置IP，请先设置IP地址");
            }
        }
        //监控点拖动设置
        Label temEle;
        Point ps ;
        private void Drag_MouseDown(object sender, MouseEventArgs e)
        {
            ps = e.Location;
           temEle = sender as Label;
          // MessageBox.Show("123");
        }
        private void Drag_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left&&!isProcessGo)//监控线程没开始才能移动
            {
                Point temP = new Point(temEle.Location.X + (e.X - ps.X), temEle.Location.Y + (e.Y - ps.Y));
                temEle.Location = temP;
            }
        }
        /// <summary>
        /// 鼠标坐标写入函数 继续
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Drag_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                temEle = sender as Label;
                Boolean isIntext = false;
                PointsWriteFile(temEle);                          
            }
        }
        private Boolean isProcessGo = false;
        private void StartMonitor_Click(object sender, EventArgs e)
        {
            if (!isProcessGo)
            {
                isProcessGo = true;
               // NetMonitor taa = new NetMonitor();
                Thread MonitorThread = new Thread(MonitorProcess);
                
                //Thread MonitorThread = new Thread(new ThreadStart(MonitorProcess));

                MonitorThread.IsBackground = true;
                MonitorThread.Name = "PingMonitor";
                MonitorThread.Start();
                this.StartMonitor.Text = "停止";
               // this.BackColor =  System.Drawing.Color.LightBlue;
                this.viewer.BackColor = System.Drawing.Color.LightBlue;
            }
            else
            {
                isProcessGo = false;
                this.StartMonitor.Text = "启动";
                //this.BackColor =  System.Drawing.Color.White;
                this.viewer.BackColor = System.Drawing.Color.White; ;
            }
            
        }
        //监控主线程
        private void MonitorProcess()
        {
            int pingOutTimes = 0;
            if (btnCount > 0)
            {
                while (isProcessGo)
                {
                    int count = btnCount;
                    for (count = btnCount; count > 0; count--)
                    {
                        Control[] ct = this.netpanel.Controls.Find("lb" + count, false);
                        if (ct.Length > 0)
                        {
                            temEle = ct[0] as Label;
                            String ipaddress = temEle.AccessibleDescription;
                            if (ipaddress.Length > 0)
                            {
                                if (Ping(ipaddress))
                                {
                                    temEle.BackColor = System.Drawing.Color.Green;
                                   
                                }// MessageBox.Show("成功");
                                else
                                {
                                    temEle.BackColor = System.Drawing.Color.Crimson;
                                    pingOutTimes++;
                                    if (pingOutTimes>5){//无效ping次数高达连续五次则报警
                                        this.alarmOn = true;//存在IP无连接
                                        pingOutTimes = 0;
                                    }
                                }

                            }
                            //此种为删除或者未写ip的
                            else
                            {
                                //10s循环更新一次  声音限制再10s之内

                                temEle.BackColor = System.Drawing.Color.Gray;
                            }
                        }
                        Thread.Sleep(2000 / btnCount);
                    }
                    if (alarmOn) {
                        soundDel del = new soundDel(Alarm);
                        this.Invoke(del, new Object[] {});
                        alarmOn = false;//每次重置
                    }
                }
            }
            else
            {
                MessageBox.Show("请添加监控点");
            }

        }
        //ping 命令 通过c# System.Net.NetworkInformation.Ping 实现
        public bool Ping(string ip)
        {
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
            options.DontFragment = true;
            string data = "Test Data!";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 1000; // Timeout 时间，单位：毫秒  
            System.Net.NetworkInformation.PingReply reply = p.Send(ip, timeout, buffer, options);
            if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                return true;
            else
                return false;
        }
        private Boolean alarmOn = false;
        //播放音乐

        [DllImport("winmm.dll")]
        public static extern uint mciSendString(string lpstrCommand,
        string lpstrReturnString, uint uReturnLength, uint hWndCallback); 
        public void Alarm()
        {
            mciSendString(@"close temp_alias", null, 0, 0);
            mciSendString(@"open ""5.mp3"" alias temp_alias", null, 0, 0);
            mciSendString("play temp_alias", null, 0, 0);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void MonitorStripMenuItem2_Click(object sender, EventArgs e)
        {
            //表单删除
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            ContextMenuStrip strip = menuItem.GetCurrentParent() as ContextMenuStrip;
            Label lb = strip.SourceControl as Label;
            this.netpanel.Controls.Remove(lb);
            //文件中是非能 lb
            PointsWriteFile(lb, 0);
 
        }

        private void MonitorSetItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            ContextMenuStrip strip = menuItem.GetCurrentParent() as ContextMenuStrip;
            Label lb = strip.SourceControl as Label;
           
            MonitorPoint mp = new MonitorPoint(lb);
            //mp.Show();
            if (mp.ShowDialog(this) == DialogResult.OK)
            {
                lb.AccessibleDescription = mp.GetIP();

                //写入文件
                PointsWriteFile(lb);
                //MessageBox.Show(bt.Name);
            }
        }
        /// <summary>
        /// 写入lable点到文件
        /// </summary>
        /// <param name="lb"></param>
        /// <param name="enable"></param>
        public void  PointsWriteFile(Label lablepoint,int enable = 1)
        {
            Label temEle = lablepoint;
            Boolean isIntext = false;
            FileStream monitorPoints = new FileStream(this.filename + ".set", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(monitorPoints);

            string stringPoint = reader.ReadToEnd();

            string[] points = stringPoint.Split('\n');
            for (int i = 0; i < points.Length - 1; i++)//需要减1
            {
                if (points[i].Contains(temEle.Name))//里面存在这个点改写位置
                {
                    points[i] = points[i].Trim();
                    string[] point = points[i].Split(' ');
                    point[1] = temEle.Location.X.ToString();
                    point[2] = temEle.Location.Y.ToString();
                    point[3] = temEle.AccessibleDescription.ToString();
                    point[4] = enable.ToString();
                    points[i] = "";//初始化
                    for (int j = 0; j < point.Length; j++)
                    {
                        points[i] += ' ' + point[j];
                    }
                    isIntext = true;
                }
            }
            monitorPoints.Close();
            reader.Close();
            monitorPoints = new FileStream(this.filename + ".set", FileMode.Truncate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(monitorPoints);
            //写入旧结点
            for (int i = 0; i < points.Length - 1; i++)
            {
                writer.Write(points[i] + '\n');
            }
            //是新结点
            if (!isIntext)
            {
                string point = temEle.Name + " " + temEle.Location.X.ToString() + " " + temEle.Location.Y.ToString() + " " + temEle.AccessibleDescription + " " + enable.ToString();
                writer.WriteLine(point);
            }
            writer.Close();
            monitorPoints.Close();
        }

        private void AboutItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("netMonitor v1.00  powerdby JOHN 联系方式：18655429576");
        }

        private void NetMonitor_Load(object sender, EventArgs e)
        {

        }
    }
}
