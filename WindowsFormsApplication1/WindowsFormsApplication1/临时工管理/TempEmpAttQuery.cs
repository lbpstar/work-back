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
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;

namespace SMTCost
{
    public partial class TempEmpAttQuery : DevExpress.XtraEditors.XtraForm
    {
        public TempEmpAttQuery()
        {
            InitializeComponent();
        }
        private static TempEmpAttQuery weqform = null;
        private bool ischange = false;
        private List<string> mlist = new List<string>();
        private DataSet mds;
        public static TempEmpAttQuery GetInstance()
        {
            if (weqform == null || weqform.IsDisposed)
            {
                weqform = new TempEmpAttQuery();
            }
            return weqform;
        }
        public static void RefreshEX()
        {
            if (weqform == null || weqform.IsDisposed)
            {

            }
            else
            {
                weqform.showDetail();
            }
        }
        
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="level"></param>
        public static void Check(int level)
        {
            string sql, month, cdate; ;
            bool isdone = true, isok = true, closed = false;
            ConnDB conn = new ConnDB();
            DataSet ds;
            bool right = weqform.CheckRight(level);
            if (weqform == null || weqform.IsDisposed)
            {
                MessageBox.Show("没有选中要审核的记录！");
            }
            else if (weqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要审核的记录！");
            }
            else if(!right)
            {
                MessageBox.Show("没有权限！");
            }
            else
            {
                for (int i = 0; i < weqform.gridView1.SelectedRowsCount; i++)
                {
                    cdate = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[8].ToString();
                    if (Convert.ToDateTime(cdate) > weqform.DayOfMonth(Convert.ToDateTime(cdate)))
                    {
                        month = weqform.NextMonth(Convert.ToDateTime(cdate));
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
                    if (closed)
                    {
                        MessageBox.Show("该月考勤已经关闭，不能再操作！");
                        isok = false;
                        break;
                    }
                    if (level == 2 && weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[19].ToString() != "已提报")
                    {
                        MessageBox.Show("只能审核已提报的记录！");
                        isok = false;
                        break;
                    }
                    if (level == 3 && weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[19].ToString() != "考勤员已审核")
                    {
                        MessageBox.Show("只能审批考勤员已审核的记录！");
                        isok = false;
                        break;
                    }
                    sql = "select * from  COST_TEMP_EMPLOYEE e left join COST_CHECK_DEPT d on e.DEPT1 = d.DEPT1 and e.DEPT2 = d.DEPT2 and e.DEPT3 = d.DEPT3 where d.LOG_NAME = '" + Logon.GetCname() + "' and e.CNO = '"+ weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString() + "'";
                    ds = conn.ReturnDataSet(sql); 
                    if (level == 3 && ds.Tables[0].Rows.Count ==0)
                    {
                        MessageBox.Show("只能审批自己部门的记录！");
                        isok = false;
                        break;
                    }
                }
                if(isok)
                {
                    MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                    DialogResult dr = MessageBox.Show("确定要审核吗?", "审核", messButton);
                    if (dr == DialogResult.OK)
                    {
                        if(level == 2)
                        {
                            for (int i = 0; i < weqform.gridView1.SelectedRowsCount; i++)
                            {
                                sql = "update i set i.status = 2,shift = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[7].ToString() + "',ng_type = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[21].ToString() + "' from  COST_TEMP_EMPLOYEE_ATTENDANCE i  where isnull(isclose,0) = 0 and cno = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[1].ToString() + "' and begin_date = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[8].ToString() + "' and end_date = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[9].ToString()  + "'";
                                isdone = conn.EditDatabase(sql);
                                //if (!isdone)
                                //    break;
                            }
                            if (isdone)
                            {
                                MessageBox.Show("审核成功！");
                                weqform.showDetail();
                            }
                        }
                        else
                        {
                            for (int i = 0; i < weqform.gridView1.SelectedRowsCount; i++)
                            {
                                sql = "update i set i.status = 3 from  COST_TEMP_EMPLOYEE_ATTENDANCE i  where isnull(isclose,0) = 0 and cno = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[1].ToString() + "' and begin_date = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[8].ToString() + "' and end_date = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[9].ToString() + "'";
                                isdone = conn.EditDatabase(sql);
                                //if (!isdone)
                                //    break;
                            }
                            if (isdone)
                            {
                                MessageBox.Show("审批成功！");
                                weqform.showDetail();
                            }
                        }
                        
                    }
                }
                
            }
            conn.Close();
        }
        public static void Reject(int level)
        {
            string sql,month,cdate;
            bool isdone = true, isok = true, closed = false;
            ConnDB conn = new ConnDB();
            DataSet ds;
            bool right = weqform.CheckRight(level);
            if (weqform == null || weqform.IsDisposed)
            {
                MessageBox.Show("没有选中要反审的记录！");
            }
            else if (weqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要反审的记录！");
            }
            else if (!right)
            {
                MessageBox.Show("没有权限！");
            }
            else
            {
                for (int i = 0; i < weqform.gridView1.SelectedRowsCount; i++)
                {
                    cdate = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[8].ToString();
                    if (Convert.ToDateTime(cdate) > weqform.DayOfMonth(Convert.ToDateTime(cdate)))
                    {
                        month = weqform.NextMonth(Convert.ToDateTime(cdate));
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
                    if (closed)
                    {
                        MessageBox.Show("该月考勤已经关闭，不能再操作！");
                        isok = false;
                        break;
                    }
                    if (level == 1 && weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[19].ToString() != "已提报")
                    {
                        MessageBox.Show("只能驳回已提报状态的记录！");
                        isok = false;
                        break;
                    }
                    if (level == 2 && weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[19].ToString() != "考勤员已审核")
                    {
                        MessageBox.Show("只能反审考勤员审核级别的记录！");
                        isok = false;
                        break;
                    }
                    if (level == 3 && weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[19].ToString() != "主管已审批")
                    {
                        MessageBox.Show("只能反审主管已审批的记录！");
                        isok = false;
                        break;
                    }
                    sql = "select * from  COST_TEMP_EMPLOYEE e left join COST_CHECK_DEPT d on e.DEPT1 = d.DEPT1 and e.DEPT2 = d.DEPT2 and e.DEPT3 = d.DEPT3 where d.LOG_NAME = '" + Logon.GetCname() + "' and e.CNO = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString() + "'";
                    ds = conn.ReturnDataSet(sql);
                    if (level == 3 && ds.Tables[0].Rows.Count == 0)
                    {
                        MessageBox.Show("只能反审自己部门的记录！");
                        isok = false;
                        break;
                    }
                }
                if (isok)
                {
                    if (level == 1)
                    {
                        TempEmpStatusNote Frm = TempEmpStatusNote.GetInstance();
                        //Frm.TopLevel = false;
                        //Frm.Parent = this;
                        Frm.Show();
                        //Frm.BringToFront();
                    }
                    else
                    {
                        MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                        DialogResult dr = MessageBox.Show("确定要反审（驳回）吗?", "反审（驳回）", messButton);
                        if (dr == DialogResult.OK)
                        {

                            if (level == 2)
                            {
                                for (int i = 0; i < weqform.gridView1.SelectedRowsCount; i++)
                                {
                                    sql = "update i set i.status = 0 from  COST_TEMP_EMPLOYEE_ATTENDANCE i  where isnull(isclose,0) = 0 and cno = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[1].ToString() + "' and begin_date = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[8].ToString() + "' and end_date = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[9].ToString() + "'";
                                    isdone = conn.EditDatabase(sql);
                                    //if (!isdone)
                                    //    break;
                                }
                                if (isdone)
                                {
                                    MessageBox.Show("反审成功！");
                                    weqform.showDetail();
                                }
                            }
                            else
                            {
                                for (int i = 0; i < weqform.gridView1.SelectedRowsCount; i++)
                                {
                                    sql = "update i set i.status = 2 from  COST_TEMP_EMPLOYEE_ATTENDANCE i  where isnull(isclose,0) = 0 and cno = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[1].ToString() + "' and begin_date = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[8].ToString() + "' and end_date = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[9].ToString() + "'";
                                    isdone = conn.EditDatabase(sql);
                                    //if (!isdone)
                                    //    break;
                                }
                                if (isdone)
                                {
                                    MessageBox.Show("反审成功！");
                                    weqform.showDetail();
                                }
                            }
                        }
                    }
                    
                }

            }
            conn.Close();
        }
        public static void Reject2(string msg)
        {
            string sql;
            bool isdone = true;
            ConnDB conn = new ConnDB();
            for (int i = 0; i < weqform.gridView1.SelectedRowsCount; i++)
            {
                sql = "update i set i.status = 0,i.status_note = isnull(i.status_note,'') + '" + msg + "' from  COST_TEMP_EMPLOYEE_ATTENDANCE i  where isnull(isclose,0) = 0 and cno = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[1].ToString() + "' and begin_date = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[8].ToString() + "' and end_date = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[9].ToString() + "'";
                isdone = conn.EditDatabase(sql);
                //if (!isdone)
                //    break;
            }
            if (isdone)
            {
                MessageBox.Show("驳回成功！");
                weqform.showDetail();
            }
            conn.Close();
        }
        /// <summary>
        /// 审核权限检查
        /// </summary>
        private bool CheckRight(int level)
        {
            ConnDB conn = new ConnDB();
            string sql;
            bool right = false;
            sql = "select m.permission from COST_USER i left join COST_USER_ROLE r on i.CID = r.USER_ID and r.HAVE_RIGHT = 'true' left join COST_ROLE_PERMISSION p on r.ROLE_ID = p.ROLE_ID and p.HAVE_RIGHT = 'true' left join COST_MODULE_PERMISSION m on p.PERMISSION_ID = m.CID where i.CNAME = '" + Logon.GetCname() + "' and m.module_name = '临时工考勤'";
            DataSet ds = conn.ReturnDataSet(sql);
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                if (level == 1 && ds.Tables[0].Rows[j][0].ToString() == "考勤员审核")
                {
                    right = true;
                    break;
                }
                else if (level == 2 && ds.Tables[0].Rows[j][0].ToString() == "考勤员审核")
                {
                    right = true;
                    break;
                }
                else if (level == 3 && ds.Tables[0].Rows[j][0].ToString() == "主管审批")
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
        public static bool SubmitRight()
        {
            ConnDB conn = new ConnDB();
            string sql;
            bool right = false;
            sql = "select m.permission from COST_USER i left join COST_USER_ROLE r on i.CID = r.USER_ID and r.HAVE_RIGHT = 'true' left join COST_ROLE_PERMISSION p on r.ROLE_ID = p.ROLE_ID and p.HAVE_RIGHT = 'true' left join COST_MODULE_PERMISSION m on p.PERMISSION_ID = m.CID where i.CNAME = '" + Logon.GetCname() + "' and m.module_name = '临时工考勤'";
            DataSet ds = conn.ReturnDataSet(sql);
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                if (ds.Tables[0].Rows[j][0].ToString() == "操作")
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
        /// <summary>
        /// 提报时对部门的检查
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public static bool SubmitRight(string cno)
        {
            ConnDB conn = new ConnDB();
            string sql,sql2;
            bool right = false;
            sql = "select * from  COST_TEMP_EMPLOYEE e left join COST_USER U on e.DEPT1 = u.DEPT1 and e.DEPT2 = u.DEPT2  where u.CNAME = '" + Logon.GetCname() + "' and e.CNO = '" + cno + "'";
            DataSet ds = conn.ReturnDataSet(sql);
            sql2 = "select * from COST_USER i left join COST_USER_ROLE r on i.CID = r.USER_ID and r.HAVE_RIGHT = 'true'  where i.CNAME = '" + Logon.GetCname() + "' and r.role_id = 10";
            DataSet ds2 = conn.ReturnDataSet(sql2);
            if (ds.Tables[0].Rows.Count == 0 && ds2.Tables[0].Rows.Count == 0)
            {
                string msg = cno + "属于其它部门，不能提报！";
                MessageBox.Show(msg);
                right = false;
            }
            else
            {
                right = true;
            }
            return right;
        }
        public static void GetInfo(ref string cno, ref string cname, ref string dept,ref string cfrom,ref string shift,ref string begin_date, ref string end_date,ref string begin_time,ref string end_time,ref string begin_apply,ref string end_apply,ref string rest_hours,ref string hours,ref string normal_hours,ref string overtime_hours,ref string reason,ref string status,ref string ng_type,ref string s_begin,ref string s_end, ref string s_rest_hours, ref string s_overtime_rest_hours, ref string overtime_begin)
        {
            if (weqform == null || weqform.IsDisposed)
            {
                //MessageBox.Show("没有选中要提报的记录！");
            }
            else if (weqform.gridView1.SelectedRowsCount == 0)
            {
                //MessageBox.Show("没有选中要提报的记录！");
            }
            else
            {
                cno =weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString();
                cname = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[2].ToString();
                dept = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[5].ToString();
                cfrom = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[6].ToString();
                //shiftid = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[4].ToString();
                shift = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[7].ToString();
                begin_date = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[8].ToString();
                end_date = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[9].ToString();
                begin_time = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[10].ToString();
                end_time = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[11].ToString();
                begin_apply = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[12].ToString();
                end_apply = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[13].ToString();
                rest_hours = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[14].ToString();
                hours = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[15].ToString();
                normal_hours = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[16].ToString();
                overtime_hours = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[17].ToString();
                reason = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[18].ToString();
                status = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[19].ToString();
                ng_type = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[21].ToString();

                s_begin = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[22].ToString();
                s_end = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[23].ToString();
                s_rest_hours = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[24].ToString();
                s_overtime_rest_hours = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[25].ToString();
                overtime_begin = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[26].ToString();
            }
        }
        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql,yyyymm,status,emp_status;
            yyyymm = dateTimePickerBegin.Text.ToString();
            status = comboBoxStatus.SelectedIndex.ToString();
            emp_status = comboBoxEmpStatus.SelectedValue.ToString();
            //select * from COST_TEMP_EMPLOYEE_ATTENDANCE where CNO = '201700001' and isnull(status,0)=case when isnull(status,0) = 5 then 1 else isnull(status,0) end
            if (textEditNo.Text.ToString().Trim() != "" || textEditName.Text.ToString().Trim() != "")
            {
                strsql = "select e.status 员工状态,e.cno 员工工号,e.cname 姓名,e.REGISTER_DATE 报道日期,e.leave_date 离职日期,e.dept 部门,e.cfrom 输送渠道,班次 = case when isnull(a.shift,'') <> '' then a.shift else e.shift end,a.begin_date 出勤开始日期,a.end_date 出勤结束日期,a.begin_time 第一次打卡,a.end_time 最后一次打卡,a.begin_apply 提报开始时间,a.end_apply 提报结束时间,a.rest_hours 扣除休息时数,a.hours 实际出勤时数,a.normal_hours 正常班时数,a.overtime_hours 加班时数,a.REASON_OVERTIME 加班事由,状态= case when isnull(a.status,0) = 0 then '未提报' when isnull(a.status,0) = 1 then '已提报' when isnull(a.status,0) = 2 then '考勤员已审核' else '主管已审批' end,提报异常= case when (a.begin_apply IS null and a.END_APPLY IS null) or (DATEDIFF(second, CAST(a.begin_time as datetime) , CAST(a.begin_apply as datetime)) >=0 and DATEDIFF(second, CAST(a.end_apply as datetime) , CAST(a.end_time as datetime)) >=0) then '' else 'NG' end,a.ng_type 异常类型,s.cbegin,s.cend,s.rest_hours,s.overtime_rest_hours,s.overtime_begin,u.person_name 提报人,a.apply_time 提报时间,a.status_note 消息 from COST_TEMP_EMPLOYEE  e full join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno left join cost_shift s on case when isnull(a.shift,'') <> '' then a.shift else e.shift end = s.cname left join cost_user u on a.apply_user = u.cname where not (e.status = '离职' and a.begin_date is null and a.end_date is null) and isnull(isclose,0) = 0 and a.begin_date >= '" + dateTimePickerBegin.Text.ToString() + "' and a.begin_date <= '" + dateTimePickerEnd.Text.ToString() + "' and (a.cno = '" + textEditNo.Text.ToString().Trim() + "' or e.cname = '" + textEditName.Text.ToString().Trim() + "') and isnull(a.status,0)= case when '" + status + "' = '4' then isnull(a.status,'0') else '" + status + "' end and isnull(e.status,'在职')= case when '" + emp_status + "' = '全部' then isnull(e.status,'在职') else '" + emp_status + "' end order by e.cno";
            }
            else if (comboBoxDept.SelectedValue.ToString()!= "0")
            {
                strsql = "select e.status 员工状态,e.cno 员工工号,e.cname 姓名,e.REGISTER_DATE 报道日期,e.leave_date 离职日期,e.dept 部门,e.cfrom 输送渠道,班次 = case when isnull(a.shift,'') <> '' then a.shift else e.shift end,a.begin_date 出勤开始日期,a.end_date 出勤结束日期,a.begin_time 第一次打卡,a.end_time 最后一次打卡,a.begin_apply 提报开始时间,a.end_apply 提报结束时间,a.rest_hours 扣除休息时数,a.hours 实际出勤时数,a.normal_hours 正常班时数,a.overtime_hours 加班时数,a.REASON_OVERTIME 加班事由,状态= case when isnull(a.status,0) = 0 then '未提报' when isnull(a.status,0) = 1 then '已提报' when isnull(a.status,0) = 2 then '考勤员已审核' else '主管已审批' end,提报异常= case when (a.begin_apply IS null and a.END_APPLY IS null) or (DATEDIFF(second, CAST(a.begin_time as datetime) , CAST(a.begin_apply as datetime)) >=0 and DATEDIFF(second, CAST(a.end_apply as datetime) , CAST(a.end_time as datetime)) >=0) then '' else 'NG' end,a.ng_type 异常类型,s.cbegin,s.cend,s.rest_hours,s.overtime_rest_hours,s.overtime_begin,u.person_name 提报人,a.apply_time 提报时间,a.status_note 消息 from COST_TEMP_EMPLOYEE  e full join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno left join cost_shift s on case when isnull(a.shift,'') <> '' then a.shift else e.shift end = s.cname left join cost_user u on a.apply_user = u.cname where not (e.status = '离职' and a.begin_date is null and a.end_date is null) and isnull(isclose,0) = 0 and a.begin_date >= '" + dateTimePickerBegin.Text.ToString() + "' and a.begin_date <= '" + dateTimePickerEnd.Text.ToString() + "' and e.dept = '" + comboBoxDept.SelectedValue + "' and isnull(a.status,0)= case when '" + status + "' = '4' then isnull(a.status,'0') else '" + status + "' end and isnull(e.status,'在职')= case when '" + emp_status + "' = '全部' then isnull(e.status,'在职') else '" + emp_status + "' end order by e.cno";
            }
            else if(comboBoxDept3.SelectedValue.ToString() != "0")
            {
                strsql = "select e.status 员工状态,e.cno 员工工号,e.cname 姓名,e.REGISTER_DATE 报道日期,e.leave_date 离职日期,e.dept 部门,e.cfrom 输送渠道,班次 = case when isnull(a.shift,'') <> '' then a.shift else e.shift end,a.begin_date 出勤开始日期,a.end_date 出勤结束日期,a.begin_time 第一次打卡,a.end_time 最后一次打卡,a.begin_apply 提报开始时间,a.end_apply 提报结束时间,a.rest_hours 扣除休息时数,a.hours 实际出勤时数,a.normal_hours 正常班时数,a.overtime_hours 加班时数,a.REASON_OVERTIME 加班事由,状态= case when isnull(a.status,0) = 0 then '未提报' when isnull(a.status,0) = 1 then '已提报' when isnull(a.status,0) = 2 then '考勤员已审核' else '主管已审批' end,提报异常= case when (a.begin_apply IS null and a.END_APPLY IS null) or (DATEDIFF(second, CAST(a.begin_time as datetime) , CAST(a.begin_apply as datetime)) >=0 and DATEDIFF(second, CAST(a.end_apply as datetime) , CAST(a.end_time as datetime)) >=0) then '' else 'NG' end,a.ng_type 异常类型,s.cbegin,s.cend,s.rest_hours,s.overtime_rest_hours,s.overtime_begin,u.person_name 提报人,a.apply_time 提报时间,a.status_note 消息 from COST_TEMP_EMPLOYEE  e full join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno left join cost_shift s on case when isnull(a.shift,'') <> '' then a.shift else e.shift end = s.cname left join cost_user u on a.apply_user = u.cname where not (e.status = '离职' and a.begin_date is null and a.end_date is null) and isnull(isclose,0) = 0 and a.begin_date >= '" + dateTimePickerBegin.Text.ToString() + "' and a.begin_date <= '" + dateTimePickerEnd.Text.ToString() + "' and e.dept3 = '" + comboBoxDept3.SelectedValue + "' and e.dept2 = '" + comboBoxDept2.SelectedValue + "' and e.dept1 = '" + comboBoxDept1.SelectedValue + "' and isnull(a.status,0)= case when '" + status + "' = '4' then isnull(a.status,'0') else '" + status + "' end and isnull(e.status,'在职')= case when '" + emp_status + "' = '全部' then isnull(e.status,'在职') else '" + emp_status + "' end order by e.cno";
            }
            else if (comboBoxDept2.SelectedValue.ToString() != "0")
            {
                strsql = "select e.status 员工状态,e.cno 员工工号,e.cname 姓名,e.REGISTER_DATE 报道日期,e.leave_date 离职日期,e.dept 部门,e.cfrom 输送渠道,班次 = case when isnull(a.shift,'') <> '' then a.shift else e.shift end,a.begin_date 出勤开始日期,a.end_date 出勤结束日期,a.begin_time 第一次打卡,a.end_time 最后一次打卡,a.begin_apply 提报开始时间,a.end_apply 提报结束时间,a.rest_hours 扣除休息时数,a.hours 实际出勤时数,a.normal_hours 正常班时数,a.overtime_hours 加班时数,a.REASON_OVERTIME 加班事由,状态= case when isnull(a.status,0) = 0 then '未提报' when isnull(a.status,0) = 1 then '已提报' when isnull(a.status,0) = 2 then '考勤员已审核' else '主管已审批' end,提报异常= case when (a.begin_apply IS null and a.END_APPLY IS null) or (DATEDIFF(second, CAST(a.begin_time as datetime) , CAST(a.begin_apply as datetime)) >=0 and DATEDIFF(second, CAST(a.end_apply as datetime) , CAST(a.end_time as datetime)) >=0) then '' else 'NG' end,a.ng_type 异常类型,s.cbegin,s.cend,s.rest_hours,s.overtime_rest_hours,s.overtime_begin,u.person_name 提报人,a.apply_time 提报时间,a.status_note 消息 from COST_TEMP_EMPLOYEE  e full join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno left join cost_shift s on case when isnull(a.shift,'') <> '' then a.shift else e.shift end = s.cname left join cost_user u on a.apply_user = u.cname where not (e.status = '离职' and a.begin_date is null and a.end_date is null) and isnull(isclose,0) = 0 and a.begin_date >= '" + dateTimePickerBegin.Text.ToString() + "' and a.begin_date <= '" + dateTimePickerEnd.Text.ToString() + "' and e.dept2 = '" + comboBoxDept2.SelectedValue + "' and e.dept1 = '" + comboBoxDept1.SelectedValue + "' and isnull(a.status,0)= case when '" + status + "' = '4' then isnull(a.status,'0') else '" + status + "' end and isnull(e.status,'在职')= case when '" + emp_status + "' = '全部' then isnull(e.status,'在职') else '" + emp_status + "' end order by e.cno";
            }
            else if (comboBoxDept1.SelectedValue.ToString() != "0")
            {
                strsql = "select e.status 员工状态,e.cno 员工工号,e.cname 姓名,e.REGISTER_DATE 报道日期,e.leave_date 离职日期,e.dept 部门,e.cfrom 输送渠道,班次 = case when isnull(a.shift,'') <> '' then a.shift else e.shift end,a.begin_date 出勤开始日期,a.end_date 出勤结束日期,a.begin_time 第一次打卡,a.end_time 最后一次打卡,a.begin_apply 提报开始时间,a.end_apply 提报结束时间,a.rest_hours 扣除休息时数,a.hours 实际出勤时数,a.normal_hours 正常班时数,a.overtime_hours 加班时数,a.REASON_OVERTIME 加班事由,状态= case when isnull(a.status,0) = 0 then '未提报' when isnull(a.status,0) = 1 then '已提报' when isnull(a.status,0) = 2 then '考勤员已审核' else '主管已审批' end,提报异常= case when (a.begin_apply IS null and a.END_APPLY IS null) or (DATEDIFF(second, CAST(a.begin_time as datetime) , CAST(a.begin_apply as datetime)) >=0 and DATEDIFF(second, CAST(a.end_apply as datetime) , CAST(a.end_time as datetime)) >=0) then '' else 'NG' end,a.ng_type 异常类型,s.cbegin,s.cend,s.rest_hours,s.overtime_rest_hours,s.overtime_begin,u.person_name 提报人,a.apply_time 提报时间,a.status_note 消息 from COST_TEMP_EMPLOYEE  e full join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno left join cost_shift s on case when isnull(a.shift,'') <> '' then a.shift else e.shift end = s.cname left join cost_user u on a.apply_user = u.cname where not (e.status = '离职' and a.begin_date is null and a.end_date is null) and isnull(isclose,0) = 0 and a.begin_date >= '" + dateTimePickerBegin.Text.ToString() + "' and a.begin_date <= '" + dateTimePickerEnd.Text.ToString() + "' and e.dept1 = '" + comboBoxDept1.SelectedValue + "' and isnull(a.status,0)= case when '" + status + "' = '4' then isnull(a.status,'0') else '" + status + "' end and isnull(e.status,'在职')= case when '" + emp_status + "' = '全部' then isnull(e.status,'在职') else '" + emp_status + "' end order by e.cno";
            }
            else 
            {
                strsql = "select e.status 员工状态,e.cno 员工工号,e.cname 姓名,e.REGISTER_DATE 报道日期,e.leave_date 离职日期,e.dept 部门,e.cfrom 输送渠道,班次 = case when isnull(a.shift,'') <> '' then a.shift else e.shift end,a.begin_date 出勤开始日期,a.end_date 出勤结束日期,a.begin_time 第一次打卡,a.end_time 最后一次打卡,a.begin_apply 提报开始时间,a.end_apply 提报结束时间,a.rest_hours 扣除休息时数,a.hours 实际出勤时数,a.normal_hours 正常班时数,a.overtime_hours 加班时数,a.REASON_OVERTIME 加班事由,状态= case when isnull(a.status,0) = 0 then '未提报' when isnull(a.status,0) = 1 then '已提报' when isnull(a.status,0) = 2 then '考勤员已审核' else '主管已审批' end,提报异常= case when (a.begin_apply IS null and a.END_APPLY IS null) or (DATEDIFF(second, CAST(a.begin_time as datetime) , CAST(a.begin_apply as datetime)) >=0 and DATEDIFF(second, CAST(a.end_apply as datetime) , CAST(a.end_time as datetime)) >=0) then '' else 'NG' end,a.ng_type 异常类型,s.cbegin,s.cend,s.rest_hours,s.overtime_rest_hours,s.overtime_begin,u.person_name 提报人,a.apply_time 提报时间,a.status_note 消息 from COST_TEMP_EMPLOYEE  e full join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno left join cost_shift s on case when isnull(a.shift,'') <> '' then a.shift else e.shift end = s.cname left join cost_user u on a.apply_user = u.cname where not (e.status = '离职' and a.begin_date is null and a.end_date is null) and isnull(isclose,0) = 0 and a.begin_date >= '" + dateTimePickerBegin.Text.ToString() + "' and a.begin_date <= '" + dateTimePickerEnd.Text.ToString() + "' and isnull(a.status,0)= case when '" + status + "' = '4' then isnull(a.status,'0') else '" + status + "' end and isnull(e.status,'在职')= case when '" + emp_status + "' = '全部' then isnull(e.status,'在职') else '" + emp_status + "' end order by e.cno";
            }

            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            //gridView1.Columns[4].Visible = false;
            gridView1.Columns[18].Visible = false;
            gridView1.Columns[22].Visible = false;
            gridView1.Columns[23].Visible = false;
            gridView1.Columns[24].Visible = false;
            gridView1.Columns[25].Visible = false;
            gridView1.Columns[26].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[4].OptionsColumn.ReadOnly = true;
            gridView1.Columns[5].OptionsColumn.ReadOnly = true;
            gridView1.Columns[6].OptionsColumn.ReadOnly = true;
            //gridView1.Columns[7].OptionsColumn.ReadOnly = true;
            gridView1.Columns[8].OptionsColumn.ReadOnly = true;
            gridView1.Columns[9].OptionsColumn.ReadOnly = true;
            gridView1.Columns[10].OptionsColumn.ReadOnly = true;
            gridView1.Columns[11].OptionsColumn.ReadOnly = true;
            //gridView1.Columns[12].OptionsColumn.ReadOnly = true;
            //gridView1.Columns[13].OptionsColumn.ReadOnly = true;
            //gridView1.Columns[14].OptionsColumn.ReadOnly = true;
            //gridView1.Columns[15].OptionsColumn.ReadOnly = true;
            //gridView1.Columns[16].OptionsColumn.ReadOnly = true;
            //gridView1.Columns[17].OptionsColumn.ReadOnly = true;
            //gridView1.Columns[18].OptionsColumn.ReadOnly = true;
            //gridView1.Columns[19].OptionsColumn.ReadOnly = true;
            //gridView1.Columns[25].OptionsColumn.ReadOnly = true;
            //gridView1.Columns[26].OptionsColumn.ReadOnly = true;
            gridView1.Columns[27].OptionsColumn.ReadOnly = true;
            gridView1.Columns[28].OptionsColumn.ReadOnly = true;
            gridView1.Columns[29].OptionsColumn.ReadOnly = true;

            gridView1.Columns[1].OptionsColumn.FixedWidth = true;
            gridView1.Columns[3].OptionsColumn.FixedWidth = true;
            gridView1.Columns[4].OptionsColumn.FixedWidth = true;
            gridView1.Columns[7].OptionsColumn.FixedWidth = true;
            gridView1.Columns[8].OptionsColumn.FixedWidth = true;
            gridView1.Columns[10].OptionsColumn.FixedWidth = true;
            gridView1.Columns[11].OptionsColumn.FixedWidth = true;
            gridView1.Columns[12].OptionsColumn.FixedWidth = true;
            gridView1.Columns[13].OptionsColumn.FixedWidth = true;
            gridView1.Columns[14].OptionsColumn.FixedWidth = true;
            gridView1.Columns[15].OptionsColumn.FixedWidth = true;
            gridView1.Columns[16].OptionsColumn.FixedWidth = true;
            gridView1.Columns[17].OptionsColumn.FixedWidth = true;
            gridView1.Columns[10].Width = 52;
            gridView1.Columns[11].Width = 52;
            gridView1.Columns[12].Width = 52;
            gridView1.Columns[13].Width = 52;
            gridView1.Columns[14].Width = 52;
            gridView1.Columns[15].Width = 52;
            gridView1.Columns[16].Width = 52;
            gridView1.Columns[17].Width = 52;
            //班次下拉列表
            string sql = "select cname from cost_shift where isnull(forbidden,'false') != 'true'";
            DataSet ds2 = conn.ReturnDataSet(sql);
            ComboBoxEdit combo = new ComboBoxEdit();
            ComboBoxItemCollection coll = combo.Properties.Items;
            coll.BeginUpdate();
            try
            {
                for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                {
                    coll.Add(ds2.Tables[0].Rows[i][0].ToString());
                }
            }
            finally
            {
                coll.EndUpdate();
            }
            RepositoryItemComboBox riCombo = new RepositoryItemComboBox();;
            riCombo.Items.Assign(coll);
            gridControl1.RepositoryItems.Add(riCombo);
            gridView1.Columns[7].ColumnEdit = riCombo;
            //异常类型下拉列表
            ComboBoxEdit combo2 = new ComboBoxEdit();   
            ComboBoxItemCollection coll2 = combo2.Properties.Items;
            coll2.BeginUpdate();
            try
            {
                coll2.Add("");
                coll2.Add("忘打卡");
                coll2.Add("漏报");
                coll2.Add("多报");
                coll2.Add("未录脸");
                coll2.Add("卡机异常");
                coll2.Add("迟到");
                coll2.Add("早退");
                coll2.Add("其它");
            }
            finally
            {
                coll2.EndUpdate();
            }

            RepositoryItemComboBox riCombo2 = new RepositoryItemComboBox(); ;
            riCombo2.Items.Assign(coll2);
            gridControl1.RepositoryItems.Add(riCombo2);
            gridView1.Columns[21].ColumnEdit = riCombo2;
            //时间控件
            RepositoryItemTimeEdit riTime = new RepositoryItemTimeEdit();
            //riTime.DisplayFormat.FormatString = "HH:mm";
            //riTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //riTime.EditFormat.FormatString = "HH:mm";
            //riTime.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            riTime.EditMask = "HH:mm";
            riTime.TimeEditStyle = TimeEditStyle.TouchUI;
            riTime.Mask.UseMaskAsDisplayFormat = true;
            riTime.TouchUIMinuteIncrement = 10;
            gridControl1.RepositoryItems.Add(riTime);
            gridView1.Columns[12].ColumnEdit = riTime;
            gridView1.Columns[13].ColumnEdit = riTime;

            //表头设置
            gridView1.ColumnPanelRowHeight = 35;
            gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //表头及行内容居中显示
            //gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //设置NG
            //for (int i = 0; i < gridView1.RowCount; i++)
            //{
            //    if (gridView1.GetDataRow(i).ItemArray[19].ToString() == "NG")
            //    {
            //        gridView1.GetDataRow(i).
            //    }
            //}
            conn.Close();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            ischange = true;
            gridView1.SelectRow(e.RowHandle);
            decimal interval,hours;
            DateTime dtbegin, dtend;
            TimeSpan ts;
            if (e.Column.FieldName == "提报结束时间")
            {
                if(gridView1.GetDataRow(e.RowHandle).ItemArray[12].ToString() == "")
                {
                    MessageBox.Show("请先填写提报开始时间");
                    return;
                }
                else
                {
                    dtbegin = Convert.ToDateTime(gridView1.GetDataRow(e.RowHandle).ItemArray[12].ToString());
                }
                dtend = Convert.ToDateTime(gridView1.GetDataRow(e.RowHandle).ItemArray[13].ToString());
                ts = dtend - dtbegin;
                if (gridView1.GetDataRow(e.RowHandle).ItemArray[7].ToString().Contains("夜班"))
                {
                    ts = dtend.AddDays(1) - dtbegin;
                }                
                interval = Math.Round(ts.Hours + Convert.ToDecimal(ts.Minutes / 60.00),2, MidpointRounding.AwayFromZero);
                string shiftbegin = gridView1.GetDataRow(e.RowHandle).ItemArray[22].ToString();
                string begin = Convert.ToDateTime(gridView1.GetDataRow(e.RowHandle).ItemArray[12]).ToString("HH:mm");
                string end = Convert.ToDateTime(gridView1.GetDataRow(e.RowHandle).ItemArray[13]).ToString("HH:mm");
                if (gridView1.GetDataRow(e.RowHandle).ItemArray[7].ToString().Contains("白班"))
                {
                    if (gridView1.GetDataRow(e.RowHandle).ItemArray[22].ToString() == begin)
                    {
                        if (gridView1.GetDataRow(e.RowHandle).ItemArray[23].ToString() == end)
                        {
                            hours = interval - Convert.ToDecimal(gridView1.GetDataRow(e.RowHandle).ItemArray[24]);
                            hours = (Convert.ToInt32(hours * 100) / 50) * Convert.ToDecimal(0.5);
                            gridView1.SetRowCellValue(e.RowHandle, "扣除休息时数", gridView1.GetDataRow(e.RowHandle).ItemArray[24].ToString());
                            gridView1.SetRowCellValue(e.RowHandle, "实际出勤时数", (int)(hours * 100) / 100.00);
                            gridView1.SetRowCellValue(e.RowHandle, "正常班时数", (int)(hours * 100) / 100.00);
                            gridView1.SetRowCellValue(e.RowHandle, "加班时数", 0);
                        }
                        else if (DateTime.Compare(Convert.ToDateTime(gridView1.GetDataRow(e.RowHandle).ItemArray[23].ToString()), Convert.ToDateTime(end)) < 0 && DateTime.Compare(Convert.ToDateTime(gridView1.GetDataRow(e.RowHandle).ItemArray[26].ToString()), Convert.ToDateTime(end)) < 0)
                        {
                            hours = interval - Convert.ToDecimal(gridView1.GetDataRow(e.RowHandle).ItemArray[25]);
                            hours = (Convert.ToInt32(hours*100)/50)*Convert.ToDecimal(0.5);
                            gridView1.SetRowCellValue(e.RowHandle, "扣除休息时数", gridView1.GetDataRow(e.RowHandle).ItemArray[25].ToString());
                            gridView1.SetRowCellValue(e.RowHandle, "实际出勤时数", (int)(hours * 100) / 100.00);
                            gridView1.SetRowCellValue(e.RowHandle, "正常班时数", 8);
                            gridView1.SetRowCellValue(e.RowHandle, "加班时数", (int)(hours * 100) / 100.00 - 8);
                        }
                    }
                }
                else
                {
                    if (gridView1.GetDataRow(e.RowHandle).ItemArray[22].ToString() == begin)
                    {
                        if (gridView1.GetDataRow(e.RowHandle).ItemArray[23].ToString() == end)
                        {
                            hours = interval - Convert.ToDecimal(gridView1.GetDataRow(e.RowHandle).ItemArray[24]);
                            hours = (Convert.ToInt32(hours * 100) / 50) * Convert.ToDecimal(0.5);
                            gridView1.SetRowCellValue(e.RowHandle, "扣除休息时数", gridView1.GetDataRow(e.RowHandle).ItemArray[24].ToString());
                            gridView1.SetRowCellValue(e.RowHandle, "实际出勤时数", (int)(hours * 100) / 100.00);
                            gridView1.SetRowCellValue(e.RowHandle, "正常班时数", (int)(hours * 100) / 100.00);
                            gridView1.SetRowCellValue(e.RowHandle, "加班时数", 0);
                        }
                        else if (DateTime.Compare(Convert.ToDateTime(gridView1.GetDataRow(e.RowHandle).ItemArray[23].ToString()), Convert.ToDateTime(end)) < 0 && DateTime.Compare(Convert.ToDateTime(gridView1.GetDataRow(e.RowHandle).ItemArray[26].ToString()), Convert.ToDateTime(end)) < 0 && DateTime.Compare(Convert.ToDateTime(gridView1.GetDataRow(e.RowHandle).ItemArray[22].ToString()), Convert.ToDateTime(end)) > 0)
                        {
                            hours = interval - Convert.ToDecimal(gridView1.GetDataRow(e.RowHandle).ItemArray[25]);
                            hours = (Convert.ToInt32(hours * 100) / 50) * Convert.ToDecimal(0.5);
                            gridView1.SetRowCellValue(e.RowHandle, "扣除休息时数", gridView1.GetDataRow(e.RowHandle).ItemArray[25].ToString());
                            gridView1.SetRowCellValue(e.RowHandle, "实际出勤时数", (int)(hours * 100) / 100.00);
                            gridView1.SetRowCellValue(e.RowHandle, "正常班时数", 8);
                            gridView1.SetRowCellValue(e.RowHandle, "加班时数", (int)(hours * 100) / 100.00 - 8);
                        }
                    }
                }
               ////如果打卡结束时间不为空
               // if(gridView1.GetDataRow(e.RowHandle).ItemArray[11].ToString() != "")
               // {
               //     //如果提报结束时间大于打卡结束时间
               //     if (DateTime.Compare(Convert.ToDateTime(gridView1.GetDataRow(e.RowHandle).ItemArray[13].ToString()), Convert.ToDateTime(gridView1.GetDataRow(e.RowHandle).ItemArray[11].ToString())) > 0)
               //     {
               //         gridView1.SetRowCellValue(e.RowHandle, "提报异常", "NG");
               //     }
               //     else if (gridView1.GetDataRow(e.RowHandle).ItemArray[12].ToString() != "" && gridView1.GetDataRow(e.RowHandle).ItemArray[10].ToString() !="")
               //     {
               //         if (DateTime.Compare(Convert.ToDateTime(gridView1.GetDataRow(e.RowHandle).ItemArray[12].ToString()), Convert.ToDateTime(gridView1.GetDataRow(e.RowHandle).ItemArray[10].ToString())) < 0)
               //         {
               //             gridView1.SetRowCellValue(e.RowHandle, "提报异常", "NG");
               //         }
               //         else
               //         {
               //             gridView1.SetRowCellValue(e.RowHandle, "提报异常", "");
               //         }
               //     }
               //     else
               //     {
               //         gridView1.SetRowCellValue(e.RowHandle, "提报异常", "NG");
               //     }
               // }
               // else
               // {
               //     gridView1.SetRowCellValue(e.RowHandle, "提报异常", "NG");
               // }

            }
            //if (e.Column.FieldName == "提报开始时间")
            //{
            //    if (gridView1.GetDataRow(e.RowHandle).ItemArray[10].ToString() != "")
            //    {
            //        //如果提报开始时间小于打卡开始时间
            //        if (DateTime.Compare(Convert.ToDateTime(gridView1.GetDataRow(e.RowHandle).ItemArray[12].ToString()), Convert.ToDateTime(gridView1.GetDataRow(e.RowHandle).ItemArray[10].ToString())) < 0)
            //        {
            //            gridView1.SetRowCellValue(e.RowHandle, "提报异常", "NG");
            //        }
            //        else if (gridView1.GetDataRow(e.RowHandle).ItemArray[13].ToString() != "" && gridView1.GetDataRow(e.RowHandle).ItemArray[11].ToString() != "")
            //        {
            //            if (DateTime.Compare(Convert.ToDateTime(gridView1.GetDataRow(e.RowHandle).ItemArray[13].ToString()), Convert.ToDateTime(gridView1.GetDataRow(e.RowHandle).ItemArray[11].ToString())) > 0)
            //            {
            //                gridView1.SetRowCellValue(e.RowHandle, "提报异常", "NG");
            //            }
            //            else
            //            {
            //                gridView1.SetRowCellValue(e.RowHandle, "提报异常", "");
            //            }
            //        }
            //        else
            //        {
            //            gridView1.SetRowCellValue(e.RowHandle, "提报异常", "NG");
            //        }
            //    }
            //    else
            //    {
            //        gridView1.SetRowCellValue(e.RowHandle, "提报异常", "NG");
            //    }
            //}
        }
        private void simpleButton查询_Click(object sender, EventArgs e)
        {
            showDetail();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //showDetail();
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

        private void TempEmpQuery_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            BindDept1();
            BindDept2();
            //BindDept3();
            //BindDept();
            
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            //DateTime dt = DateTime.Now;
            //DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            //this.dateTimePickerBegin.Value = startMonth;
            //dateTimePickerBegin.Focus();
            //SendKeys.Send("{RIGHT} ");
            dateTimePickerBegin.Text = DateTime.Now.AddDays(-1).ToString();
            //绑定提报状态
            Dictionary<int, string> kvDictonary = new Dictionary<int, string>();
            kvDictonary.Add(0, "未提报");
            kvDictonary.Add(1, "已提报");
            kvDictonary.Add(2, "考勤员已审核");
            kvDictonary.Add(3, "主管已审批");
            kvDictonary.Add(4, "全部");

            BindingSource bs = new BindingSource();
            bs.DataSource = kvDictonary;
            comboBoxStatus.DataSource = bs;
            comboBoxStatus.ValueMember = "Key";
            comboBoxStatus.DisplayMember = "Value";
            comboBoxStatus.SelectedIndex = 4;
            //绑定人员状态
            Dictionary<string, string> kvDictonary2 = new Dictionary<string, string>();
            kvDictonary2.Add("在职", "在职");
            kvDictonary2.Add("离职", "离职");
            kvDictonary2.Add("转正", "转正");
            kvDictonary2.Add("全部", "全部");

            BindingSource bs2 = new BindingSource();
            bs2.DataSource = kvDictonary2;
            comboBoxEmpStatus.DataSource = bs2;
            comboBoxEmpStatus.ValueMember = "Key";
            comboBoxEmpStatus.DisplayMember = "Value";
            comboBoxEmpStatus.SelectedIndex = 0;
            //默认二级部门
            if(Logon.GetDept2() !="")
            {
                comboBoxDept2.SelectedIndex = -1;
                comboBoxDept2.SelectedValue = Logon.GetDept2();
            }
            
            showDetail();
        }

        //private void comboBoxDept1_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    BindDept2();
        //}

        //private void comboBoxDept2_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    BindDept3();
        //}

        //private void comboBoxDept3_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    BindDept();
        //}

        private void comboBoxDept1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxDept1.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                BindDept2();
            }
        }

        private void comboBoxDept2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxDept2.SelectedValue != null && comboBoxDept2.SelectedValue.ToString() != "System.Data.DataRowView")
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
        /// <summary>
        /// 批量提报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql,sql2, begin, end,month,cdate;
            bool isok = false, right = true,right2 = true,closed= false;
            DataSet ds;
            right2 = SubmitRight();
            //gridView1.FocusInvalidRow();
            if (right2)
            {
                if (ischange == true)
                {

                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {

                        cdate = gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[8].ToString();
                        if (Convert.ToDateTime(cdate) > DayOfMonth(Convert.ToDateTime(cdate)))
                        {
                            month = NextMonth(Convert.ToDateTime(cdate));
                        }
                        else
                        {
                            month = Convert.ToDateTime(cdate).ToString("yyyy-MM");
                        }
                        sql2 = "select closed from cost_temp_close where cmonth = '" + month + "'";
                        ds = conn.ReturnDataSet(sql2);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "True")
                            {
                                closed = true;
                            }
                        }
                        if (gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[12].ToString() == "")
                        {
                            begin = "null";
                        }
                        else
                        {
                            begin = Convert.ToDateTime(gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[12]).ToString("HH:mm");
                        }
                        if (gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[13].ToString() == "")
                        {
                            end = "null";
                        }
                        else
                        {
                            end = Convert.ToDateTime(gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[13]).ToString("HH:mm");
                        }
                        right = SubmitRight(weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString());

                        if (!right)
                        {
                            continue;
                        }
                        else if (closed)
                        {
                            MessageBox.Show("该月考勤已经关闭，不能再提报！");
                            break;
                        }
                        else if (weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[19].ToString() != "未提报")
                        {
                            MessageBox.Show("只能提报未提报的记录！");
                            break;
                        }
                        else
                        {
                            sql = "update COST_TEMP_EMPLOYEE_ATTENDANCE set shift = '" + gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[7].ToString() + "',BEGIN_APPLY = '" + begin + "',END_APPLY = '" + end + "',rest_hours = '" + gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[14].ToString() + "',hours = '" + gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[15].ToString() + "',normal_hours = '" + gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[16].ToString() + "',overtime_hours = '" + gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[17].ToString() + "',apply_user = '" + Logon.GetCname() + "',apply_time = '" + DateTime.Now.ToString() + "',ng_type = '" + gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[21].ToString() + "',status = 1";
                            sql = sql + " where cno = '" + gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[1].ToString() + "' and begin_date = '" + gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[8].ToString() + "' and end_date = '" + gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[9].ToString() + "' and isnull(isclose,0) = 0";
                            isok = conn.EditDatabase(sql);
                        }

                    }
                    if (isok)
                    {
                        MessageBox.Show("提交成功！");
                        showDetail();
                    }
                    else
                    {
                        MessageBox.Show("失败！");
                    }
                    conn.Close();
                    ischange = false;
                }
                else
                {
                    MessageBox.Show("没有可提交的数据！");
                }
            }
            else
            {
                MessageBox.Show("没有权限！");
            }
        }
        /// <summary>
        /// 获取上个月26号
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        private DateTime DayOfLastMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(-1).AddDays(25);
        }
        /// <summary>
        /// 某月第25号
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        private DateTime DayOfMonth(DateTime datetime)
        {
            return datetime.AddDays(25 - datetime.Day);
        }
        private string NextMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(1).ToString("yyyy-MM");
        }
        private void simpleButtonExport_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, yyyymm, status, emp_status;
            yyyymm = dateTimePickerBegin.Text.ToString();
            status = comboBoxStatus.SelectedIndex.ToString();
            emp_status = comboBoxEmpStatus.SelectedValue.ToString();
            simpleButtonExport.Enabled = false;
            if (textEditNo.Text.ToString().Trim() != "" || textEditName.Text.ToString().Trim() != "")
            {
                strsql = "select e.status 员工状态,'''' + e.cno 员工工号,e.cname 姓名,e.REGISTER_DATE 报道日期,e.leave_date 离职日期,e.dept 部门,e.cfrom 输送渠道,班次 = case when isnull(a.shift,'') <> '' then a.shift else e.shift end,a.begin_date 出勤开始日期,a.end_date 出勤结束日期,a.begin_time 第一次打卡,a.end_time 最后一次打卡,a.begin_apply 提报开始时间,a.end_apply 提报结束时间,a.rest_hours 扣除休息时数,a.hours 实际出勤时数,a.normal_hours 正常班时数,a.overtime_hours 加班时数,a.REASON_OVERTIME 加班事由,状态= case when isnull(a.status,0) = 0 then '未提报' when isnull(a.status,0) = 1 then '已提报' when isnull(a.status,0) = 2 then '考勤员已审核' else '主管已审批' end,提报异常= case when (a.begin_apply IS null and a.END_APPLY IS null) or (DATEDIFF(second, CAST(a.begin_time as datetime) , CAST(a.begin_apply as datetime)) >=0 and DATEDIFF(second, CAST(a.end_apply as datetime) , CAST(a.end_time as datetime)) >=0) then '' else 'NG' end,a.ng_type 异常类型,u.person_name 提报人,a.apply_time 提报时间 from COST_TEMP_EMPLOYEE  e full join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno left join cost_shift s on case when isnull(a.shift,'') <> '' then a.shift else e.shift end = s.cname left join cost_user u on a.apply_user = u.cname where not (e.status = '离职' and a.begin_date is null and a.end_date is null) and isnull(isclose,0) = 0 and a.begin_date >= '" + dateTimePickerBegin.Text.ToString() + "' and a.begin_date <= '" + dateTimePickerEnd.Text.ToString() + "' and (a.cno = '" + textEditNo.Text.ToString().Trim() + "' or e.cname = '" + textEditName.Text.ToString().Trim() + "') and isnull(a.status,0)= case when '" + status + "' = '4' then isnull(a.status,'0') else '" + status + "' end and isnull(e.status,'在职')= case when '" + emp_status + "' = '全部' then isnull(e.status,'在职') else '" + emp_status + "' end order by e.cno";
            }
            else if (comboBoxDept.SelectedValue.ToString() != "0")
            {
                strsql = "select e.status 员工状态,'''' + e.cno 员工工号,e.cname 姓名,e.REGISTER_DATE 报道日期,e.leave_date 离职日期,e.dept 部门,e.cfrom 输送渠道,班次 = case when isnull(a.shift,'') <> '' then a.shift else e.shift end,a.begin_date 出勤开始日期,a.end_date 出勤结束日期,a.begin_time 第一次打卡,a.end_time 最后一次打卡,a.begin_apply 提报开始时间,a.end_apply 提报结束时间,a.rest_hours 扣除休息时数,a.hours 实际出勤时数,a.normal_hours 正常班时数,a.overtime_hours 加班时数,a.REASON_OVERTIME 加班事由,状态= case when isnull(a.status,0) = 0 then '未提报' when isnull(a.status,0) = 1 then '已提报' when isnull(a.status,0) = 2 then '考勤员已审核' else '主管已审批' end,提报异常= case when (a.begin_apply IS null and a.END_APPLY IS null) or (DATEDIFF(second, CAST(a.begin_time as datetime) , CAST(a.begin_apply as datetime)) >=0 and DATEDIFF(second, CAST(a.end_apply as datetime) , CAST(a.end_time as datetime)) >=0) then '' else 'NG' end,a.ng_type 异常类型,u.person_name 提报人,a.apply_time 提报时间 from COST_TEMP_EMPLOYEE  e full join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno left join cost_shift s on case when isnull(a.shift,'') <> '' then a.shift else e.shift end = s.cname left join cost_user u on a.apply_user = u.cname where not (e.status = '离职' and a.begin_date is null and a.end_date is null) and isnull(isclose,0) = 0 and a.begin_date >= '" + dateTimePickerBegin.Text.ToString() + "' and a.begin_date <= '" + dateTimePickerEnd.Text.ToString() + "' and e.dept = '" + comboBoxDept.SelectedValue + "' and isnull(a.status,0)= case when '" + status + "' = '4' then isnull(a.status,'0') else '" + status + "' end and isnull(e.status,'在职')= case when '" + emp_status + "' = '全部' then isnull(e.status,'在职') else '" + emp_status + "' end order by e.cno";
            }
            else if (comboBoxDept3.SelectedValue.ToString() != "0")
            {
                strsql = "select e.status 员工状态,'''' + e.cno 员工工号,e.cname 姓名,e.REGISTER_DATE 报道日期,e.leave_date 离职日期,e.dept 部门,e.cfrom 输送渠道,班次 = case when isnull(a.shift,'') <> '' then a.shift else e.shift end,a.begin_date 出勤开始日期,a.end_date 出勤结束日期,a.begin_time 第一次打卡,a.end_time 最后一次打卡,a.begin_apply 提报开始时间,a.end_apply 提报结束时间,a.rest_hours 扣除休息时数,a.hours 实际出勤时数,a.normal_hours 正常班时数,a.overtime_hours 加班时数,a.REASON_OVERTIME 加班事由,状态= case when isnull(a.status,0) = 0 then '未提报' when isnull(a.status,0) = 1 then '已提报' when isnull(a.status,0) = 2 then '考勤员已审核' else '主管已审批' end,提报异常= case when (a.begin_apply IS null and a.END_APPLY IS null) or (DATEDIFF(second, CAST(a.begin_time as datetime) , CAST(a.begin_apply as datetime)) >=0 and DATEDIFF(second, CAST(a.end_apply as datetime) , CAST(a.end_time as datetime)) >=0) then '' else 'NG' end,a.ng_type 异常类型,u.person_name 提报人,a.apply_time 提报时间 from COST_TEMP_EMPLOYEE  e full join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno left join cost_shift s on case when isnull(a.shift,'') <> '' then a.shift else e.shift end = s.cname left join cost_user u on a.apply_user = u.cname where not (e.status = '离职' and a.begin_date is null and a.end_date is null) and isnull(isclose,0) = 0 and a.begin_date >= '" + dateTimePickerBegin.Text.ToString() + "' and a.begin_date <= '" + dateTimePickerEnd.Text.ToString() + "' and e.dept3 = '" + comboBoxDept3.SelectedValue + "' and e.dept2 = '" + comboBoxDept2.SelectedValue + "' and e.dept1 = '" + comboBoxDept1.SelectedValue + "' and isnull(a.status,0)= case when '" + status + "' = '4' then isnull(a.status,'0') else '" + status + "' end and isnull(e.status,'在职')= case when '" + emp_status + "' = '全部' then isnull(e.status,'在职') else '" + emp_status + "' end order by e.cno";
            }
            else if (comboBoxDept2.SelectedValue.ToString() != "0")
            {
                strsql = "select e.status 员工状态,'''' + e.cno 员工工号,e.cname 姓名,e.REGISTER_DATE 报道日期,e.leave_date 离职日期,e.dept 部门,e.cfrom 输送渠道,班次 = case when isnull(a.shift,'') <> '' then a.shift else e.shift end,a.begin_date 出勤开始日期,a.end_date 出勤结束日期,a.begin_time 第一次打卡,a.end_time 最后一次打卡,a.begin_apply 提报开始时间,a.end_apply 提报结束时间,a.rest_hours 扣除休息时数,a.hours 实际出勤时数,a.normal_hours 正常班时数,a.overtime_hours 加班时数,a.REASON_OVERTIME 加班事由,状态= case when isnull(a.status,0) = 0 then '未提报' when isnull(a.status,0) = 1 then '已提报' when isnull(a.status,0) = 2 then '考勤员已审核' else '主管已审批' end,提报异常= case when (a.begin_apply IS null and a.END_APPLY IS null) or (DATEDIFF(second, CAST(a.begin_time as datetime) , CAST(a.begin_apply as datetime)) >=0 and DATEDIFF(second, CAST(a.end_apply as datetime) , CAST(a.end_time as datetime)) >=0) then '' else 'NG' end,a.ng_type 异常类型,u.person_name 提报人,a.apply_time 提报时间 from COST_TEMP_EMPLOYEE  e full join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno left join cost_shift s on case when isnull(a.shift,'') <> '' then a.shift else e.shift end = s.cname left join cost_user u on a.apply_user = u.cname where not (e.status = '离职' and a.begin_date is null and a.end_date is null) and isnull(isclose,0) = 0 and a.begin_date >= '" + dateTimePickerBegin.Text.ToString() + "' and a.begin_date <= '" + dateTimePickerEnd.Text.ToString() + "' and e.dept2 = '" + comboBoxDept2.SelectedValue + "' and e.dept1 = '" + comboBoxDept1.SelectedValue + "' and isnull(a.status,0)= case when '" + status + "' = '4' then isnull(a.status,'0') else '" + status + "' end and isnull(e.status,'在职')= case when '" + emp_status + "' = '全部' then isnull(e.status,'在职') else '" + emp_status + "' end order by e.cno";
            }
            else if (comboBoxDept1.SelectedValue.ToString() != "0")
            {
                strsql = "select e.status 员工状态,'''' + e.cno 员工工号,e.cname 姓名,e.REGISTER_DATE 报道日期,e.leave_date 离职日期,e.dept 部门,e.cfrom 输送渠道,班次 = case when isnull(a.shift,'') <> '' then a.shift else e.shift end,a.begin_date 出勤开始日期,a.end_date 出勤结束日期,a.begin_time 第一次打卡,a.end_time 最后一次打卡,a.begin_apply 提报开始时间,a.end_apply 提报结束时间,a.rest_hours 扣除休息时数,a.hours 实际出勤时数,a.normal_hours 正常班时数,a.overtime_hours 加班时数,a.REASON_OVERTIME 加班事由,状态= case when isnull(a.status,0) = 0 then '未提报' when isnull(a.status,0) = 1 then '已提报' when isnull(a.status,0) = 2 then '考勤员已审核' else '主管已审批' end,提报异常= case when (a.begin_apply IS null and a.END_APPLY IS null) or (DATEDIFF(second, CAST(a.begin_time as datetime) , CAST(a.begin_apply as datetime)) >=0 and DATEDIFF(second, CAST(a.end_apply as datetime) , CAST(a.end_time as datetime)) >=0) then '' else 'NG' end,a.ng_type 异常类型,u.person_name 提报人,a.apply_time 提报时间 from COST_TEMP_EMPLOYEE  e full join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno left join cost_shift s on case when isnull(a.shift,'') <> '' then a.shift else e.shift end = s.cname left join cost_user u on a.apply_user = u.cname where not (e.status = '离职' and a.begin_date is null and a.end_date is null) and isnull(isclose,0) = 0 and a.begin_date >= '" + dateTimePickerBegin.Text.ToString() + "' and a.begin_date <= '" + dateTimePickerEnd.Text.ToString() + "' and e.dept1 = '" + comboBoxDept1.SelectedValue + "' and isnull(a.status,0)= case when '" + status + "' = '4' then isnull(a.status,'0') else '" + status + "' end and isnull(e.status,'在职')= case when '" + emp_status + "' = '全部' then isnull(e.status,'在职') else '" + emp_status + "' end order by e.cno";
            }
            else
            {
                strsql = "select e.status 员工状态,'''' + e.cno 员工工号,e.cname 姓名,e.REGISTER_DATE 报道日期,e.leave_date 离职日期,e.dept 部门,e.cfrom 输送渠道,班次 = case when isnull(a.shift,'') <> '' then a.shift else e.shift end,a.begin_date 出勤开始日期,a.end_date 出勤结束日期,a.begin_time 第一次打卡,a.end_time 最后一次打卡,a.begin_apply 提报开始时间,a.end_apply 提报结束时间,a.rest_hours 扣除休息时数,a.hours 实际出勤时数,a.normal_hours 正常班时数,a.overtime_hours 加班时数,a.REASON_OVERTIME 加班事由,状态= case when isnull(a.status,0) = 0 then '未提报' when isnull(a.status,0) = 1 then '已提报' when isnull(a.status,0) = 2 then '考勤员已审核' else '主管已审批' end,提报异常= case when (a.begin_apply IS null and a.END_APPLY IS null) or (DATEDIFF(second, CAST(a.begin_time as datetime) , CAST(a.begin_apply as datetime)) >=0 and DATEDIFF(second, CAST(a.end_apply as datetime) , CAST(a.end_time as datetime)) >=0) then '' else 'NG' end,a.ng_type 异常类型,u.person_name 提报人,a.apply_time 提报时间 from COST_TEMP_EMPLOYEE  e full join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno left join cost_shift s on case when isnull(a.shift,'') <> '' then a.shift else e.shift end = s.cname left join cost_user u on a.apply_user = u.cname where not (e.status = '离职' and a.begin_date is null and a.end_date is null) and isnull(isclose,0) = 0 and a.begin_date >= '" + dateTimePickerBegin.Text.ToString() + "' and a.begin_date <= '" + dateTimePickerEnd.Text.ToString() + "' and isnull(a.status,0)= case when '" + status + "' = '4' then isnull(a.status,'0') else '" + status + "' end and isnull(e.status,'在职')= case when '" + emp_status + "' = '全部' then isnull(e.status,'在职') else '" + emp_status + "' end order by e.cno";
            }
            DataSet ds = conn.ReturnDataSet(strsql);
            bool isok = Common.DataSetToExcel(ds, true);
            if (isok)
            {
                simpleButtonExport.Enabled = true;
                MessageBox.Show("导出完成！");
                
            }
        }

        private void timeEditBegin_EditValueChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                 gridView1.SetRowCellValue(gridView1.GetSelectedRows()[i], "提报开始时间", timeEditBegin.Text.ToString());
            }
            ischange = true;
        }

        private void timeEditEnd_EditValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                gridView1.SetRowCellValue(gridView1.GetSelectedRows()[i], "提报结束时间", timeEditEnd.Text.ToString());
            }
            ischange = true;
        }

        private void simpleButtonSetTime_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                gridView1.SetRowCellValue(gridView1.GetSelectedRows()[i], "提报开始时间", timeEditBegin.Text.ToString());
                gridView1.SetRowCellValue(gridView1.GetSelectedRows()[i], "提报结束时间", timeEditEnd.Text.ToString());
            }
            ischange = true;
        }

        private void simpleButtonCalculateTime_Click(object sender, EventArgs e)
        {
            DateTime dkbegin, dkend,bcbegin,bcend,jbbegin,temp;
            string shift;
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                //班次
                shift = gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[7].ToString();
                //班次开始时间
                bcbegin = Convert.ToDateTime(gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[22]);
                //班次结束时间
                bcend = Convert.ToDateTime(gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[23]);
                //加班开始时间
                jbbegin = Convert.ToDateTime(gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[26]);
                if(gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[10].ToString() == "" || gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[11].ToString() == "")
                {
                    continue;
                }
                //白班
                else if (gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[7].ToString().Contains("白班"))
                {
                    //打卡开始时间
                    dkbegin = Convert.ToDateTime(gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[10]);
                    //打卡结束时间
                    dkend = Convert.ToDateTime(gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[11]);
                    if (DateTime.Compare(dkbegin, bcbegin) > 0)
                    {
                        continue;
                    }
                    else if(DateTime.Compare(dkend, bcend) < 0)
                    {
                        continue;
                    }
                    else 
                    {
                        gridView1.SetRowCellValue(gridView1.GetSelectedRows()[i], "提报开始时间", bcbegin.ToString("HH:mm"));
                        if (DateTime.Compare(dkend, jbbegin) <= 0)
                        {
                            gridView1.SetRowCellValue(gridView1.GetSelectedRows()[i], "提报结束时间", bcend.ToString("HH:mm"));
                        }
                        else
                        {
                            TimeSpan ts;
                            ts = dkend - jbbegin;
                            temp = jbbegin;
                            temp = temp.AddHours(ts.Hours);
                            temp = temp.AddMinutes((ts.Minutes / 30) * 30);
                            gridView1.SetRowCellValue(gridView1.GetSelectedRows()[i], "提报结束时间", temp.ToString("HH:mm"));
                        }
                    }
                }
                else//晚班
                {
                    //打卡开始时间
                    dkbegin = Convert.ToDateTime(gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[10]);
                    //打卡结束时间
                    dkend = Convert.ToDateTime(gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[11]);
                    if (DateTime.Compare(dkbegin, bcbegin) > 0 || DateTime.Compare(dkbegin, bcend) < 0)
                    {
                        continue;
                    }
                    else if (DateTime.Compare(dkend, bcend) < 0 || DateTime.Compare(dkend, bcbegin) > 0)
                    {
                        continue;
                    }
                    else
                    {
                        gridView1.SetRowCellValue(gridView1.GetSelectedRows()[i], "提报开始时间", bcbegin.ToString("HH:mm"));
                        if (DateTime.Compare(dkend, jbbegin) <= 0)
                        {
                            gridView1.SetRowCellValue(gridView1.GetSelectedRows()[i], "提报结束时间", bcend.ToString("HH:mm"));
                        }
                        else
                        {
                            TimeSpan ts;
                            ts = dkend - jbbegin;
                            temp = jbbegin;
                            temp = temp.AddHours(ts.Hours);
                            temp = temp.AddMinutes((ts.Minutes / 30) * 30);
                            gridView1.SetRowCellValue(gridView1.GetSelectedRows()[i], "提报结束时间", temp.ToString("HH:mm"));
                        }
                    }
                }
            }
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (gridView1.GetDataRow(e.RowHandle) == null) return;
            ////if(e.CellValue.ToString() != "NG") return;
            //if (gridView1.GetDataRow(e.RowHandle)["提报异常"].ToString() == "NG" && e.RowHandle != gridView1.FocusedRowHandle)
            //{
            //    ////e.Appearance.BackColor = Color.LightCoral;
            //    //if(e.CellValue.ToString() == "NG")
            //    //{
            //    StyleFormatCondition cn;
            //        cn = new StyleFormatCondition(FormatConditionEnum.GreaterOrEqual, gridView1.Columns["提报异常"], null, 1);
            //        cn.ApplyToRow = false;
            //        cn.Appearance.BackColor = Color.LightCoral;
            //        gridView1.FormatConditions.Add(cn);
            //    //}
               
            //}

            //if (e.RowHandle == gridView1.FocusedRowHandle && gridView1.GetDataRow(e.RowHandle)["提报异常"].ToString() != "NG")
            //{
            //    e.Appearance.BackColor = Color.Lavender;
            //}
            //if (e.RowHandle == gridView1.FocusedRowHandle && gridView1.GetDataRow(e.RowHandle)["提报异常"].ToString() == "NG")
            //{
            //    e.Appearance.BackColor = Color.LightCoral;
            //}
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString().Trim();
            }
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "提报异常")
            {
                e.Appearance.ForeColor = Color.Red;
            }
        }

        private void textEditNo_KeyDown(object sender, KeyEventArgs e)
        {
            ConnDB conn = new ConnDB();
            DataSet ds;
            string sql,status, emp_status;
            status = comboBoxStatus.SelectedIndex.ToString();
            emp_status = comboBoxEmpStatus.SelectedValue.ToString();
            if (e.KeyCode == Keys.Enter)
            {
                mlist.Add(textEditNo.ToString().Trim());
                if (textEditNo.Text.ToString().Trim() != "" || textEditName.Text.ToString().Trim() != "")
                {
                    sql = "select e.status 员工状态,e.cno 员工工号,e.cname 姓名,e.REGISTER_DATE 报道日期,e.leave_date 离职日期,e.dept 部门,e.cfrom 输送渠道,班次 = case when isnull(a.shift,'') <> '' then a.shift else e.shift end,a.begin_date 出勤开始日期,a.end_date 出勤结束日期,a.begin_time 第一次打卡,a.end_time 最后一次打卡,a.begin_apply 提报开始时间,a.end_apply 提报结束时间,a.rest_hours 扣除休息时数,a.hours 实际出勤时数,a.normal_hours 正常班时数,a.overtime_hours 加班时数,a.REASON_OVERTIME 加班事由,状态= case when isnull(a.status,0) = 0 then '未提报' when isnull(a.status,0) = 1 then '已提报' when isnull(a.status,0) = 2 then '考勤员已审核' else '主管已审批' end,提报异常= case when (a.begin_apply IS null and a.END_APPLY IS null) or (DATEDIFF(second, CAST(a.begin_time as datetime) , CAST(a.begin_apply as datetime)) >=0 and DATEDIFF(second, CAST(a.end_apply as datetime) , CAST(a.end_time as datetime)) >=0) then '' else 'NG' end,a.ng_type 异常类型,s.cbegin,s.cend,s.rest_hours,s.overtime_rest_hours,s.overtime_begin,u.person_name 提报人,a.apply_time 提报时间,a.status_note 消息 from COST_TEMP_EMPLOYEE  e full join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno left join cost_shift s on case when isnull(a.shift,'') <> '' then a.shift else e.shift end = s.cname left join cost_user u on a.apply_user = u.cname where not (e.status = '离职' and a.begin_date is null and a.end_date is null) and isnull(isclose,0) = 0 and a.begin_date >= '" + dateTimePickerBegin.Text.ToString() + "' and a.begin_date <= '" + dateTimePickerEnd.Text.ToString() + "' and (a.cno = '" + textEditNo.Text.ToString().Trim() + "' or e.cname = '" + textEditName.Text.ToString().Trim() + "') and isnull(a.status,0)= case when '" + status + "' = '4' then isnull(a.status,'0') else '" + status + "' end and isnull(e.status,'在职')= case when '" + emp_status + "' = '全部' then isnull(e.status,'在职') else '" + emp_status + "' end order by e.cno";
                    ds = conn.ReturnDataSet(sql);
                    if(mds == null)
                    {
                        mds = ds;
                    }
                    else
                    {
                        mds.Merge(ds, true, MissingSchemaAction.Add);
                    }
                }
                gridControl1.DataSource = mds.Tables[0].DefaultView;
                textEditNo.Text = "";

            }
        }
    }
}