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
    public partial class Logon : DevExpress.XtraEditors.XtraForm
    {
        public Logon()
        {
            InitializeComponent();
        }
        private static string cname = "", dept2;
        private void simpleButton登录_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, password;
            password = Common.MD5Encrypt(textEditPsw.Text.ToString().Trim());
            strsql = "select password,dept2 from cost_user where cname = '" + textEditName.Text.ToString().Trim() + "'";
            if (textEditName.Text.ToString().Trim() != "" && textEditPsw.Text.ToString().Trim() != "")
            {
                DataSet ds = conn.ReturnDataSet(strsql);
                if (ds.Tables[0].Rows.Count>0)
                {
                    if (password == ds.Tables[0].Rows[0][0].ToString())
                    {
                        //MainForm2.ShowMe();
                        cname = textEditName.Text.ToString().Trim();
                        dept2 = ds.Tables[0].Rows[0][1].ToString().Trim();
                        MainForm2 frm = new MainForm2();
                        frm.Show();
                       // MainForm2.CheckRight();
                        this.Visible = false;

                    }
                    else
                    {
                        MessageBox.Show("密码不正确！");
                    }
                }
                else
                {
                    MessageBox.Show("用户名不存在！");
                }

            }
            else
            {
                MessageBox.Show("用户名或密码不能为空！");
            }
            conn.Close();
        }

        private void hyperlinkLabelControl1_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, password;
            password = Common.MD5Encrypt(textEditPsw.Text.ToString().Trim());
            strsql = "select password from cost_user where cname = '" + textEditName.Text.ToString().Trim() + "'";
            if (textEditName.Text.ToString().Trim() != "" && textEditPsw.Text.ToString().Trim() != "")
            {
                DataSet ds = conn.ReturnDataSet(strsql);
                if (ds.Tables[0].Rows[0][0].ToString() != "")
                {
                    if (password == ds.Tables[0].Rows[0][0].ToString())
                    {
                        ChangePass frm = new ChangePass();
                        frm.Show();
                        cname = textEditName.Text.ToString().Trim();
                    }
                    else
                    {
                        MessageBox.Show("密码不正确！");
                    }
                }
                else
                {
                    MessageBox.Show("用户名不存在！");
                }

            }
            else
            {
                MessageBox.Show("用户名或密码不能为空！");
            }
            conn.Close();
        }
        public static string GetCname()
        {
            return cname;
        }
        public static string GetDept2()
        {
            return dept2;
        }
        public string Cname
        {
            get
            {
                return cname;
            }
        }
        public string Dept2
        {
            get
            {
                return dept2;
            }
        }
        private void Logon_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Logon_Load(object sender, EventArgs e)
        {
            //ConnDB conn = new ConnDB();
            //string sql, rolename;
            //sql = "SELECT top 1 i.cname from COST_USER i left join COST_USER_ROLE r on i.cid = r.USER_ID and r.HAVE_RIGHT = 'true'  where i.CNAME = '" + Logon.GetCname() + "'";
            //DataSet ds = conn.ReturnDataSet(sql);
            //rolename = ds.Tables[0].Rows[0][0].ToString();
            //if (rolename == "临时工管理")
            //{
            //    this.Text = "临时工管理";
            //}
        }
    }
}