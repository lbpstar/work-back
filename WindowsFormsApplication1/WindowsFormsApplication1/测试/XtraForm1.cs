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
using DevExpress.XtraCharts;
using DevExpress.Utils;

namespace SMTCost
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm1()
        {
            InitializeComponent();
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {
            #region
            ConnDB conn = new ConnDB();
            string strsql;
            //month = dateTimePicker1.Text.ToString();
            strsql = "select SALE_TYPE_NAME 营业类型,CDATE 日期,DIRECT_HOURS 直接人工小时数,DIRECT_COST 直接人工成本,INDIRECT_HOURS 间接人工小时数,INDIRECT_COST 间接人工成本,DEPRECIATION 折旧费,RENT_EXPENSE 租赁费,WATER_ELECTRICITY 水电费,POINTCOUNT 点数,COST 预估成本,COST_POINT 预估单点成本,STANDARD_POINT 标准单点成本,STANDARD_COST 标准成本,PROFIT 盈亏 from COST_DAY_CALCULATE where cdate like '" + "2017-05" + "%'";
            DataSet ds = conn.ReturnDataSet(strsql);
            DataTable dt = ds.Tables[0];
            chartControl1.Series.Clear();
            chartControl1.DataSource = dt;
            Series series1 = new Series("预估成本", DevExpress.XtraCharts.ViewType.Spline);
            series1.ArgumentDataMember = dt.Columns["日期"].ToString();
            series1.ValueDataMembersSerializable = dt.Columns["预估成本"].ToString();
            chartControl1.Series.Add(series1);

            ////标准成本
            Series series2 = new Series("标准成本", DevExpress.XtraCharts.ViewType.Spline);
            series2.ArgumentDataMember = dt.Columns["日期"].ToString();
            series2.ValueDataMembersSerializable = dt.Columns["标准成本"].ToString();
            chartControl1.Series.Add(series2);
            #endregion

            


        }
    }
}