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
    public partial class EmsHhHours : DevExpress.XtraEditors.XtraForm
    {
        public EmsHhHours()
        {
            InitializeComponent();
        }
        private static EmsHhHours weform = null;
        public static EmsHhHours GetInstance()
        {
            if (weform == null || weform.IsDisposed)
            {
                weform = new EmsHhHours();
            }
            return weform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            EmsHhHoursQuery Frm = EmsHhHoursQuery.GetInstance();
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
            EmsHhHoursInsert Frm = EmsHhHoursInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            EmsHhHoursQuery.Delete();
            EmsHhHoursQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            EmsHhHoursQuery.RefreshEX();
        }

        private void SteelNetRate_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            EmsHhHoursQuery Frm = EmsHhHoursQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }
    }
}