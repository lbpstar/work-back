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
    public partial class Encrypt : DevExpress.XtraEditors.XtraForm
    {
        public Encrypt()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            textEdit3.Text = EncryptUtility.DesEncrypt(textEdit1.Text.ToString(), "12345678");
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            textEdit3.Text = EncryptUtility.DesDecrypt(textEdit2.Text.ToString(), "12345678");
        }
    }
}