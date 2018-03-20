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
    public partial class CostDeptRate : DevExpress.XtraEditors.XtraForm
    {
        public CostDeptRate()
        {
            InitializeComponent();
        }
        private static CostDeptRate weform = null;
        public static CostDeptRate GetInstance()
        {
            if (weform == null || weform.IsDisposed)
            {
                weform = new CostDeptRate();
            }
            return weform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            CostDeptRateQuery Frm = CostDeptRateQuery.GetInstance();
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
            CostDeptRateInsert Frm = CostDeptRateInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string yyyy = "";
            int id = 0, saletypeid = 0,quarterid = 0,deptid = 0;
            decimal standardpoint = 0;
            CostDeptRateQuery.GetInfo(ref id, ref yyyy, ref quarterid,ref saletypeid, ref deptid,ref standardpoint);
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
            CostDeptRateQuery.Delete();
            CostDeptRateQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            CostDeptRateQuery.RefreshEX();
        }

        private void CostRate_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            CostDeptRateQuery Frm = CostDeptRateQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            Frm.Height = this.Height - 20;
        }
    }
}