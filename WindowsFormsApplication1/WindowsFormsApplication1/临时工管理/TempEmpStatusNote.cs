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

namespace SMTCost
{
    public partial class TempEmpStatusNote : DevExpress.XtraEditors.XtraForm
    {
        public TempEmpStatusNote()
        {
            InitializeComponent();
        }
        private static TempEmpStatusNote wtiform = null;

        public static TempEmpStatusNote GetInstance()
        {
            if (wtiform == null || wtiform.IsDisposed)
            {
                wtiform = new TempEmpStatusNote();
            }
            return wtiform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            string msg = "驳回:" + textBoxMsg.Text.ToString().Trim()+";";
            TempEmpAttQuery.Reject2(msg);
            this.Close();
        }

        private void simpleButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}