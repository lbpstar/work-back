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
    public partial class TempEmpAttSalarySum : DevExpress.XtraEditors.XtraForm
    {
        public TempEmpAttSalarySum()
        {
            InitializeComponent();
        }
        private static TempEmpAttSalarySum weqform = null;

        public static TempEmpAttSalarySum GetInstance()
        {
            if (weqform == null || weqform.IsDisposed)
            {
                weqform = new TempEmpAttSalarySum();
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
            ConnDB conn = new ConnDB();
            string     dept1,dept2,dept3,table_name,cmonth;
            table_name = "salary";
            DataSet ds;
            cmonth = dateTimePickerMonth.Text.ToString();
            dept1 = comboBoxDept1.SelectedValue.ToString();
            dept2 = comboBoxDept2.SelectedValue.ToString();
            dept3 = comboBoxDept3.SelectedValue.ToString();

            bool success = true;
            IDataParameter[] parameters = new IDataParameter[] { new SqlParameter("@cmonth", cmonth), new SqlParameter("@dept1", dept1), new SqlParameter("@dept2", dept2), new SqlParameter("@dept3",dept3) };
            try
            {
                ds=conn.RunProcedure("COST_TEMP_EMPLOYEE_SALARY_SUM", parameters,table_name);
                gridControl1.DataSource = ds.Tables[0].DefaultView;
            }
            catch
            {
                MessageBox.Show("失败！");
                success = false;
            }
            if(success)
            {
                for (int i = 0;i<=20;i++)
                {
                    gridView1.Columns[i].OptionsColumn.ReadOnly = true;
                    gridView1.Columns[i].OptionsColumn.FixedWidth = true;
                    gridView1.Columns[i].Width = 70;
                }   
            }

            //表头设置
            gridView1.ColumnPanelRowHeight = 35;
            gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //表头及行内容居中显示
            //gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            conn.Close();
        }

        private void simpleButton查询_Click(object sender, EventArgs e)
        {
            showDetail();
        }

        public void BindDept1()
        {
            ConnDB conn = new ConnDB();
            string sql = "select distinct dept1 from COST_DEPT_LIST";
            DataSet ds = conn.ReturnDataSet(sql);
            DataRow dr = ds.Tables[0].NewRow();
            //dr[0] = "0";
            //dr[1] = "请选择";
            ////插在第一位

            //ds.Tables[0].Rows.InsertAt(dr, 0);
            comboBoxDept1.DataSource = ds.Tables[0];
            comboBoxDept1.DisplayMember = "dept1";
            comboBoxDept1.ValueMember = "dept1";
            conn.Close();
        }
        public void BindDept2()
        {
            ConnDB conn = new ConnDB();
            string sql = "select distinct dept2 as id,dept2 as name from COST_DEPT_LIST where dept1 = '" + comboBoxDept1.SelectedValue + "'";
            DataSet ds = conn.ReturnDataSet(sql);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = "0";
            dr[1] = "请选择";
            //插在第一位

            ds.Tables[0].Rows.InsertAt(dr, 0);
            comboBoxDept2.DataSource = ds.Tables[0];
            comboBoxDept2.DisplayMember = "name";
            comboBoxDept2.ValueMember = "id";
            conn.Close();
        }
        public void BindDept3()
        {
            ConnDB conn = new ConnDB();
            string sql = "select distinct dept3 as id,dept3 as name from COST_DEPT_LIST where dept1 = '" + comboBoxDept1.SelectedValue + "' and dept2 = '" + comboBoxDept2.SelectedValue + "'";
            DataSet ds = conn.ReturnDataSet(sql);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = "0";
            dr[1] = "请选择";
            //插在第一位

            ds.Tables[0].Rows.InsertAt(dr, 0);
            comboBoxDept3.DataSource = ds.Tables[0];
            comboBoxDept3.DisplayMember = "name";
            comboBoxDept3.ValueMember = "id";
            conn.Close();
        }

        private void TempEmpQuery_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            BindDept1();
            BindDept2();
            
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePickerMonth.Value = startMonth;
            dateTimePickerMonth.Focus();
            SendKeys.Send("{RIGHT} ");
            //dateTimePickerBegin.Text = DateTime.Now.AddDays(-1).ToString();

            showDetail();
        }

        private void comboBoxDept1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxDept1.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                BindDept2();
            }
        }

        private void comboBoxDept2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxDept2.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                BindDept3();
            }
        }

        private void comboBoxDept3_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxDept3.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                showDetail();
            }
        }
        

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gridView1.SelectRow(e.RowHandle);
        }

        private void simpleButtonExport_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string dept1, dept2, dept3, table_name, cmonth;
            table_name = "salary";
            DataSet ds = new DataSet();
            cmonth = dateTimePickerMonth.Text.ToString();
            dept1 = comboBoxDept1.SelectedValue.ToString();
            dept2 = comboBoxDept2.SelectedValue.ToString();
            dept3 = comboBoxDept3.SelectedValue.ToString();


            IDataParameter[] parameters = new IDataParameter[] { new SqlParameter("@cmonth", cmonth), new SqlParameter("@dept1", dept1), new SqlParameter("@dept2", dept2), new SqlParameter("@dept3", dept3) };
            try
            {
                ds = conn.RunProcedure("COST_TEMP_EMPLOYEE_SALARY_SUM", parameters, table_name);
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