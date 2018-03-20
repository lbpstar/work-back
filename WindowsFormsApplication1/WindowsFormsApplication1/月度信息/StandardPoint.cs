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
    public partial class StandardPoint : DevExpress.XtraEditors.XtraForm
    {
        public StandardPoint()
        {
            InitializeComponent();
        }
        private static StandardPoint weform = null;
        public static StandardPoint GetInstance()
        {
            if (weform == null || weform.IsDisposed)
            {
                weform = new StandardPoint();
            }
            return weform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            StandardPointQuery Frm = StandardPointQuery.GetInstance();
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
            StandardPointInsert Frm = StandardPointInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string yyyymm = "";
            int id = 0, saletypeid = 0;
            decimal standardpoint = 0;
            StandardPointQuery.GetInfo(ref id, ref yyyymm, ref saletypeid, ref standardpoint);
            if (id!= 0)
            {
                StandardPointUpdate Frm = new StandardPointUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            StandardPointQuery.Delete();
            StandardPointQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            StandardPointQuery.RefreshEX();
        }
        private void StandardPoint_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            StandardPointQuery Frm = StandardPointQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            Frm.Height = this.Height - 20;
        }
    }
}