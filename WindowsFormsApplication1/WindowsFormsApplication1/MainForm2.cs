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
    public partial class MainForm2 : DevExpress.XtraEditors.XtraForm
    {
        public MainForm2()
        {
            InitializeComponent();
        }
        private static MainForm2 mf = null;

        public static void ShowMe()
        {
            mf.Visible = true;
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
            //mf = this;
            //this.Visible = false;
            //Logon l = new Logon();
            //l.Show();

            //MessageBox.Show(accordionControl1.Elements[0].Elements[0].ToString());
            //MessageBox.Show(accordionControl1.GetElements()[0].Tag.ToString());
            //MessageBox.Show(accordionControl1.GetElements()[0].Name.ToString());
            CheckRight();
            ConnDB conn = new ConnDB();
            string sql,formname;
            sql = "SELECT top 1 f.form_name from COST_USER i left join COST_USER_ROLE r on i.cid = r.USER_ID and r.HAVE_RIGHT = 'true' left join cost_role_form f on r.ROLE_ID = f.role_id where i.CNAME = '" + Logon.GetCname() + "'";
            DataSet ds = conn.ReturnDataSet(sql);
            formname = ds.Tables[0].Rows[0][0].ToString();
            if(formname == "SMTCostDay")
            {
                SMTCostDay Frm = SMTCostDay.GetInstance();
                Frm.MdiParent = this;
                //Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
                barManager1.BeginUpdate();
                barManager1.Bars["Tools"].Visible = false;
                barManager1.EndUpdate();
            }
            else if(formname == "EMSSMTCostDay")
            {
                EMSSMTCostDay Frm = EMSSMTCostDay.GetInstance();
                Frm.MdiParent = this;
                //Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
                barManager1.BeginUpdate();
                barManager1.Bars["Tools"].Visible = false;
                barManager1.EndUpdate();
            }
            else if (formname == "SMTHHCostDay")
            {
                SMTHHCostDay Frm = SMTHHCostDay.GetInstance();
                Frm.MdiParent = this;
                //Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
                barManager1.BeginUpdate();
                barManager1.Bars["Tools"].Visible = false;
                barManager1.EndUpdate();
            }
            else if (formname == "ProductCostDay")
            {
                ProductCostDay Frm = ProductCostDay.GetInstance();
                Frm.MdiParent = this;
                //Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
                barManager1.BeginUpdate();
                barManager1.Bars["Tools"].Visible = false;
                barManager1.EndUpdate();
            }
            else if (formname == "SystemCostDay")
            {
                SystemCostDay Frm = SystemCostDay.GetInstance();
                Frm.MdiParent = this;
                //Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
                barManager1.BeginUpdate();
                barManager1.Bars["Tools"].Visible = false;
                barManager1.EndUpdate();
            }
            else if (formname == "TempEmpAtt")
            {
                TempEmpAtt Frm = TempEmpAtt.GetInstance();
                Frm.MdiParent = this;
                //Frm.WindowState = FormWindowState.Maximized;
                this.Text = "临时工管理";
                Frm.Show();
            }

            

        }
        private  void CheckRight()
        {
            ConnDB conn = new ConnDB();
            string sql;
            sql = "select m.module_name from COST_USER i left join COST_USER_ROLE r on i.CID = r.USER_ID and r.HAVE_RIGHT = 'true' left join COST_ROLE_PERMISSION p on r.ROLE_ID = p.ROLE_ID and p.HAVE_RIGHT = 'true' left join COST_MODULE_PERMISSION m on p.PERMISSION_ID = m.CID where i.CNAME = '" + Logon.GetCname() + "'";
            DataSet ds = conn.ReturnDataSet(sql);
            //accordionControl1.Visible = false;
            for (int i = 0; i < accordionControl1.GetElements().Count; i++)
            {
                accordionControl1.GetElements()[i].Visible = false;     
            }
            for (int i = 0;i< accordionControl1.GetElements().Count;i++)
            {
                //accordionControl1.GetElements()[i].Visible = false;
                for (int j = 0;j<ds.Tables[0].Rows.Count;j++)
                {
                    if(accordionControl1.GetElements()[i].Tag.ToString() == ds.Tables[0].Rows[j][0].ToString())
                    {
                        accordionControl1.GetElements()[i].Visible = true;
                    }
                }
            }
            accordionControlElement退出系统.Visible = true;
            //accordionControl1.Visible = true;
            conn.Close();
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

        private void accordionControlElement退出系统_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void accordionControlElement营业分类_Click(object sender, EventArgs e)
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

        private void accordionControlElement拉别_Click(object sender, EventArgs e)
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

        private void accordionControlElement部门_Click(object sender, EventArgs e)
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

        private void accordionControlElement职位_Click(object sender, EventArgs e)
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

        private void accordionControlElement直接人工_Click(object sender, EventArgs e)
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

        private void accordionControlElement间接人工_Click(object sender, EventArgs e)
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

        private void accordionControlElement上班类型_Click(object sender, EventArgs e)
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

        private void accordionControlElement直接人工费率_Click(object sender, EventArgs e)
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

        private void accordionControlElement间接人工费率_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "IndirectLabourLevelPrice")
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
                InDirectLabourLevelPrice Frm = InDirectLabourLevelPrice.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement折旧费用_Click(object sender, EventArgs e)
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

        private void accordionControlElement租赁费用_Click(object sender, EventArgs e)
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

        private void accordionControlElement水电费_Click(object sender, EventArgs e)
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

        private void accordionControlElement标准单点成本_Click(object sender, EventArgs e)
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

        private void accordionControlElement部门标准单点成本_Click(object sender, EventArgs e)
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

        private void accordionControlElement钢网成本占比_Click(object sender, EventArgs e)
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

        private void accordionControlElement成本比率_Click(object sender, EventArgs e)
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

        private void accordionControlElement直接人工考勤_Click(object sender, EventArgs e)
        {
                bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "DirectLabourAttendance")
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
                DirectLabourAttendance Frm = DirectLabourAttendance.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement间接人工考勤_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "IndirectLabourAttendance")
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
                IndirectLabourAttendance Frm = IndirectLabourAttendance.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElementEMSHH产出工时_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "HHImport")
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
                HHImport Frm = HHImport.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }
        private void accordionControlElement点数汇总_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "PointCountSum")
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
                PointCountSum Frm = PointCountSum.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement原始点数_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "PointCountRaw")
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
                PointCountRaw Frm = PointCountRaw.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

       

        private void accordionControlElement成本运算_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SMTCosting")
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
                SMTCosting Frm = SMTCosting.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement单日成本_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SMTCostDay")
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
                SMTCostDay Frm = SMTCostDay.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement月成本_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SMTCostMonth")
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
                SMTCostMonth Frm = SMTCostMonth.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }
        private void accordionControlElement部门日成本_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SMTCostDeptDay")
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
                SMTCostDeptDay Frm = SMTCostDeptDay.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement部门月成本_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SMTCostDeptMonth")
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
                SMTCostDeptMonth Frm = SMTCostDeptMonth.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement日成本报表_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SMTCostDayChart")
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
                SMTCostDayChart Frm = SMTCostDayChart.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement部门日成本报表_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SMTCostDeptDayChart")
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
                SMTCostDeptDayChart Frm = SMTCostDeptDayChart.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElementEMSSMT成本运算_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "EMSSMTCosting")
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
                EMSSMTCosting Frm = EMSSMTCosting.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElementEMSSMT日成本_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "EMSSMTCostDay")
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
                EMSSMTCostDay Frm = EMSSMTCostDay.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElementEMSSMT月成本_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "EMSSMTCostMonth")
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
                EMSSMTCostMonth Frm = EMSSMTCostMonth.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElementEMSSMT部门日成本_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "EMSSMTCostDeptDay")
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
                EMSSMTCostDeptDay Frm = EMSSMTCostDeptDay.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElementEMSSMT部门月成本_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "EMSSMTCostDeptMonth")
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
                EMSSMTCostDeptMonth Frm = EMSSMTCostDeptMonth.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElementEMSSMT日成本报表_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "EMSSMTCostDayChart")
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
                EMSSMTCostDayChart Frm = EMSSMTCostDayChart.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElementEMSSMT部门日成本报表_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "EMSSMTCostDeptDayChart")
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
                EMSSMTCostDeptDayChart Frm = EMSSMTCostDeptDayChart.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement特殊工单归集_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SpecialDeal")
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
                SpecialDeal Frm = SpecialDeal.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElementSMTHH成本运算_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SMTHHCosting")
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
                SMTHHCosting Frm = SMTHHCosting.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElementSMTHH日成本_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SMTHHCostDay")
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
                SMTHHCostDay Frm = SMTHHCostDay.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElementSMTHH月成本_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SMTHHCostMonth")
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
                SMTHHCostMonth Frm = SMTHHCostMonth.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElementSMTHH部门日成本_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SMTHHCostDeptDay")
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
                SMTHHCostDeptDay Frm = SMTHHCostDeptDay.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElementSMTHH部门月成本_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SMTHHCostDeptMonth")
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
                SMTHHCostDeptMonth Frm = SMTHHCostDeptMonth.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElementSMTHH日成本报表_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SMTHHCostDayChart")
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
                SMTHHCostDayChart Frm = SMTHHCostDayChart.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElementSMTHH部门日成本报表_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SMTHHCostDeptDayChart")
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
                SMTHHCostDeptDayChart Frm = SMTHHCostDeptDayChart.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement转嫁费率_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "TransferPrice")
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
                TransferPrice Frm = TransferPrice.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement费用转嫁_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "CostTransfer")
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
                CostTransfer Frm = CostTransfer.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement终端台数_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "ProductQuantityImport")
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
                ProductQuantityImport Frm = ProductQuantityImport.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement临时工工时_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "TempWorker")
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
                TempWorker Frm = TempWorker.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement临时工费率_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "TempWorkerPrice")
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
                TempWorkerPrice Frm = TempWorkerPrice.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement主营综合费用_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "CompositeExpense")
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
                CompositeExpense Frm = CompositeExpense.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement终端成本运算_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "ProductCosting")
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
                ProductCosting Frm = ProductCosting.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement终端日成本_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "ProductCostDay")
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
                ProductCostDay Frm = ProductCostDay.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement终端月成本_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "ProductCostMonth")
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
                ProductCostMonth Frm = ProductCostMonth.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement终端部门日成本_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "ProductCostDeptDay")
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
                ProductCostDeptDay Frm = ProductCostDeptDay.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement终端部门月成本_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "ProductCostDeptMonth")
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
                ProductCostDeptMonth Frm = ProductCostDeptMonth.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement终端日成本报表_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "ProductCostDayChart")
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
                ProductCostDayChart Frm = ProductCostDayChart.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement终端部门日成本报表_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "ProductCostDeptDayChart")
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
                ProductCostDeptDayChart Frm = ProductCostDeptDayChart.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement信道数_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "ChannelQuantity")
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
                ChannelQuantity Frm = ChannelQuantity.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement系统成本运算_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SystemCosting")
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
                SystemCosting Frm = SystemCosting.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement系统日成本_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SystemCostDay")
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
                SystemCostDay Frm = SystemCostDay.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement系统月成本_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SystemCostMonth")
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
                SystemCostMonth Frm = SystemCostMonth.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement系统部门日成本_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SystemCostDeptDay")
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
                SystemCostDeptDay Frm = SystemCostDeptDay.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement系统部门月成本_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SystemCostDeptMonth")
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
                SystemCostDeptMonth Frm = SystemCostDeptMonth.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement系统日成本报表_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SystemCostDayChart")
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
                SystemCostDayChart Frm = SystemCostDayChart.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement系统部门日成本报表_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SystemCostDeptDayChart")
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
                SystemCostDeptDayChart Frm = SystemCostDeptDayChart.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement角色管理_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "Role")
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
                Role Frm = Role.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement用户管理_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "User")
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
                User Frm = User.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void MainForm2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void accordionControlElement部门对照表_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "DeptMap")
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
                DeptMap Frm = DeptMap.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement考勤_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "AttendanceImport")
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
                AttendanceImport Frm = AttendanceImport.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement标准单机工时_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "HHStandardHours")
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
                HHStandardHours Frm = HHStandardHours.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement员工等级_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "EmpLevelImport")
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
                EmpLevelImport Frm = EmpLevelImport.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement部门成本比率_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "CostDeptRate")
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
                CostDeptRate Frm = CostDeptRate.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement临时工基本信息_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "TempEmp")
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
                TempEmp Frm = TempEmp.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement终端台数比率_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "QuantityRate")
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
                QuantityRate Frm = QuantityRate.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement考勤班次_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "Shift")
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
                Shift Frm = Shift.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement临时工考勤_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "TempEmpAtt")
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
                TempEmpAtt Frm = TempEmpAtt.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement按部门审批_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "UserDeptCheck")
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
                UserDeptCheck Frm = UserDeptCheck.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement考勤报表_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "TempEmpAttReport")
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
                TempEmpAttReport Frm = TempEmpAttReport.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement出勤价格_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "TempEmpPrice")
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
                TempEmpPrice Frm = TempEmpPrice.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement节日设置_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "Festival")
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
                Festival Frm = Festival.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement薪酬计算_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "TempEmpAttSalary")
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
                TempEmpAttSalary Frm = TempEmpAttSalary.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement1_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SMTCostDayPointChart")
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
                SMTCostDayPointChart Frm = SMTCostDayPointChart.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement部门日成本单点报表_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SMTCostDeptDayPointChart")
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
                SMTCostDeptDayPointChart Frm = SMTCostDeptDayPointChart.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElementEMSSMT日成本单点报表_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "EMSSMTCostDayPointChart")
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
                EMSSMTCostDayPointChart Frm = EMSSMTCostDayPointChart.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElementEMSSMT部门日成本单点报表_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "EMSSMTCostDeptDayPointChart")
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
                EMSSMTCostDeptDayPointChart Frm = EMSSMTCostDeptDayPointChart.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElementSMTHH日成本单小时报表_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SMTHHCostDayPointChart")
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
                SMTHHCostDayPointChart Frm = SMTHHCostDayPointChart.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElementSMTHH部门日成本单小时报表_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SMTHHCostDeptDayPointChart")
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
                SMTHHCostDeptDayPointChart Frm = SMTHHCostDeptDayPointChart.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement系统日成本单信道报表_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SystemCostDayPointChart")
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
                SystemCostDayPointChart Frm = SystemCostDayPointChart.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement系统部门日成本单信道报表_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "SystemCostDeptDayPointChart")
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
                SystemCostDeptDayPointChart Frm = SystemCostDeptDayPointChart.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement终端日成本单台报表_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "ProductCostDayPointChart")
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
                ProductCostDayPointChart Frm = ProductCostDayPointChart.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement终端部门日成本单台报表_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "ProductCostDeptDayPointChart")
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
                ProductCostDeptDayPointChart Frm = ProductCostDeptDayPointChart.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement最低工资标准_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "TempEmpStandardPrice")
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
                TempEmpStandardPrice Frm = TempEmpStandardPrice.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement其它费用_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "TempEmpExpense")
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
                TempEmpExpense Frm = TempEmpExpense.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement交通补贴_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "TempEmpTravelExpense")
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
                TempEmpTravelExpense Frm = TempEmpTravelExpense.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement人员状态分布_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "TempEmpStatus")
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
                TempEmpStatus Frm = TempEmpStatus.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement费用分析_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "TempEmpAttSalarySum")
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
                TempEmpAttSalarySum Frm = TempEmpAttSalarySum.GetInstance();
                Frm.MdiParent = this;
                Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement月末关帐_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "TempEmpClose")
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
                TempEmpClose Frm = TempEmpClose.GetInstance();
                Frm.MdiParent = this;
                //Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement线体_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "LineType")
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
                LineType Frm = LineType.GetInstance();
                Frm.MdiParent = this;
                //Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }


        private void accordionControlElement用料明细_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "MatlQuery")
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
                MatlQuery Frm = MatlQuery.GetInstance();
                Frm.MdiParent = this;
                //Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement线体用料_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "MatlQueryLine")
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
                MatlQueryLine Frm = MatlQueryLine.GetInstance();
                Frm.MdiParent = this;
                //Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }

        private void accordionControlElement物料单价_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "MatlPriceImport")
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
                MatlPriceImport Frm = MatlPriceImport.GetInstance();
                Frm.MdiParent = this;
                //Frm.WindowState = FormWindowState.Maximized;
                Frm.Show();
            }
        }
    }
}