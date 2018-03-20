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
    public partial class WorkType : DevExpress.XtraEditors.XtraForm
    {
        public WorkType()
        {
            InitializeComponent();
        }
        private static WorkType wtform = null;

        public static WorkType GetInstance()
        {
            if (wtform == null || wtform.IsDisposed)
            {
                wtform = new WorkType();
            }
            return wtform;
        }
        public static void ForbiddenEnable()
        {
            wtform.barButtonItem禁用.Enabled = true;
        }
        public static void ForbiddenDisable()
        {
            wtform.barButtonItem禁用.Enabled = false;
        }
        public static void UnforbiddenEnable()
        {
            wtform.barButtonItem反禁用.Enabled = true;
        }
        public static void UnforbiddenDisable()
        {
            wtform.barButtonItem反禁用.Enabled = false;
        }
        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            WorkTypeQuery Frm = WorkTypeQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void SaleType_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            WorkTypeQuery Frm = WorkTypeQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            //Frm.Width = this.Width - 15;
            //Frm.Height = this.Width - 60;
        }

        private void barButtonItem退出_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem新增_ItemClick(object sender, ItemClickEventArgs e)
        {
            WorkTypeInsert Frm = WorkTypeInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            //Frm.MdiParent = this.ParentForm;
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            int id = 0;
            string cname = "";
            WorkTypeQuery.GetInfo(ref id, ref  cname);
            if (id != 0)
            {
                //SaleTypeUpdate Frm = SaleTypeUpdate.GetInstance();
                WorkTypeUpdate Frm = new WorkTypeUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            WorkTypeQuery.Delete();
            WorkTypeQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            WorkTypeQuery.RefreshEX();
        }

        private void barButtonItem反禁用_ItemClick(object sender, ItemClickEventArgs e)
        {
            WorkTypeQuery.cEnable();
            WorkTypeQuery.RefreshEX();

        }

        private void barButtonItem禁用_ItemClick(object sender, ItemClickEventArgs e)
        {
            WorkTypeQuery.cDisable();
            WorkTypeQuery.RefreshEX();

        }
    }
}