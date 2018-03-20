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
    public partial class SMTCostDay : DevExpress.XtraEditors.XtraForm
    {
        public SMTCostDay()
        {
            InitializeComponent();
        }
        private static SMTCostDay smtcdform = null;

        public static SMTCostDay GetInstance()
        {
            if (smtcdform == null || smtcdform.IsDisposed)
            {
                smtcdform = new SMTCostDay();
            }
            return smtcdform;
        }
       
        private void SMTCosting_Load(object sender, EventArgs e)
        {
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

        private void simpleButton查询_Click(object sender, EventArgs e)
        {
            ShowDetail();
        }
        private void ShowDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql, month;
            month = dateTimePicker1.Text.ToString();
            strsql = "select * from (select SALE_TYPE_NAME 营业类型,CONVERT(varchar(100), CDATE, 23) 日期,DIRECT_HOURS 直接人工小时数,cast(round(DIRECT_COST,2) as decimal(18,2)) 直接人工成本,INDIRECT_HOURS 间接人工小时数,cast(round(INDIRECT_COST,2) as decimal(18,2)) 间接人工成本,cast(round(DEPRECIATION,2) as decimal(18,2)) 折旧费,cast(round(RENT_EXPENSE,2) as decimal(18,2)) 租赁费,cast(round(WATER_ELECTRICITY,2) as decimal(18,2)) 水电费,cast(round(POINTCOUNT,2) as decimal(18,2)) 点数,cast(round(COST,2) as decimal(18,2)) 预估成本,COST_POINT 预估单点成本,STANDARD_POINT 标准单点成本,cast(round(STANDARD_COST,2) as decimal(18,2)) 标准成本,cast(round(PROFIT,2) as decimal(18,2)) 盈亏 from COST_DAY_CALCULATE where cdate like '" + month + "%' and sale_type_id =2 ";
            strsql = strsql + " union select SALE_TYPE_NAME 营业类型,'汇总：' 日期,DIRECT_HOURS 直接人工小时数,cast(round(DIRECT_COST,2) as decimal(18,2)) 直接人工成本,INDIRECT_HOURS 间接人工小时数,cast(round(INDIRECT_COST,2) as decimal(18,2)) 间接人工成本,cast(round(DEPRECIATION,2) as decimal(18,2)) 折旧费,cast(round(RENT_EXPENSE,2) as decimal(18,2)) 租赁费,cast(round(WATER_ELECTRICITY,2) as decimal(18,2)) 水电费,cast(round(POINTCOUNT,2) as decimal(18,2)) 点数,cast(round(COST,2) as decimal(18,2)) 预估成本,COST_POINT 预估单点成本,STANDARD_POINT 标准单点成本,cast(round(STANDARD_COST,2) as decimal(18,2)) 标准成本,cast(round(PROFIT,2) as decimal(18,2)) 盈亏 from COST_MONTH_CALCULATE where cmonth = '" + month + "' and sale_type_id =2) i  order by 日期";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;
            //gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            //gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            conn.Close();
        }

        private void simpleButtonChart_Click(object sender, EventArgs e)
        {
            SMTCostDayChart Frm = new SMTCostDayChart();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            Frm.Show();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ShowDetail();
        }
    }
}