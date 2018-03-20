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
    public partial class DirectLabourQuery : DevExpress.XtraEditors.XtraForm
    {
        public DirectLabourQuery()
        {
            InitializeComponent();
        }
        private static DirectLabourQuery dlqform = null;

        public static DirectLabourQuery GetInstance()
        {
            if (dlqform == null || dlqform.IsDisposed)
            {
                dlqform = new DirectLabourQuery();
            }
            return dlqform;
        }
        public static void RefreshEX()
        {
            if (dlqform == null || dlqform.IsDisposed)
            {

            }
            else
            {
                dlqform.showDetail();
            }
        }
        public static void Delete()
        {
            string sql;
            bool isdone = true;
            ConnDB conn = new ConnDB();
            if (dlqform == null || dlqform.IsDisposed)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }
            else if (dlqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除吗?", "直接人工删除", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < dlqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "delete from cost_direct_labour where cid = '" + dlqform.gridView1.GetDataRow(dlqform.gridView1.GetSelectedRows()[i]).ItemArray[5].ToString() + "'";
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
        public static void GetInfo(ref int id, ref string cno,ref string cname, ref int deptid,ref string deptname,ref int positionid, ref string positionname,ref int linetypeid,ref string linetypename)
        {
            if (dlqform == null || dlqform.IsDisposed)
            {
                MessageBox.Show("没有选中要修改的记录！");
                cno = "";
            }
            else if (dlqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要修改的记录！");
                cno = "";
            }
            else
            {
                cno = dlqform.gridView1.GetDataRow(dlqform.gridView1.GetSelectedRows()[0]).ItemArray[0].ToString();
                cname = dlqform.gridView1.GetDataRow(dlqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString();
                deptname = dlqform.gridView1.GetDataRow(dlqform.gridView1.GetSelectedRows()[0]).ItemArray[2].ToString();
                positionname = dlqform.gridView1.GetDataRow(dlqform.gridView1.GetSelectedRows()[0]).ItemArray[3].ToString();
                linetypename = dlqform.gridView1.GetDataRow(dlqform.gridView1.GetSelectedRows()[0]).ItemArray[4].ToString();
                id = int.Parse(Common.IsNull(dlqform.gridView1.GetDataRow(dlqform.gridView1.GetSelectedRows()[0]).ItemArray[5].ToString()));
                positionid = int.Parse(Common.IsNull(dlqform.gridView1.GetDataRow(dlqform.gridView1.GetSelectedRows()[0]).ItemArray[6].ToString()));
                linetypeid = int.Parse(Common.IsNull(dlqform.gridView1.GetDataRow(dlqform.gridView1.GetSelectedRows()[0]).ItemArray[7].ToString()));
                deptid = int.Parse(Common.IsNull(dlqform.gridView1.GetDataRow(dlqform.gridView1.GetSelectedRows()[0]).ItemArray[8].ToString()));

            }
        }
        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            int linetypeid,deptid;
            deptid = (int)comboBoxDept.SelectedValue;
            linetypeid = (int)comboBoxLineType.SelectedValue;
            if(deptid>0)
            {
                if (linetypeid > 0)
                {
                    strsql = "select i.cno 工号,i.cname 姓名,d.cname 部门,j.cname 职位,e.cname 拉别,i.cid,j.cid,e.cid,d.cid,i.forbidden 禁用 from cost_direct_labour i left join cost_position j on i.position_id = j.cid left join cost_linetype e on i.linetype_id = e.cid left join cost_dept d on i.dept_id = d.cid where i.linetype_id = " + Common.IsZero(linetypeid.ToString()) + " and i.dept_id = " + Common.IsZero(deptid.ToString()) + " and i.person_type_id = 3 order by i.cname";
                }
                else
                {
                    strsql = "select i.cno 工号,i.cname 姓名,d.cname 部门,j.cname 职位,e.cname 拉别,i.cid,j.cid,e.cid,d.cid,i.forbidden 禁用 from cost_direct_labour i left join cost_position j on i.position_id = j.cid left join cost_linetype e on i.linetype_id = e.cid left join cost_dept d on i.dept_id = d.cid where i.dept_id = " + Common.IsZero(deptid.ToString()) + " and i.person_type_id = 3 order by i.cname";
                }
            }
            else
            {
                if (linetypeid > 0)
                {
                    strsql = "select i.cno 工号,i.cname 姓名,d.cname 部门,j.cname 职位,e.cname 拉别,i.cid,j.cid,e.cid,d.cid,i.forbidden 禁用 from cost_direct_labour i left join cost_position j on i.position_id = j.cid left join cost_linetype e on i.linetype_id = e.cid left join cost_dept d on i.dept_id = d.cid where i.linetype_id = " + Common.IsZero(linetypeid.ToString()) + " and i.person_type_id = 3 order by i.cname";
                }
                else
                {
                    strsql = "select i.cno 工号,i.cname 姓名,d.cname 部门,j.cname 职位,e.cname 拉别,i.cid,j.cid,e.cid,d.cid,i.forbidden 禁用 from cost_direct_labour i left join cost_position j on i.position_id = j.cid left join cost_linetype e on i.linetype_id = e.cid left join cost_dept d on i.dept_id = d.cid where i.person_type_id = 3 order by i.cname";
                }
            }
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[5].Visible = false;
            gridView1.Columns[6].Visible = false;
            gridView1.Columns[7].Visible = false;
            gridView1.Columns[8].Visible = false;
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
            IsForbidden();
            conn.Close();
        }
        private void IsForbidden()
        {
            if (dlqform == null || dlqform.IsDisposed)
            {

            }
            else if (dlqform.gridView1.SelectedRowsCount == 0)
            {

            }

            else
            {
                if (dlqform.gridView1.GetDataRow(dlqform.gridView1.GetSelectedRows()[0]).ItemArray[9].ToString() == "True")
                {
                    DirectLabour.ForbiddenDisable();
                    DirectLabour.UnforbiddenEnable();
                }
                else
                {
                    DirectLabour.ForbiddenEnable();
                    DirectLabour.UnforbiddenDisable();
                }
            }
        }
        public static void cEnable()
        {
            string sql;
            bool isdone = true;
            ConnDB conn = new ConnDB();
            if (dlqform == null || dlqform.IsDisposed)
            {
                MessageBox.Show("没有选中要反禁用的记录！");
            }
            else if (dlqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要反禁用的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要反禁用吗?", "直接人工反禁用", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < dlqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "update i set i.forbidden = 'false' from COST_DIRECT_LABOUR i where cid = '" + dlqform.gridView1.GetDataRow(dlqform.gridView1.GetSelectedRows()[i]).ItemArray[5].ToString() + "'";
                        isdone = conn.EditDatabase(sql);
                        if (!isdone)
                            break;
                    }
                    if (isdone)
                    {
                        DirectLabour.ForbiddenEnable();
                        DirectLabour.UnforbiddenDisable();
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
            if (dlqform == null || dlqform.IsDisposed)
            {
                MessageBox.Show("没有选中要禁用的记录！");
            }
            else if (dlqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要禁用的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要禁用吗?", "直接人工禁用", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < dlqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "update i set i.forbidden = 'true' from COST_DIRECT_LABOUR i where cid = '" + dlqform.gridView1.GetDataRow(dlqform.gridView1.GetSelectedRows()[i]).ItemArray[5].ToString() + "'";
                        isdone = conn.EditDatabase(sql);
                        if (!isdone)
                            break;
                    }
                    if (isdone)
                    {
                        DirectLabour.ForbiddenDisable();
                        DirectLabour.UnforbiddenEnable();
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

        private void DirectLabourQuery_Load(object sender, EventArgs e)
        {
            Common.BasicDataBind("cost_dept", comboBoxDept);
            LineTypeBind();
            //this.WindowState = FormWindowState.Maximized;
            showDetail();
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        }
        private void LineTypeBind()
        {
            ConnDB conn = new ConnDB();
            string sql = "select l.cid,l.cname from COST_LINETYPE l left join COST_SALETYPE s on l.SALETYPE_ID = s.CID left join COST_DEPT d on s.CID = d.SALETYPE_ID where d.CID = " + comboBoxDept.SelectedValue.ToString();
            DataSet ds = conn.ReturnDataSet(sql);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = "0";
            dr[1] = "请选择";
            //插在第一位
            ds.Tables[0].Rows.InsertAt(dr, 0);
            comboBoxLineType.DataSource = ds.Tables[0];
            comboBoxLineType.DisplayMember = "CNAME";
            comboBoxLineType.ValueMember = "CID";
            conn.Close();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            IsForbidden();
        }

        private void comboBoxDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDept.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                LineTypeBind();
                showDetail();
            }
        }

        private void comboBoxLineType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxLineType.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                showDetail();
            }
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            IsForbidden();
        }
    }
}