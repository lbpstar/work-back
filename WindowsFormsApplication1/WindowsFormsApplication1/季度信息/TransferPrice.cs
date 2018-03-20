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
    public partial class TransferPrice : DevExpress.XtraEditors.XtraForm
    {
        public TransferPrice()
        {
            InitializeComponent();
        }
        private static TransferPrice weform = null;
        public static TransferPrice GetInstance()
        {
            if (weform == null || weform.IsDisposed)
            {
                weform = new TransferPrice();
            }
            return weform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            TransferPriceQuery Frm = TransferPriceQuery.GetInstance();
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
            TransferPriceInsert Frm = TransferPriceInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string yyyy = "";
            int id = 0, saletypeid = 0,quarterid = 0;
            decimal standardpoint = 0;
            TransferPriceQuery.GetInfo(ref id, ref yyyy, ref quarterid,ref saletypeid, ref standardpoint);
            if (id!= 0)
            {
                TransferPriceUpdate Frm = new TransferPriceUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            TransferPriceQuery.Delete();
            TransferPriceQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            TransferPriceQuery.RefreshEX();
        }

        private void CostRate_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            TransferPriceQuery Frm = TransferPriceQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            Frm.Height = this.Height - 20;
        }
    }
}