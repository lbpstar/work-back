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
    public partial class SMTCostDeptDayPointChart : DevExpress.XtraEditors.XtraForm
    {
        public SMTCostDeptDayPointChart()
        {
            InitializeComponent();
        }
        private static SMTCostDeptDayPointChart smtcddcform = null;

        public static SMTCostDeptDayPointChart GetInstance()
        {
            if (smtcddcform == null || smtcddcform.IsDisposed)
            {
                smtcddcform = new SMTCostDeptDayPointChart();
            }
            return smtcddcform;
        }
       
        private void SMTCosting_Load(object sender, EventArgs e)
        {
            SetTitle();
            BindDept();
            BindChart();
            
            this.Height = ParentForm.Height-20;
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
            BindChart();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            BindChart();
        }
        private void BindChart()
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2,month;
            int deptid;
            deptid = (int)comboBoxDept.SelectedValue;
            List<Series> myseries = new List<Series>();
            month = dateTimePicker1.Text.ToString();
            if(deptid==0)
            {
                strsql = "select SALE_TYPE_NAME 营业类型,dept_name 部门,Cdate 日期,DIRECT_HOURS 直接人工小时数,cast(round(DIRECT_COST,2) as decimal(18,2)) 直接人工成本,INDIRECT_HOURS 间接人工小时数,cast(round(INDIRECT_COST,2) as decimal(18,2)) 间接人工成本,cast(round(POINTCOUNT,2) as decimal(18,2)) 点数,cast(round(COST,2) as decimal(18,2)) 预估成本,COST_POINT 预估单点成本,STANDARD_POINT 标准单点成本,cast(round(isnull(STANDARD_COST,0),2) as decimal(18,2)) 标准成本,cast(round(PROFIT,2) as decimal(18,2)) 盈亏 from COST_DEPT_CALCULATE where cdate like '" + month + "%' and sale_type_id =2";
                strsql2 = "select distinct dept_name 部门 from COST_DEPT_CALCULATE where cdate like '" + month + "%' and sale_type_id =2";
            }
            else
            {
                strsql = "select SALE_TYPE_NAME 营业类型,dept_name 部门,Cdate 日期,DIRECT_HOURS 直接人工小时数,cast(round(DIRECT_COST,2) as decimal(18,2)) 直接人工成本,INDIRECT_HOURS 间接人工小时数,cast(round(INDIRECT_COST,2) as decimal(18,2)) 间接人工成本,cast(round(POINTCOUNT,2) as decimal(18,2)) 点数,cast(round(COST,2) as decimal(18,2)) 预估成本,COST_POINT 预估单点成本,STANDARD_POINT 标准单点成本,cast(round(isnull(STANDARD_COST,0),2) as decimal(18,2)) 标准成本,cast(round(PROFIT,2) as decimal(18,2)) 盈亏 from COST_DEPT_CALCULATE where cdate like '" + month + "%' and sale_type_id =2 and dept_id =" + deptid;
                strsql2 = "select distinct dept_name 部门 from COST_DEPT_CALCULATE where cdate like '" + month + "%' and sale_type_id =2 and dept_id = " + deptid;
            }

            DataSet ds = conn.ReturnDataSet(strsql);
            DataTable dt = ds.Tables[0];
            DataSet ds2 = conn.ReturnDataSet(strsql2);

            XYDiagram dg = (XYDiagram)chartControlDeptDay.Diagram;
            if (dg != null)
            {
                dg.Panes.Clear();
            }

            chartControlDeptDay.Series.Clear();
            chartControlDeptDay.Annotations.Clear();
            chartControlDeptDay.DataSource = dt;



            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            {
                //预估成本
                myseries.Add(new Series(ds2.Tables[0].Rows[i][0].ToString() + "预估单点成本", DevExpress.XtraCharts.ViewType.Spline));
                myseries[i * 2].ArgumentDataMember = dt.Columns["日期"].ToString();
                myseries[i * 2].ValueDataMembersSerializable = dt.Columns["预估单点成本"].ToString();
                chartControlDeptDay.Series.Add(myseries[i * 2]);
                //标准成本
                myseries.Add(new Series(ds2.Tables[0].Rows[i][0].ToString() + "标准单点成本", DevExpress.XtraCharts.ViewType.Spline));
                myseries[i * 2 + 1].ArgumentDataMember = dt.Columns["日期"].ToString();
                myseries[i * 2 + 1].ValueDataMembersSerializable = dt.Columns["标准单点成本"].ToString();
                chartControlDeptDay.Series.Add(myseries[i * 2 + 1]);
                //设置过滤
                DataFilter df = new DataFilter("部门", "System.String", DataFilterCondition.Equal, ds2.Tables[0].Rows[i][0].ToString());
                //series1.DataFiltersConjunctionMode = ConjunctionTypes.Or;
                myseries[i * 2].DataFilters.Clear();
                myseries[i * 2].DataFilters.AddRange(new DataFilter[] { df });
                myseries[i * 2 + 1].DataFilters.Clear();
                myseries[i * 2 + 1].DataFilters.AddRange(new DataFilter[] { df });
                //设置pane
                XYDiagram diagram = (XYDiagram)chartControlDeptDay.Diagram;
                if (i > 0)
                {
                    //// Add secondary axes to the diagram, and adjust their options. 
                    //diagram.SecondaryAxesX.Add(new SecondaryAxisX(Convert.ToString(i - 1)));
                    //diagram.SecondaryAxesY.Add(new SecondaryAxisY(Convert.ToString(i - 1)));
                    //diagram.SecondaryAxesX[i - 1].Alignment = AxisAlignment.Near;
                    //diagram.SecondaryAxesY[i - 1].Alignment = AxisAlignment.Near;

                    //diagram.Panes[ds2.Tables[0].Rows[i][0].ToString()]
                    diagram.Panes.Add(new XYDiagramPane(ds2.Tables[0].Rows[i][0].ToString()));
                    SplineSeriesView myView1 = (SplineSeriesView)myseries[i * 2].View;
                    //myView1.AxisX = diagram.SecondaryAxesX[i - 1];
                    //myView1.AxisY = diagram.SecondaryAxesY[i - 1];
                    myView1.Pane = diagram.Panes[i - 1];
                    SplineSeriesView myView2 = (SplineSeriesView)myseries[i * 2 + 1].View;
                    //myView2.AxisX = diagram.SecondaryAxesX[i - 1];
                    //myView2.AxisY = diagram.SecondaryAxesY[i - 1];
                    myView2.Pane = diagram.Panes[i - 1];

                    chartControlDeptDay.Annotations.AddTextAnnotation(Convert.ToString(i));
                    XYDiagramPaneBase myPane = diagram.Panes[i - 1];
                    ((FreePosition)chartControlDeptDay.Annotations[i].ShapePosition).DockTarget = diagram.Panes[i - 1];
                    ((FreePosition)chartControlDeptDay.Annotations[i].ShapePosition).DockCorner = DockCorner.LeftTop;
                    TextAnnotation myTextAnnotation = (TextAnnotation)chartControlDeptDay.AnnotationRepository.GetElementByName(Convert.ToString(i));
                    //myTextAnnotation.Text = "<color=red>" + ds2.Tables[0].Rows[i][0].ToString() + " </color>";
                    myTextAnnotation.Text = ds2.Tables[0].Rows[i][0].ToString();
                    myTextAnnotation.ShapeKind = ShapeKind.Rectangle;
                    myTextAnnotation.ConnectorStyle = AnnotationConnectorStyle.None;
                }
                else
                {
                    chartControlDeptDay.Annotations.AddTextAnnotation("Annotation 0");
                    XYDiagramPaneBase myPane = diagram.DefaultPane;
                    ((FreePosition)chartControlDeptDay.Annotations[i].ShapePosition).DockTarget = myPane;
                    ((FreePosition)chartControlDeptDay.Annotations[i].ShapePosition).DockCorner = DockCorner.LeftTop;

                    TextAnnotation myTextAnnotation = (TextAnnotation)chartControlDeptDay.AnnotationRepository.GetElementByName("Annotation 0");
                    //myTextAnnotation.Text = "<color=red>" + ds2.Tables[0].Rows[i][0].ToString() + " </color>";
                    myTextAnnotation.Text = ds2.Tables[0].Rows[i][0].ToString();
                    myTextAnnotation.ShapeKind = ShapeKind.Rectangle;
                    myTextAnnotation.ConnectorStyle = AnnotationConnectorStyle.None;
                }
               ((SplineSeriesView)myseries[i * 2].View).MarkerVisibility = DefaultBoolean.True;
                ((SplineSeriesView)myseries[i * 2 + 1].View).MarkerVisibility = DefaultBoolean.True;
                ((SplineSeriesView)myseries[i * 2].View).Color = Color.Orange;
                ((SplineSeriesView)myseries[i * 2 + 1].View).Color = Color.OliveDrab;
                ((FreePosition)chartControlDeptDay.Annotations[i].ShapePosition).InnerIndents.All = 0;
                ((FreePosition)chartControlDeptDay.Annotations[i].ShapePosition).OuterIndents.Top = 10;
                diagram.PaneDistance = 10;
                diagram.PaneLayoutDirection = PaneLayoutDirection.Vertical;
                diagram.DefaultPane.SizeMode = PaneSizeMode.UseWeight;
                diagram.DefaultPane.Weight = 1.2;

            }        
            //外观
            chartControlDeptDay.Legend.Visibility = DefaultBoolean.True;
            chartControlDeptDay.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            chartControlDeptDay.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            chartControlDeptDay.Legend.Direction = LegendDirection.LeftToRight;

            conn.Close();

            

        }
        private void SetTitle()
        {
            chartControlDeptDay.Titles.Clear();
            ChartTitle titles = new ChartTitle();
            titles.Text =  "部门日成本-单点";
            titles.TextColor = System.Drawing.Color.Red;
            //titles.Indent = 1;                                 
            titles.Font = new Font("Tahoma", 18, FontStyle.Bold);
            titles.Dock = ChartTitleDockStyle.Top;
            titles.Indent = 0;
            titles.Alignment = StringAlignment.Center;


            // Place the titles where it's required. 
            titles.Dock = ChartTitleDockStyle.Top;



            chartControlDeptDay.Titles.Add(titles);
        }
        public void BindDept()
        {
            ConnDB conn = new ConnDB();
            string sql = "select d.cid,d.cname from  COST_DEPT d where d.saletype_id = 2";
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
        private void comboBoxDept_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxDept.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                BindChart();
            }
        }
    }
}