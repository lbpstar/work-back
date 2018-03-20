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
    public partial class WaterElectricity : DevExpress.XtraEditors.XtraForm
    {
        public WaterElectricity()
        {
            InitializeComponent();
        }
        private static WaterElectricity weform = null;
        public static WaterElectricity GetInstance()
        {
            if (weform == null || weform.IsDisposed)
            {
                weform = new WaterElectricity();
            }
            return weform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            WaterElectricityQuery Frm = WaterElectricityQuery.GetInstance();
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
            WaterElectricityInsert Frm = WaterElectricityInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string yyyymm = "";
            int id = 0, saletypeid = 0;
            decimal waterelectricity = 0;
            WaterElectricityQuery.GetInfo(ref id, ref yyyymm, ref saletypeid, ref waterelectricity);
            if (id!= 0)
            {
                WaterElectricityUpdate Frm = new WaterElectricityUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            WaterElectricityQuery.Delete();
            WaterElectricityQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            WaterElectricityQuery.RefreshEX();
        }

        private void LineType_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            WaterElectricityQuery Frm = WaterElectricityQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            Frm.Height = this.Height - 20;
        }

    }
}