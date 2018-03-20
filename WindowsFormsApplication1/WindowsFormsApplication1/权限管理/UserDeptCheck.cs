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
    public partial class UserDeptCheck : DevExpress.XtraEditors.XtraForm
    {
        public UserDeptCheck()
        {
            InitializeComponent();
        }
        private static UserDeptCheck dform = null;
        //public static List<XtraForm> Children = new List<XtraForm>();
        

        //bool isno = false;//控制是否第一次改变。因为第一次打开主窗体里就会执行Resize事件
        //int fheight, fwidth;//放入每次改变时的父窗体值

        public static UserDeptCheck GetInstance()
        {
            if (dform == null || dform.IsDisposed)
            {
                dform = new UserDeptCheck();
            }
            return dform;
        }
        
        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            UserDeptCheckQuery Frm = UserDeptCheckQuery.GetInstance();
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
            UserDeptCheckInsert Frm = UserDeptCheckInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string logname = "",personname ="",dept1 = "",dept2="",dept3="";
            UserDeptCheckQuery.GetInfo(ref logname,ref personname,ref dept1,ref dept2,ref dept3);
            if (logname != "")
            {
                UserDeptCheckUpdate Frm = new UserDeptCheckUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            UserDeptCheckQuery.Delete();
            UserDeptCheckQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            UserDeptCheckQuery.RefreshEX();
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

        
        private void User_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            UserDeptCheckQuery Frm = UserDeptCheckQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            //xtraTabControl1.MakePageVisible(xtraTabPage1);
            //isno = true;
            //Children.Add(Frm);
        }

        
    }
}