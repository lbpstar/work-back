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
    public partial class TransferPriceUpdate : DevExpress.XtraEditors.XtraForm
    {
        public TransferPriceUpdate()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql = "update cost_transfer_price set price = ltrim(rtrim('" + Common.IsNull(textEditTransferPirce.Text.ToString()) + "'))";
            sql = sql + " where cid = " + textEditID.Text.ToString();
            bool isok = conn.EditDatabase(sql);
            if (isok)
            {
                MessageBox.Show("修改成功！");
                TransferPriceQuery.RefreshEX();
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

        private void CostRateUpdate_Load(object sender, EventArgs e)
        {
            string yyyy = "";
            int id = 0, saletypeid = 0,quarterid = 0;
            decimal price = 0;
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePicker1.Value = startMonth;
            TransferPriceQuery.GetInfo(ref id, ref yyyy,ref quarterid,ref saletypeid, ref price);
            Common.BasicDataBind("cost_saletype", comboBoxSaleType);
            Common.BasicDataBind("cost_quarter", comboBoxQuarter);
            if (id != 0)
            {
                comboBoxSaleType.SelectedIndex = -1;
                comboBoxSaleType.SelectedValue = saletypeid;
                comboBoxQuarter.SelectedIndex = -1;
                comboBoxQuarter.SelectedValue = quarterid;
                dateTimePicker1.Value = Convert.ToDateTime(yyyy + "-01");
                textEditTransferPirce.Text = price.ToString();
                textEditID.Text = id.ToString();

            }
        }
    }
}