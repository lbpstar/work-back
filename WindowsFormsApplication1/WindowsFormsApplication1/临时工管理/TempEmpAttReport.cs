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
    public partial class TempEmpAttReport : DevExpress.XtraEditors.XtraForm
    {
        public TempEmpAttReport()
        {
            InitializeComponent();
        }
        private static TempEmpAttReport weqform = null;

        public static TempEmpAttReport GetInstance()
        {
            if (weqform == null || weqform.IsDisposed)
            {
                weqform = new TempEmpAttReport();
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
            string cno,dept1,dept2,dept3,dept,status,days,cmonth;
            days = "days";
            //DateTime begin_date, end_date;
            DataSet ds;
            status = comboBoxStatus.SelectedIndex.ToString();
            cno = textEditNo.Text.ToString().Trim();
            cmonth = dateTimePickerMonth.Text.ToString();
            //begin_date = Convert.ToDateTime(dateTimePickerBegin.Text.ToString());
            //end_date = Convert.ToDateTime(dateTimePickerEnd.Text.ToString());
            dept1 = comboBoxDept1.SelectedValue.ToString();
            dept2 = comboBoxDept2.SelectedValue.ToString();
            dept3 = comboBoxDept3.SelectedValue.ToString();
            dept = comboBoxDept.SelectedValue.ToString();

            bool success = true;
            //IDataParameter[] parameters = new IDataParameter[] { new SqlParameter("@begin", begin_date), new SqlParameter("@end", end_date), new SqlParameter("@cno", cno), new SqlParameter("@dept1", dept1), new SqlParameter("@dept2", dept2), new SqlParameter("@dept3",dept3), new SqlParameter("@dept", dept), new SqlParameter("@status", status) };
            IDataParameter[] parameters = new IDataParameter[] { new SqlParameter("@month", cmonth), new SqlParameter("@cno", cno), new SqlParameter("@dept1", dept1), new SqlParameter("@dept2", dept2), new SqlParameter("@dept3", dept3), new SqlParameter("@dept", dept) };
            try
            {
                ds=conn.RunProcedure("COST_TEMP_EMPLOYEE_DAYS", parameters,days);
                gridControl1.DataSource = ds.Tables[0].DefaultView;
            }
            catch
            {
                MessageBox.Show("失败！");
                success = false;
            }
            if(success)
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

                gridView1.Columns[0].OptionsColumn.FixedWidth = true;
                gridView1.Columns[0].Width = 40;
                gridView1.Columns[1].OptionsColumn.FixedWidth = true;
                gridView1.Columns[1].Width = 80;
                gridView1.Columns[2].OptionsColumn.FixedWidth = true;
                gridView1.Columns[2].Width = 50;
                gridView1.Columns[3].OptionsColumn.FixedWidth = true;
                gridView1.Columns[3].Width = 80;
                gridView1.Columns[4].OptionsColumn.FixedWidth = true;
                gridView1.Columns[4].Width = 80;
                for(int i =11;i<=46;i++)
                {
                    gridView1.Columns[i].OptionsColumn.FixedWidth = true;
                    gridView1.Columns[i].Width = 40;
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //showDetail();
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
        public void BindDept()
        {
            ConnDB conn = new ConnDB();
            string sql = "select distinct dept as id,dept as name from COST_DEPT_LIST where dept1 = '" + comboBoxDept1.SelectedValue + "' and dept2 = '" + comboBoxDept2.SelectedValue + "' and dept3 = '" + comboBoxDept3.SelectedValue + "'";
            DataSet ds = conn.ReturnDataSet(sql);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = "0";
            dr[1] = "请选择";
            //插在第一位

            ds.Tables[0].Rows.InsertAt(dr, 0);
            comboBoxDept.DataSource = ds.Tables[0];
            comboBoxDept.DisplayMember = "name";
            comboBoxDept.ValueMember = "id";
            conn.Close();
        }

        private void TempEmpQuery_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            BindDept1();
            BindDept2();
            //BindDept3();
            //BindDept();
            
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            //DateTime dt = DateTime.Now;
            //DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            //this.dateTimePickerBegin.Value = startMonth;
            //dateTimePickerBegin.Focus();
            //SendKeys.Send("{RIGHT} ");
            //绑定状态
            Dictionary<int, string> kvDictonary = new Dictionary<int, string>();
            kvDictonary.Add(0, "未提报");
            kvDictonary.Add(1, "已提报");
            kvDictonary.Add(2, "考勤员已审核");
            kvDictonary.Add(3, "主管已审批");
            kvDictonary.Add(4, "全部");

            BindingSource bs = new BindingSource();
            bs.DataSource = kvDictonary;
            comboBoxStatus.DataSource = bs;
            comboBoxStatus.ValueMember = "Key";
            comboBoxStatus.DisplayMember = "Value";
            comboBoxStatus.SelectedIndex = 4;

            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePickerMonth.Value = startMonth;


            dateTimePickerMonth.Focus();
            SendKeys.Send("{RIGHT} ");

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
                BindDept();
            }
        }
        

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gridView1.SelectRow(e.RowHandle);
        }

        private void simpleButtonExport_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string cno, dept1, dept2, dept3, dept, status, days,cmonth;
            days = "days";
            //DateTime begin_date, end_date;
            DataSet ds = new DataSet();
            status = comboBoxStatus.SelectedIndex.ToString();
            cno = textEditNo.Text.ToString().Trim();
            cmonth = dateTimePickerMonth.Text.ToString();
            //begin_date = Convert.ToDateTime(dateTimePickerBegin.Text.ToString());
            //end_date = Convert.ToDateTime(dateTimePickerEnd.Text.ToString());
            dept1 = comboBoxDept1.SelectedValue.ToString();
            dept2 = comboBoxDept2.SelectedValue.ToString();
            dept3 = comboBoxDept3.SelectedValue.ToString();
            dept = comboBoxDept.SelectedValue.ToString();

            IDataParameter[] parameters = new IDataParameter[] { new SqlParameter("@month", cmonth), new SqlParameter("@cno", cno), new SqlParameter("@dept1", dept1), new SqlParameter("@dept2", dept2), new SqlParameter("@dept3", dept3), new SqlParameter("@dept", dept)};
            try
            {
                ds = conn.RunProcedure("COST_TEMP_EMPLOYEE_DAYS", parameters, days);
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

        private void dateTimePickerMonth_ValueChanged(object sender, EventArgs e)
        {
            showDetail();
        }
    }
}