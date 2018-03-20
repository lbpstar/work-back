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
    public partial class TempEmpPriceUpdate : DevExpress.XtraEditors.XtraForm
    {
        string id = "",supplier = "", begin_date = "", end_date = "", from_type = "0",price = "",meal_price = "",night_price = "",travel_price = "", regular_price = "";
        public TempEmpPriceUpdate()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql = "update COST_TEMP_EMPLOYEE_price set supplier = '" + textEditSupplier.Text.ToString().Trim() + "',begin_date = '" + dateTimePickerBegin.Text + "',end_date='" + dateTimePickerEnd.Text + "',from_type='" + comboBoxType.SelectedValue.ToString() + "',price='" + Common.IsNull(textEditPrice.Text.ToString().Trim()) + "',meal_price='" + Common.IsNull(textEditMeal.Text.ToString().Trim()) + "',night_price='" + Common.IsNull(textEditNight.Text.ToString().Trim()) + "',travel_price='" + Common.IsNull(textEditTravel.Text.ToString().Trim()) + "',regular_price='" + Common.IsNull(textEditRegular.Text.ToString().Trim()) + "'";
            sql = sql + " where cid =  '" + id + "'";
            string sql2 = "select * from cost_temp_employee_price where cid <> '" + id + "' and supplier = '" + textEditSupplier.Text.ToString().Trim() + "' and not (begin_date > '" + dateTimePickerEnd.Text.ToString() + "' or end_date <'" + dateTimePickerBegin.Text.ToString() + "') and from_type = '" + comboBoxType.SelectedValue.ToString() + "' and type =1";
            int rows = conn.ReturnRecordCount(sql2);
            if(rows > 0)
            {
                MessageBox.Show("此时间范围的价格信息已经存在！");
            }
            else
            {
                bool isok = conn.EditDatabase(sql);
                if (isok)
                {
                    MessageBox.Show("修改成功！");
                    TempEmpPriceQuery.RefreshEX();
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
            BindType();
            TempEmpPriceQuery.GetInfo(ref id,ref supplier, ref begin_date, ref end_date, ref from_type,ref price,ref meal_price,ref night_price,ref travel_price, ref regular_price);
            if (supplier != "")
            {
                textEditSupplier.Text = supplier;
                dateTimePickerBegin.Text = begin_date;
                dateTimePickerEnd.Text = end_date;
                textEditPrice.Text = price;
                textEditMeal.Text =meal_price;
                textEditNight.Text = night_price;
                textEditTravel.Text = travel_price;
                textEditRegular.Text = regular_price;
                comboBoxType.SelectedIndex = -1;
                comboBoxType.SelectedValue = from_type;

            }
        }
        public void BindType()
        {
            ConnDB conn = new ConnDB();
            string sql = "select sub_id cid,cname value from cost_base_data where module_id = 1";
            DataSet ds = conn.ReturnDataSet(sql);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = 0;
            dr[1] = "请选择";
            //插在第一位

            ds.Tables[0].Rows.InsertAt(dr, 0);
            comboBoxType.DataSource = ds.Tables[0];
            comboBoxType.DisplayMember = "value";
            comboBoxType.ValueMember = "CID";
            conn.Close();
        }
    }
}