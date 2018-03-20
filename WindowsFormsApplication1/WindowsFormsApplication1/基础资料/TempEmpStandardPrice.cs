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
    public partial class TempEmpStandardPrice : DevExpress.XtraEditors.XtraForm
    {
        public TempEmpStandardPrice()
        {
            InitializeComponent();
        }
        private static TempEmpStandardPrice reform = null;
        public static TempEmpStandardPrice GetInstance()
        {
            if (reform == null || reform.IsDisposed)
            {
                reform = new TempEmpStandardPrice();
            }
            return reform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempEmpStandardPriceQuery Frm = TempEmpStandardPriceQuery.GetInstance();
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
            TempEmpStandardPriceInsert Frm = TempEmpStandardPriceInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string id = "",begin_date = "", end_date = "",price = "",insurance_price = "";
            TempEmpStandardPriceQuery.GetInfo(ref id,ref begin_date, ref end_date, ref price,ref insurance_price);
            if (id != "")
            {
                TempEmpStandardPriceUpdate Frm = new TempEmpStandardPriceUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempEmpStandardPriceQuery.Delete();
            TempEmpStandardPriceQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempEmpStandardPriceQuery.RefreshEX();
        }

        private void LineType_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            TempEmpStandardPriceQuery Frm = TempEmpStandardPriceQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

    }
}