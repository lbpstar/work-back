using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Configuration;

namespace SMTCost
{
    public partial class SMTCosting : DevExpress.XtraEditors.XtraForm
    {
        public SMTCosting()
        {
            InitializeComponent();
        }
        private static SMTCosting smtcform = null;

        public static SMTCosting GetInstance()
        {
            if (smtcform == null || smtcform.IsDisposed)
            {
                smtcform = new SMTCosting();
            }
            return smtcform;
        }
        private void simpleButton数据检查_Click(object sender, EventArgs e)
        {
            CheckData(true);
        }

        private void simpleButton成本计算_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string month = dateTimePicker1.Text;
            string sql;
            int i,rows;
            bool isok;
            bool success =true;
            isok = CheckData(false);
            if(isok)
            {
                sql = "select * from COST_MONTH_CALCULATE where cmonth ='" + dateTimePicker1.Text.ToString() + "' and sale_type_id =2";
                IDataParameter[] parameters = new IDataParameter[] { new SqlParameter("@cmonth", month), new SqlParameter("@saletypeid", "2") };

                rows = conn.ReturnRecordCount(sql);
                if (rows > 0)
                {
                    MessageBox.Show("该月已经存在成本计算结果，如要重新计算，请先清空该月计算结果！");
                }
                else
                {
                    try
                    {
                        conn.RunProcedure("costing", parameters, out i);
                    }
                    catch
                    {
                        MessageBox.Show("失败！");
                        success = false;
                    }
                    if (success)
                    {
                        MessageBox.Show("计算完成！");
                        ShowDetail();
                    }
                }
            }
            
            conn.Close();
        }

