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
    public partial class TempEmpStandardPriceUpdate : DevExpress.XtraEditors.XtraForm
    {
        string id = "", begin_date = "", end_date = "", price = "",insurance_price = "";
        public TempEmpStandardPriceUpdate()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql = "update COST_TEMP_EMPLOYEE_price set begin_date = '" + dateTimePickerBegin.Text + "',end_date='" + dateTimePickerEnd.Text + "',price='" + Common.IsNull(textEditPrice.Text.ToString().Trim()) + "',insurance_price='" + Common.IsNull(textEditInsurance.Text.ToString().Trim()) + "'";
            sql = sql + " where cid =  '" + id + "'";
            string sql2 = "select * from cost_temp_employee_price where cid <> '" + id + "' and type = 2 and not (begin_date > '" + dateTimePickerEnd.Text.ToString() + "' or end_date <'" + dateTimePickerBegin.Text.ToString() + "')";
            int rows = conn.ReturnRecordCount(sql2);
            if (rows > 0)
            {
                MessageBox.Show("此时间范围的记录已经存在！");
            }
            else
            {
                bool isok = conn.EditDatabase(sql);
                if (isok)
                {
                    MessageBox.Show("修改成功！");
                    TempEmpStandardPriceQuery.RefreshEX();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("失败！");
                }
            }
            conn.Close();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TempWorkerPriceUpdate_Load(object sender, EventArgs e)
        {

            TempEmpStandardPriceQuery.GetInfo(ref id, ref begin_date, ref end_date, ref price,ref insurance_price);
            if (id != "")
            {
                dateTimePickerBegin.Text = begin_date;
                dateTimePickerEnd.Text = end_date;
                textEditPrice.Text = price;
                textEditInsurance.Text = insurance_price;
            }

        }
    }
}