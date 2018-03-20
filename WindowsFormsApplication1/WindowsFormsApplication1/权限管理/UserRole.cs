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
    public partial class UserRole : DevExpress.XtraEditors.XtraForm
    {
        public UserRole()
        {
            InitializeComponent();
        }
        private static UserRole rpform = null;
        private bool ischange = false;
        public static UserRole GetInstance()
        {
            if (rpform == null || rpform.IsDisposed)
            {
                rpform = new UserRole();
            }
            return rpform;
        }
        public static void RefreshEX()
        {
            if (rpform == null || rpform.IsDisposed)
            {

            }
            else
            {
                rpform.showDetail();
            }
        }

        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            string logname = "", personname = "", dept1 = "", dept2 = "", dept3 = "", dept = "",remark = "";
            int cid = 0;
            UserQuery.GetInfo(ref cid, ref logname, ref personname, ref dept1, ref dept2, ref dept3, ref dept,ref remark);
            strsql = "select i.cid,p.cid,p.cname 角色,isnull(r.have_right,'false') ' ' from COST_user i cross join COST_role p left join COST_user_role r on i.CID = r.user_ID and p.CID = r.role_ID where i.cid = " + cid;
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;

            conn.Close();
        }
      

        private void simpleButtonSubmit_Click(object sender, EventArgs e)
        {
            
            if (ischange == true)
            {
                ConnDB conn = new ConnDB();
                string strsql;
                string logname = "", personname = "", dept1 = "", dept2 = "", dept3 = "", dept = "",remark = "";
                int cid = 0;
                UserQuery.GetInfo(ref cid, ref logname, ref personname, ref dept1, ref dept2, ref dept3, ref dept,ref remark);
                string sql = "insert into COST_user_role select i.cid,p.cid,'false' from COST_user i cross join COST_role p left join COST_user_role r on i.CID = r.user_ID and p.CID = r.role_ID where i.cid = " + cid + " and r.user_id is null";
                conn.EditDatabase(sql);
                bool isok = false;

                //gridView1.FocusInvalidRow();
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    strsql = "update i set i.have_right = '" + gridView1.GetDataRow(i).ItemArray[3].ToString() + "' from COST_user_role i where i.user_ID = " + gridView1.GetDataRow(i).ItemArray[0].ToString() + " and i.role_ID = " + gridView1.GetDataRow(i).ItemArray[1].ToString();
                    isok = conn.EditDatabase(strsql);
                }

                if (isok)
                {
                    MessageBox.Show("提交成功！");
                    showDetail();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("失败！");
                }
                conn.Close();
                ischange = false;
            }
            else
            {
                MessageBox.Show("没有可更新的数据！");
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            ischange = true;
            //gridView1.SelectRow(e.RowHandle);
        }

        private void RolePermission_Load(object sender, EventArgs e)
        {
            string logname = "", personname = "", dept1 = "", dept2 = "", dept3 = "", dept = "",remark = "";
            int cid = 0;
            UserQuery.GetInfo(ref cid, ref logname, ref personname, ref dept1, ref dept2, ref dept3, ref dept,ref remark);
            labelControlRole.Text = logname + "所属的角色：";
            showDetail();
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        }
    }
}