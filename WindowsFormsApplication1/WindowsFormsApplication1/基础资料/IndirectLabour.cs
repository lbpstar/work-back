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
    public partial class IndirectLabour : DevExpress.XtraEditors.XtraForm
    {
        public IndirectLabour()
        {
            InitializeComponent();
        }
        private static IndirectLabour idlform = null;
        public static IndirectLabour GetInstance()
        {
            if (idlform == null || idlform.IsDisposed)
            {
                idlform = new IndirectLabour();
            }
            return idlform;
        }
        public static void ForbiddenEnable()
        {
            idlform.barButtonItem禁用.Enabled = true;
        }
        public static void ForbiddenDisable()
        {
            idlform.barButtonItem禁用.Enabled = false;
        }
        public static void UnforbiddenEnable()
        {
            idlform.barButtonItem反禁用.Enabled = true;
        }
        public static void UnforbiddenDisable()
        {
            idlform.barButtonItem反禁用.Enabled = false;
        }
        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            IndirectLabourQuery Frm = IndirectLabourQuery.GetInstance();
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
            IndirectLabourInsert Frm = IndirectLabourInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string cno = "", cname = "", positionname = "", deptname = "";
            int id = 0, positionid = 0, deptid = 0,person_level = 0;
            IndirectLabourQuery.GetInfo(ref id, ref cno, ref cname, ref positionid, ref positionname, ref person_level,ref deptid, ref deptname);
            if (cno != "")
            {
                IndirectLabourUpdate Frm = new IndirectLabourUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            IndirectLabourQuery.Delete();
            IndirectLabourQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            IndirectLabourQuery.RefreshEX();
        }

        private void LineType_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            IndirectLabourQuery Frm = IndirectLabourQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            //Frm.Width = this.Width - 15;
            //Frm.Height = this.Width - 60;
        }

        private void barButtonItem禁用_ItemClick(object sender, ItemClickEventArgs e)
        {
            IndirectLabourQuery.cDisable();
            IndirectLabourQuery.RefreshEX();

        }

        private void barButtonItem反禁用_ItemClick(object sender, ItemClickEventArgs e)
        {
            IndirectLabourQuery.cEnable();
            IndirectLabourQuery.RefreshEX();

        }
    }
}