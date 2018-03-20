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

namespace SMTCost
{
    public partial class DeptMapQuery : DevExpress.XtraEditors.XtraForm
    {
        public DeptMapQuery()
        {
            InitializeComponent();
        }
        private static DeptMapQuery pqform = null;

        public static DeptMapQuery GetInstance()
        {
            if (pqform == null || pqform.IsDisposed)
            {
                pqform = new DeptMapQuery();
            }
            return pqform;
        }
        public static void RefreshEX()
        {
            if (pqform == null || pqform.IsDisposed)
            {

            }
            else
            {
                pqform.showDetail();
            }
        }
        public static void Delete()
        {
            string sql;
            bool isdone = true;
            ConnDB conn = new ConnDB();
            if (pqform == null || pqform.IsDisposed)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }
            else if (pqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除吗?", "部门对照表删除", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < pqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "delete from cost_dept_map where DEPT2 = '" + pqform.gridView1.GetDataRow(pqform.gridView1.GetSelectedRows()[i]).ItemArray[2].ToString() + "' and DEPT3 = '" + pqform.gridView1.GetDataRow(pqform.gridView1.GetSelectedRows()[i]).ItemArray[3].ToString() + "' and DEPT4 = '" + pqform.gridView1.GetDataRow(pqform.gridView1.GetSelectedRows()[i]).ItemArray[4].ToString() + "'";
                        isdone = conn.DeleteDatabase(sql);
                        if (!isdone)
                            break;
                    }
                    if (isdone)
                    {
                        MessageBox.Show("删除成功！");
                    }
                }
            }
            conn.Close();
        }
        public static void GetInfo(ref int deptid, ref string cname, ref string attendance,ref string attendance3rd, ref string attendance4th)
        {
            if (pqform == null || pqform.IsDisposed)
            {
                MessageBox.Show("没有选中要修改的记录！");
                cname = "";
            }
            else if (pqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要修改的记录！");
                cname = "";
            }
            else
            {
                deptid = int.Parse(Common.IsNull(pqform.gridView1.GetDataRow(pqform.gridView1.GetSelectedRows()[0]).ItemArray[0].ToString()));
                cname = pqform.gridView1.GetDataRow(pqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString();
                attendance = pqform.gridView1.GetDataRow(pqform.gridView1.GetSelectedRows()[0]).ItemArray[2].ToString();
                attendance3rd = pqform.gridView1.GetDataRow(pqform.gridView1.GetSelectedRows()[0]).ItemArray[3].ToString();
                attendance4th = pqform.gridView1.GetDataRow(pqform.gridView1.GetSelectedRows()[0]).ItemArray[4].ToString();
            }
        }
        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            strsql = "select j.cid,j.cname 部门,i.DEPT2 考勤二级部门,i.DEPT3 考勤三级部门,i.DEPT4 考勤四级部门 from cost_dept_map i right join cost_dept j on i.dept_id = j.cid where isnull(j.forbidden,'false') != 'true' order by j.cname";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[2].Visible = false;
            gridView1.Columns[3].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[4].OptionsColumn.ReadOnly = true;
            conn.Close();
        }
       
        private void simpleButton查询_Click(object sender, EventArgs e)
        {
            showDetail();
        }

        private void comboBoxSaleType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            showDetail();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {

        }

        private void DeptMapQuery_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            showDetail();
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql;
            strsql = " select i.dept4 四级部门 from (select distinct dept4 = case when len(replace(dept,'\','--'))-len(dept) < 4 then dept else left(dept,charindex('\',dept,charindex('\',dept,charindex('\',dept,CHARINDEX('\',dept,1)+1)+1)+1)-1) end from cost_dept_list) i left join COST_DEPT_MAP e on i.dept4 = e.DEPT4 where e.DEPT4 is null";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            
            //gridView1.;
            //gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            conn.Close();
        }
    }
}