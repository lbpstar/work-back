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
    public partial class IndirectLabourPriceQuery : DevExpress.XtraEditors.XtraForm
    {
        public IndirectLabourPriceQuery()
        {
            InitializeComponent();
        }
        private static IndirectLabourPriceQuery ilpqform = null;

        public static IndirectLabourPriceQuery GetInstance()
        {
            if (ilpqform == null || ilpqform.IsDisposed)
            {
                ilpqform = new IndirectLabourPriceQuery();
            }
            return ilpqform;
        }
        public static void RefreshEX()
        {
            if (ilpqform == null || ilpqform.IsDisposed)
            {

            }
            else
            {
                ilpqform.ShowDetail();
            }
        }
        public static void Delete()
        {
            string sql;
            bool isdone= true;
            ConnDB conn = new ConnDB();
            if (ilpqform == null || ilpqform.IsDisposed)
            {
                MessageBox.Show("没有要删除的数据！");
            }
            else if (ilpqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有要删除的数据！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                string ms = "确定要删除查询出来的所有数据吗？";
                DialogResult dr = MessageBox.Show(ms, "间接人工费率删除", messButton);
                if (dr == DialogResult.OK)
                {
                    if(ilpqform.comboBoxDept.SelectedValue.ToString() =="0")
                    {
                        sql = "delete from cost_indirect_labour_price where YYYYMM = '" + ilpqform.dateTimePicker1.Text.ToString() + "'";
                    }
                    else if(ilpqform.comboBoxIndirectLabour.SelectedValue.ToString() == "0")
                    {
                        sql = "delete i from cost_indirect_labour_price i left join cost_direct_labour j on i.indirect_labour_id = j.cid where YYYYMM = '" + ilpqform.dateTimePicker1.Text.ToString() + "' and j.dept_id = " + ilpqform.comboBoxDept.SelectedValue.ToString();
                    }
                    else
                    {
                        sql = "delete i from cost_indirect_labour_price i where YYYYMM = '" + ilpqform.dateTimePicker1.Text.ToString() + "' and i.indirect_labour_id = " + ilpqform.comboBoxIndirectLabour.SelectedValue.ToString();

                    }
                    isdone = conn.DeleteDatabase(sql);
                    if (isdone)
                    {
                        MessageBox.Show("删除成功！");
                    }
                }
            }
            conn.Close();
        }

        public static void GetInfo(ref string month, ref string deptid, ref string indirectlabourid)
        {
            if (ilpqform == null || ilpqform.IsDisposed)
            {
                MessageBox.Show("没有要修改的记录！");
            }
            else if (ilpqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有要修改的记录！");
            }
            else
            {
                month = ilpqform.dateTimePicker1.Text.ToString();
                deptid = ilpqform.comboBoxDept.SelectedValue.ToString();
                indirectlabourid = ilpqform.comboBoxIndirectLabour.SelectedValue.ToString();
            }
        }
        public static int GetRowCount()
        {
            return ilpqform.gridView1.RowCount;
        }
        private void ShowDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql, yyyymm;
            yyyymm = dateTimePicker1.Text.ToString();
            if (ilpqform.comboBoxDept.SelectedValue.ToString() == "0")
            {
                strsql = "select i.indirect_labour_id 员工id,i.yyyymm 年月,i.deptname 部门,i.cno 工号,i.cname 姓名,j.CID,j.CNAME 上班类型,i.price '费率(元/小时)' from (select i.*,j.cno,j.cname,d.cid as deptid,d.cname as deptname from cost_indirect_labour_price i left join cost_direct_labour j on i.indirect_labour_id = j.cid left join cost_dept d on j.dept_id = d.cid where isnull(YYYYMM,'') ='" + yyyymm + "') i left join (select * from COST_WORK_TYPE where forbidden = 'false') j on i.WORK_TYPE_id = j.cid  ";

            }
            else if (ilpqform.comboBoxIndirectLabour.SelectedValue.ToString() == "0")
            {
                strsql = "select i.indirect_labour_id 员工id,i.yyyymm 年月,i.deptname 部门,i.cno 工号,i.cname 姓名,j.CID,j.CNAME 上班类型,i.price '费率(元/小时)' from (select i.*,j.cno,j.cname,d.cid as deptid,d.cname as deptname from cost_indirect_labour_price i left join cost_direct_labour j on i.indirect_labour_id = j.cid left join cost_dept d on j.dept_id = d.cid where isnull(YYYYMM,'') ='" + yyyymm + "' and j.dept_id = " + comboBoxDept.SelectedValue.ToString() + ") i left join (select * from COST_WORK_TYPE where forbidden = 'false') j on i.WORK_TYPE_id = j.cid  ";

            }
            else
            {
                strsql = "select i.indirect_labour_id 员工id,i.yyyymm 年月,i.deptname 部门,i.cno 工号,i.cname 姓名,j.CID,j.CNAME 上班类型,i.price '费率(元/小时)' from (select i.*,j.cno,j.cname,d.cid as deptid,d.cname as deptname from cost_indirect_labour_price i left join cost_direct_labour j on i.indirect_labour_id = j.cid left join cost_dept d on j.dept_id = d.cid where isnull(YYYYMM,'') ='" + yyyymm + "' and j.dept_id = " + comboBoxDept.SelectedValue.ToString() + " and indirect_labour_id = " + comboBoxIndirectLabour.SelectedValue.ToString() + ") i left join (select * from COST_WORK_TYPE where forbidden = 'false') j on i.WORK_TYPE_id = j.cid  ";

            }
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;

            gridView1.Columns[0].Visible = false;
            gridView1.Columns[5].Visible = false;

            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[4].OptionsColumn.ReadOnly = true;
            gridView1.Columns[5].OptionsColumn.ReadOnly = true;
            gridView1.Columns[6].OptionsColumn.ReadOnly = true;
            gridView1.Columns[7].OptionsColumn.ReadOnly = true;
            conn.Close();
        }

        private void BindIndirectLabour()
        {
            if (comboBoxDept.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                ConnDB conn = new ConnDB();
                string sql = "select CID,CNO + ' ' + CNAME as CNAME from cost_direct_labour where person_type_id = 4 and isnull(forbidden,'false') != 'true' and dept_id = " + comboBoxDept.SelectedValue.ToString();
                DataSet ds = conn.ReturnDataSet(sql);
                DataRow dr = ds.Tables[0].NewRow();
                dr[0] = "0";
                dr[1] = "请选择";
                //插在第一位
                ds.Tables[0].Rows.InsertAt(dr, 0);
                comboBoxIndirectLabour.DataSource = ds.Tables[0];
                comboBoxIndirectLabour.DisplayMember = "CNAME";
                comboBoxIndirectLabour.ValueMember = "CID";
                conn.Close();
            }

        }

        private void simpleButton查询_Click(object sender, EventArgs e)
        {
            ShowDetail();
        }

        private void DirectLabourPriceQuery_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            Common.BasicDataBind("cost_dept", comboBoxDept);
            BindIndirectLabour();
            ShowDetail();
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePicker1.Value = startMonth;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ShowDetail();
        }

        private void comboBoxDept_SelectedValueChanged(object sender, EventArgs e)
        {
            BindIndirectLabour();
            if (comboBoxDept.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                ShowDetail();
            }
        }

        private void comboBoxIndirectLabour_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxIndirectLabour.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                ShowDetail();
            }
        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            ShowDetail();
        }
    }
}