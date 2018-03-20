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
    public partial class SteelNetRateUpdate : DevExpress.XtraEditors.XtraForm
    {
        public SteelNetRateUpdate()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql = "update cost_steel_net_rate set steel_net_rate = ltrim(rtrim('" + textEditSteelNetRate.Text.ToString() + "'))";
            sql = sql + " where cid = " + textEditID.Text.ToString();
            bool isok = conn.EditDatabase(sql);
            if (isok)
            {
                MessageBox.Show("修改成功！");
                SteelNetRateQuery.RefreshEX();
                this.Close();
            }
            else
            {
                MessageBox.Show("失败！");
            }
            conn.Close();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WaterElectricityUpdate_Load(object sender, EventArgs e)
        {
            string yyyymm = "";
            int id = 0;
            decimal steelnetrate = 0;
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePicker1.Value = startMonth;
            SteelNetRateQuery.GetInfo(ref id, ref yyyymm, ref steelnetrate);
            if (id != 0)
            {
                dateTimePicker1.Text = yyyymm;
                textEditSteelNetRate.Text = steelnetrate.ToString();
                textEditID.Text = id.ToString();

            }

            dateTimePicker1.Focus();
            SendKeys.Send("{RIGHT} ");
        }
    }
}