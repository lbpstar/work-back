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
    public partial class LineTypeQuery : DevExpress.XtraEditors.XtraForm
    {
        public LineTypeQuery()
        {
            InitializeComponent();
        }
        private static LineTypeQuery ltqform = null;

        public static LineTypeQuery GetInstance()
        {
            if (ltqform == null || ltqform.IsDisposed)
            {
                ltqform = new LineTypeQuery();
            }
            return ltqform;
        }
        public static void RefreshEX()
        {
            if (ltqform == null || ltqform.IsDisposed)
            {

            }
            else
            {
                ltqform.showDetail();
            }
        }
        public static void Delete()
        {
            string sql;
            bool isdone = true;
            ConnDB conn = new ConnDB();
            if (ltqform == null || ltqform.IsDisposed)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }
            else if (ltqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除吗?", "线体删除", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < ltqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "delete from cost_linetype where cid = '" + ltqform.gridView1.GetDataRow(ltqform.gridView1.GetSelectedRows()[i]).ItemArray[5].ToString() + "'";
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
        public static void GetInfo(ref int id, ref string cname, ref string cnamemes, ref int saletypeid, ref string saletypename,ref int workshopid,ref string workshop)
        {
            if (ltqform == null || ltqform.IsDisposed)
            {
                MessageBox.Show("没有选中要修改的记录！");
                cname = "";
            }
            else if (ltqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要修改的记录！");
                cname = "";
            }
            else
            {
                cname = ltqform.gridView1.GetDataRow(ltqform.gridView1.GetSelectedRows()[0]).ItemArray[2].ToString();
                cnamemes = ltqform.gridView1.GetDataRow(ltqform.gridView1.GetSelectedRows()[0]).ItemArray[3].ToString();
                saletypename = ltqform.gridView1.GetDataRow(ltqform.gridView1.GetSelectedRows()[0]).ItemArray[0].ToString();
                workshop = ltqform.gridView1.GetDataRow(ltqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString();
                workshopid = int.Parse(Common.IsNull(ltqform.gridView1.GetDataRow(ltqform.gridView1.GetSelectedRows()[0]).ItemArray[4].ToString()));
                id = int.Parse(Common.IsNull(ltqform.gridView1.GetDataRow(ltqform.gridView1.GetSelectedRows()[0]).ItemArray[5].ToString()));
                saletypeid = int.Parse(Common.IsNull(ltqform.gridView1.GetDataRow(ltqform.gridView1.GetSelectedRows()[0]).ItemArray[6].ToString()));

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
                strsql = "select j.cname 营业类型,d.cname 车间,i.cname ERP中线体,i.cname_mes MES中线体,d.sub_id,i.cid,j.cid,i.forbidden 禁用 from cost_linetype i left join cost_saletype j on i.saletype_id = j.cid left join cost_base_data d on d.module_id = 3 and i.work_shop = d.sub_id where i.saletype_id = " + Common.IsZero(saletypeid.ToString()) + " order by i.cname";
            }
            else
            {
                strsql = "select j.cname 营业类型,d.cname 车间,i.cname ERP中线体,i.cname_mes MES中线体,d.sub_id,i.cid,j.cid,i.forbidden 禁用 from cost_linetype i left join cost_saletype j on i.saletype_id = j.cid left join cost_base_data d on d.module_id = 3 and i.work_shop = d.sub_id order by i.cname";
            }

            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[4].Visible = false;
            gridView1.Columns[5].Visible = false;
            gridView1.Columns[6].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[4].OptionsColumn.ReadOnly = true;
            gridView1.Columns[5].OptionsColumn.ReadOnly = true;
            gridView1.Columns[6].OptionsColumn.ReadOnly = true;
            gridView1.Columns[7].OptionsColumn.ReadOnly = true;

            IsForbidden();
            conn.Close();
        }
        private void IsForbidden()
        {
            if (ltqform == null || ltqform.IsDisposed)
            {

            }
            else if (ltqform.gridView1.SelectedRowsCount == 0)
            {

            }

            else
            {
                if (ltqform.gridView1.GetDataRow(ltqform.gridView1.GetSelectedRows()[0]).ItemArray[7].ToString() == "True")
                {
                    LineType.ForbiddenDisable();
                    LineType.UnforbiddenEnable();
                }
                else
                {
                    LineType.ForbiddenEnable();
                    LineType.UnforbiddenDisable();
                }
            }
        }
        public static void cEnable()
        {
            string sql;
            bool isdone = true;
            ConnDB conn = new ConnDB();
            if (ltqform == null || ltqform.IsDisposed)
            {
                MessageBox.Show("没有选中要反禁用的记录！");
            }
            else if (ltqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要反禁用的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要反禁用吗?", "线体反禁用", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < ltqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "update i set i.forbidden = 'false' from cost_linetype i where cid = '" + ltqform.gridView1.GetDataRow(ltqform.gridView1.GetSelectedRows()[i]).ItemArray[5].ToString() + "'";
                        isdone = conn.EditDatabase(sql);
                        if (!isdone)
                            break;
                    }
                    if (isdone)
                    {
                        LineType.ForbiddenEnable();
                        LineType.UnforbiddenDisable();
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
            if (ltqform == null || ltqform.IsDisposed)
            {
                MessageBox.Show("没有选中要禁用的记录！");
            }
            else if (ltqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要禁用的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要禁用吗?", "线体禁用", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < ltqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "update i set i.forbidden = 'true' from cost_linetype i where cid = '" + ltqform.gridView1.GetDataRow(ltqform.gridView1.GetSelectedRows()[i]).ItemArray[5].ToString() + "'";
                        isdone = conn.EditDatabase(sql);
                        if (!isdone)
                            break;
                    }
                    if (isdone)
                    {
                        LineType.ForbiddenDisable();
                        LineType.UnforbiddenEnable();
                        MessageBox.Show("禁用成功！");
                    }
                }
            }
            conn.Close();
        }
        private void LineTypeQuery_Load(object sender, EventArgs e)
        {
            Common.BasicDataBind("cost_saletype", comboBoxSaleType);
            //this.WindowState = FormWindowState.Maximized;
            showDetail();
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
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
            IsForbidden();
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            IsForbidden();
        }
    }
}