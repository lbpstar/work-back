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
    public partial class PointCount : DevExpress.XtraEditors.XtraForm
    {
        public PointCount()
        {
            InitializeComponent();
        }
        private static PointCount weform = null;
        public static PointCount GetInstance()
        {
            if (weform == null || weform.IsDisposed)
            {
                weform = new PointCount();
            }
            return weform;
        }

        private void barButtonItem退出_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void SteelNetRate_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            PointCountRaw Frm = PointCountRaw.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem汇总_ItemClick(object sender, ItemClickEventArgs e)
        {
            PointCountSum Frm = PointCountSum.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItemERP数据_ItemClick(object sender, ItemClickEventArgs e)
        {
            PointCountRaw Frm = PointCountRaw.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }
    }
}