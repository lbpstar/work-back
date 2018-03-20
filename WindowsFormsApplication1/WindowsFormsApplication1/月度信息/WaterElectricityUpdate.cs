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
    public partial class WaterElectricityUpdate : DevExpress.XtraEditors.XtraForm
    {
        public WaterElectricityUpdate()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql = "update cost_water_electricity set water_electricity = ltrim(rtrim('" + textEditWaterElectricity.Text.ToString() + "'))";
            sql = sql + " where cid = " + textEditID.Text.ToString();
            bool isok = conn.EditDatabase(sql);
            if (isok)
            {
                MessageBox.Show("修改成功！");
                WaterElectricityQuery.RefreshEX();
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
            int id = 0, saletypeid = 0;
            decimal waterelectricity = 0;
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePicker1.Value = startMonth;
            WaterElectricityQuery.GetInfo(ref id, ref yyyymm, ref saletypeid, ref waterelectricity);
            Common.BasicDataBind("cost_saletype", comboBoxSaleType);
            if (id != 0)
            {
                comboBoxSaleType.SelectedIndex = -1;
                comboBoxSaleType.SelectedValue = saletypeid;
                dateTimePicker1.Text = yyyymm;
                textEditWaterElectricity.Text = waterelectricity.ToString();
                textEditID.Text = id.ToString();

            }

            dateTimePicker1.Focus();
            SendKeys.Send("{RIGHT} ");
        }
    }
}