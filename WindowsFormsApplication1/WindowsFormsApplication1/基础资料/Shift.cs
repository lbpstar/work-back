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
    public partial class Shift : DevExpress.XtraEditors.XtraForm
    {
        public Shift()
        {
            InitializeComponent();
        }
        private static Shift wtform = null;

        public static Shift GetInstance()
        {
            if (wtform == null || wtform.IsDisposed)
            {
                wtform = new Shift();
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
            ShiftQuery Frm = ShiftQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void SaleType_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            ShiftQuery Frm = ShiftQuery.GetInstance();
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
            ShiftInsert Frm = ShiftInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            //Frm.MdiParent = this.ParentForm;
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string cname = "",cbegin = "", cend = "",overtime_begin = "";
            decimal rest_hours = 0, overtime_rest_hours = 0;
            ShiftQuery.GetInfo(ref cname, ref cbegin, ref cend, ref rest_hours, ref overtime_rest_hours,ref overtime_begin);
            if (cname != "")
            {
                //SaleTypeUpdate Frm = SaleTypeUpdate.GetInstance();
                ShiftUpdate Frm = new ShiftUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShiftQuery.Delete();
            ShiftQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShiftQuery.RefreshEX();
        }

        private void barButtonItem反禁用_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShiftQuery.cEnable();
            ShiftQuery.RefreshEX();

        }

        private void barButtonItem禁用_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShiftQuery.cDisable();
            ShiftQuery.RefreshEX();

        }
    }
}