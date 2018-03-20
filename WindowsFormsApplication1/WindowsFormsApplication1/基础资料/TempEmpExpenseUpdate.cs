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
    public partial class TempEmpExpenseUpdate : DevExpress.XtraEditors.XtraForm
    {
        string id = "",ctype = "", expense = "", note = "";
        public TempEmpExpenseUpdate()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql = "update COST_expense set ctype = '" + comboBoxType.SelectedValue.ToString() + "',type_des = '" + comboBoxType.Text.ToString() + "',cmonth='" + dateTimePickerMonth.Text + "',expense='" + Common.IsNull(textEditExpense.Text.ToString().Trim()) + "',note='" +textEditExpense.Text.ToString().Trim() + "'";
            sql = sql + " where cid =  '" + id + "'";  
            bool isok = conn.EditDatabase(sql);
            if (isok)
            {
                MessageBox.Show("修改成功！");
                TempEmpExpenseQuery.RefreshEX();
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
            BindType();
            TempEmpExpenseQuery.GetInfo(ref id, ref ctype, ref expense, ref note);
            if (id != "")
            {
                textEditExpense.Text = expense;
                textEditNote.Text = note;
                comboBoxType.SelectedIndex = -1;
                comboBoxType.SelectedValue = ctype;

            }
        }
        public void BindType()
        {
            ConnDB conn = new ConnDB();
            string sql = "select sub_id cid,cname value from cost_base_data where module_id = 2";
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