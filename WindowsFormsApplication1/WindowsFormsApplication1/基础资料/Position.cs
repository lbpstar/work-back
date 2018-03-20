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
    public partial class Position : DevExpress.XtraEditors.XtraForm
    {
        public Position()
        {
            InitializeComponent();
        }
        private static Position pform = null;
        public static Position GetInstance()
        {
            if (pform == null || pform.IsDisposed)
            {
                pform = new Position();
            }
            return pform;
        }
        public static void ForbiddenEnable()
        {
            pform.barButtonItem禁用.Enabled = true;
        }
        public static void ForbiddenDisable()
        {
            pform.barButtonItem禁用.Enabled = false;
        }
        public static void UnforbiddenEnable()
        {
            pform.barButtonItem反禁用.Enabled = true;
        }
        public static void UnforbiddenDisable()
        {
            pform.barButtonItem反禁用.Enabled = false;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            PositionQuery Frm = PositionQuery.GetInstance();
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
            PositionInsert Frm = PositionInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string cname = "";
            int cid = 0;
            int persontypeid = 0;
            string persontypename = "";
            PositionQuery.GetInfo(ref cid, ref cname, ref persontypeid, ref persontypename);
            if (cname != "")
            {
                PositionUpdate Frm = new PositionUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            PositionQuery.Delete();
            PositionQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            PositionQuery.RefreshEX();
        }


        private void Position_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            PositionQuery Frm = PositionQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem禁用_ItemClick(object sender, ItemClickEventArgs e)
        {
            PositionQuery.cDisable();
            PositionQuery.RefreshEX();
        }

        private void barButtonItem反禁用_ItemClick(object sender, ItemClickEventArgs e)
        {
            PositionQuery.cEnable();
            PositionQuery.RefreshEX();

        }
    }
}