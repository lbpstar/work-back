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
    public partial class UserDeptCheckQuery : DevExpress.XtraEditors.XtraForm
    {
        public UserDeptCheckQuery()
        {
            InitializeComponent();
        }
        private static UserDeptCheckQuery dqform = null;

        public static UserDeptCheckQuery GetInstance()
        {
            if (dqform == null || dqform.IsDisposed)
            {
                dqform = new UserDeptCheckQuery();
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
                        sql = "delete from cost_check_dept where log_name = '" + dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[i]).ItemArray[0].ToString() + "' and dept1 = '" + dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[i]).ItemArray[2].ToString() + "' and dept2= '" + dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[i]).ItemArray[3].ToString() + "' and dept3 = '" + dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[i]).ItemArray[4].ToString() + "'";
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
        public static void GetInfo(ref string logname,ref string person_name,ref string dept1,ref string dept2,ref string dept3)
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
                logname = dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[0]).ItemArray[0].ToString();
                person_name = dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString();
                dept1 = dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[0]).ItemArray[2].ToString();
                dept2 = dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[0]).ItemArray[3].ToString();
                dept3 = dqform.gridView1.GetDataRow(dqform.gridView1.GetSelectedRows()[0]).ItemArray[4].ToString();
            }
        }
        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            strsql = "select d.log_name 登录名,u.person_name 姓名,d.dept1 一级部门,d.dept2 二级部门,d.dept3 三级部门 from cost_check_dept d left join cost_user u on d.log_name = u.cname order by cname";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;

            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[4].OptionsColumn.ReadOnly = true;
            conn.Close();
        }
        
        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {

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