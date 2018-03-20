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
    public partial class SMTCost : DevExpress.XtraEditors.XtraForm
    {
        public SMTCost()
        {
            InitializeComponent();
        }
        private static SMTCost dlform = null;
        public static SMTCost GetInstance()
        {
            if (dlform == null || dlform.IsDisposed)
            {
                dlform = new SMTCost();
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

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            DepreciationUpdate Frm = new DepreciationUpdate();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
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
        }

        private void barButtonItem计算_ItemClick(object sender, ItemClickEventArgs e)
        {
            SMTCosting Frm = SMTCosting.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void SMTCost_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            SMTCosting Frm = SMTCosting.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem总盈亏_ItemClick(object sender, ItemClickEventArgs e)
        {
            SMTCostDay Frm = SMTCostDay.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem总盈亏月_ItemClick(object sender, ItemClickEventArgs e)
        {
            SMTCostMonth Frm = SMTCostMonth.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem部门盈亏日_ItemClick(object sender, ItemClickEventArgs e)
        {
            SMTCostDeptDay Frm = SMTCostDeptDay.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem部门盈亏月_ItemClick(object sender, ItemClickEventArgs e)
        {
            SMTCostDeptMonth Frm = SMTCostDeptMonth.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }
    }
}