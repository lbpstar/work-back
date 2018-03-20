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
    public partial class SaleType : DevExpress.XtraEditors.XtraForm
    {
        public SaleType()
        {
            InitializeComponent();
        }
        private static SaleType stform = null;

        public static SaleType GetInstance()
        {
            if (stform == null || stform.IsDisposed)
            {
                stform = new SaleType();
            }
            return stform;
        }
        public static void ForbiddenEnable()
        {
            stform.barButtonItem禁用.Enabled = true;
        }
        public static void ForbiddenDisable()
        {
            stform.barButtonItem禁用.Enabled = false;
        }
        public static void UnforbiddenEnable()
        {
            stform.barButtonItem反禁用.Enabled = true;
        }
        public static void UnforbiddenDisable()
        {
            stform.barButtonItem反禁用.Enabled = false;
        }
        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaleTypeQuery Frm = SaleTypeQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void SaleType_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            SaleTypeQuery Frm = SaleTypeQuery.GetInstance();
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
            SaleTypeInsert Frm = SaleTypeInsert.GetInstance();
            Frm.TopLevel = false;
            //Frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string cname = SaleTypeQuery.GetCname();
            if (cname != "")
            {
                //SaleTypeUpdate Frm = SaleTypeUpdate.GetInstance();
                SaleTypeUpdate Frm = new SaleTypeUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaleTypeQuery.Delete();
            SaleTypeQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaleTypeQuery.RefreshEX();
        }

        private void barButtonItem禁用_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaleTypeQuery.cDisable();

            SaleTypeQuery.RefreshEX();
        }

        private void barButtonItem反禁用_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaleTypeQuery.cEnable();
            SaleTypeQuery.RefreshEX();
        }
    }
}