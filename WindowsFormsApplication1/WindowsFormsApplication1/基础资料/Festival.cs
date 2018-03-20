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
    public partial class Festival : DevExpress.XtraEditors.XtraForm
    {
        public Festival()
        {
            InitializeComponent();
        }
        private static Festival wtform = null;

        public static Festival GetInstance()
        {
            if (wtform == null || wtform.IsDisposed)
            {
                wtform = new Festival();
            }
            return wtform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            FestivalQuery Frm = FestivalQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void SaleType_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            FestivalQuery Frm = FestivalQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            Frm.Height = this.Height - 20;
            //Frm.Width = this.Width - 15;
            //Frm.Height = this.Width - 60;
        }

        private void barButtonItem退出_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem新增_ItemClick(object sender, ItemClickEventArgs e)
        {
            FestivalInsert Frm = FestivalInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            //Frm.MdiParent = this.ParentForm;
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string cnote = "",cdate = "";
            int ctype = 0;
            FestivalQuery.GetInfo(ref cdate, ref ctype,ref cnote);
            if (cdate != "")
            {
                //SaleTypeUpdate Frm = SaleTypeUpdate.GetInstance();
                FestivalUpdate Frm = new FestivalUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            FestivalQuery.Delete();
            FestivalQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            FestivalQuery.RefreshEX();
        }

    }
}