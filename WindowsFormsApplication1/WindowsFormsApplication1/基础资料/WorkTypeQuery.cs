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
    public partial class WorkTypeQuery : DevExpress.XtraEditors.XtraForm
    {
        public WorkTypeQuery()
        {
            InitializeComponent();
        }
        private static WorkTypeQuery wtqform = null;

        public static WorkTypeQuery GetInstance()
        {
            if (wtqform == null || wtqform.IsDisposed)
            {
                wtqform = new WorkTypeQuery();
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
                DialogResult dr = MessageBox.Show("确定要删除吗?", "上班类型删除", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < wtqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "delete from cost_work_type where cid   = '" + wtqform.gridView1.GetDataRow(wtqform.gridView1.GetSelectedRows()[i]).ItemArray[0].ToString() + "'";
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
        public static void GetInfo(ref int id, ref string cname)
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
                id = int.Parse(Common.IsNull(wtqform.gridView1.GetDataRow(wtqform.gridView1.GetSelectedRows()[0]).ItemArray[0].ToString()));
                cname = wtqform.gridView1.GetDataRow(wtqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString();
            }
            //return "";
        }
        //protected override void WndProc(ref System.Windows.Forms.Message m)
        //{
        //    base.WndProc(ref m);//基类执行 
        //    if (m.Msg == 132)//鼠标的移动消息（包括非窗口的移动） 
        //    {
        //        //基类执行后m有了返回值,鼠标在窗口的每个地方的返回值都不同 
        //        if ((IntPtr)2 == m.Result)//如果返回值是2，则说明鼠标是在标题拦 
        //        {
        //            //将返回值改为1(窗口的客户区)，这样系统就以为是 
        //            //在客户区拖动的鼠标，窗体就不会移动 
        //            m.Result = (IntPtr)1;
        //        }
        //    }
        //}
        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            strsql = "select cid,cname as 名称,forbidden 禁用 from cost_work_type";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;

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
                if (wtqform.gridView1.GetDataRow(wtqform.gridView1.GetSelectedRows()[0]).ItemArray[2].ToString() == "True")
                {
                    WorkType.ForbiddenDisable();
                    WorkType.UnforbiddenEnable();
                }
                else
                {
                    WorkType.ForbiddenEnable();
                    WorkType.UnforbiddenDisable();
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
                DialogResult dr = MessageBox.Show("确定要反禁用吗?", "上班类型反禁用", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < wtqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "update i set i.forbidden = 'false' from cost_work_type i where cid = '" + wtqform.gridView1.GetDataRow(wtqform.gridView1.GetSelectedRows()[i]).ItemArray[0].ToString() + "'";
                        isdone = conn.EditDatabase(sql);
                        if (!isdone)
                            break;
                    }
                    if (isdone)
                    {
                        WorkType.ForbiddenEnable();
                        WorkType.UnforbiddenDisable();
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
                DialogResult dr = MessageBox.Show("确定要禁用吗?", "上班类型禁用", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < wtqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "update i set i.forbidden = 'true' from cost_work_type i where cid = '" + wtqform.gridView1.GetDataRow(wtqform.gridView1.GetSelectedRows()[i]).ItemArray[0].ToString() + "'";
                        isdone = conn.EditDatabase(sql);
                        if (!isdone)
                            break;
                    }
                    if (isdone)
                    {
                        WorkType.ForbiddenDisable();
                        WorkType.UnforbiddenEnable();
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