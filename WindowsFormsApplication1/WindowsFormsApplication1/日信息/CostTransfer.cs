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
    public partial class CostTransfer : DevExpress.XtraEditors.XtraForm
    {
        public CostTransfer()
        {
            InitializeComponent();
        }
        private static CostTransfer weform = null;
        public static CostTransfer GetInstance()
        {
            if (weform == null || weform.IsDisposed)
            {
                weform = new CostTransfer();
            }
            return weform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            CostTransferQuery Frm = CostTransferQuery.GetInstance();
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
            CostTransferInsert Frm = CostTransferInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            CostTransferQuery.Delete();
            CostTransferQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            CostTransferQuery.RefreshEX();
        }

        private void CostTransfer_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            CostTransferQuery Frm = CostTransferQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            Frm.Height = this.Height - 20;
        }
    }
}