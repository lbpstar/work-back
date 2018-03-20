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
    public partial class CompositeExpense : DevExpress.XtraEditors.XtraForm
    {
        public CompositeExpense()
        {
            InitializeComponent();
        }
        private static CompositeExpense weform = null;
        public static CompositeExpense GetInstance()
        {
            if (weform == null || weform.IsDisposed)
            {
                weform = new CompositeExpense();
            }
            return weform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            CompositeExpenseQuery Frm = CompositeExpenseQuery.GetInstance();
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
            CompositeExpenseInsert Frm = CompositeExpenseInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string yyyymm = "";
            int id = 0, saletypeid = 0;
            decimal price = 0;
            CompositeExpenseQuery.GetInfo(ref id, ref yyyymm, ref saletypeid, ref price);
            if (id!= 0)
            {
                CompositeExpenseUpdate Frm = new CompositeExpenseUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            CompositeExpenseQuery.Delete();
            CompositeExpenseQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            CompositeExpenseQuery.RefreshEX();
        }

        private void CompositeExpense_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            CompositeExpenseQuery Frm = CompositeExpenseQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
            Frm.Height = this.Height - 20;
        }
    }
}