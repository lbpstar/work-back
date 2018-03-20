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
    public partial class TempEmpPrice : DevExpress.XtraEditors.XtraForm
    {
        public TempEmpPrice()
        {
            InitializeComponent();
        }
        private static TempEmpPrice reform = null;
        public static TempEmpPrice GetInstance()
        {
            if (reform == null || reform.IsDisposed)
            {
                reform = new TempEmpPrice();
            }
            return reform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempEmpPriceQuery Frm = TempEmpPriceQuery.GetInstance();
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
            TempEmpPriceInsert Frm = TempEmpPriceInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string id = "",supplier = "", begin_date = "", end_date = "", from_type = "0",price = "",meal_price = "",night_price = "",travel_price = "", regular_price = "";
            TempEmpPriceQuery.GetInfo(ref id,ref supplier, ref begin_date, ref end_date, ref from_type, ref price, ref meal_price, ref night_price, ref travel_price,ref regular_price);
            if (supplier != "")
            {
                TempEmpPriceUpdate Frm = new TempEmpPriceUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempEmpPriceQuery.Delete();
            TempEmpPriceQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempEmpPriceQuery.RefreshEX();
        }

        private void LineType_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            TempEmpPriceQuery Frm = TempEmpPriceQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

    }
}