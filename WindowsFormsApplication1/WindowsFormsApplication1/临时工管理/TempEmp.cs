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
    public partial class TempEmp : DevExpress.XtraEditors.XtraForm
    {
        public TempEmp()
        {
            InitializeComponent();
        }
        private static TempEmp reform = null;
        public static TempEmp GetInstance()
        {
            if (reform == null || reform.IsDisposed)
            {
                reform = new TempEmp();
            }
            return reform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempEmpQuery Frm = TempEmpQuery.GetInstance();
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
            bool right = TempEmpUpdate.ModifyRight();
            if(right)
            {
                TempEmpInsert Frm = TempEmpInsert.GetInstance();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
            else
            {
                MessageBox.Show("没有权限！");
            }
            
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string cno = "", cname = "", sex = "", register_date = "", leave_date = "", cfrom = "", dept1 = "", dept2 = "", dept3 = "", dept = "", id_number = "",phone_no = "",shift = "", status = "";
            int fromtype = 1;
            TempEmpQuery.GetInfo(ref cno, ref cname, ref sex, ref register_date,ref leave_date, ref cfrom, ref fromtype,ref dept1,ref dept2,ref dept3,ref dept, ref id_number,ref phone_no,ref shift, ref status);
            if (cno != "")
            {
                TempEmpUpdate Frm = new TempEmpUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool right = TempEmpUpdate.ModifyRight();
            if (right)
            {
                TempEmpQuery.Delete();
                TempEmpQuery.RefreshEX();
            }
            else
            {
                MessageBox.Show("没有权限！");
            }
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempEmpQuery.RefreshEX();
        }

        private void LineType_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            TempEmpQuery Frm = TempEmpQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            Frm.Height = this.Height - 20;
        }

        private void barButtonItem临时工导入_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool right = TempEmpUpdate.ModifyRight();
            if (right)
            {
                TempEmpImport Frm = TempEmpImport.GetInstance();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
            else
            {
                MessageBox.Show("没有权限！");
            }
        }

        private void barButtonItem批量更新_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool right = TempEmpUpdate.ModifyRight();
            if (right)
            {
                TempEmpImportUpdate Frm = TempEmpImportUpdate.GetInstance();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
            else
            {
                MessageBox.Show("没有权限！");
            }
        }
    }
}