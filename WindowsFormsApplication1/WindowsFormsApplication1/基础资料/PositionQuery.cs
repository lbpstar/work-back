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
    public partial class PositionQuery : DevExpress.XtraEditors.XtraForm
    {
        public PositionQuery()
        {
            InitializeComponent();
        }
        private static PositionQuery pqform = null;

        public static PositionQuery GetInstance()
        {
            if (pqform == null || pqform.IsDisposed)
            {
                pqform = new PositionQuery();
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
                DialogResult dr = MessageBox.Show("确定要删除吗?", "职位删除", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < pqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "delete from cost_position where cid = '" + pqform.gridView1.GetDataRow(pqform.gridView1.GetSelectedRows()[i]).ItemArray[2].ToString() + "'";
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
        public static void GetInfo(ref int id, ref string cname, ref int persontypeid, ref string persontypename)
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
                cname = pqform.gridView1.GetDataRow(pqform.gridView1.GetSelectedRows()[0]).ItemArray[0].ToString();
                persontypename = pqform.gridView1.GetDataRow(pqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString();
                id = int.Parse(Common.IsNull(pqform.gridView1.GetDataRow(pqform.gridView1.GetSelectedRows()[0]).ItemArray[2].ToString()));
                persontypeid = int.Parse(Common.IsNull(pqform.gridView1.GetDataRow(pqform.gridView1.GetSelectedRows()[0]).ItemArray[3].ToString()));

            }
        }
        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            int persontypeid;
            persontypeid = (int)comboBoxPersonType.SelectedValue; 
            if (persontypeid > 0)
            {
                strsql = "select i.cname 职位,j.cname 人力类型,i.cid,j.cid,i.forbidden 禁用 from cost_position i left join cost_person_type j on i.person_type_id = j.cid where i.person_type_id = " + Common.IsZero(persontypeid.ToString()) + " order by i.cname";
            }
            else
            {
                strsql = "select i.cname 职位,j.cname 人力类型,i.cid,j.cid,i.forbidden 禁用 from cost_position i left join cost_person_type j on i.person_type_id = j.cid order by i.cname";
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
            if (pqform == null || pqform.IsDisposed)
            {

            }
            else if (pqform.gridView1.SelectedRowsCount == 0)
            {

            }

            else
            {
                if (pqform.gridView1.GetDataRow(pqform.gridView1.GetSelectedRows()[0]).ItemArray[4].ToString() == "True")
                {
                    Position.ForbiddenDisable();
                    Position.UnforbiddenEnable();
                }
                else
                {
                    Position.ForbiddenEnable();
                    Position.UnforbiddenDisable();
                }
            }
        }
        public static void cEnable()
        {
            string sql;
            bool isdone = true;
            ConnDB conn = new ConnDB();
            if (pqform == null || pqform.IsDisposed)
            {
                MessageBox.Show("没有选中要反禁用的记录！");
            }
            else if (pqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要反禁用的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要反禁用吗?", "职位反禁用", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < pqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "update i set i.forbidden = 'false' from cost_position i where cid = '" + pqform.gridView1.GetDataRow(pqform.gridView1.GetSelectedRows()[i]).ItemArray[2].ToString() + "'";
                        isdone = conn.EditDatabase(sql);
                        if (!isdone)
                            break;
                    }
                    if (isdone)
                    {
                        Position.ForbiddenEnable();
                        Position.UnforbiddenDisable();
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
            if (pqform == null || pqform.IsDisposed)
            {
                MessageBox.Show("没有选中要禁用的记录！");
            }
            else if (pqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要禁用的记录！");

            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要禁用吗?", "职位禁用", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < pqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "update i set i.forbidden = 'true' from cost_position i where cid = '" + pqform.gridView1.GetDataRow(pqform.gridView1.GetSelectedRows()[i]).ItemArray[2].ToString() + "'";
                        isdone = conn.EditDatabase(sql);
                        if (!isdone)
                            break;
                    }
                    if (isdone)
                    {
                        Position.ForbiddenDisable();
                        Position.UnforbiddenEnable();
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

        private void PositionQuery_Load(object sender, EventArgs e)
        {
            Common.BasicDataBind("cost_person_type", comboBoxPersonType);
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