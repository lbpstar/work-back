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
    public partial class DirectLabourPrice : DevExpress.XtraEditors.XtraForm
    {
        public DirectLabourPrice()
        {
            InitializeComponent();
        }
        private static DirectLabourPrice dlpform = null;
        public static string savetype = "";

        public static DirectLabourPrice GetInstance()
        {
            if (dlpform == null || dlpform.IsDisposed)
            {
                dlpform = new DirectLabourPrice();
            }
            return dlpform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            DirectLabourPriceQuery Frm = DirectLabourPriceQuery.GetInstance();
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
            DirectLabourPriceInsert Frm = DirectLabourPriceInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string month = DirectLabourPriceQuery.GetMonth();
            if (month != "")
            {
                DirectLabourPriceUpdate Frm = DirectLabourPriceUpdate.GetInstance();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            DirectLabourPriceQuery.Delete();
            DirectLabourPriceQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            DirectLabourPriceQuery.RefreshEX();
        }

        private void barButtonItem反禁用_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem保存_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(savetype == "insert")
            {
                DirectLabourPriceInsert.Save();
                DirectLabourPriceInsert.MyClose();
            }
            else if(savetype == "update")
            {
                DirectLabourPriceUpdate.Save();
                DirectLabourPriceUpdate.MyClose();
            }
            
        }

        private void DirectLabourPrice_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            DirectLabourPriceQuery Frm = DirectLabourPriceQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            Frm.Height = this.Height - 20;
        }
    }
}