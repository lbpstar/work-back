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
    public partial class TempEmpExpenseInsert : DevExpress.XtraEditors.XtraForm
    {
        public TempEmpExpenseInsert()
        {
            InitializeComponent();
        }
        private static TempEmpExpenseInsert weiform = null;

        public static TempEmpExpenseInsert GetInstance()
        {
            if (weiform == null || weiform.IsDisposed)
            {
                weiform = new TempEmpExpenseInsert();
            }
            return weiform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql;
            strsql = "insert into COST_EXPENSE(CTYPE,TYPE_DES,CMONTH,EXPENSE,NOTE) values('" + comboBoxType.SelectedValue.ToString() + "','" + comboBoxType.Text.ToString() + "','" + dateTimePickerMonth.Text.ToString() + "','" + Common.IsNull(textEditExpense.Text.ToString().Trim()) + "','" + textEditNote.Text.ToString().Trim() + "')";
            if (textEditExpense.Text.ToString().Trim() != "")
            {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
                        TempEmpExpenseQuery.RefreshEX();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("失败！");
                    }
            }
            else
            {
                MessageBox.Show("费用金额不能为空！");
            }
            conn.Close();
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
        private void TempEmpInsert_Load(object sender, EventArgs e)
        {

            BindType();
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePickerMonth.Value = startMonth;

            dateTimePickerMonth.Focus();
            SendKeys.Send("{RIGHT} ");
        }

    }
}