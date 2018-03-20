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
    public partial class ShiftQuery : DevExpress.XtraEditors.XtraForm
    {
        public ShiftQuery()
        {
            InitializeComponent();
        }
        private static ShiftQuery wtqform = null;

        public static ShiftQuery GetInstance()
        {
            if (wtqform == null || wtqform.IsDisposed)
            {
                wtqform = new ShiftQuery();
            }
            return wtqform;
        }
        public static void RefreshEX()
        {
            if (wtqform == null || wtqform.IsDisposed)
            {

            }
            else
            {
                wtqform.showDetail();
            }
        }
        public static void Delete()
        {
            string sql;
            bool isdone= true;
            ConnDB conn = new ConnDB();
            if (wtqform == null || wtqform.IsDisposed)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }
            else if (wtqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除吗?", "考勤班次删除", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < wtqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "delete from cost_shift where cname   = '" + wtqform.gridView1.GetDataRow(wtqform.gridView1.GetSelectedRows()[i]).ItemArray[0].ToString() + "'";
                        isdone = conn.DeleteDatabase(sql);
                        if (!isdone)
                            break;
                    }
               
                    if(isdone)
                    {
                        MessageBox.Show("删除成功！");
                    }
                }
            }
            conn.Close();
        }
        public static void GetInfo(ref string cname,ref string cbegin,ref string cend,ref decimal rest_hours,ref decimal overtime_rest_hours,ref string overtime_begin)
        {
            if (wtqform == null || wtqform.IsDisposed)
            {
                MessageBox.Show("没有选中要修改的记录！");
                cname =  "";
            }
            else if (wtqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要修改的记录！");
                cname = "";
            }
            else
            {
                //return wtqform.gridView1.CheckedItems[0].SubItems[0].Text.ToString();
                //id = int.Parse(Common.IsNull(wtqform.gridView1.GetDataRow(wtqform.gridView1.GetSelectedRows()[0]).ItemArray[0].ToString()));
                cname = wtqform.gridView1.GetDataRow(wtqform.gridView1.GetSelectedRows()[0]).ItemArray[0].ToString();
                cbegin = wtqform.gridView1.GetDataRow(wtqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString();
                cend = wtqform.gridView1.GetDataRow(wtqform.gridView1.GetSelectedRows()[0]).ItemArray[2].ToString();
                rest_hours = Convert.ToDecimal(wtqform.gridView1.GetDataRow(wtqform.gridView1.GetSelectedRows()[0]).ItemArray[3].ToString());
                overtime_rest_hours = Convert.ToDecimal(wtqform.gridView1.GetDataRow(wtqform.gridView1.GetSelectedRows()[0]).ItemArray[4].ToString());
                overtime_begin = wtqform.gridView1.GetDataRow(wtqform.gridView1.GetSelectedRows()[0]).ItemArray[5].ToString();
            }
            //return "";
        }

        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            strsql = "select cname as 名称,cbegin 开始时间,cend 结束时间,isnull(rest_hours,0) 休息时数,isnull(overtime_rest_hours,0) 加班后休息时数,overtime_begin 加班开始时间,forbidden 禁用 from cost_shift";
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
            IsForbidden();
            conn.Close();
        }

        private void SaleTypeQuery_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            showDetail();
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        }
        private void IsForbidden()
        {
            if (wtqform == null || wtqform.IsDisposed)
            {

            }
            else if (wtqform.gridView1.SelectedRowsCount == 0)
            {

            }

            else
            {
                if (wtqform.gridView1.GetDataRow(wtqform.gridView1.GetSelectedRows()[0]).ItemArray[6].ToString() == "True")
                {
                    Shift.ForbiddenDisable();
                    Shift.UnforbiddenEnable();
                }
                else
                {
                    Shift.ForbiddenEnable();
                    Shift.UnforbiddenDisable();
                }
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            IsForbidden();
        }
        public static void cEnable()
        {
            string sql;
            bool isdone = true;
            ConnDB conn = new ConnDB();
            if (wtqform == null || wtqform.IsDisposed)
            {
                MessageBox.Show("没有选中要反禁用的记录！");
            }
            else if (wtqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要反禁用的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要反禁用吗?", "考勤班次反禁用", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < wtqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "update i set i.forbidden = 'false' from cost_shift i where cname = '" + wtqform.gridView1.GetDataRow(wtqform.gridView1.GetSelectedRows()[i]).ItemArray[0].ToString() + "'";
                        isdone = conn.EditDatabase(sql);
                        if (!isdone)
                            break;
                    }
                    if (isdone)
                    {
                        Shift.ForbiddenEnable();
                        Shift.UnforbiddenDisable();
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
            if (wtqform == null || wtqform.IsDisposed)
            {
                MessageBox.Show("没有选中要禁用的记录！");
            }
            else if (wtqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要禁用的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要禁用吗?", "考勤班次禁用", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < wtqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "update i set i.forbidden = 'true' from cost_shift i where cname = '" + wtqform.gridView1.GetDataRow(wtqform.gridView1.GetSelectedRows()[i]).ItemArray[0].ToString() + "'";
                        isdone = conn.EditDatabase(sql);
                        if (!isdone)
                            break;
                    }
                    if (isdone)
                    {
                        Shift.ForbiddenDisable();
                        Shift.UnforbiddenEnable();
                        MessageBox.Show("禁用成功！");
                    }
                }
            }
            conn.Close();
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            IsForbidden();
        }
    }
}