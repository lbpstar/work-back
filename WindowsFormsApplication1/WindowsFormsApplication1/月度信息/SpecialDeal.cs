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
    public partial class SpecialDeal : DevExpress.XtraEditors.XtraForm
    {
        public SpecialDeal()
        {
            InitializeComponent();
        }
        private static SpecialDeal weform = null;
        public static SpecialDeal GetInstance()
        {
            if (weform == null || weform.IsDisposed)
            {
                weform = new SpecialDeal();
            }
            return weform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            SpecialDealQuery Frm = SpecialDealQuery.GetInstance();
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
            SpecialDealInsert Frm = SpecialDealInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string yyyymm = "", taskname = "";
            int id = 0, organizationid = 0, toorganizationid = 0;
            SpecialDealQuery.GetInfo(ref id, ref yyyymm, ref taskname, ref organizationid, ref toorganizationid);
            if (id!= 0)
            {
                SpecialDealUpdate Frm = new SpecialDealUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            SpecialDealQuery.Delete();
            SpecialDealQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            SpecialDealQuery.RefreshEX();
        }
       
        private void SpecialDeal_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            SpecialDealQuery Frm = SpecialDealQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            Frm.Height = this.Height - 20;
        }
    }
}