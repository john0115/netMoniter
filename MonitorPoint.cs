using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace openVisio
{
    public partial class MonitorPoint : Form
    {
        private string ip ;
        Label lb;
        public MonitorPoint()
        {
            InitializeComponent();
        }
        public MonitorPoint(Label p)
        {
            InitializeComponent();
            this.lb = p;
            this.IpText.Text = this.lb.AccessibleDescription;
        }
        //函数去除ip
        public string GetIP()
        {
            return this.ip;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            this.ip = this.IpText.Text.Trim();
            //校验格式
            string pattern = @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}";
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
            if (regex.IsMatch(this.ip)) 
            {
                regex = new System.Text.RegularExpressions.Regex(@"\d{1,3}");;
                System.Text.RegularExpressions.MatchCollection mc = regex.Matches(this.ip);
                int ele =0 ;
                for (int i = 0; i < mc.Count; i++)
                {
                    ele = Convert.ToInt32(mc[i].Value);
                    if (ele > 254 || ele < 1)//不在 1~254内
                    { ele = -1; }
                    
                }
                if (ele < 0)
                {
                    this.ip = "";
                    MessageBox.Show("输入的IP地址必须再0~255范围之内 格式：（*.*.*.*）");
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                this.ip = "";
                MessageBox.Show("输入的IP地址不符合要求 格式：（*.*.*.*）");
            }
        }

    }
}
