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
    public partial class TempEmpUpdate : DevExpress.XtraEditors.XtraForm
    {
        bool i= false;
        public TempEmpUpdate()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string leave_date,sql;
            if((comboBoxStatus.Text.ToString()=="离职" || comboBoxStatus.Text.ToString() == "转正") && textEditLeaveDate.Text.ToString() == "")
            {
                MessageBox.Show("请先填写离职日期！");
            }
            else if (comboBoxStatus.Text.ToString() == "在职" && textEditLeaveDate.Text.ToString() != "")
            {
                MessageBox.Show("请去掉离职日期！");
            }
            else
            {
                if (textEditLeaveDate.Text.ToString() == "")
                {
                    sql = "update COST_TEMP_EMPLOYEE set cname = '" + textEditName.Text.ToString().Trim() + "',sex = '" + comboBoxSex.Text + "',register_date='" + dateTimePickerRegister.Text + "',leave_date=NULL,cfrom = '" + textEditFrom.Text.ToString().Trim() + "',from_type = '" + comboBoxType.SelectedValue.ToString() + "',dept1 = '" + comboBoxDept1.Text + "',dept2 = '" + comboBoxDept2.Text + "',dept3 = '" + comboBoxDept3.Text + "',dept = '" + comboBoxDept.Text + "',id_number = '" + textEditIdNumber.Text.ToString().Trim() + "',phone_no = '" + textEditPhone.Text.ToString().Trim() + "',shift = '" + comboBoxShift.Text + "',status = '" + comboBoxStatus.SelectedItem + "'";
                    sql = sql + " where cno = '" + textEditCno.Text.ToString() + "'";
                }
                else
                {
                    leave_date = textEditLeaveDate.Text.ToString();
                    sql = "update COST_TEMP_EMPLOYEE set cname = '" + textEditName.Text.ToString().Trim() + "',sex = '" + comboBoxSex.Text + "',register_date='" + dateTimePickerRegister.Text + "',leave_date='" + leave_date + "',cfrom = '" + textEditFrom.Text.ToString().Trim() + "',from_type = '" + comboBoxType.SelectedValue.ToString() + "',dept1 = '" + comboBoxDept1.Text + "',dept2 = '" + comboBoxDept2.Text + "',dept3 = '" + comboBoxDept3.Text + "',dept = '" + comboBoxDept.Text + "',id_number = '" + textEditIdNumber.Text.ToString().Trim() + "',phone_no = '" + textEditPhone.Text.ToString().Trim() + "',shift = '" + comboBoxShift.Text + "',status = '" + comboBoxStatus.SelectedItem + "'";
                    sql = sql + " where cno = '" + textEditCno.Text.ToString() + "'";
                }

                bool isok = conn.EditDatabase(sql);
                if (isok)
                {
                    MessageBox.Show("修改成功！");
                    TempEmpQuery.RefreshEX();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("失败！");
                }
            }
            
            conn.Close();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TempWorkerPriceUpdate_Load(object sender, EventArgs e)
        {
            BindDept1();
            BindDept2();
            BindShift();
            BindType();
            string cno = "", cname = "", sex = "", register_date = "", leave_date = "", cfrom = "", dept1 = "", dept2 = "", dept3 = "", dept = "", id_number = "",phone_no = "", shift = "",status = "";
            int fromtype = 1;
            bool right;
            TempEmpQuery.GetInfo(ref cno, ref cname, ref sex, ref register_date, ref leave_date,ref cfrom,ref fromtype, ref dept1,ref dept2,ref dept3,ref dept, ref id_number,ref phone_no,ref shift, ref status);
            if (cno != "")
            {
                right = ModifyRight();
                textEditCno.Text = cno;
                textEditName.Text = cname;
                //comboBoxSex.SelectedIndex = -1;
                //comboBoxSex.SelectedValue = sex;
                comboBoxSex.SelectedItem = sex;
                dateTimePickerRegister.Text = register_date;
                textEditLeaveDate.Text = leave_date;
                i = true;
                dateTimePickerLeave.Text = "";
                textEditFrom.Text = cfrom;
                //comboBoxDept1.SelectedIndex = -1;
                //comboBoxDept1.SelectedValue = dept1;
                //comboBoxDept2.SelectedIndex = -1;
                //comboBoxDept2.SelectedValue = dept2;
                comboBoxDept1.Text = dept1;
                comboBoxDept2.Text = dept2;
                comboBoxDept3.Text = dept3;
                comboBoxDept.Text = dept;
                textEditIdNumber.Text = id_number;
                textEditPhone.Text = phone_no;
                comboBoxShift.SelectedIndex = -1;
                comboBoxShift.SelectedValue = shift;
                comboBoxType.SelectedIndex = -1;
                if(fromtype ==0)
                {
                    comboBoxType.SelectedValue = DBNull.Value;
                }
                else
                {
                    comboBoxType.SelectedValue = fromtype;
                }
                comboBoxStatus.SelectedItem = status;
                if(!right)
                {
                    textEditName.Enabled = false;
                    comboBoxSex.Enabled = false;
                    dateTimePickerRegister.Enabled = false;
                    textEditLeaveDate.Enabled = false;
                    dateTimePickerLeave.Enabled = false;
                    textEditFrom.Enabled = false;
                    comboBoxType.Enabled = false;
                    textEditIdNumber.Enabled = false;
                    textEditPhone.Enabled = false;
                    comboBoxStatus.Enabled = false;
                }
            }

            //dateTimePicker1.Focus();
            //SendKeys.Send("{RIGHT} ");
        }
        public static bool ModifyRight()
        {
            ConnDB conn = new ConnDB();
            string sql;
            bool right = false;
            sql = "select m.permission from COST_USER i left join COST_USER_ROLE r on i.CID = r.USER_ID and r.HAVE_RIGHT = 'true' left join COST_ROLE_PERMISSION p on r.ROLE_ID = p.ROLE_ID and p.HAVE_RIGHT = 'true' left join COST_MODULE_PERMISSION m on p.PERMISSION_ID = m.CID where i.CNAME = '" + Logon.GetCname() + "' and m.module_name = '临时工基本信息'";
            DataSet ds = conn.ReturnDataSet(sql);
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                if (ds.Tables[0].Rows[j][0].ToString() == "修改")
                {
                    right = true;
                    break;
                }
                else
                    right = false;

            }
            conn.Close();
            return right;
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
        public void BindShift()
        {
            ConnDB conn = new ConnDB();
            string sql = "select cname cid,cname value from cost_shift where isnull(forbidden,'false') != 'true'";
            DataSet ds = conn.ReturnDataSet(sql);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = "0";//其实这里也许设置为"null"更好
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

        private void dateTimePickerLeave_ValueChanged(object sender, EventArgs e)
        {
            if(i == false)
            {
                textEditLeaveDate.Text = dateTimePickerLeave.Text;
                
            }
            else
            {
                i = false;
            }
            
        }
    }
}