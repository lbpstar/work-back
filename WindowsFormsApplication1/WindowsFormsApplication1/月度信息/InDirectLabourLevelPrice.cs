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
    public partial class InDirectLabourLevelPrice : DevExpress.XtraEditors.XtraForm
    {
        public InDirectLabourLevelPrice()
        {
            InitializeComponent();
        }
        private static InDirectLabourLevelPrice dlpform = null;
        public static string savetype = "";

        public static InDirectLabourLevelPrice GetInstance()
        {
            if (dlpform == null || dlpform.IsDisposed)
            {
                dlpform = new InDirectLabourLevelPrice();
            }
            return dlpform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            InDirectLabourLevelPriceQuery Frm = InDirectLabourLevelPriceQuery.GetInstance();
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
            InDirectLabourLevelPriceInsert Frm = InDirectLabourLevelPriceInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string yyyymm = "";
            int id = 0, level_begin = 0, level_end = 0;
            InDirectLabourLevelPriceQuery.GetInfo(ref id, ref yyyymm, ref level_begin, ref level_end);
            if (yyyymm != "")
            {
                InDirectLabourLevelPriceUpdate Frm = InDirectLabourLevelPriceUpdate.GetInstance();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            InDirectLabourLevelPriceQuery.Delete();
            InDirectLabourLevelPriceQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            InDirectLabourLevelPriceQuery.RefreshEX();
        }

        private void barButtonItem反禁用_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem保存_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(savetype == "insert")
            {
                InDirectLabourLevelPriceInsert.Save();
                InDirectLabourLevelPriceInsert.MyClose();
            }
            else if(savetype == "update")
            {
                InDirectLabourLevelPriceUpdate.Save();
                InDirectLabourLevelPriceUpdate.MyClose();
            }
            
        }

        private void DirectLabourPrice_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            InDirectLabourLevelPriceQuery Frm = InDirectLabourLevelPriceQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            Frm.Height = this.Height - 20;
        }
    }
}