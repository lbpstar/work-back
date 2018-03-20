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
    public partial class Dept : DevExpress.XtraEditors.XtraForm
    {
        public Dept()
        {
            InitializeComponent();
        }
        private static Dept dform = null;
        //public static List<XtraForm> Children = new List<XtraForm>();
        

        //bool isno = false;//控制是否第一次改变。因为第一次打开主窗体里就会执行Resize事件
        //int fheight, fwidth;//放入每次改变时的父窗体值

        public static Dept GetInstance()
        {
            if (dform == null || dform.IsDisposed)
            {
                dform = new Dept();
            }
            return dform;
        }
        public static void ForbiddenEnable()
        {
            dform.barButtonItem禁用.Enabled = true;
        }
        public static void ForbiddenDisable()
        {
            dform.barButtonItem禁用.Enabled = false;
        }
        public static void UnforbiddenEnable()
        {
            dform.barButtonItem反禁用.Enabled = true;
        }
        public static void UnforbiddenDisable()
        {
            dform.barButtonItem反禁用.Enabled = false;
        }
        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeptQuery Frm = DeptQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            //Children.Add(Frm);

        }

        private void barButtonItem退出_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem新增_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeptInsert Frm = DeptInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string cname = "";
            int cid = 0;
            int saletypeid = 0;
            string saletypename = "";
            DeptQuery.GetInfo(ref cid, ref cname, ref saletypeid, ref saletypename);
            if (cname != "")
            {
                DeptUpdate Frm = new DeptUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeptQuery.Delete();
            DeptQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeptQuery.RefreshEX();
        }

        private void Dept_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            DeptQuery Frm = DeptQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            Frm.Height = this.Height - 20;
            //xtraTabControl1.MakePageVisible(xtraTabPage1);
            //isno = true;
            //Children.Add(Frm);


        }

        //public void ChangAllChildRenSize()
        //{
        //    foreach (Form ChildForm in Children)
        //    {
        //        ChildForm.Height = fheight;
        //        ChildForm.Width = fwidth;
        //    }
        //}

        //private void Dept_SizeChanged(object sender, EventArgs e)
        //{
            //if (isno)
            //{
            //    fheight = this.Height;
            //    fwidth = this.Width;
            //    ChangAllChildRenSize();
            //}
        //}

        private void barButtonItem禁用_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeptQuery.cDisable();
            DeptQuery.RefreshEX();
        }

        private void barButtonItem反禁用_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeptQuery.cEnable();
            DeptQuery.RefreshEX();
        }
    }
}