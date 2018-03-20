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
    public partial class LineType : DevExpress.XtraEditors.XtraForm
    {
        public LineType()
        {
            InitializeComponent();
        }
        private static LineType ltform = null;
        public static LineType GetInstance()
        {
            if (ltform == null || ltform.IsDisposed)
            {
                ltform = new LineType();
            }
            return ltform;
        }
        public static void ForbiddenEnable()
        {
            ltform.barButtonItem禁用.Enabled = true;
        }
        public static void ForbiddenDisable()
        {
            ltform.barButtonItem禁用.Enabled = false;
        }
        public static void UnforbiddenEnable()
        {
            ltform.barButtonItem反禁用.Enabled = true;
        }
        public static void UnforbiddenDisable()
        {
            ltform.barButtonItem反禁用.Enabled = false;
        }
        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            LineTypeQuery Frm = LineTypeQuery.GetInstance();
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
            LineTypeInsert Frm = LineTypeInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string cname = "", cnamemes = "";
            int cid = 0,saletypeid = 0, workshopid = 0;
            string saletypename = "", workshop = "";
            LineTypeQuery.GetInfo(ref cid, ref cname, ref cnamemes, ref saletypeid, ref saletypename, ref workshopid, ref workshop);
            if (cname != "")
            {
                LineTypeUpdate Frm = new LineTypeUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            LineTypeQuery.Delete();
            LineTypeQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            LineTypeQuery.RefreshEX();
        }

        private void LineType_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            LineTypeQuery Frm = LineTypeQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            //Frm.Width = this.Width - 15;
            //Frm.Height = this.Width - 60;
        }

        private void barButtonItem禁用_ItemClick(object sender, ItemClickEventArgs e)
        {
            LineTypeQuery.cDisable();
            LineTypeQuery.RefreshEX();

        }

        private void barButtonItem反禁用_ItemClick(object sender, ItemClickEventArgs e)
        {
            LineTypeQuery.cEnable();
            LineTypeQuery.RefreshEX();

        }
    }
}