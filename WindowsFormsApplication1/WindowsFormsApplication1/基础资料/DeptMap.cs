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
using DevExpress.XtraBars;

namespace SMTCost
{
    public partial class DeptMap : DevExpress.XtraEditors.XtraForm
    {
        public DeptMap()
        {
            InitializeComponent();
        }
        private static DeptMap pform = null;
        public static DeptMap GetInstance()
        {
            if (pform == null || pform.IsDisposed)
            {
                pform = new DeptMap();
            }
            return pform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeptMapQuery Frm = DeptMapQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem退出_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem新增_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeptMapInsert Frm = DeptMapInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            int deptid = 0;
            string cname = "", attendance = "", attendance3rd = "", attendance4th = "";
            DeptMapQuery.GetInfo(ref deptid, ref cname, ref attendance,ref attendance3rd, ref attendance4th);
            if (cname != "" && attendance4th != "")
            {
                DeptMapUpdate Frm = new DeptMapUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
            else
            {
                MessageBox.Show("该对照关系还不存在，请先新增！");
            }
        }



        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeptMapQuery.RefreshEX();
        }

        private void barButtonItem禁用_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem反禁用_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeptMapQuery.Delete();
            DeptMapQuery.RefreshEX();
        }

        private void DeptMap_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            DeptMapQuery Frm = DeptMapQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            Frm.Height = this.Height - 20;
        }
    }
}