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
    public partial class TempEmpTravelExpenseInsert : DevExpress.XtraEditors.XtraForm
    {
        public TempEmpTravelExpenseInsert()
        {
            InitializeComponent();
        }
        private static TempEmpTravelExpenseInsert weiform = null;

        public static TempEmpTravelExpenseInsert GetInstance()
        {
            if (weiform == null || weiform.IsDisposed)
            {
                weiform = new TempEmpTravelExpenseInsert();
            }
            return weiform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            strsql = "insert into COST_EXPENSE(CTYPE,TYPE_DES,BEGIN_DATE,EMP_NO,EXPENSE) values(2,'交通补贴','" + dateTimePickerBegin.Text.ToString() + "','" + textEditEmpno.Text.ToString().Trim() + "','" + textEditPrice.Text.ToString().Trim() + "')";
            strsql2 = "select EXPENSE from  COST_EXPENSE where ctype =2 and emp_no ='" + textEditEmpno.Text.ToString().Trim() + "' and begin_date = '" + dateTimePickerBegin.Text.ToString() + "'";
            if (textEditEmpno.Text.ToString().Trim() != "")
            {
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("该员工此日期的交通补贴已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
                        TempEmpTravelExpenseQuery.RefreshEX();
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
                MessageBox.Show("工号不能为空！");
            }
            conn.Close();
        }                                
        private void TempEmpInsert_Load(object sender, EventArgs e)
        {

            //DateTime dt = DateTime.Now;
            //DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            //this.dateTimePicker1.Value = startMonth;

            //dateTimePicker1.Focus();
            //SendKeys.Send("{RIGHT} ");
        }

        private void textEditEmpno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnDB conn = new ConnDB();
                string sql;
                sql = "select cname from COST_TEMP_EMPLOYEE where cno = '" + textEditEmpno.Text.ToString().Trim() + "' and status != '离职'";
                DataSet ds = conn.ReturnDataSet(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    textEditName.Text = ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    textEditName.Text = "";
                }
            }
        }
    }
}