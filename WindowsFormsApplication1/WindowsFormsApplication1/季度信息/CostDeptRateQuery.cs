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
    public partial class CostDeptRateQuery : DevExpress.XtraEditors.XtraForm
    {
        public CostDeptRateQuery()
        {
            InitializeComponent();
        }
        private static CostDeptRateQuery crqform = null;

        public static CostDeptRateQuery GetInstance()
        {
            if (crqform == null || crqform.IsDisposed)
            {
                crqform = new CostDeptRateQuery();
            }
            return crqform;
        }
        public static void RefreshEX()
        {
            if (crqform == null || crqform.IsDisposed)
            {

            }
            else
            {
                crqform.showDetail();
            }
        }
        public static void Delete()
        {
            string sql;
            bool isdone = true;
            ConnDB conn = new ConnDB();
            if (crqform == null || crqform.IsDisposed)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }
            else if (crqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除吗?", "删除", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < crqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "delete from cost_rate where cid = '" + crqform.gridView1.GetDataRow(crqform.gridView1.GetSelectedRows()[i]).ItemArray[0].ToString() + "'";
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
        public static void GetInfo(ref int id, ref string yyyy,ref int quarterid,ref int saletypeid,ref int deptid,ref decimal costrate)
        {
            if (crqform == null || crqform.IsDisposed)
            {
                MessageBox.Show("没有选中要修改的记录！");
            }
            else if (crqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要修改的记录！");
            }
            else
            {
                id =Convert.ToInt32(Common.IsNull(crqform.gridView1.GetDataRow(crqform.gridView1.GetSelectedRows()[0]).ItemArray[0].ToString()));
                yyyy = crqform.gridView1.GetDataRow(crqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString();
                quarterid = Convert.ToInt32(Common.IsNull(crqform.gridView1.GetDataRow(crqform.gridView1.GetSelectedRows()[0]).ItemArray[2].ToString()));
                saletypeid = Convert.ToInt32(Common.IsNull(crqform.gridView1.GetDataRow(crqform.gridView1.GetSelectedRows()[0]).ItemArray[4].ToString()));
                deptid = Convert.ToInt32(Common.IsNull(crqform.gridView1.GetDataRow(crqform.gridView1.GetSelectedRows()[0]).ItemArray[6].ToString()));
                costrate = Convert.ToDecimal(Common.IsNull(crqform.gridView1.GetDataRow(crqform.gridView1.GetSelectedRows()[0]).ItemArray[8].ToString()));
            }
        }
        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql,yyyy;
            yyyy = dateTimePicker1.Text.ToString();
            if (comboBoxQuarter.SelectedValue.ToString() == "0")
            {
                strsql = "select i.cid,i.yyyy 年,i.quarter_id 季度id,q.cname 季度,i.sale_type_id 营业类型id,j.cname 营业类型,d.cid 部门id,d.cname 部门,i.cost_rate 成本比率 from cost_rate i left join cost_saletype j on i.sale_type_id = j.cid left join cost_dept d on i.dept_id = d.cid left join cost_quarter q on i.quarter_id = q.cid where i.yyyy = '" + yyyy + "' and i.dept_id >0 order by i.yyyy,i.quarter_id";
            }
            else
            {
                strsql = "select i.cid,i.yyyy 年,i.quarter_id 季度id,q.cname 季度,i.sale_type_id 营业类型id,j.cname 营业类型,d.cid 部门id,d.cname 部门,i.cost_rate 成本比率 from cost_rate i left join cost_saletype j on i.sale_type_id = j.cid left join cost_dept d on i.dept_id = d.cid left join cost_quarter q on i.quarter_id = q.cid where i.yyyy = '" + yyyy + "' and i.quarter_id = " + comboBoxQuarter.SelectedValue.ToString() + " and i.dept_id >0 order by i.yyyy,i.quarter_id";
            }
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[2].Visible = false;
            gridView1.Columns[4].Visible = false;
            gridView1.Columns[6].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[4].OptionsColumn.ReadOnly = true;
            gridView1.Columns[5].OptionsColumn.ReadOnly = true;
            gridView1.Columns[6].OptionsColumn.ReadOnly = true;
            gridView1.Columns[7].OptionsColumn.ReadOnly = true;
            gridView1.Columns[8].OptionsColumn.ReadOnly = true;
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

        private void CostRateQuery_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            Common.BasicDataBind("cost_quarter", comboBoxQuarter);
            showDetail();
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePicker1.Value = startMonth;
        }

        private void comboBoxQuarter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxQuarter.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                showDetail(); ;
            }
        }
    }
}