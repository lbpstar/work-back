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
using DevExpress.XtraCharts;
using DevExpress.Utils;

namespace SMTCost
{
    public partial class ProductCostDayChart : DevExpress.XtraEditors.XtraForm
    {
        public ProductCostDayChart()
        {
            InitializeComponent();
        }
        private static ProductCostDayChart smtcdcform = null;

        public static ProductCostDayChart GetInstance()
        {
            if (smtcdcform == null || smtcdcform.IsDisposed)
            {
                smtcdcform = new ProductCostDayChart();
            }
            return smtcdcform;
        }


        private void simpleButton查询_Click(object sender, EventArgs e)
        {
            BindChart();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            BindChart();
        }
        private void BindChart()
        {
            ConnDB conn = new ConnDB();
            string strsql, month;
            month = dateTimePicker1.Text.ToString();
            strsql = "select SALE_TYPE_NAME 营业类型,CDATE 日期,DIRECT_HOURS 直接人工小时数,cast(round(DIRECT_COST, 2) as decimal(18, 2)) 直接人工成本,TEMP_HOURS 临时工小时数,cast(round(TEMP_COST, 2) as decimal(18, 2)) 临时工成本,INDIRECT_HOURS 间接人工小时数,cast(round(INDIRECT_COST, 2) as decimal(18, 2)) 间接人工成本,cast(round(DEPRECIATION, 2) as decimal(18, 2)) 折旧费,cast(round(OPERATION_TRANSFER, 2) as decimal(18, 2)) 运营部费用转嫁,cast(round(TRIAL_TRANSFER, 2) as decimal(18, 2)) 试产费用转嫁,cast(round(COMPOSITE_EXPENSE, 2) as decimal(18, 2)) 主营综合费用,cast(round(POINTCOUNT, 2) as decimal(18, 2)) 产出台数,cast(round(COST, 2) as decimal(18, 2)) 预估成本,COST_POINT 预估单台成本,STANDARD_POINT 标准单台成本,cast(round(STANDARD_COST, 2) as decimal(18, 2)) 标准成本,cast(round(PROFIT, 2) as decimal(18, 2)) 盈亏 from COST_DAY_CALCULATE where cdate like '" + month + "%' and sale_type_id =14";
            DataSet ds = conn.ReturnDataSet(strsql);
            DataTable dt = ds.Tables[0];
            chartControlDay.Series.Clear();
            chartControlDay.DataSource = dt;
            //预估成本
            Series series1 = new Series("预估成本", DevExpress.XtraCharts.ViewType.Spline);
            series1.ArgumentDataMember = dt.Columns["日期"].ToString();
            series1.ValueDataMembersSerializable = dt.Columns["预估成本"].ToString();
            chartControlDay.Series.Add(series1);

            //标准成本
            Series series2 = new Series("标准成本", DevExpress.XtraCharts.ViewType.Spline);
            series2.ArgumentDataMember = dt.Columns["日期"].ToString();
            series2.ValueDataMembersSerializable = dt.Columns["标准成本"].ToString();
            chartControlDay.Series.Add(series2);

            //外观
            ((SplineSeriesView)series1.View).MarkerVisibility = DefaultBoolean.True;
            chartControlDay.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            chartControlDay.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            chartControlDay.Legend.Direction = LegendDirection.LeftToRight;
            ((SplineSeriesView)series1.View).Color = Color.Orange;
            ((SplineSeriesView)series2.View).Color = Color.OliveDrab;
            //((XYDiagram)chartControlDay.Diagram).SecondaryAxesY.Clear();
            //SecondaryAxisY myAxis = new SecondaryAxisY(series2.Name);
            //((XYDiagram)chartControlDay.Diagram).SecondaryAxesY.Add(myAxis);
            //((BarSeriesView)series2.View).AxisY = myAxis;

            conn.Close();

            

        }
        private void SetTitle()
        {
            chartControlDay.Titles.Clear();
            ChartTitle titles = new ChartTitle();
            titles.Text =  "预估单点成本";
            titles.TextColor = System.Drawing.Color.Red;
            //titles.Indent = 1;                                 
            titles.Font = new Font("Tahoma", 18, FontStyle.Bold);
            titles.Dock = ChartTitleDockStyle.Top;
            titles.Indent = 0;
            titles.Alignment = StringAlignment.Center;
            chartControlDay.Titles.Add(titles);
        }

        private void ProductCostDayChart_Load(object sender, EventArgs e)
        {
            BindChart();
            //SetTitle();
            this.Height = ParentForm.Height - 20;
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