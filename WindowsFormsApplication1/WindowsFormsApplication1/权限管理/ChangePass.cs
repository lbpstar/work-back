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
    public partial class ChangePass : DevExpress.XtraEditors.XtraForm
    {
        public ChangePass()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string password;
            password = Common.MD5Encrypt(textEditAgain.Text.ToString().Trim());
            string sql = "update cost_user set password ='" + password;
            sql = sql + "' where cname = '" + Logon.GetCname() + "'";
            string sql2 = "select cname from cost_user where cname = '" + Logon.GetCname() + "' and password ='" + password + "'";
            if (textEditNew.Text.ToString().Trim() != "" && textEditAgain.Text.ToString().Trim() != "")
            {
                if (textEditNew.Text.ToString().Trim() == textEditAgain.Text.ToString().Trim())
                {
                    int rows = conn.ReturnRecordCount(sql2);
                    if (rows > 0)
                    {
                        MessageBox.Show("不能和旧密码相同！");
                    }
                    else
                    {
                        bool isok = conn.EditDatabase(sql);
                        if (isok)
                        {
                            MessageBox.Show("修改成功！");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("失败！");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("两次输入的不一致！");
                }
                    
            }
            else
            {
                MessageBox.Show("不能为空值！");
            }           
            conn.Close();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}