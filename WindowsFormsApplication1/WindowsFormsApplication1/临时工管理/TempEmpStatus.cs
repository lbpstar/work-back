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
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

namespace SMTCost
{
    public partial class TempEmpStatus : DevExpress.XtraEditors.XtraForm
    {
        public TempEmpStatus()
        {
            InitializeComponent();
        }
        private static TempEmpStatus weqform = null;

        public static TempEmpStatus GetInstance()
        {
            if (weqform == null || weqform.IsDisposed)
            {
                weqform = new TempEmpStatus();
            }
            return weqform;
        }
        public static void RefreshEX()
        {
            if (weqform == null || weqform.IsDisposed)
            {

            }
            else
            {
                weqform.showDetail();
            }
        }
       
        private void showDetail()
        {
            gridView1.Columns.Clear();
            ConnDB conn = new ConnDB();
            string month,group;
            string table_name = "status";
            DataSet ds;
            month = dateTimePickerMonth.Text.ToString();
            group = comboBoxGroup.Text.ToString();


            bool success = true;
            IDataParameter[] parameters = new IDataParameter[] { new SqlParameter("@month", month), new SqlParameter("@group", group)};
            try
            {
                ds = conn.RunProcedure("COST_TEMP_EMPLOYEE_STATUS", parameters, table_name);
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
            }
            
            //表头设置
            //gridView1.ColumnPanelRowHeight = 35;
            //gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            //gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //表头及行内容居中显示
            //gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            conn.Close();
        }

        private void simpleButton查询_Click(object sender, EventArgs e)
        {
            showDetail();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //showDetail();
        }
        

        private void TempEmpQuery_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePickerMonth.Value = startMonth;
            comboBoxGroup.Text = "部门";
            showDetail();
        }
      
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gridView1.SelectRow(e.RowHandle);
        }

        private void simpleButtonExport_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string month, group;
            string table_name = "status";
            DataSet ds = new DataSet();
            month = dateTimePickerMonth.Text.ToString();
            group = comboBoxGroup.Text.ToString();
            IDataParameter[] parameters = new IDataParameter[] { new SqlParameter("@month", month), new SqlParameter("@group", group) };
            try
            {
                ds = conn.RunProcedure("COST_TEMP_EMPLOYEE_STATUS", parameters, table_name);
            }
            catch
            {
                MessageBox.Show("失败！");
            }

            bool isok = Common.DataSetToExcel(ds, true);
            if (isok)
            {
                MessageBox.Show("导出完成！");
            }
        }
    }
}