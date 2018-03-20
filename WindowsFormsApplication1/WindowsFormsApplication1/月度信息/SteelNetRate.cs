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
    public partial class SteelNetRate : DevExpress.XtraEditors.XtraForm
    {
        public SteelNetRate()
        {
            InitializeComponent();
        }
        private static SteelNetRate weform = null;
        public static SteelNetRate GetInstance()
        {
            if (weform == null || weform.IsDisposed)
            {
                weform = new SteelNetRate();
            }
            return weform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            SteelNetRateQuery Frm = SteelNetRateQuery.GetInstance();
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
            SteelNetRateInsert Frm = SteelNetRateInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string yyyymm = "";
            int id = 0;
            decimal steelnetrate = 0;
            SteelNetRateQuery.GetInfo(ref id, ref yyyymm, ref steelnetrate);
            if (id!= 0)
            {
                SteelNetRateUpdate Frm = new SteelNetRateUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            SteelNetRateQuery.Delete();
            SteelNetRateQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            SteelNetRateQuery.RefreshEX();
        }

        private void SteelNetRate_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            SteelNetRateQuery Frm = SteelNetRateQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            Frm.Height = this.Height - 20;
        }
    }
}