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
    public partial class WaterElectricityInsert : DevExpress.XtraEditors.XtraForm
    {
        public WaterElectricityInsert()
        {
            InitializeComponent();
        }
        private static WaterElectricityInsert weiform = null;

        public static WaterElectricityInsert GetInstance()
        {
            if (weiform == null || weiform.IsDisposed)
            {
                weiform = new WaterElectricityInsert();
            }
            return weiform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            strsql = "insert into cost_water_electricity(yyyymm,sale_type_id,water_electricity) values('" + dateTimePicker1.Text.ToString() + "'," + comboBoxSaleType.SelectedValue + ",ltrim(rtrim(" + textEditWaterElectricity.Text.ToString() + ")))";
            strsql2 = "select yyyymm from cost_water_electricity where yyyymm ='" + dateTimePicker1.Text.ToString() + "' and sale_type_id = " + comboBoxSaleType.SelectedValue;
            if (textEditWaterElectricity.Text.ToString() != "")
            {
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("该水电费已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
                        WaterElectricityQuery.RefreshEX();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("失败！");
                    }
                }

            }
            else
            {
                MessageBox.Show("水电费不能为空！");
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
                strsql2 = "select yyyymm from cost_water_electricity where yyyymm ='" + dateTimePicker1.Text.ToString() + "'";
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("该月度水电费已经存在！");
                }
                else if(month < -1)
                {
                    MessageBox.Show("月份错误！");
                }
                else
                {
                    strsql = "insert into cost_water_electricity(yyyymm,sale_type_id,water_electricity) select '" + dateTimePicker1.Text + "',sale_type_id,water_electricity from cost_water_electricity where YYYYMM = (select Max(yyyymm) from cost_water_electricity) ";
                    isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("复制成功！");
                        WaterElectricityQuery.RefreshEX();
                        this.Close();
                    }
                }
                conn.Close();
            }
        }

        private void WaterElectricityInsert_Load(object sender, EventArgs e)
        {
            Common.BasicDataBind("cost_saletype", comboBoxSaleType);
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePicker1.Value = startMonth;

            dateTimePicker1.Focus();
            SendKeys.Send("{RIGHT} ");
        }
    }
}