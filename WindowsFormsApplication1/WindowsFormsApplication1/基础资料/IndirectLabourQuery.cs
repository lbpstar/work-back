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
    public partial class IndirectLabourQuery : DevExpress.XtraEditors.XtraForm
    {
        public IndirectLabourQuery()
        {
            InitializeComponent();
        }
        private static IndirectLabourQuery ilqform = null;

        public static IndirectLabourQuery GetInstance()
        {
            if (ilqform == null || ilqform.IsDisposed)
            {
                ilqform = new IndirectLabourQuery();
            }
            return ilqform;
        }
        public static void RefreshEX()
        {
            if (ilqform == null || ilqform.IsDisposed)
            {

            }
            else
            {
                ilqform.showDetail();
            }
        }
        public static void Delete()
        {
            string sql;
            bool isdone = true;
            ConnDB conn = new ConnDB();
            if (ilqform == null || ilqform.IsDisposed)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }
            else if (ilqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除吗?", "间接人工删除", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < ilqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "delete from cost_direct_labour where cid = '" + ilqform.gridView1.GetDataRow(ilqform.gridView1.GetSelectedRows()[i]).ItemArray[5].ToString() + "'";
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
        public static void GetInfo(ref int id, ref string cno,ref string cname, ref int positionid, ref string positionname, ref int person_level, ref int deptid,ref string deptname)
        {
            if (ilqform == null || ilqform.IsDisposed)
            {
                MessageBox.Show("没有选中要修改的记录！");
                cno = "";
            }
            else if (ilqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要修改的记录！");
                cno = "";
            }
            else
            {
                cno = ilqform.gridView1.GetDataRow(ilqform.gridView1.GetSelectedRows()[0]).ItemArray[0].ToString();
                cname = ilqform.gridView1.GetDataRow(ilqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString();
                positionname = ilqform.gridView1.GetDataRow(ilqform.gridView1.GetSelectedRows()[0]).ItemArray[2].ToString();
                person_level = int.Parse(Common.IsNull(ilqform.gridView1.GetDataRow(ilqform.gridView1.GetSelectedRows()[0]).ItemArray[3].ToString()));
                deptname = ilqform.gridView1.GetDataRow(ilqform.gridView1.GetSelectedRows()[0]).ItemArray[4].ToString();
                id = int.Parse(Common.IsNull(ilqform.gridView1.GetDataRow(ilqform.gridView1.GetSelectedRows()[0]).ItemArray[5].ToString()));
                positionid = int.Parse(Common.IsNull(ilqform.gridView1.GetDataRow(ilqform.gridView1.GetSelectedRows()[0]).ItemArray[6].ToString()));
                deptid = int.Parse(Common.IsNull(ilqform.gridView1.GetDataRow(ilqform.gridView1.GetSelectedRows()[0]).ItemArray[7].ToString()));

            }
        }
        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            int deptid;
            deptid = (int)comboBoxDept.SelectedValue; 
            if (deptid > 0)
            {
                strsql = "select i.cno 工号,i.cname 姓名,j.cname 职位,i.PERSON_LEVEL 员工等级,e.cname 部门,i.cid,j.cid,e.cid,i.forbidden 禁用 from cost_direct_labour i left join cost_position j on i.position_id = j.cid left join cost_dept e on i.dept_id = e.cid where i.dept_id = " + Common.IsZero(deptid.ToString()) + " and i.person_type_id = 4 order by i.cname";
            }
            else
            {
                strsql = "select i.cno 工号,i.cname 姓名,j.cname 职位,i.PERSON_LEVEL 员工等级,e.cname 部门,i.cid,j.cid,e.cid,i.forbidden 禁用 from cost_direct_labour i left join cost_position j on i.position_id = j.cid left join cost_dept e on i.dept_id = e.cid where i.person_type_id = 4 order by i.cname";
            }

            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[5].Visible = false;
            gridView1.Columns[6].Visible = false;
            gridView1.Columns[7].Visible = false;
            gridView1.Columns[2].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[4].OptionsColumn.ReadOnly = true;
            gridView1.Columns[5].OptionsColumn.ReadOnly = true;
            gridView1.Columns[6].OptionsColumn.ReadOnly = true;
            gridView1.Columns[7].OptionsColumn.ReadOnly = true;
            gridView1.Columns[8].OptionsColumn.ReadOnly = true;
            IsForbidden();
            conn.Close();
        }
        private void IsForbidden()
        {
            if (ilqform == null || ilqform.IsDisposed)
            {

            }
            else if (ilqform.gridView1.SelectedRowsCount == 0)
            {

            }

            else
            {
                if (ilqform.gridView1.GetDataRow(ilqform.gridView1.GetSelectedRows()[0]).ItemArray[8].ToString() == "True")
                {
                    IndirectLabour.ForbiddenDisable();
                    IndirectLabour.UnforbiddenEnable();
                }
                else
                {
                    IndirectLabour.ForbiddenEnable();
                    IndirectLabour.UnforbiddenDisable();
                }
            }
        }
        public static void cEnable()
        {
            string sql;
            bool isdone = true;
            ConnDB conn = new ConnDB();
            if (ilqform == null || ilqform.IsDisposed)
            {
                MessageBox.Show("没有选中要反禁用的记录！");
            }
            else if (ilqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要反禁用的记录！");

            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要反禁用吗?", "间接人工反禁用", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < ilqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "update i set i.forbidden = 'false' from COST_DIRECT_LABOUR i where cid = '" + ilqform.gridView1.GetDataRow(ilqform.gridView1.GetSelectedRows()[i]).ItemArray[5].ToString() + "'";
                        isdone = conn.EditDatabase(sql);
                        if (!isdone)
                            break;
                    }
                    if (isdone)
                    {
                        IndirectLabour.ForbiddenEnable();
                        IndirectLabour.UnforbiddenDisable();
                        MessageBox.Show("反禁用成功！");
                    }
                }
            }
            conn.Close();
        }
        public static void cDisable()
        {
            string sql;
            bool isdone = true;
            ConnDB conn = new ConnDB();
            if (ilqform == null || ilqform.IsDisposed)
            {
                MessageBox.Show("没有选中要禁用的记录！");
            }
            else if (ilqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要禁用的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要禁用吗?", "间接人工禁用", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < ilqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "update i set i.forbidden = 'true' from COST_DIRECT_LABOUR i where cid = '" + ilqform.gridView1.GetDataRow(ilqform.gridView1.GetSelectedRows()[i]).ItemArray[5].ToString() + "'";
                        isdone = conn.EditDatabase(sql);
                        if (!isdone)
                            break;
                    }
                    if (isdone)
                    {
                        IndirectLabour.ForbiddenDisable();
                        IndirectLabour.UnforbiddenEnable();
                        MessageBox.Show("禁用成功！");
                    }
                }
            }
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

        private void IndirectLabourQuery_Load(object sender, EventArgs e)
        {
            Common.BasicDataBind("cost_dept", comboBoxDept);
            //this.WindowState = FormWindowState.Maximized;
            showDetail();
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            IsForbidden();
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            IsForbidden();
        }
    }
}