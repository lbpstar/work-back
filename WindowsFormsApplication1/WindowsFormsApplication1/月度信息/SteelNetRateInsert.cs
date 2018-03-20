using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SMTCost
{
    public partial class SteelNetRateInsert : DevExpress.XtraEditors.XtraForm
    {
        public SteelNetRateInsert()
        {
            InitializeComponent();
        }
        private static SteelNetRateInsert weiform = null;

        public static SteelNetRateInsert GetInstance()
        {
            if (weiform == null || weiform.IsDisposed)
            {
                weiform = new SteelNetRateInsert();
            }
            return weiform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            strsql = "insert into cost_steel_net_rate(yyyymm,steel_net_rate) values('" + dateTimePicker1.Text.ToString() + "',ltrim(rtrim(" + textEditSteelNetRate.Text.ToString() + ")))";
            strsql2 = "select yyyymm from cost_steel_net_rate where yyyymm ='" + dateTimePicker1.Text.ToString() + "'";
            if (textEditSteelNetRate.Text.ToString() != "")
            {
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("该钢网占比已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
                        SteelNetRateQuery.RefreshEX();
                        this.Close();
                    }
                }

            }
            else
            {
                MessageBox.Show("钢网占比不能为空！");
            }
            conn.Close();
        }

        private void simpleButton复制_Click(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("将按最近的月度数据自动新增当前选择月份的数据，确认复制吗?", "复制最近月份数据", messButton);
            if (dr == DialogResult.OK)
            {
                ConnDB conn = new ConnDB();
                string strsql,strsql2;
                int rows;
                bool isok = false;
                DateTime dt1 = Convert.ToDateTime(dateTimePicker1.Text);
                DateTime dt2 = System.DateTime.Now;
                int month = (dt2.Year - dt1.Year) * 12 + (dt2.Month - dt1.Month);
                strsql2 = "select yyyymm from cost_steel_net_rate where yyyymm ='" + dateTimePicker1.Text.ToString() + "'";
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("该月度钢网占比已经存在！");
                }
                else if(month < -1)
                {
                    MessageBox.Show("月份错误！");
                }
                else
                {
                    strsql = "insert into cost_steel_net_rate(yyyymm,steel_net_rate) select '" + dateTimePicker1.Text + "',steel_net_rate from cost_steel_net_rate where YYYYMM = (select Max(yyyymm) from cost_steel_net_rate) ";
                    isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("复制成功！");
                        SteelNetRateQuery.RefreshEX();
                        this.Close();
                    }
                }
                conn.Close();
            }
        }

        private void SteelNetRateInsert_Load(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePicker1.Value = startMonth;

            dateTimePicker1.Focus();
            SendKeys.Send("{RIGHT} ");
        }
    }
}