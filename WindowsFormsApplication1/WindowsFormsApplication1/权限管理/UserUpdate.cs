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
    public partial class UserUpdate : DevExpress.XtraEditors.XtraForm
    {
        public UserUpdate()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string dept1, dept2, dept3, dept,remark;
            dept1 = comboBoxDept1.Text;
            dept2 = comboBoxDept2.Text;
            dept3 = comboBoxDept3.Text;
            dept = comboBoxDept.Text;
            remark = textEditRemark.Text.ToString().Trim();

            if (dept1 == "请选择")
            {
                dept1 = "";
            }
            if (dept2 == "请选择")
            {
                dept2 = "";
            }
            if (dept3 == "请选择")
            {
                dept3 = "";
            }
            if (dept == "请选择")
            {
                dept = "";
            }
            string sql = "update cost_user set cname ='" + textEditLogName.Text.ToString().Trim()  + "',person_name ='" + textEditName.Text.ToString().Trim() + "',dept1 ='" + dept1 + "',dept2 ='" + dept2 + "',dept3 ='" + dept3 + "',dept ='" + dept + "',remark ='" + remark;
            sql = sql + "' where cid = " + textEditID.Text.ToString();
           // string sql2 = "select cname from cost_user where cname = '" + textEditLogName.Text.ToString().Trim() + "' and password ='" + password + "'";
            if (textEditLogName.Text.ToString().Trim() != "")
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
        public void BindDept()
        {
            ConnDB conn = new ConnDB();
            string sql = "select distinct dept as id,dept as name from COST_DEPT_LIST where dept1 = '" + comboBoxDept1.SelectedValue + "' and dept2 = '" + comboBoxDept2.SelectedValue + "' and dept3 = '" + comboBoxDept3.SelectedValue + "'";
            DataSet ds = conn.ReturnDataSet(sql);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = "0";
            dr[1] = "请选择";
            //插在第一位

            ds.Tables[0].Rows.InsertAt(dr, 0);
            comboBoxDept.DataSource = ds.Tables[0];
            comboBoxDept.DisplayMember = "name";
            comboBoxDept.ValueMember = "id";
            conn.Close();
        }
        private void UserUpdate_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            BindDept1();
            BindDept2();
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
                textEditRemark.Text = remark;

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
            if (comboBoxDept3.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                BindDept();
            }
        }
    }
}