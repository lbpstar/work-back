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
using DevExpress.XtraTabbedMdi;

namespace SMTCost
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void barButtonItem老版入口_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem退出系统_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barManager1_Merge(object sender, DevExpress.XtraBars.BarManagerMergeEventArgs e)
        {
            var parentBM = sender as BarManager;
            parentBM.BeginUpdate();
            int count = e.ChildManager.Bars.Count;
            for (int i = 0; i < count; i++)
            {
                if (e.ChildManager.Bars[i].BarName == "ToolsSaleType")
                {
                    parentBM.Bars["Tools"].Visible = true;
                    parentBM.Bars["Tools"].Merge(e.ChildManager.Bars["ToolsSaleType"]);
                }
                else if (e.ChildManager.Bars[i].BarName == "Tools2")
                {
                    parentBM.Bars["Tools"].Visible = true;
                    parentBM.Bars["Tools"].Merge(e.ChildManager.Bars["Tools2"]);
                }

            }
            parentBM.EndUpdate();
        }

        private void barManager1_UnMerge(object sender, DevExpress.XtraBars.BarManagerMergeEventArgs e)
        {
            var parentBM = sender as BarManager;
            parentBM.BeginUpdate();
            int count = e.ChildManager.Bars.Count;
            for (int i = 0; i < count; i++)
            {
                if (e.ChildManager.Bars[i].BarName == "ToolsSaleType")
                {
                    parentBM.Bars["Tools"].UnMerge();
                    parentBM.Bars["Tools"].Visible = false;
                }
                else if (e.ChildManager.Bars[i].BarName == "Tools2")
                {
                    parentBM.Bars["Tools"].UnMerge();
                    parentBM.Bars["Tools"].Visible = false;
                }

            }
            parentBM.EndUpdate();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //默认打开界面
            //XtraTabbedMdiManager mdiManager = new XtraTabbedMdiManager();
            //mdiManager.MdiParent = this;

            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SaleType")
                {
                    f.Visible = true;
                    f.Activate();
                    //f.WindowState = FormWindowState.Maximized;
                    isopen = false;
                    break;
                }
            }
            if (isopen)
            {
                SaleType Frm = SaleType.GetInstance();
                Frm.MdiParent = this;
                //Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void barButtonItem营业分类_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SaleType")
                {
                    f.Visible = true;
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    isopen = false;
                    break;
                }
            }
            if (isopen)
            {
                SaleType Frm = SaleType.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void barButtonItem拉别_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "LineType")
                {
                    f.Visible = true;
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    isopen = false;
                    break;
                }
            }
            if (isopen)
            {
                LineType Frm = LineType.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void barButtonItem部门_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "Dept")
                {
                    f.Visible = true;
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    isopen = false;
                    break;
                }
            }
            if (isopen)
            {
                Dept Frm = Dept.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void barButtonItem职位_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "Position")
                {
                    f.Visible = true;
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    isopen = false;
                    break;
                }
            }
            if (isopen)
            {
                Position Frm = Position.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void barButtonItem直接人工_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "DirectLabour")
                {
                    f.Visible = true;
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    isopen = false;
                    break;
                }
            }
            if (isopen)
            {
                DirectLabour Frm = DirectLabour.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void barButtonItem间接人工_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "IndirectLabour")
                {
                    f.Visible = true;
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    isopen = false;
                    break;
                }
            }
            if (isopen)
            {
                IndirectLabour Frm = IndirectLabour.GetInstance(); 
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void barButtonItem上班类型_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "WorkType")
                {
                    f.Visible = true;
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    isopen = false;
                    break;
                }
            }
            if (isopen)
            {
                WorkType Frm = WorkType.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void barButtonItem直接人工费率_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "DirectLabourPrice")
                {
                    f.Visible = true;
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    isopen = false;
                    break;
                }
            }
            if (isopen)
            {
                DirectLabourPrice Frm = DirectLabourPrice.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void barButtonItem间接人工费率_ItemClick(object sender, ItemClickEventArgs e)
        {
                bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "IndirectLabourPrice")
                {
                    f.Visible = true;
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    isopen = false;
                    break;
                }
            }
            if (isopen)
            {
                IndirectLabourPrice Frm = IndirectLabourPrice.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }

        }

        private void barButtonItem折旧费用_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "Depreciation")
                {
                    f.Visible = true;
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    isopen = false;
                    break;
                }
            }
            if (isopen)
            {
                Depreciation Frm = Depreciation.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void barButtonItem租赁费用_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "RentExpense")
                {
                    f.Visible = true;
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    isopen = false;
                    break;
                }
            }
            if (isopen)
            {
                RentExpense Frm = RentExpense.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void barButtonItem水电费_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "WaterElectricity")
                {
                    f.Visible = true;
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    isopen = false;
                    break;
                }
            }
            if (isopen)
            {
                WaterElectricity Frm = WaterElectricity.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void barButtonItem标准单点成本_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "StandardPoint")
                {
                    f.Visible = true;
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    isopen = false;
                    break;
                }
            }
            if (isopen)
            {
                StandardPoint Frm = StandardPoint.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void barButtonItem成本比率_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "CostRate")
                {
                    f.Visible = true;
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    isopen = false;
                    break;
                }
            }
            if (isopen)
            {
                CostRate Frm = CostRate.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void barButtonItem部门标准单点成本_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "DeptStandardPoint")
                {
                    f.Visible = true;
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    isopen = false;
                    break;
                }
            }
            if (isopen)
            {
                DeptStandardPoint Frm = DeptStandardPoint.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void barButtonItem钢网成本占比_ItemClick(object sender, ItemClickEventArgs e)
        {
            
                bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SteelNetRate")
                {
                    f.Visible = true;
                    f.Activate();
                    f.WindowState = FormWindowState.Maximized;
                    isopen = false;
                    break;
                }
            }
            if (isopen)
            {
                SteelNetRate Frm = SteelNetRate.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }
    }
}