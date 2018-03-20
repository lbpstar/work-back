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
    public partial class User : DevExpress.XtraEditors.XtraForm
    {
        public User()
        {
            InitializeComponent();
        }
        private static User dform = null;
        //public static List<XtraForm> Children = new List<XtraForm>();
        

        //bool isno = false;//控制是否第一次改变。因为第一次打开主窗体里就会执行Resize事件
        //int fheight, fwidth;//放入每次改变时的父窗体值

        public static User GetInstance()
        {
            if (dform == null || dform.IsDisposed)
            {
                dform = new User();
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
            UserQuery Frm = UserQuery.GetInstance();
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
            UserInsert Frm = UserInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string logname = "",personname ="",dept1 = "",dept2="",dept3="",dept = "",remark = "";
            int cid = 0;
            UserQuery.GetInfo(ref cid, ref logname,ref personname,ref dept1,ref dept2,ref dept3,ref dept,ref remark);
            if (logname != "")
            {
                UserUpdate Frm = new UserUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            UserQuery.Delete();
            UserQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            UserQuery.RefreshEX();
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
            UserQuery.cDisable();
            UserQuery.RefreshEX();
        }

        private void barButtonItem反禁用_ItemClick(object sender, ItemClickEventArgs e)
        {
            UserQuery.cEnable();
            UserQuery.RefreshEX();
        }

        private void User_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            UserQuery Frm = UserQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            //xtraTabControl1.MakePageVisible(xtraTabPage1);
            //isno = true;
            //Children.Add(Frm);
        }

        private void barButtonItem授权_ItemClick(object sender, ItemClickEventArgs e)
        {
            string logname = "", personname = "", dept1 = "", dept2 = "", dept3 = "", dept = "",remark = "";
            int cid = 0;
            UserQuery.GetInfo(ref cid, ref logname, ref personname, ref dept1, ref dept2, ref dept3, ref dept,ref remark);
            if (logname != "")
            {
                UserRole Frm = new UserRole();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem重置密码_ItemClick(object sender, ItemClickEventArgs e)
        {
            string logname = "", personname = "", dept1 = "", dept2 = "", dept3 = "", dept = "",remark = "";
            int cid = 0;
            UserQuery.GetInfo(ref cid, ref logname, ref personname, ref dept1, ref dept2, ref dept3, ref dept,ref remark);
            if (logname != "")
            {
                UserPassReset Frm = new UserPassReset();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }
    }
}