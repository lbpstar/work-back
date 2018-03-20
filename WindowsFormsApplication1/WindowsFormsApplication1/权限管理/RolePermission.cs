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
    public partial class RolePermission : DevExpress.XtraEditors.XtraForm
    {
        public RolePermission()
        {
            InitializeComponent();
        }
        private static RolePermission rpform = null;
        private bool ischange = false;
        public static RolePermission GetInstance()
        {
            if (rpform == null || rpform.IsDisposed)
            {
                rpform = new RolePermission();
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
            int cid = RoleQuery.GetCid();
            strsql = "select i.cid,p.cid,p.module_name 模块,p.permission 权限,isnull(r.have_right,'false') ' ' from COST_ROLE i cross join COST_MODULE_PERMISSION p left join COST_ROLE_PERMISSION r on i.CID = r.ROLE_ID and p.CID = r.PERMISSION_ID where i.cid = " + cid;
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;

            conn.Close();
        }
      

        private void simpleButtonSubmit_Click(object sender, EventArgs e)
        {
            
            if (ischange == true)
            {
                ConnDB conn = new ConnDB();
                string strsql;
                int cid = RoleQuery.GetCid();
                string sql = "insert into COST_ROLE_PERMISSION select i.cid,p.cid,'false' from COST_ROLE i cross join COST_MODULE_PERMISSION p left join COST_ROLE_PERMISSION r on i.CID = r.ROLE_ID and p.CID = r.PERMISSION_ID where i.cid = " + cid + " and r.role_id is null";
                conn.EditDatabase(sql);
                bool isok = false;

                //gridView1.FocusInvalidRow();
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    strsql = "update i set i.have_right = '" + gridView1.GetDataRow(i).ItemArray[4].ToString() + "' from COST_ROLE_PERMISSION i where i.ROLE_ID = " + gridView1.GetDataRow(i).ItemArray[0].ToString() + " and i.PERMISSION_ID = " + gridView1.GetDataRow(i).ItemArray[1].ToString();
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
            labelControlRole.Text = RoleQuery.GetCname() + "的权限：";
            showDetail();
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        }

        private void simpleButton全选_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql;
            int cid = RoleQuery.GetCid();
            strsql = "select i.cid,p.cid,p.module_name 模块,p.permission 权限, cast('true' as bit) ' ' from COST_ROLE i cross join COST_MODULE_PERMISSION p left join COST_ROLE_PERMISSION r on i.CID = r.ROLE_ID and p.CID = r.PERMISSION_ID where i.cid = " + cid;
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            ischange = true;
            conn.Close();
        }
    }
}