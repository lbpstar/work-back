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
    public partial class TempWorkerPrice : DevExpress.XtraEditors.XtraForm
    {
        public TempWorkerPrice()
        {
            InitializeComponent();
        }
        private static TempWorkerPrice weform = null;
        public static TempWorkerPrice GetInstance()
        {
            if (weform == null || weform.IsDisposed)
            {
                weform = new TempWorkerPrice();
            }
            return weform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempWorkerPriceQuery Frm = TempWorkerPriceQuery.GetInstance();
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
            TempWorkerPriceInsert Frm = TempWorkerPriceInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string yyyymm = "";
            int id = 0, saletypeid = 0;
            decimal price = 0;
            TempWorkerPriceQuery.GetInfo(ref id, ref yyyymm, ref saletypeid, ref price);
            if (id!= 0)
            {
                TempWorkerPriceUpdate Frm = new TempWorkerPriceUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempWorkerPriceQuery.Delete();
            TempWorkerPriceQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempWorkerPriceQuery.RefreshEX();
        }

        private void TempWorkerPrice_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            TempWorkerPriceQuery Frm = TempWorkerPriceQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            Frm.Height = this.Height - 20;
        }
    }
}