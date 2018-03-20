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
using System.Globalization;

namespace SMTCost
{
    public partial class TempEmpAttUpdate : DevExpress.XtraEditors.XtraForm
    {
        string cno = "", begin_date = "", end_date = "", s_begin = "", s_end = "", s_rest_hours = "", s_overtime_rest_hours = "", overtime_begin = "";
        public TempEmpAttUpdate()
        {
            InitializeComponent();
        }

        private void comboBoxShift_SelectedValueChanged(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql = "select cname,cbegin,cend,rest_hours,overtime_rest_hours,overtime_begin from cost_shift where isnull(forbidden,'false') != 'true' and cname = '" + comboBoxShift.SelectedValue + "'";
            DataSet ds = conn.ReturnDataSet(sql);
            if(ds.Tables[0].Rows.Count > 0)
            {
                s_begin = ds.Tables[0].Rows[0][1].ToString();
                s_end = ds.Tables[0].Rows[0][2].ToString();
                s_rest_hours = ds.Tables[0].Rows[0][3].ToString();
                s_overtime_rest_hours = ds.Tables[0].Rows[0][4].ToString();
                overtime_begin = ds.Tables[0].Rows[0][5].ToString();
            }
            conn.Close();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql, month, cdate;
            bool isok = false, closed = false;
            DataSet ds;
            cdate = dateTimePickerDateBegin.Text.ToString();
            if (Convert.ToDateTime(cdate) > DayOfMonth(Convert.ToDateTime(cdate)))
            {
                month = NextMonth(Convert.ToDateTime(cdate));
            }
            else
            {
                month = Convert.ToDateTime(cdate).ToString("yyyy-MM");
            }
            sql = "select closed from cost_temp_close where cmonth = '" + month + "'";
            ds = conn.ReturnDataSet(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "True")
                {
                    closed = true;
                }
            }
            bool right = TempEmpAttQuery.SubmitRight(textEditCno.Text.ToString().Trim());
            if (!right)
            {
                
            }
            else if (closed)
            {
                MessageBox.Show("该月考勤已经关闭，不能再提报！");
            }
            else if (textEditStatus.Text != "未提报" && textEditStatus.Text !="")
            {
                MessageBox.Show("已提报的记录，不能再提报!");
            }
            else if(comboBoxShift.Text.ToString().Trim() =="" || textEditCno.Text.ToString().Trim() == "" || textEditRest.Text.ToString().Trim() == "" || textEditRealHours.Text.ToString().Trim() == "" || textEditNormal.Text.ToString().Trim() == "")
            {
                MessageBox.Show("有关键信息为空，请检查!");
            }
            else if(this.Text == "异常提报" && comboBoxNGType.SelectedValue.ToString() == "")
            {
                MessageBox.Show("请选择异常类型!");
            }
            else
            {
                string checkdata = "select isnull(status,0) status from COST_TEMP_EMPLOYEE_ATTENDANCE  where isnull(isclose,0) = 0 and cno = '" + textEditCno.Text.ToString() + "' and begin_date = '" + dateTimePickerDateBegin.Text + "' and end_date = '" + dateTimePickerDateEnd.Text + "'"; ;
                ds = conn.ReturnDataSet(checkdata);
                if (begin_date != "")
                {
                    if (ds.Tables[0].Rows.Count > 0 && (Convert.ToDateTime(begin_date).ToString("yyyy-MM-dd") != dateTimePickerDateBegin.Text.ToString() || Convert.ToDateTime(end_date).ToString("yyyy-MM-dd") != dateTimePickerDateEnd.Text.ToString()))
                    {
                        MessageBox.Show("该出勤日期已经存在！");
                    }
                    else
                    {
                        //TimeSpan ts = Convert.ToDateTime(dateTimePickerEnd.Text) - Convert.ToDateTime(dateTimePickerBegin.Text);
                        //textEditRealHours.Text = Convert.ToString(ts.TotalHours - Convert.ToDouble(textEditRest.Text.ToString().Trim()));
                         sql = "update COST_TEMP_EMPLOYEE_ATTENDANCE set shift = '" + comboBoxShift.SelectedValue.ToString() + "',begin_date = '" + dateTimePickerDateBegin.Text + "',end_date = '" + dateTimePickerDateEnd.Text + "',BEGIN_APPLY = '" + timeEditBegin.Text + "',END_APPLY = '" + timeEditEnd.Text + "',rest_hours = '" + textEditRest.Text.ToString().Trim() + "',hours = '" + textEditRealHours.Text.ToString().Trim() + "',normal_hours = '" + textEditNormal.Text.ToString().Trim() + "',overtime_hours = '" + textEditOverTime.Text.ToString().Trim() + "',REASON_OVERTIME='" + textEditReason.Text.ToString().Trim() + "',apply_user = '" + Logon.GetCname() + "',apply_time = '" + DateTime.Now.ToString() + "',ng_type = '" + comboBoxNGType.SelectedValue.ToString() + "',status = 1";
                        sql = sql + " where cno = '" + cno + "' and begin_date = '" + begin_date + "' and end_date = '" + end_date + "' and isnull(isclose,0) = 0";
                        isok = conn.EditDatabase(sql);
                        if (isok)
                        {
                            MessageBox.Show("提报成功！");
                            TempEmpAttQuery.RefreshEX();
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
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("此员工该出勤日期已经存在，请直接选择相应记录进行提报！");
                    }
                    else
                    {
                        sql = string.Format("Insert into COST_TEMP_EMPLOYEE_ATTENDANCE(cno,shift,begin_date,end_date,begin_apply,end_apply,rest_hours,hours,normal_hours,overtime_hours,reason_overtime,status,apply_user,apply_time,ng_type) Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')", textEditCno.Text.ToString().Trim(), comboBoxShift.SelectedValue.ToString(), dateTimePickerDateBegin.Text, dateTimePickerDateEnd.Text, timeEditBegin.Text, timeEditEnd.Text, textEditRest.Text.ToString().Trim(), textEditRealHours.Text.ToString().Trim(), textEditNormal.Text.ToString().Trim(), textEditOverTime.Text.ToString().Trim(), textEditReason.Text.ToString().Trim(), 1, Logon.GetCname(), DateTime.Now.ToString(), comboBoxNGType.SelectedValue.ToString());
                        isok = conn.EditDatabase(sql);
                        if (isok)
                        {
                            MessageBox.Show("异常提报成功！");
                            TempEmpAttQuery.RefreshEX();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("失败！");
                        }
                    } 
                    
                }
            }
            
            conn.Close();
        }
        private DateTime DayOfMonth(DateTime datetime)
        {
            return datetime.AddDays(25 - datetime.Day);
        }
        private string NextMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(1).ToString("yyyy-MM");
        }
        private void textEditCno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnDB conn = new ConnDB();
                string sql;
                sql = "select cname,cfrom,dept,shift from COST_TEMP_EMPLOYEE where cno = '" + textEditCno.Text.ToString().Trim() + "' and status != '离职'";
                DataSet ds = conn.ReturnDataSet(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    textEditName.Text = ds.Tables[0].Rows[0][0].ToString();
                    textEditDept.Text = ds.Tables[0].Rows[0][2].ToString();
                    textEditFrom.Text = ds.Tables[0].Rows[0][1].ToString();
                    comboBoxShift.SelectedIndex = -1;
                    comboBoxShift.SelectedValue = ds.Tables[0].Rows[0][3].ToString();
                }
                else
                {
                    textEditName.Text = "";
                    textEditDept.Text = "";
                    textEditFrom.Text = "";
                    comboBoxShift.SelectedIndex = -1;
                    comboBoxShift.SelectedValue = "0";
                }
                bool right = TempEmpAttQuery.SubmitRight(textEditCno.Text.ToString().Trim());
            }
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            bool right = TempEmpAttQuery.SubmitRight(textEditCno.Text.ToString().Trim());
            if (!right)
            {

            }
            else if (textEditStatus.Text != "未提报" && textEditStatus.Text != "")
            {
                MessageBox.Show("已提报状态的记录，不能再提报!");
            }
            else if (comboBoxShift.Text.ToString().Trim() == "" || textEditCno.Text.ToString().Trim() == "" || textEditRest.Text.ToString().Trim() == "" || textEditRealHours.Text.ToString().Trim() == "" || textEditNormal.Text.ToString().Trim() == "")
            {
                MessageBox.Show("有关键信息为空，请检查!");
            }
            else
            {
                string checkdata = "select isnull(status,0) status from COST_TEMP_EMPLOYEE_ATTENDANCE  where isnull(isclose,0) = 0 and cno = '" + textEditCno.Text.ToString() + "' and begin_date = '" + dateTimePickerDateBegin.Text + "' and end_date = '" + dateTimePickerDateEnd.Text + "'"; ;
                DataSet ds = conn.ReturnDataSet(checkdata);
                if(begin_date != "")
                {
                    if (ds.Tables[0].Rows.Count > 0 && (Convert.ToDateTime(begin_date).ToString("yyyy-MM-dd") != dateTimePickerDateBegin.Text.ToString() || Convert.ToDateTime(end_date).ToString("yyyy-MM-dd") != dateTimePickerDateEnd.Text.ToString()))
                    {
                        MessageBox.Show("该出勤日期已经存在！");
                    }
                    else
                    {
                        //TimeSpan ts = Convert.ToDateTime(dateTimePickerEnd.Text) - Convert.ToDateTime(dateTimePickerBegin.Text);
                        //textEditRealHours.Text = Convert.ToString(ts.TotalHours - Convert.ToDouble(textEditRest.Text.ToString().Trim()));
                        string sql = "update COST_TEMP_EMPLOYEE_ATTENDANCE set shift = '" + comboBoxShift.SelectedValue.ToString() + "',begin_date = '" + dateTimePickerDateBegin.Text + "',end_date = '" + dateTimePickerDateEnd.Text + "',BEGIN_APPLY = '" + timeEditBegin.Text + "',END_APPLY = '" + timeEditEnd.Text + "',rest_hours = '" + textEditRest.Text.ToString().Trim() + "',hours = '" + textEditRealHours.Text.ToString().Trim() + "',normal_hours = '" + textEditNormal.Text.ToString().Trim() + "',overtime_hours = '" + textEditOverTime.Text.ToString().Trim() + "',REASON_OVERTIME='" + textEditReason.Text.ToString().Trim() + "',apply_user = '" + Logon.GetCname() + "',apply_time = '" + DateTime.Now.ToString() + "',ng_type = '" + comboBoxNGType.SelectedValue.ToString() + "',status = 0";
                        sql = sql + " where cno = '" + cno + "' and begin_date = '" + begin_date + "' and end_date = '" + end_date + "' and isnull(isclose,0) = 0";
                        bool isok = conn.EditDatabase(sql);
                        if (isok)
                        {
                            MessageBox.Show("保存成功！");
                            TempEmpAttQuery.RefreshEX();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("失败！");
                        }
                    }          
                }
                else//没有出勤日期的异常提报
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("此员工该出勤日期已经存在，请直接选择相应记录进行提报！");
                    }
                    else
                    {
                        string sql = string.Format("Insert into COST_TEMP_EMPLOYEE_ATTENDANCE(cno,shift,begin_date,end_date,begin_apply,end_apply,rest_hours,hours,normal_hours,overtime_hours,reason_overtime,status,apply_user,apply_time,ng_type) Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')", textEditCno.Text.ToString().Trim(), comboBoxShift.SelectedValue.ToString(), dateTimePickerDateBegin.Text, dateTimePickerDateEnd.Text, timeEditBegin.Text, timeEditEnd.Text, textEditRest.Text.ToString().Trim(), textEditRealHours.Text.ToString().Trim(), textEditNormal.Text.ToString().Trim(), textEditOverTime.Text.ToString().Trim(), textEditReason.Text.ToString().Trim(), 0, Logon.GetCname(), DateTime.Now.ToString(), comboBoxNGType.SelectedValue.ToString());
                        bool isok = conn.EditDatabase(sql);
                        if (isok)
                        {
                            MessageBox.Show("保存成功！");
                            TempEmpAttQuery.RefreshEX();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("失败！");
                        }
                    }      
                }
            }

            conn.Close();
        }

        private void timeEditEnd_EditValueChanged(object sender, EventArgs e)
        {
            decimal interval, hours;
            DateTime dtbegin, dtend;
            TimeSpan ts;
            if (timeEditBegin.Text == "")
            {
                MessageBox.Show("请先填写提报开始时间");
                return;
            }
            else
            {
                dtbegin = Convert.ToDateTime(timeEditBegin.Text.ToString());
            }
            dtend = Convert.ToDateTime(timeEditEnd.Text.ToString());
            ts = dtend - dtbegin;
            if (comboBoxShift.SelectedValue.ToString().Contains("晚班"))
            {
                ts = dtend.AddDays(1) - dtbegin;
            }
            interval = Math.Round(ts.Hours + Convert.ToDecimal(ts.Minutes / 60.00), 2, MidpointRounding.AwayFromZero);
            string shiftbegin = s_begin;
            string begin = Convert.ToDateTime(timeEditBegin.Text.ToString()).ToString("HH:mm");
            string end = Convert.ToDateTime(timeEditEnd.Text.ToString()).ToString("HH:mm");
            if (comboBoxShift.SelectedValue.ToString().Contains("白班"))
            {
                if (s_begin == begin)
                {
                    if (s_end == end)
                    {
                        hours = interval - Convert.ToDecimal(s_rest_hours);
                        textEditRest.Text = s_rest_hours;
                        textEditRealHours.Text = Convert.ToString((int)(hours * 100) / 100.00);
                        textEditNormal.Text = Convert.ToString((int)(hours * 100) / 100.00);
                        textEditOverTime.Text = "0";
                    }
                    else if (DateTime.Compare(Convert.ToDateTime(s_end), Convert.ToDateTime(end)) < 0 && DateTime.Compare(Convert.ToDateTime(overtime_begin), Convert.ToDateTime(end)) < 0)
                    {
                        hours = interval - Convert.ToDecimal(s_overtime_rest_hours);
                        textEditRest.Text = s_overtime_rest_hours;
                        textEditRealHours.Text = Convert.ToString((int)(hours * 100) / 100.00);
                        textEditNormal.Text = "8";
                        textEditOverTime.Text = Convert.ToString((int)(hours * 100) / 100.00 - 8);
                    }
                }
            }
            else
            {
                if (s_begin == begin)
                {
                    if (s_end == end)
                    {
                        hours = interval - Convert.ToDecimal(s_rest_hours);
                        textEditRest.Text = s_rest_hours;
                        textEditRealHours.Text = Convert.ToString((int)(hours * 100) / 100.00);
                        textEditNormal.Text = Convert.ToString((int)(hours * 100) / 100.00);
                        textEditOverTime.Text = "0";
                    }
                    else if (DateTime.Compare(Convert.ToDateTime(s_end), Convert.ToDateTime(end)) < 0 && DateTime.Compare(Convert.ToDateTime(overtime_begin), Convert.ToDateTime(end)) < 0 && DateTime.Compare(Convert.ToDateTime(s_begin), Convert.ToDateTime(end)) > 0)
                    {
                        hours = interval - Convert.ToDecimal(s_overtime_rest_hours);
                        textEditRest.Text = s_overtime_rest_hours;
                        textEditRealHours.Text = Convert.ToString((int)(hours * 100) / 100.00);
                        textEditNormal.Text = "8";
                        textEditOverTime.Text = Convert.ToString((int)(hours * 100) / 100.00 - 8);
                    }
                }
            }
        }

        private void TempEmpAttUpdate_Load(object sender, EventArgs e)
        {
            BindShift();
            //绑定异常类型
            Dictionary<string, string> kvDictonary = new Dictionary<string, string>();
            kvDictonary.Add("", "");
            kvDictonary.Add("忘打卡", "忘打卡");
            kvDictonary.Add("漏报", "漏报");
            kvDictonary.Add("多报", "多报");
            kvDictonary.Add("未录脸", "未录脸");
            kvDictonary.Add("卡机异常", "卡机异常");
            kvDictonary.Add("迟到", "迟到");
            kvDictonary.Add("早退", "早退");
            kvDictonary.Add("其它", "其它");
            BindingSource bs = new BindingSource();
            bs.DataSource = kvDictonary;
            comboBoxNGType.DataSource = bs;
            comboBoxNGType.ValueMember = "Key";
            comboBoxNGType.DisplayMember = "Value";
            comboBoxNGType.SelectedIndex = 0;

            string cname = "", cfrom = "", shift = "", begin_time = "", end_time = "", begin_apply = "", end_apply = "", dept = "", rest_hours = "", hours = "", normal_hours = "", overtime_hours = "", reason = "", status = "",ng_type = "";
            TempEmpAttQuery.GetInfo(ref cno, ref cname, ref dept, ref cfrom, ref shift, ref begin_date, ref end_date, ref begin_time, ref end_time, ref begin_apply, ref end_apply, ref rest_hours, ref hours, ref normal_hours, ref overtime_hours, ref reason, ref status, ref ng_type, ref s_begin, ref s_end, ref s_rest_hours, ref s_overtime_rest_hours, ref overtime_begin);
            if (cno != "")
            {
                textEditCno.Text = cno;
                textEditName.Text = cname;
                textEditDept.Text = dept;
                textEditFrom.Text = cfrom;
                comboBoxShift.SelectedIndex = -1;
                comboBoxShift.SelectedValue = shift;
                comboBoxNGType.SelectedIndex = -1;
                comboBoxNGType.SelectedValue = ng_type;
                dateTimePickerDateBegin.Text = begin_date;
                dateTimePickerDateEnd.Text = end_date;
                textEditMachineTime.Text = begin_time + "—" + end_time;
                if (begin_apply != "")
                {
                    timeEditBegin.Time = Convert.ToDateTime(begin_apply);
                }
                if (end_apply != "")
                {
                    timeEditEnd.Time = Convert.ToDateTime(end_apply);
                }


                textEditRest.Text = rest_hours;
                textEditRealHours.Text = hours;
                textEditNormal.Text = normal_hours;
                textEditOverTime.Text = overtime_hours;
                textEditReason.Text = reason;
                textEditStatus.Text = status;
            }
            else 
            {
                textEditCno.Enabled = true;
                
            }
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
    }
}