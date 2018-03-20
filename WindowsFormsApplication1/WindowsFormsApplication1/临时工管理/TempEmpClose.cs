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
    public partial class TempEmpClose : DevExpress.XtraEditors.XtraForm
    {
        public TempEmpClose()
        {
            InitializeComponent();
        }
        private static TempEmpClose weiform = null;

        public static TempEmpClose GetInstance()
        {
            if (weiform == null || weiform.IsDisposed)
            {
                weiform = new TempEmpClose();
            }
            return weiform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql, sql2;
            string month = dateTimePickerMonth.Text.ToString();
            int rows;
            sql2 = "select cmonth from COST_TEMP_CLOSE where cmonth ='" + month + "'";
            rows = conn.ReturnRecordCount(sql2);
            if (rows > 0)
            {
                sql = "update i set i.closed = 1 from  COST_TEMP_CLOSE i where cmonth = '" + month + "'";
                simpleButtonClose.Enabled = false;
                simpleButtonOpen.Enabled = true;
            }
            else
            {
                sql = "insert into COST_TEMP_CLOSE(cmonth,closed) values('" + month + "',1)";
                simpleButtonClose.Enabled = false;
                simpleButtonOpen.Enabled = true;
            }
            conn.EditDatabase(sql);
            conn.Close();
        }

        private void TempEmpInsert_Load(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePickerMonth.Value = startMonth;
            dateTimePickerMonth.Focus();
            SendKeys.Send("{RIGHT} ");

            ConnDB conn = new ConnDB();
            string sql;
            string month = dateTimePickerMonth.Text.ToString();
            sql = "select closed from COST_TEMP_CLOSE where cmonth ='" + month + "'";
            DataSet ds = conn.ReturnDataSet(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if(ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    simpleButtonClose.Enabled = false;
                    simpleButtonOpen.Enabled = true;
                }
                else
                {
                    simpleButtonClose.Enabled = true;
                    simpleButtonOpen.Enabled = false;
                }

            }
            else
            {
                simpleButtonClose.Enabled = true;
                simpleButtonOpen.Enabled = false;
            }
            conn.Close();
        }

        private void simpleButtonOpen_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql;
            string month = dateTimePickerMonth.Text.ToString();
            sql = "update i set i.closed = 0 from  COST_TEMP_CLOSE i where cmonth = '" + month + "'";
            simpleButtonClose.Enabled = true;
            simpleButtonOpen.Enabled = false;
            conn.EditDatabase(sql);
            conn.Close();
        }
    }
}