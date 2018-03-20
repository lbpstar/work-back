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
    public partial class TempEmpInsert : DevExpress.XtraEditors.XtraForm
    {
        public TempEmpInsert()
        {
            InitializeComponent();
        }
        private static TempEmpInsert weiform = null;

        public static TempEmpInsert GetInstance()
        {
            if (weiform == null || weiform.IsDisposed)
            {
                weiform = new TempEmpInsert();
            }
            return weiform;
        }
        private string GenerateNo()
        {
            string maxno, strbegin, num, sql;
            int i;
            strbegin = DateTime.Now.ToString("yyyy");
            sql = "select maxno = max(cno) from COST_TEMP_EMPLOYEE where cno like '" + strbegin + "%'";
            ConnDB conn = new ConnDB();
            DataSet ds = conn.ReturnDataSet(sql);
            if (ds.Tables[0].Rows[0][0].ToString() != "")
            {
                maxno = ds.Tables[0].Rows[0][0].ToString();
                num = maxno.Substring(maxno.Length - 5, 5);
                i = Convert.ToInt32(num) + 1;
                return maxno.Substring(0, maxno.Length - 5) + i.ToString().PadLeft(5, '0');
            }
            else
            {
                return strbegin + "00001";
            }
        }
        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            string cno = GenerateNo();
            strsql = "insert into COST_TEMP_EMPLOYEE(cno,cname,sex,register_date,cfrom,from_type,dept1,dept2,dept3,dept,id_number,phone_no,shift,status) values('" + cno + "','" + textEditName.Text.ToString().Trim() + "','" + comboBoxSex.Text.ToString() + "','" + dateTimePicker1.Text.ToString() + "','" + textEditFrom.Text.ToString().Trim() + "','" + comboBoxType.SelectedValue + "','" + comboBoxDept1.SelectedValue + "','" + comboBoxDept2.SelectedValue + "','" + comboBoxDept3.SelectedValue+ "','" + comboBoxDept.SelectedValue + "','" + textEditIDNumber.Text.ToString().Trim() + "','" + textEditPhone.Text.ToString().Trim() + "','" + comboBoxShift.SelectedValue.ToString() + "','在职')";
            strsql2 = "select cno from COST_TEMP_EMPLOYEE where cno ='" + cno + "'";
            if (textEditIDNumber.Text.ToString().Trim() != "" && textEditName.Text.ToString().Trim() != "")
            {
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("该工号已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
                        TempEmpQuery.RefreshEX();
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
                MessageBox.Show("姓名和身份证号不能为空！");
            }
            conn.Close();
        }

        private void TempEmpInsert_Load(object sender, EventArgs e)
        {
            BindDept1();
            BindDept2();
            BindShift();
            BindType();
            //DateTime dt = DateTime.Now;
            //DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            //this.dateTimePicker1.Value = startMonth;

            //dateTimePicker1.Focus();
            //SendKeys.Send("{RIGHT} ");
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
            dr[0] = "";
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
            dr[0] = "";
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
            dr[0] = "";
            dr[1] = "请选择";
            //插在第一位

            ds.Tables[0].Rows.InsertAt(dr, 0);
            comboBoxDept.DataSource = ds.Tables[0];
            comboBoxDept.DisplayMember = "name";
            comboBoxDept.ValueMember = "id";
            conn.Close();
        }
        public void BindShift()
        {
            ConnDB conn = new ConnDB();
            string sql = "select cname cid,cname value from cost_shift where isnull(forbidden,'false') != 'true'";
            DataSet ds = conn.ReturnDataSet(sql);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = "";//其实这里也许设置为"null"更好
            dr[1] = "请选择";
            //插在第一位

            ds.Tables[0].Rows.InsertAt(dr, 0);
            comboBoxShift.DataSource = ds.Tables[0];
            comboBoxShift.DisplayMember = "value";
            comboBoxShift.ValueMember = "CID";
            conn.Close();
        }
        public void BindType()
        {
            ConnDB conn = new ConnDB();
            string sql = "select sub_id cid,cname value from cost_base_data where module_id = 1";
            DataSet ds = conn.ReturnDataSet(sql);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = DBNull.Value;
            dr[1] = "请选择";
            //插在第一位

            ds.Tables[0].Rows.InsertAt(dr, 0);
            comboBoxType.DataSource = ds.Tables[0];
            comboBoxType.DisplayMember = "value";
            comboBoxType.ValueMember = "CID";
            conn.Close();
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