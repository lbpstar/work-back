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
    public partial class CostRate : DevExpress.XtraEditors.XtraForm
    {
        public CostRate()
        {
            InitializeComponent();
        }
        private static CostRate weform = null;
        public static CostRate GetInstance()
        {
            if (weform == null || weform.IsDisposed)
            {
                weform = new CostRate();
            }
            return weform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            CostRateQuery Frm = CostRateQuery.GetInstance();
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
            CostRateInsert Frm = CostRateInsert.GetInstance();
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
            CostRateQuery.GetInfo(ref id, ref yyyy, ref quarterid,ref saletypeid, ref standardpoint);
            if (id!= 0)
            {
                CostRateUpdate Frm = new CostRateUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            CostRateQuery.Delete();
            CostRateQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            CostRateQuery.RefreshEX();
        }

        private void CostRate_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            CostRateQuery Frm = CostRateQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            Frm.Height = this.Height - 20;
        }
    }
}