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
    public partial class TempWorkerPriceUpdate : DevExpress.XtraEditors.XtraForm
    {
        public TempWorkerPriceUpdate()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql = "update COST_TEMP_WORKER_PRICE set price = ltrim(rtrim('" + textEditTempWorkerPrice.Text.ToString() + "'))";
            sql = sql + " where cid = " + textEditID.Text.ToString();
            bool isok = conn.EditDatabase(sql);
            if (isok)
            {
                MessageBox.Show("修改成功！");
                TempWorkerPriceQuery.RefreshEX();
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

        private void TempWorkerPriceUpdate_Load(object sender, EventArgs e)
        {
            string yyyymm = "";
            int id = 0, saletypeid = 0;
            decimal price = 0;
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePicker1.Value = startMonth;
            TempWorkerPriceQuery.GetInfo(ref id, ref yyyymm, ref saletypeid, ref price);
            Common.BasicDataBind("cost_saletype", comboBoxSaleType);
            if (id != 0)
            {
                comboBoxSaleType.SelectedIndex = -1;
                comboBoxSaleType.SelectedValue = saletypeid;
                dateTimePicker1.Text = yyyymm;
                textEditTempWorkerPrice.Text = price.ToString();
                textEditID.Text = id.ToString();

            }

            dateTimePicker1.Focus();
            SendKeys.Send("{RIGHT} ");
        }
    }
}