        private void simpleButton清空_Click(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要清空吗?", "清空", messButton);
            if (dr == DialogResult.OK)
            {
                SqlConnection Connection;
                string connectionString;
                string decryptStr;
                connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
                decryptStr = ConnDB.Decrypt(connectionString);
                Connection = new SqlConnection(decryptStr);

                string sql1, sql2, sql3, sql4;
                sql1 = "delete from COST_DAY_CALCULATE where cdate like '" + dateTimePicker1.Text.ToString() + "%' and sale_type_id =2";
                sql2 = "delete from COST_MONTH_CALCULATE where cmonth ='" + dateTimePicker1.Text.ToString() + "' and sale_type_id =2";
                sql3 = "delete from COST_DEPT_CALCULATE where cdate like '" + dateTimePicker1.Text.ToString() + "%' and sale_type_id =2";
                sql4 = "delete from COST_DEPT_MONTH_CALCULATE where cmonth ='" + dateTimePicker1.Text.ToString() + "' and sale_type_id =2";

                bool successState = false;

                Connection.Open();
                SqlTransaction myTrans = Connection.BeginTransaction();
                SqlCommand command1 = new SqlCommand(sql1, Connection, myTrans);
                SqlCommand command2 = new SqlCommand(sql2, Connection, myTrans);
                SqlCommand command3 = new SqlCommand(sql3, Connection, myTrans);
                SqlCommand command4 = new SqlCommand(sql4, Connection, myTrans);
                try
                {
                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    command3.ExecuteNonQuery();
                    command4.ExecuteNonQuery();
                    myTrans.Commit();
                    successState = true;
                }
                catch
                {
                    myTrans.Rollback();
                }
                finally
                {
                    Connection.Close();
                }
                if (successState)
                {
                    MessageBox.Show("清空成功！");
                    ShowDetail();
                }
                else
                {
                    MessageBox.Show("失败！");
                }

            }


        }
        private bool CheckData(bool ck)
        {
            ConnDB conn = new ConnDB();
            string sql;
            string yyyymm = dateTimePicker1.Text.ToString(), yyyy = "";
            int quarter = 0;
            int count;
            GetQuarter(yyyymm, ref yyyy, ref quarter);
            sql = "select top 1 * from COST_DIRECT_LABOUR_ATTENDANCE where cdate like '" + dateTimePicker1.Text.ToString() + "%'";
            count = conn.ReturnRecordCount(sql);
            if(count == 0)
            {
                MessageBox.Show("没有考勤数据！");
                return false;
            }

            sql = "select top 1 * from cost_pointcount_sum where saletype_id = 2 and cdate like '" + dateTimePicker1.Text.ToString() + "%'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                MessageBox.Show("没有生产点数数据！");
                return false;
            }
            sql = "select top 1 * from cost_depreciation where YYYYMM = '" + dateTimePicker1.Text.ToString() + "' and SALE_TYPE_ID = 2";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                MessageBox.Show("没有折旧数据！");
                return false;
            }
            sql = "select top 1 * from COST_RENT_EXPENSE where YYYYMM = '" + dateTimePicker1.Text.ToString() + "' and SALE_TYPE_ID = 2";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                MessageBox.Show("没有租赁费用数据！");
                return false;
            }
            sql = "select top 1 * from COST_WATER_ELECTRICITY where YYYYMM = '" + dateTimePicker1.Text.ToString() + "' and SALE_TYPE_ID = 2";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                MessageBox.Show("没有水电费数据！");
                return false;
            }
            sql = "select top 1 * from COST_STANDARD_POINT where YYYYMM = '" + dateTimePicker1.Text.ToString() + "' and SALE_TYPE_ID = 2";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                MessageBox.Show("没有标准单点成本数据！");
                return false;
            }
            sql = "select top 1 * from COST_DEPT_STANDARD_POINT where YYYYMM = '" + dateTimePicker1.Text.ToString() + "' and SALE_TYPE_ID = 2";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                MessageBox.Show("没有部门标准单点成本数据！");
                return false;
            }

            sql = "select top 1 * from COST_DIRECT_LABOUR_PRICE where YYYYMM = '" + dateTimePicker1.Text.ToString() + "'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                MessageBox.Show("没有直接人工费率！");
                return false;
            }
            sql = "select top 1 * from COST_INDIRECT_LABOUR_LEVEL_PRICE where YYYYMM = '" + dateTimePicker1.Text.ToString() + "'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                MessageBox.Show("没有间接人工费率！");
                return false;
            }
            sql = "select top 1 * from cost_rate where yyyy ='" + yyyy + "' and isnull(dept_id,0) >0 and quarter_id = " + quarter + " and sale_type_id = 2";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                MessageBox.Show("没有部门成本比率！");
                return false;
            }
            sql = "select top 1 * from COST_RATE where yyyy ='" + yyyy + "' and isnull(dept_id,0) = 0 and QUARTER_ID = " + quarter + " and sale_type_id = 2";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                MessageBox.Show("没有成本比率！");
                return false;
            }
            if (ck)
            {
                MessageBox.Show("检查完成！");
            }
            return true;

        }
        public void GetQuarter(string yyyymm, ref string yyyy, ref int quarter)
        {
            yyyy = yyyymm.Substring(0, 4);
            int mm = Convert.ToInt32(yyyymm.Substring(5, 2));
            if (mm < 4)
                quarter = 1;
            else if (mm >= 4 && mm < 7)
                quarter = 2;
            else if (mm >= 7 && mm < 10)
                quarter = 3;
            else
                quarter = 4;
        }
        private void ShowDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql, month;
            month = dateTimePicker1.Text.ToString();
            strsql = "select * from (select SALE_TYPE_NAME 营业类型,CONVERT(varchar(100), CDATE, 23) 日期,DIRECT_HOURS 直接人工小时数,cast(round(DIRECT_COST,2) as decimal(18,2)) 直接人工成本,INDIRECT_HOURS 间接人工小时数,cast(round(INDIRECT_COST,2) as decimal(18,2)) 间接人工成本,cast(round(DEPRECIATION,2) as decimal(18,2)) 折旧费,cast(round(RENT_EXPENSE,2) as decimal(18,2)) 租赁费,cast(round(WATER_ELECTRICITY,2) as decimal(18,2)) 水电费,cast(round(POINTCOUNT,2) as decimal(18,2)) 点数,cast(round(COST,2) as decimal(18,2)) 预估成本,COST_POINT 预估单点成本,STANDARD_POINT 标准单点成本,cast(round(STANDARD_COST,2) as decimal(18,2)) 标准成本,cast(round(PROFIT,2) as decimal(18,2)) 盈亏 from COST_DAY_CALCULATE where cdate like '" + month + "%' and sale_type_id =2";
            strsql = strsql + " union select SALE_TYPE_NAME 营业类型,'汇总：' 日期,DIRECT_HOURS 直接人工小时数,cast(round(DIRECT_COST,2) as decimal(18,2)) 直接人工成本,INDIRECT_HOURS 间接人工小时数,cast(round(INDIRECT_COST,2) as decimal(18,2)) 间接人工成本,cast(round(DEPRECIATION,2) as decimal(18,2)) 折旧费,cast(round(RENT_EXPENSE,2) as decimal(18,2)) 租赁费,cast(round(WATER_ELECTRICITY,2) as decimal(18,2)) 水电费,cast(round(POINTCOUNT,2) as decimal(18,2)) 点数,cast(round(COST,2) as decimal(18,2)) 预估成本,COST_POINT 预估单点成本,STANDARD_POINT 标准单点成本,cast(round(STANDARD_COST,2) as decimal(18,2)) 标准成本,cast(round(PROFIT,2) as decimal(18,2)) 盈亏 from COST_MONTH_CALCULATE where cmonth = '" + month + "' and sale_type_id =2) i order by 日期";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;
            //gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            //gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            conn.Close();
        }

        private void SMTCosting_Load(object sender, EventArgs e)
        {
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            ShowDetail();
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePicker1.Value = startMonth;

            dateTimePicker1.Focus();
            SendKeys.Send("{RIGHT} ");
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ShowDetail();
        }
    }
}