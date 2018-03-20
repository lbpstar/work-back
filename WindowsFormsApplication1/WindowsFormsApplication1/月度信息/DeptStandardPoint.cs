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
    public partial class DeptStandardPoint : DevExpress.XtraEditors.XtraForm
    {
        public DeptStandardPoint()
        {
            InitializeComponent();
        }
        private static DeptStandardPoint weform = null;
        public static DeptStandardPoint GetInstance()
        {
            if (weform == null || weform.IsDisposed)
            {
                weform = new DeptStandardPoint();
            }
            return weform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeptStandardPointQuery Frm = DeptStandardPointQuery.GetInstance();
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
            DeptStandardPointInsert Frm = DeptStandardPointInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string yyyymm = "";
            int id = 0, saletypeid = 0,deptid = 0;
            decimal deptstandardpoint = 0;
            DeptStandardPointQuery.GetInfo(ref id, ref yyyymm, ref saletypeid,ref deptid,ref deptstandardpoint);
            if (id!= 0)
            {
                DeptStandardPointUpdate Frm = new DeptStandardPointUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeptStandardPointQuery.Delete();
            DeptStandardPointQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeptStandardPointQuery.RefreshEX();
        }
        private void StandardPoint_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            DeptStandardPointQuery Frm = DeptStandardPointQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            Frm.Height = this.Height - 20;
        }
    }
}