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
    public partial class AttendanceImport : DevExpress.XtraEditors.XtraForm
    {
        public delegate bool MethodCaller();
        public AttendanceImport()
        {
            InitializeComponent();
        }
        private static AttendanceImport ilaform = null;

        public static AttendanceImport GetInstance()
        {
            if (ilaform == null || ilaform.IsDisposed)
            {
                ilaform = new AttendanceImport();
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
            string strsql;
            if (comboBoxDept.SelectedValue.ToString() == "0")
            {
                strsql = "select cdate 日期,emp_no 工号,emp_name 姓名,wkname 星期,gz_int 正常上班,jb_ps_int 平时加班,jb_xx_int 休息日加班,jb_jr_int 节假日加班,t.dept4 部门,d.cname 本系统部门,rank 员工组,PERSON_TYPE 员工类型 from COST_DIRECT_LABOUR_ATTENDANCE t  left join cost_dept_map m on  T.DEPT4 = M.DEPT4 left join cost_dept d on m.dept_id = d.cid where t.cdate like '" + dateTimePickerMonth.Text + "%'  order by cdate";
            }
            else
            {
                strsql = "select cdate 日期,emp_no 工号,emp_name 姓名,wkname 星期,gz_int 正常上班,jb_ps_int 平时加班,jb_xx_int 休息日加班,jb_jr_int 节假日加班,t.dept4 部门,d.cname 本系统部门,rank 员工组,PERSON_TYPE 员工类型 from COST_DIRECT_LABOUR_ATTENDANCE t  left join cost_dept_map m on  T.DEPT4 = M.DEPT4 left join cost_dept d on m.dept_id = d.cid where t.cdate like '" + dateTimePickerMonth.Text + "%'  and d.cid = '" + comboBoxDept.SelectedValue.ToString() + "' order by cdate";
            }
              DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            //gridView1.Columns[0].Visible = false;
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
                conn.Close();
        }
        private bool Import()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            string month = dateTimePickerMonth.Text;
            int rows, i;
            bool success = true;
            strsql = "select * from COST_DIRECT_LABOUR_ATTENDANCE where cdate like '%" + dateTimePickerMonth.Text + "%'";
            IDataParameter[] parameters = new IDataParameter[] { new SqlParameter("@cmonth", month) };

            rows = conn.ReturnRecordCount(strsql);
            if (rows > 0)
            {
                MessageBox.Show("该月考勤数据已经存在，要重新导入，请先清空该月数据！");
            }
            else
            {
                try
                {
                    conn.RunProcedure("COST_ATTENDANCE_IMPORT", parameters, out i);
                }
                catch
                {
                    MessageBox.Show("失败！");
                    success = false;
                }
                if (success)
                {
                    MessageBox.Show("导入成功！");
                    //showDetail();
                }
            }
            conn.Close();
            return success;
        }
        private void Clear()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            strsql = "delete from COST_DIRECT_LABOUR_ATTENDANCE where cdate like '%" + dateTimePickerMonth.Text + "%'";

            bool isok = conn.EditDatabase(strsql);
            if (isok)
            {
                MessageBox.Show("该月考勤已成功清空！");
                showDetail();
            }
            else
            {
                MessageBox.Show("失败！");
            }
            
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
        private void simpleButtonImport_Click(object sender, EventArgs e)
        {
            //Import();
            simpleButtonImport.Enabled = false;
            simpleButtonClear.Enabled = false;
            MethodCaller mc = new MethodCaller(Import);
            IAsyncResult result = mc.BeginInvoke(AsyncShowDetail, mc);
        }
        private void AsyncShowDetail(IAsyncResult result)
        {
            MethodCaller aysnDelegate = result.AsyncState as MethodCaller;
            if (aysnDelegate != null)
            {
                bool success = aysnDelegate.EndInvoke(result);
                if (success)
                {
                    ConnDB conn = new ConnDB();
                    string strsql;
                    strsql = "select cdate 日期,emp_no 工号,emp_name 姓名,wkname 星期,gz_int 正常上班,jb_ps_int 平时加班,jb_xx_int 休息日加班,jb_jr_int 节假日加班,t.dept4 部门,d.cname 本系统部门,rank 员工组,PERSON_TYPE 员工类型 from COST_DIRECT_LABOUR_ATTENDANCE t  left join cost_dept_map m on  T.DEPT4 = M.DEPT4 left join cost_dept d on m.dept_id = d.cid where t.cdate like '" + dateTimePickerMonth.Text + "%'  order by cdate";
                    DataSet ds = conn.ReturnDataSet(strsql);

                    Action<DataSet> action = (data) =>
                    {
                        gridControl1.DataSource = data.Tables[0].DefaultView;
                        //gridView1.Columns[0].Visible = false;
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
                        simpleButtonImport.Enabled = true;
                        simpleButtonClear.Enabled = true;
                    };
                    Invoke(action, ds);
                    conn.Close();
                }
            }
        }
        private void simpleButtonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void dateTimePickerMonth_ValueChanged(object sender, EventArgs e)
        {
            showDetail();
        }

        private void AttendanceImport_Load(object sender, EventArgs e)
        {
            Common.BasicDataBind("cost_saletype", comboBoxSaleType);
            BindDept();
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
        public void BindDept()
        {
            ConnDB conn = new ConnDB();
            string sql = "select * from  cost_dept where isnull(forbidden,'false') != 'true' and saletype_id = " + Common.IsZero(comboBoxSaleType.SelectedValue.ToString());
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

        private void comboBoxSaleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSaleType.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                BindDept();
            }
        }

        private void comboBoxDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDept.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                showDetail();
            }
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
            string strsql;
            if (comboBoxDept.SelectedValue.ToString() == "0")
            {
                strsql = "select cdate 日期,emp_no 工号,emp_name 姓名,wkname 星期,gz_int 正常上班,jb_ps_int 平时加班,jb_xx_int 休息日加班,jb_jr_int 节假日加班,t.dept4 部门,d.cname 本系统部门,rank 员工组,PERSON_TYPE 员工类型 from COST_DIRECT_LABOUR_ATTENDANCE t  left join cost_dept_map m on  T.DEPT4 = M.DEPT4 left join cost_dept d on m.dept_id = d.cid where t.cdate like '" + dateTimePickerMonth.Text + "%'  order by cdate";
            }
            else
            {
                strsql = "select cdate 日期,emp_no 工号,emp_name 姓名,wkname 星期,gz_int 正常上班,jb_ps_int 平时加班,jb_xx_int 休息日加班,jb_jr_int 节假日加班,t.dept4 部门,d.cname 本系统部门,rank 员工组,PERSON_TYPE 员工类型 from COST_DIRECT_LABOUR_ATTENDANCE t  left join cost_dept_map m on  T.DEPT4 = M.DEPT4 left join cost_dept d on m.dept_id = d.cid where t.cdate like '" + dateTimePickerMonth.Text + "%'  and d.cid = '" + comboBoxDept.SelectedValue.ToString() + "' order by cdate";
            }
            DataSet ds = conn.ReturnDataSet(strsql);
            bool isok = DataSetToExcel(ds,true);
            if(isok)
            {
                MessageBox.Show("导出完成！");
            }
        }
    }
}