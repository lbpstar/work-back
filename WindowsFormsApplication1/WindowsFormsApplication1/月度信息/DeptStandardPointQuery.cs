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
    public partial class DeptStandardPointQuery : DevExpress.XtraEditors.XtraForm
    {
        public DeptStandardPointQuery()
        {
            InitializeComponent();
        }
        private static DeptStandardPointQuery dspqform = null;

        public static DeptStandardPointQuery GetInstance()
        {
            if (dspqform == null || dspqform.IsDisposed)
            {
                dspqform = new DeptStandardPointQuery();
            }
            return dspqform;
        }
        public static void RefreshEX()
        {
            if (dspqform == null || dspqform.IsDisposed)
            {

            }
            else
            {
                dspqform.showDetail();
            }
        }
        public static void Delete()
        {
            string sql;
            bool isdone = true;
            ConnDB conn = new ConnDB();
            if (dspqform == null || dspqform.IsDisposed)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }
            else if (dspqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除吗?", "删除", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < dspqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "delete from cost_dept_standard_point where cid = '" + dspqform.gridView1.GetDataRow(dspqform.gridView1.GetSelectedRows()[i]).ItemArray[0].ToString() + "'";
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
        public static void GetInfo(ref int id, ref string yyyymm,ref int saletypeid,ref int deptid,ref decimal deptstandardpoint)
        {
            if (dspqform == null || dspqform.IsDisposed)
            {
                MessageBox.Show("没有选中要修改的记录！");
            }
            else if (dspqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要修改的记录！");
            }
            else
            {
                id =Convert.ToInt32(Common.IsNull(dspqform.gridView1.GetDataRow(dspqform.gridView1.GetSelectedRows()[0]).ItemArray[0].ToString()));
                yyyymm = dspqform.gridView1.GetDataRow(dspqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString();
                saletypeid = Convert.ToInt32(Common.IsNull(dspqform.gridView1.GetDataRow(dspqform.gridView1.GetSelectedRows()[0]).ItemArray[2].ToString()));
                deptid = Convert.ToInt32(Common.IsNull(dspqform.gridView1.GetDataRow(dspqform.gridView1.GetSelectedRows()[0]).ItemArray[4].ToString()));
                deptstandardpoint = Convert.ToDecimal(Common.IsNull(dspqform.gridView1.GetDataRow(dspqform.gridView1.GetSelectedRows()[0]).ItemArray[6].ToString()));
            }
        }
        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql,yyyymm;
            yyyymm = dateTimePicker1.Text.ToString(); 
            strsql = "select i.cid,i.yyyymm 年月,i.sale_type_id 营业类型id,j.cname 营业类型,i.dept_id 部门id,d.cname 部门,i.dept_standard_point '部门标准单点成本(元)' from cost_dept_standard_point i left join cost_saletype j on i.sale_type_id = j.cid left join cost_dept d on i.dept_id = d.cid where i.yyyymm = '" + yyyymm + "'";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[2].Visible = false;
            gridView1.Columns[4].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[4].OptionsColumn.ReadOnly = true;
            gridView1.Columns[5].OptionsColumn.ReadOnly = true;
            gridView1.Columns[6].OptionsColumn.ReadOnly = true;
            conn.Close();
        }
      
        
        private void simpleButton查询_Click(object sender, EventArgs e)
        {
            showDetail();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            showDetail();
        }

        private void StandardPointQuery_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            showDetail();
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