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

namespace SMTCost
{
    public partial class MatlQuery : DevExpress.XtraEditors.XtraForm
    {
        public delegate bool MethodCaller();
        public MatlQuery()
        {
            InitializeComponent();
        }
        private static MatlQuery ilaform = null;

        public static MatlQuery GetInstance()
        {
            if (ilaform == null || ilaform.IsDisposed)
            {
                ilaform = new MatlQuery();
            }
            return ilaform;
        }
        private void simpleButtonQuery_Click(object sender, EventArgs e)
        {
            showDetail();
        }
        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string matl, cmonth;
            matl = "matl";
            DataSet ds;
            cmonth = dateTimePickerMonth.Text.ToString();
            bool success = true;
            IDataParameter[] parameters = new IDataParameter[] { new SqlParameter("@cmonth", cmonth)};
            try
            {
                ds = conn.RunProcedure("COST_MATL_QUERY", parameters, matl);
                gridControl1.DataSource = ds.Tables[0].DefaultView;
            }
            catch
            {
                MessageBox.Show("失败！");
                success = false;
            }
            if (success)
            {
                gridView1.Columns[0].OptionsColumn.ReadOnly = true;
                gridView1.Columns[1].OptionsColumn.ReadOnly = true;
                gridView1.Columns[2].OptionsColumn.ReadOnly = true;
                gridView1.Columns[3].OptionsColumn.ReadOnly = true;
                gridView1.Columns[4].OptionsColumn.ReadOnly = true;
                gridView1.Columns[5].OptionsColumn.ReadOnly = true;
                gridView1.Columns[6].OptionsColumn.ReadOnly = true;
                gridView1.Columns[7].OptionsColumn.ReadOnly = true;
                gridView1.Columns[8].OptionsColumn.ReadOnly = true;
                gridView1.Columns[9].OptionsColumn.ReadOnly = true;
                gridView1.Columns[10].OptionsColumn.ReadOnly = true;
                gridView1.Columns[11].OptionsColumn.ReadOnly = true;
                gridView1.Columns[12].OptionsColumn.ReadOnly = true;
                gridView1.Columns[8].Visible = false;
                gridView1.Columns[10].Visible = false;
                gridView1.Columns[11].Visible = false;
                //gridView1.Columns[0].OptionsColumn.FixedWidth = true;
                //gridView1.Columns[0].Width = 40;
                //gridView1.Columns[1].OptionsColumn.FixedWidth = true;
                //gridView1.Columns[1].Width = 80;
                //gridView1.Columns[2].OptionsColumn.FixedWidth = true;
                //gridView1.Columns[2].Width = 50;
                //gridView1.Columns[3].OptionsColumn.FixedWidth = true;
                //gridView1.Columns[3].Width = 80;
                //gridView1.Columns[4].OptionsColumn.FixedWidth = true;
                //gridView1.Columns[4].Width = 80;
                //for (int i = 11; i <= 46; i++)
                //{
                //    gridView1.Columns[i].OptionsColumn.FixedWidth = true;
                //    gridView1.Columns[i].Width = 40;
                //}
            }

            ////表头设置
            //gridView1.ColumnPanelRowHeight = 35;
            //gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            //gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            ////表头及行内容居中显示
            ////gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            conn.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {    
            this.Close();
        }


        private void comboBoxSaleType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //showDetail();
        }
        
        private void dateTimePickerMonth_ValueChanged(object sender, EventArgs e)
        {
            showDetail();
        }

        private void AttendanceImport_Load(object sender, EventArgs e)
        {
            Common.BasicDataBind("cost_saletype", comboBoxSaleType);
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePickerMonth.Value = startMonth;
            showDetail();
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;

            //this.Activate();
            dateTimePickerMonth.Focus();
            SendKeys.Send("{RIGHT} ");
        }

        private void comboBoxSaleType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public bool DataSetToExcel(DataSet dataSet, bool isShowExcle)
        {
            DataTable dataTable = dataSet.Tables[0];
            int rowNumber = dataTable.Rows.Count;

            int rowIndex = 1;
            int colIndex = 0;


            if (rowNumber == 0)
            {
                return false;
            }

            //建立Excel对象
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(true);
            excel.Visible = isShowExcle;

            //生成字段名称
            foreach (DataColumn col in dataTable.Columns)
            {
                colIndex++;
                excel.Cells[1, colIndex] = col.ColumnName;
            }

            //填充数据
            foreach (DataRow row in dataTable.Rows)
            {
                rowIndex++;
                colIndex = 0;
                foreach (DataColumn col in dataTable.Columns)
                {
                    colIndex++;
                    excel.Cells[rowIndex, colIndex] = row[col.ColumnName];
                }
            }

            return true;
        }

        private void simpleButton导出到excel_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string matl, cmonth;
            matl = "matl";
            DataSet ds;
            cmonth = dateTimePickerMonth.Text.ToString();
            IDataParameter[] parameters = new IDataParameter[] { new SqlParameter("@cmonth", cmonth) };
            try
            {
                ds = conn.RunProcedure("COST_MATL_QUERY", parameters, matl);
                bool isok = DataSetToExcel(ds, true);
                if (isok)
                {
                    MessageBox.Show("导出完成！");
                }
            }
            catch
            {
                MessageBox.Show("失败！");
            }
            
        }
    }
}