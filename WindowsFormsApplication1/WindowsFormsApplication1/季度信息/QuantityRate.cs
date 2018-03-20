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
    public partial class QuantityRate : DevExpress.XtraEditors.XtraForm
    {
        public QuantityRate()
        {
            InitializeComponent();
        }
        private static QuantityRate weform = null;
        public static QuantityRate GetInstance()
        {
            if (weform == null || weform.IsDisposed)
            {
                weform = new QuantityRate();
            }
            return weform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            QuantityRateQuery Frm = QuantityRateQuery.GetInstance();
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
            QuantityRateInsert Frm = QuantityRateInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string yyyy = "";
            int id = 0,quarterid = 0;
            decimal standardpoint = 0;
            QuantityRateQuery.GetInfo(ref id, ref yyyy, ref quarterid, ref standardpoint);
            if (id!= 0)
            {
                QuantityRateUpdate Frm = new QuantityRateUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            QuantityRateQuery.Delete();
            QuantityRateQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            QuantityRateQuery.RefreshEX();
        }

        private void CostRate_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            QuantityRateQuery Frm = QuantityRateQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            Frm.Height = this.Height - 20;
        }
    }
}