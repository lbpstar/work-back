﻿using System;
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
    public partial class ProductCostDay : DevExpress.XtraEditors.XtraForm
    {
        public ProductCostDay()
        {
            InitializeComponent();
        }
        private static ProductCostDay smtcdform = null;

        public static ProductCostDay GetInstance()
        {
            if (smtcdform == null || smtcdform.IsDisposed)
            {
                smtcdform = new ProductCostDay();
            }
            return smtcdform;
        }


        private void simpleButton查询_Click(object sender, EventArgs e)
        {
            ShowDetail();
        }
        private void ShowDetail()
        {
            ConnDB conn = new ConnDB();
            string sql, sqlsum,month;
            month = dateTimePicker1.Text.ToString();
            sql = "select SALE_TYPE_NAME 营业类型,  CONVERT(varchar(100), CDATE, 23)  日期,DIRECT_HOURS 直接人工小时数,cast(round(DIRECT_COST, 2) as decimal(18, 2)) 直接人工成本,TEMP_HOURS 临时工小时数,cast(round(TEMP_COST, 2) as decimal(18, 2)) 临时工成本,INDIRECT_HOURS 间接人工小时数,cast(round(INDIRECT_COST, 2) as decimal(18, 2)) 间接人工成本,cast(round(DEPRECIATION, 2) as decimal(18, 2)) 折旧费,cast(round(OPERATION_TRANSFER, 2) as decimal(18, 2)) 运营部费用转嫁,cast(round(TRIAL_TRANSFER, 2) as decimal(18, 2)) 试产费用转嫁,cast(round(COMPOSITE_EXPENSE, 2) as decimal(18, 2)) 主营综合费用,cast(round(POINTCOUNT, 2) as decimal(18, 2)) 产出台数,cast(round(COST, 2) as decimal(18, 2)) 预估成本,COST_POINT 预估单台成本,STANDARD_POINT 标准单台成本,cast(round(STANDARD_COST, 2) as decimal(18, 2)) 标准成本,cast(round(PROFIT, 2) as decimal(18, 2)) 盈亏 from COST_DAY_CALCULATE where cdate like '" + month + "%' and sale_type_id =14 order by cdate";
            DataSet ds = conn.ReturnDataSet(sql);
            sqlsum = "select SALE_TYPE_NAME 营业类型,'汇总：' 日期,DIRECT_HOURS 直接人工小时数,cast(round(DIRECT_COST, 2) as decimal(18, 2)) 直接人工成本,TEMP_HOURS 临时工小时数,cast(round(TEMP_COST, 2) as decimal(18, 2)) 临时工成本,INDIRECT_HOURS 间接人工小时数,cast(round(INDIRECT_COST, 2) as decimal(18, 2)) 间接人工成本,cast(round(DEPRECIATION, 2) as decimal(18, 2)) 折旧费,cast(round(OPERATION_TRANSFER, 2) as decimal(18, 2)) 运营部费用转嫁,cast(round(TRIAL_TRANSFER, 2) as decimal(18, 2)) 试产费用转嫁,cast(round(COMPOSITE_EXPENSE, 2) as decimal(18, 2)) 主营综合费用,cast(round(POINTCOUNT, 2) as decimal(18, 2)) 产出台数,cast(round(COST, 2) as decimal(18, 2)) 预估成本,COST_POINT 预估单台成本,STANDARD_POINT 标准单台成本,cast(round(STANDARD_COST, 2) as decimal(18, 2)) 标准成本,cast(round(PROFIT, 2) as decimal(18, 2)) 盈亏 from COST_MONTH_CALCULATE where cmonth = '" + month + "' and sale_type_id =14";
            DataSet ds2 = conn.ReturnDataSet(sqlsum);
            ds.Merge(ds2, true, MissingSchemaAction.AddWithKey);
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

        private void ProductCostDay_Load(object sender, EventArgs e)
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
    }
}