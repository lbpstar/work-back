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
    public partial class TempEmpTravelExpense : DevExpress.XtraEditors.XtraForm
    {
        public TempEmpTravelExpense()
        {
            InitializeComponent();
        }
        private static TempEmpTravelExpense reform = null;
        public static TempEmpTravelExpense GetInstance()
        {
            if (reform == null || reform.IsDisposed)
            {
                reform = new TempEmpTravelExpense();
            }
            return reform;
        }

        private void barButtonItem查询_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempEmpTravelExpenseQuery Frm = TempEmpTravelExpenseQuery.GetInstance();
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
            TempEmpTravelExpenseInsert Frm = TempEmpTravelExpenseInsert.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

        private void barButtonItem修改_ItemClick(object sender, ItemClickEventArgs e)
        {
            string id = "", empno = "", name = "", begin_date = "", expense = "0";
            TempEmpTravelExpenseQuery.GetInfo(ref id, ref empno, ref name, ref begin_date, ref expense);
            if (id != "")
            {
                TempEmpTravelExpenseUpdate Frm = new TempEmpTravelExpenseUpdate();
                Frm.TopLevel = false;
                Frm.Parent = this;
                Frm.Show();
                Frm.BringToFront();
            }
        }

        private void barButtonItem删除_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempEmpTravelExpenseQuery.Delete();
            TempEmpTravelExpenseQuery.RefreshEX();
        }

        private void barButtonItem刷新_ItemClick(object sender, ItemClickEventArgs e)
        {
            TempEmpTravelExpenseQuery.RefreshEX();
        }

        private void LineType_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            TempEmpTravelExpenseQuery Frm = TempEmpTravelExpenseQuery.GetInstance();
            Frm.TopLevel = false;
            Frm.Parent = this;
            Frm.Show();
            Frm.BringToFront();
        }

    }
}