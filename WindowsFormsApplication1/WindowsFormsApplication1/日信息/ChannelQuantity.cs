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
    public partial class ChannelQuantity : DevExpress.XtraEditors.XtraForm
    {
        public ChannelQuantity()
        {
            InitializeComponent();
        }
        private static ChannelQuantity weform = null;
        public static ChannelQuantity GetInstance()
        {
            if (weform == null || weform.IsDisposed)
            {
                weform = new ChannelQuantity();
            }
            return weform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            ChannelQuantityQuery Frm = ChannelQuantityQuery.GetInstance();
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
            ChannelQuantityInsert Frm = ChannelQuantityInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            ChannelQuantityQuery.Delete();
            ChannelQuantityQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            ChannelQuantityQuery.RefreshEX();
        }

        private void ChannelQuantity_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            ChannelQuantityQuery Frm = ChannelQuantityQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }
    }
}