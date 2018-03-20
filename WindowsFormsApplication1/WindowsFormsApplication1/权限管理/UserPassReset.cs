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
    public partial class UserPassReset : DevExpress.XtraEditors.XtraForm
    {
        public UserPassReset()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string password;
            password = Common.MD5Encrypt(textEditPsw.Text.ToString().Trim());
            string sql = "update cost_user set password ='" + password;
            sql = sql + "' where cid = " + textEditID.Text.ToString();
            if (textEditLogName.Text.ToString().Trim() != "" && textEditPsw.Text.ToString().Trim() != "")
            {
                bool isok = conn.EditDatabase(sql);
                if (isok)
                {
                    MessageBox.Show("修改成功！");
                    UserQuery.RefreshEX();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("失败！");
                }
            }
            else
            {
                MessageBox.Show("登录名和密码不能为空值！");
            }           
            conn.Close();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UserUpdate_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            string logname = "", personname = "", dept1 = "", dept2 = "", dept3 = "", dept = "",remark = "";
            int cid = 0;
            UserQuery.GetInfo(ref cid, ref logname, ref personname, ref dept1, ref dept2, ref dept3, ref dept,ref remark);
            if (logname != "")
            {
                textEditLogName.Text = logname;
                textEditID.Text = cid.ToString();
                textEditName.Text = personname;
                comboBoxDept1.Text = dept1;
                comboBoxDept2.Text = dept2;
                comboBoxDept3.Text = dept3;
                comboBoxDept.Text = dept;

            }
        }
    }
}