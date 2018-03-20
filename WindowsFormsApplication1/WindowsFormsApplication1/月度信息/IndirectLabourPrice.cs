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
    public partial class IndirectLabourPrice : DevExpress.XtraEditors.XtraForm
    {
        public IndirectLabourPrice()
        {
            InitializeComponent();
        }
        private static IndirectLabourPrice ilpform = null;
        public static string savetype = "";

        public static IndirectLabourPrice GetInstance()
        {
            if (ilpform == null || ilpform.IsDisposed)
            {
                ilpform = new IndirectLabourPrice();
            }
            return ilpform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            IndirectLabourPriceQuery Frm = IndirectLabourPriceQuery.GetInstance();
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
            IndirectLabourPriceInsert Frm = IndirectLabourPriceInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (IndirectLabourPriceQuery.GetRowCount() > 0)
            {
                IndirectLabourPriceUpdate Frm = IndirectLabourPriceUpdate.GetInstance();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            IndirectLabourPriceQuery.Delete();
            IndirectLabourPriceQuery.RefreshEX();
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
                IndirectLabourPriceInsert.Save();
                IndirectLabourPriceInsert.MyClose();
            }
            else if(savetype == "update")
            {
                IndirectLabourPriceUpdate.Save();
                IndirectLabourPriceUpdate.MyClose();
            }
            
        }

        private void IndirectLabourPrice_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            IndirectLabourPriceQuery Frm = IndirectLabourPriceQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }
    }
}