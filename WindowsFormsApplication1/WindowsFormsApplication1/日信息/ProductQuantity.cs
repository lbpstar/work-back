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
    public partial class ProductQuantity : DevExpress.XtraEditors.XtraForm
    {
        public ProductQuantity()
        {
            InitializeComponent();
        }
        private static ProductQuantity weform = null;
        public static ProductQuantity GetInstance()
        {
            if (weform == null || weform.IsDisposed)
            {
                weform = new ProductQuantity();
            }
            return weform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            ProductQuantityQuery Frm = ProductQuantityQuery.GetInstance();
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
            ProductQuantityInsert Frm = ProductQuantityInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            ProductQuantityQuery.Delete();
            ProductQuantityQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            ProductQuantityQuery.RefreshEX();
        }

        private void ProductQuantity_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            ProductQuantityQuery Frm = ProductQuantityQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }
    }
}