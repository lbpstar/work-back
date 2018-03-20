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
    public partial class UserQuery : DevExpress.XtraEditors.XtraForm
    {
        public UserQuery()
        {
            InitializeComponent();
        }
        private static UserQuery dqform = null;

        public static UserQuery GetInstance()
        {
            if (dqform == null || dqform.IsDisposed)
            {
                dqform = new UserQuery();
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
                MessageBox.Show("没有选中要删除的用户！");
            }
            else if (dqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要删除的用户！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除吗?", "用户删除", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < dqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "delete from cost_user where cid = '" + dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[i]).ItemArray[0].ToString() + "'";
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
        public static void GetInfo(ref int id, ref string logname,ref string personname,ref string dept1,ref string dept2,ref string dept3,ref string dept,ref string remark)
        {
            if (dqform == null || dqform.IsDisposed)
            {
                MessageBox.Show("没有选中记录！");
                logname = "";
            }
            else if (dqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中记录！");
                logname = "";
            }
            else
            {
                id = int.Parse(Common.IsNull(dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[0]).ItemArray[0].ToString()));
                logname = dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString();
                personname = dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[0]).ItemArray[2].ToString();
                dept1 = dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[0]).ItemArray[3].ToString();
                dept2 = dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[0]).ItemArray[4].ToString();
                dept3 = dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[0]).ItemArray[5].ToString();
                dept = dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[0]).ItemArray[6].ToString();
                remark = dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[0]).ItemArray[7].ToString();
            }
        }
        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            strsql = "select cid,cname 登录名,person_name 姓名,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,remark 备注,forbidden 禁用 from cost_user order by cname";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;

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
            if (dqform == null || dqform.IsDisposed)
            {

            }
            else if (dqform.gridView1.SelectedRowsCount == 0)
            {

            }

            else
            {
                if (dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[0]).ItemArray[8].ToString() == "True")
                {
                    User.ForbiddenDisable();
                    User.UnforbiddenEnable();
                }
                else
                {
                    User.ForbiddenEnable();
                    User.UnforbiddenDisable();
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
                DialogResult dr = MessageBox.Show("确定要反禁用吗?", "用户反禁用", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < dqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "update i set i.forbidden = 'false' from COST_USER i where cid = '" + dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[i]).ItemArray[0].ToString() + "'";
                        isdone = conn.EditDatabase(sql);
                        if (!isdone)
                            break;
                    }
                    if (isdone)
                    {
                        User.ForbiddenEnable();
                        User.UnforbiddenDisable();
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
                DialogResult dr = MessageBox.Show("确定要禁用吗?", "用户禁用", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < dqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "update i set i.forbidden = 'true' from COST_User i where cid = '" + dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[i]).ItemArray[0].ToString() + "'";
                        isdone = conn.EditDatabase(sql);
                        if (!isdone)
                            break;
                    }
                    if (isdone)
                    {
                        User.ForbiddenDisable();
                        User.UnforbiddenEnable();
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

        private void gridControl1_Click(object sender, EventArgs e)
        {
            IsForbidden();
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            IsForbidden();
        }

        private void UserQuery_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            showDetail();
        }
    }
}