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
    public partial class UserInsert : DevExpress.XtraEditors.XtraForm
    {
        public UserInsert()
        {
            InitializeComponent();
        }
        private static UserInsert diform = null;

        public static UserInsert GetInstance()
        {
            if (diform == null || diform.IsDisposed)
            {
                diform = new UserInsert();
            }
            return diform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2,password,log_name,person_name,dept1,dept2,dept3,dept,remark;
            int rows;
            log_name = textEditLogName.Text.ToString().Trim();
            password = Common.MD5Encrypt(textEditPsw.Text.ToString().Trim());
            person_name = textEditName.Text.ToString().Trim();
            dept1 = comboBoxDept1.Text.ToString();
            dept2 = comboBoxDept2.Text.ToString();
            dept3 = comboBoxDept3.Text.ToString();
            dept = comboBoxDept.Text.ToString();
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
            strsql = "insert into cost_user(CNAME,PASSWORD,PERSON_NAME,DEPT1,DEPT2,DEPT3,DEPT,remark) values('" + log_name + "','" + password + "','" + person_name + "','" + dept1 + "','" + dept2 + "','" + dept3 + "','" + dept + "','" + remark + "')";
            strsql2 = "select cname from cost_user where cname = '" + textEditLogName.Text.ToString().Trim() + "'";
            if (log_name != "" && textEditPsw.Text.ToString().Trim() != "")
            {
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("该用户已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
                        UserQuery.RefreshEX();
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
                MessageBox.Show("不能为空！");
            }
            conn.Close();
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
        private void UserInsert_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            BindDept1();
            BindDept2();
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