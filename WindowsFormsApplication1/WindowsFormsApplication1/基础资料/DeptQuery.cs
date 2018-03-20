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
    public partial class DeptQuery : DevExpress.XtraEditors.XtraForm
    {
        public DeptQuery()
        {
            InitializeComponent();
        }
        private static DeptQuery dqform = null;

        public static DeptQuery GetInstance()
        {
            if (dqform == null || dqform.IsDisposed)
            {
                dqform = new DeptQuery();
            }
            return dqform;
        }
        public static void RefreshEX()
        {
            if (dqform == null || dqform.IsDisposed)
            {

            }
            else
            {
                dqform.showDetail();
            }
        }
        public static void Delete()
        {
            string sql;
            bool isdone = true;
            ConnDB conn = new ConnDB();
            if (dqform == null || dqform.IsDisposed)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }
            else if (dqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除吗?", "部门删除", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < dqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "delete from cost_dept where cid = '" + dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[i]).ItemArray[2].ToString() + "'";
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
        public static void GetInfo(ref int id, ref string cname,ref int saletypeid, ref string saletypename)
        {
            if (dqform == null || dqform.IsDisposed)
            {
                MessageBox.Show("没有选中要删除的记录！");
                cname = "";
            }
            else if (dqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要删除的记录！");
                cname = "";
            }
            else
            {
                cname = dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[0]).ItemArray[0].ToString();
                saletypename = dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString();
                id = int.Parse(Common.IsNull(dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[0]).ItemArray[2].ToString()));
                saletypeid = int.Parse(Common.IsNull(dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[0]).ItemArray[3].ToString()));

            }
        }
        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            int saletypeid;
            saletypeid = (int)comboBoxSaleType.SelectedValue; 
            if (saletypeid > 0)
            {
                strsql = "select i.cname 部门,j.cname 营业类型,i.cid,j.cid,i.forbidden 禁用 from cost_dept i left join cost_saletype j on i.saletype_id = j.cid where i.saletype_id = " + Common.IsZero(saletypeid.ToString()) + " order by i.cname";
            }
            else
            {
                strsql = "select i.cname 部门,j.cname 营业类型,i.cid,j.cid,i.forbidden 禁用 from cost_dept i left join cost_saletype j on i.saletype_id = j.cid order by i.cname";
            }

            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[2].Visible = false;
            gridView1.Columns[3].Visible = false;

            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[4].OptionsColumn.ReadOnly = true;

            IsForbidden();
            conn.Close();
        }
        private void IsForbidden()
        {
            if (dqform == null || dqform.IsDisposed)
            {

            }
            else if (dqform.gridView1.SelectedRowsCount == 0)
            {

            }

            else
            {
                if (dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[0]).ItemArray[4].ToString() == "True")
                {
                    Dept.ForbiddenDisable();
                    Dept.UnforbiddenEnable();
                }
                else
                {
                    Dept.ForbiddenEnable();
                    Dept.UnforbiddenDisable();
                }
            }
        }
        public static void cEnable()
        {
            string sql;
            bool isdone = true;
            ConnDB conn = new ConnDB();
            if (dqform == null || dqform.IsDisposed)
            {
                MessageBox.Show("没有选中要反禁用的记录！");
            }
            else if (dqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要反禁用的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要反禁用吗?", "部门反禁用", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < dqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "update i set i.forbidden = 'false' from COST_DEPT i where cid = '" + dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[i]).ItemArray[2].ToString() + "'";
                        isdone = conn.EditDatabase(sql);
                        if (!isdone)
                            break;
                    }
                    if (isdone)
                    {
                        Dept.ForbiddenEnable();
                        Dept.UnforbiddenDisable();
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
            if (dqform == null || dqform.IsDisposed)
            {
                MessageBox.Show("没有选中要禁用的记录！");
            }
            else if (dqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要禁用的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要禁用吗?", "部门禁用", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < dqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "update i set i.forbidden = 'true' from COST_DEPT i where cid = '" + dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[i]).ItemArray[2].ToString() + "'";
                        isdone = conn.EditDatabase(sql);
                        if (!isdone)
                            break;
                    }
                    if (isdone)
                    {
                        Dept.ForbiddenDisable();
                        Dept.UnforbiddenEnable();
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

        private void DeptQuery_Load(object sender, EventArgs e)
        {
            Common.BasicDataBind("cost_saletype", comboBoxSaleType);
            //this.WindowState = FormWindowState.Maximized;
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            showDetail();
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