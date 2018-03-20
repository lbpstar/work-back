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
    public partial class TempEmpTravelExpenseUpdate : DevExpress.XtraEditors.XtraForm
    {
        string id = "",empno = "", name = "", begin_date = "", expense = "0";
        public TempEmpTravelExpenseUpdate()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql = "update COST_EXPENSE set emp_no = '" + textEditEmpno.Text.ToString().Trim() + "',begin_date = '" + dateTimePickerDate.Text  + "',expense='" + Common.IsNull(textEditPrice.Text.ToString().Trim()) + "'";
            sql = sql + " where cid =  '" + id + "'";
            string sql2 = "select * from COST_EXPENSE where cid <> '" + id + "' and emp_no = '" + textEditEmpno.Text.ToString().Trim() + "' and begin_date = '"  + dateTimePickerDate.Text.ToString() + "'  and ctype =2";
            int rows = conn.ReturnRecordCount(sql2);
            if(rows > 0)
            {
                MessageBox.Show("该员工这天的交通补贴信息已经存在！");
            }
            else
            {
                bool isok = conn.EditDatabase(sql);
                if (isok)
                {
                    MessageBox.Show("修改成功！");
                    TempEmpTravelExpenseQuery.RefreshEX();
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
            TempEmpTravelExpenseQuery.GetInfo(ref id, ref empno, ref name, ref begin_date, ref expense);
            if (id != "")
            {
                textEditEmpno.Text = empno;
                textEditName.Text = name;
                dateTimePickerDate.Text = begin_date;
                textEditPrice.Text = expense;
            }
        }
    }
}