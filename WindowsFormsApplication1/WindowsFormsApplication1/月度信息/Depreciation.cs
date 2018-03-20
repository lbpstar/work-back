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
    public partial class Depreciation : DevExpress.XtraEditors.XtraForm
    {
        public Depreciation()
        {
            InitializeComponent();
        }
        private static Depreciation dlform = null;
        public static Depreciation GetInstance()
        {
            if (dlform == null || dlform.IsDisposed)
            {
                dlform = new Depreciation();
            }
            return dlform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            DepreciationQuery Frm = DepreciationQuery.GetInstance();
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
            DepreciationInsert Frm = DepreciationInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string yyyymm = "";
            int id = 0, saletypeid = 0;
            decimal depreciation = 0;
            DepreciationQuery.GetInfo(ref id, ref yyyymm, ref saletypeid, ref depreciation);
            if (id!= 0)
            {
                DepreciationUpdate Frm = new DepreciationUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            DepreciationQuery.Delete();
            DepreciationQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            DepreciationQuery.RefreshEX();
        }

        private void LineType_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            DepreciationQuery Frm = DepreciationQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            Frm.Height = this.Height - 20;
        }

    }
}