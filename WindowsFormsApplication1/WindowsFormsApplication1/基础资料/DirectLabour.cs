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
    public partial class DirectLabour : DevExpress.XtraEditors.XtraForm
    {
        public DirectLabour()
        {
            InitializeComponent();
        }
        private static DirectLabour dlform = null;
        public static DirectLabour GetInstance()
        {
            if (dlform == null || dlform.IsDisposed)
            {
                dlform = new DirectLabour();
            }
            return dlform;
        }
        public static void ForbiddenEnable()
        {
            dlform.barButtonItem禁用.Enabled = true;
        }
        public static void ForbiddenDisable()
        {
            dlform.barButtonItem禁用.Enabled = false;
        }
        public static void UnforbiddenEnable()
        {
            dlform.barButtonItem反禁用.Enabled = true;
        }
        public static void UnforbiddenDisable()
        {
            dlform.barButtonItem反禁用.Enabled = false;
        }
        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            DirectLabourQuery Frm = DirectLabourQuery.GetInstance();
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
            DirectLabourInsert Frm = DirectLabourInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string cno = "", cname = "", positionname = "", linetypename = "",deptname = "";
            int id = 0, positionid = 0, linetypeid = 0,deptid = 0;
            DirectLabourQuery.GetInfo(ref id, ref cno, ref cname, ref deptid,ref deptname,ref positionid, ref positionname, ref linetypeid, ref linetypename);
            if (cno != "")
            {
                DirectLabourUpdate Frm = new DirectLabourUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            DirectLabourQuery.Delete();
            DirectLabourQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            DirectLabourQuery.RefreshEX();
        }

        private void LineType_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            DirectLabourQuery Frm = DirectLabourQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            //Frm.Width = this.Width - 15;
            //Frm.Height = this.Width - 60;
        }

        private void barButtonItem禁用_ItemClick(object sender, ItemClickEventArgs e)
        {
            DirectLabourQuery.cDisable();
            DirectLabourQuery.RefreshEX();
        }

        private void barButtonItem反禁用_ItemClick(object sender, ItemClickEventArgs e)
        {
            DirectLabourQuery.cEnable();
            DirectLabourQuery.RefreshEX();
        }
    }
}