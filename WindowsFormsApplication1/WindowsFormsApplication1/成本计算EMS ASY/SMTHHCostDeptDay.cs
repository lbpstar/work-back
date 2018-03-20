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
    public partial class SMTHHCostDeptDay : DevExpress.XtraEditors.XtraForm
    {
        public SMTHHCostDeptDay()
        {
            InitializeComponent();
        }
        private static SMTHHCostDeptDay smtddform = null;

        public static SMTHHCostDeptDay GetInstance()
        {
            if (smtddform == null || smtddform.IsDisposed)
            {
                smtddform = new SMTHHCostDeptDay();
            }
            return smtddform;
        }      

        private void simpleButton查询_Click(object sender, EventArgs e)
        {
            ShowDetail();
        }
        private void ShowDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql, month;
            int deptid;
            deptid = (int)comboBoxDept.SelectedValue;
            month = dateTimePicker1.Text.ToString();
            if(deptid == 0)
            {
                strsql = "select * from (select SALE_TYPE_NAME 营业类型,dept_name 部门,CONVERT(varchar(100), CDATE, 23) 日期,DIRECT_HOURS 直接人工小时数,cast(round(DIRECT_COST, 2) as decimal(18, 2)) 直接人工成本,INDIRECT_HOURS 间接人工小时数,cast(round(INDIRECT_COST, 2) as decimal(18, 2)) 间接人工成本,EMS_HH_HOURS 产出工时,cast(round(COST, 2) as decimal(18, 2)) 预估成本,COST_POINT 预估单小时成本,STANDARD_POINT 标准单小时成本,cast(round(STANDARD_COST, 2) as decimal(18, 2)) 标准成本,cast(round(PROFIT, 2) as decimal(18, 2)) 盈亏 from COST_DEPT_CALCULATE where cdate like '" + month + "%' and sale_type_id =13 union ";
                strsql = strsql + "select SALE_TYPE_NAME 营业类型,dept_name 部门,'汇总：' 日期,DIRECT_HOURS 直接人工小时数,cast(round(DIRECT_COST, 2) as decimal(18, 2)) 直接人工成本,INDIRECT_HOURS 间接人工小时数,cast(round(INDIRECT_COST, 2) as decimal(18, 2)) 间接人工成本,EMS_HH_HOURS 产出工时,cast(round(COST, 2) as decimal(18, 2)) 预估成本,COST_POINT 预估单小时成本,STANDARD_POINT 标准单小时成本,cast(round(STANDARD_COST, 2) as decimal(18, 2)) 标准成本,cast(round(PROFIT, 2) as decimal(18, 2)) 盈亏 from COST_DEPT_MONTH_CALCULATE where cmonth = '" + month + "' and sale_type_id =13) i order by 部门";
            }
            else
            {
                strsql = "select * from (select SALE_TYPE_NAME 营业类型,dept_name 部门,CONVERT(varchar(100), CDATE, 23) 日期,DIRECT_HOURS 直接人工小时数,cast(round(DIRECT_COST, 2) as decimal(18, 2)) 直接人工成本,INDIRECT_HOURS 间接人工小时数,cast(round(INDIRECT_COST, 2) as decimal(18, 2)) 间接人工成本,EMS_HH_HOURS 产出工时,cast(round(COST, 2) as decimal(18, 2)) 预估成本,COST_POINT 预估单小时成本,STANDARD_POINT 标准单小时成本,cast(round(STANDARD_COST, 2) as decimal(18, 2)) 标准成本,cast(round(PROFIT, 2) as decimal(18, 2)) 盈亏 from COST_DEPT_CALCULATE where cdate like '" + month + "%' and sale_type_id =13 and dept_id = " + deptid;
                strsql = strsql + " union select SALE_TYPE_NAME 营业类型,dept_name 部门,'汇总：' 日期,DIRECT_HOURS 直接人工小时数,cast(round(DIRECT_COST, 2) as decimal(18, 2)) 直接人工成本,INDIRECT_HOURS 间接人工小时数,cast(round(INDIRECT_COST, 2) as decimal(18, 2)) 间接人工成本,EMS_HH_HOURS 产出工时,cast(round(COST, 2) as decimal(18, 2)) 预估成本,COST_POINT 预估单小时成本,STANDARD_POINT 标准单小时成本,cast(round(STANDARD_COST, 2) as decimal(18, 2)) 标准成本,cast(round(PROFIT, 2) as decimal(18, 2)) 盈亏 from COST_DEPT_MONTH_CALCULATE where cmonth = '" + month + "' and sale_type_id =13 and dept_id = " + deptid + ") i";
            }
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;
            //gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            //gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            conn.Close();
        }
        public void BindDept()
        {
            ConnDB conn = new ConnDB();
            string sql = "select d.cid,d.cname from  COST_DEPT d where d.saletype_id = 13";
            DataSet ds = conn.ReturnDataSet(sql);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = "0";
            dr[1] = "请选择";
            //插在第一位

            ds.Tables[0].Rows.InsertAt(dr, 0);
            comboBoxDept.DataSource = ds.Tables[0];
            comboBoxDept.DisplayMember = "CNAME";
            comboBoxDept.ValueMember = "CID";
            conn.Close();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ShowDetail();
        }

        private void comboBoxDept_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxDept.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                ShowDetail();
            }
        }

        private void SMTHHCostDeptDay_Load(object sender, EventArgs e)
        {
            BindDept();
            ShowDetail();
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePicker1.Value = startMonth;

            dateTimePicker1.Focus();
            SendKeys.Send("{RIGHT} ");
        }
    }
}