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
    public partial class UserDeptCheckUpdate : DevExpress.XtraEditors.XtraForm
    {
        string logname = "", personname = "", dept1 = "", dept2 = "", dept3 = "";
        public UserDeptCheckUpdate()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string dept11, dept22, dept33;
            dept11 = comboBoxDept1.Text;
            dept22 = comboBoxDept2.Text;
            dept33 = comboBoxDept3.Text;
            if (dept11 == "请选择")
            {
                dept11 = "";
            }
            if (dept22 == "请选择")
            {
                dept22 = "";
            }
            if (dept33 == "请选择")
            {
                dept33 = "";
            }

            string sql = "update cost_check_dept set dept1 ='" + dept11  + "',dept2 ='" + dept22 + "',dept3 ='" + dept33;
            sql = sql + "' where log_name = '" + logname + "' and dept1 = '" + dept1 + "' and dept2 = '" + dept2 + "' and dept3 = '" +dept3 + "'";
           // string sql2 = "select cname from cost_user where cname = '" + textEditLogName.Text.ToString().Trim() + "' and password ='" + password + "'";
            if (textEditLogName.Text.ToString().Trim() != "")
            {
                bool isok = conn.EditDatabase(sql);
                if (isok)
                {
                    MessageBox.Show("修改成功！");
                    UserDeptCheckQuery.RefreshEX();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("失败！");
                }
            }
            else
            {
                MessageBox.Show("登录名不能为空值！");
            }           
            conn.Close();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void BindDept1()
        {
            ConnDB conn = new ConnDB();
            string sql = "select distinct dept1 from COST_DEPT_LIST";
            DataSet ds = conn.ReturnDataSet(sql);
            DataRow dr = ds.Tables[0].NewRow();
            //dr[0] = "0";
            //dr[1] = "请选择";
            ////插在第一位

            //ds.Tables[0].Rows.InsertAt(dr, 0);
            comboBoxDept1.DataSource = ds.Tables[0];
            comboBoxDept1.DisplayMember = "dept1";
            comboBoxDept1.ValueMember = "dept1";
            conn.Close();
        }
        public void BindDept2()
        {
            ConnDB conn = new ConnDB();
            string sql = "select distinct dept2 as id,dept2 as name from COST_DEPT_LIST where dept1 = '" + comboBoxDept1.SelectedValue + "'";
            DataSet ds = conn.ReturnDataSet(sql);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = "0";
            dr[1] = "请选择";
            //插在第一位

            ds.Tables[0].Rows.InsertAt(dr, 0);
            comboBoxDept2.DataSource = ds.Tables[0];
            comboBoxDept2.DisplayMember = "name";
            comboBoxDept2.ValueMember = "id";
            conn.Close();
        }
        public void BindDept3()
        {
            ConnDB conn = new ConnDB();
            string sql = "select distinct dept3 as id,dept3 as name from COST_DEPT_LIST where dept1 = '" + comboBoxDept1.SelectedValue + "' and dept2 = '" + comboBoxDept2.SelectedValue + "'";
            DataSet ds = conn.ReturnDataSet(sql);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = "0";
            dr[1] = "请选择";
            //插在第一位

            ds.Tables[0].Rows.InsertAt(dr, 0);
            comboBoxDept3.DataSource = ds.Tables[0];
            comboBoxDept3.DisplayMember = "name";
            comboBoxDept3.ValueMember = "id";
            conn.Close();
        }

        private void UserUpdate_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            BindDept1();
            BindDept2();
            UserDeptCheckQuery.GetInfo(ref logname, ref personname, ref dept1, ref dept2, ref dept3);
            if (logname != "")
            {
                textEditLogName.Text = logname;
                textEditName.Text = personname;
                comboBoxDept1.Text = dept1;
                comboBoxDept2.Text = dept2;
                comboBoxDept3.Text = dept3;
            }
        }

        private void comboBoxDept1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxDept1.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                BindDept2();
            }
        }

        private void comboBoxDept2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxDept2.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                BindDept3();
            }
        }

        private void comboBoxDept3_SelectedValueChanged(object sender, EventArgs e)
        {

        }
    }
}