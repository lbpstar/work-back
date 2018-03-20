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
    public partial class TempEmpExpense : DevExpress.XtraEditors.XtraForm
    {
        public TempEmpExpense()
        {
            InitializeComponent();
        }
        private static TempEmpExpense reform = null;
        public static TempEmpExpense GetInstance()
        {
            if (reform == null || reform.IsDisposed)
            {
                reform = new TempEmpExpense();
            }
            return reform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempEmpExpenseQuery Frm = TempEmpExpenseQuery.GetInstance();
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
            TempEmpExpenseInsert Frm = TempEmpExpenseInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string id = "", ctype = "", expense = "", note = "";
            TempEmpExpenseQuery.GetInfo(ref id, ref ctype, ref expense, ref note);
            if (id != "")
            {
                TempEmpExpenseUpdate Frm = new TempEmpExpenseUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempEmpExpenseQuery.Delete();
            TempEmpExpenseQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempEmpExpenseQuery.RefreshEX();
        }

        private void LineType_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            TempEmpExpenseQuery Frm = TempEmpExpenseQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

    }
}