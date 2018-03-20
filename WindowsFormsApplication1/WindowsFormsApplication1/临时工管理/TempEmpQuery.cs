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
    public partial class TempEmpQuery : DevExpress.XtraEditors.XtraForm
    {
        DataSet tods;
        bool isall = false;
        public TempEmpQuery()
        {
            InitializeComponent();
        }
        private static TempEmpQuery weqform = null;

        public static TempEmpQuery GetInstance()
        {
            if (weqform == null || weqform.IsDisposed)
            {
                weqform = new TempEmpQuery();
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
        public static void Delete()
        {
            string sql;
            bool isdone = true;
            ConnDB conn = new ConnDB();
            if (weqform == null || weqform.IsDisposed)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }
            else if (weqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除吗?", "删除", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < weqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "delete from  COST_TEMP_EMPLOYEE where cno = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[0].ToString() + "'";
                        isdone = conn.DeleteDatabase(sql);
                        if (!isdone)
                            break;
                    }
                    if (isdone)
                    {
                        MessageBox.Show("删除成功！");
                    }
                }
            }
            conn.Close();
        }
        public static void GetInfo(ref string cno, ref string cname,ref string sex,ref string register_date,ref string leave_date,ref string cfrom,ref int fromtype,ref string dept1,ref string dept2,ref string dept3,ref string dept,ref string id_number,ref string phone_no,ref string shift,ref string status)
        {
            if (weqform == null || weqform.IsDisposed)
            {
                MessageBox.Show("没有选中要修改的记录！");
            }
            else if (weqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要修改的记录！");
            }
            else
            {
                cno =weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[0].ToString();
                cname = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString();
                sex = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[2].ToString();
                register_date = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[3].ToString();
                if(weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[4].ToString() == "")
                {
                    leave_date = "";
                }
                else
                {
                    leave_date = Convert.ToDateTime(weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[4]).ToString("yyyy-MM-dd");
                }
                cfrom = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[5].ToString();
                if(weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[6].ToString() == "")
                {
                    fromtype = 0;
                }
                else
                {
                    fromtype = Convert.ToInt32(weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[6].ToString());
                }

                dept1 = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[8].ToString();
                dept2 = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[9].ToString();
                dept3 = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[10].ToString();
                dept = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[11].ToString();
                id_number = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[12].ToString();
                phone_no = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[13].ToString();
                shift = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[14].ToString();
                status = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[15].ToString();
            }
        }
        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql,yyyymm;
            yyyymm = dateTimePicker1.Text.ToString(); 
            if(textEditNo.Text.ToString().Trim() != "" || textEditName.Text.ToString().Trim() != "")
            {
                strsql = "select cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,from_type 输送类型id,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where cno = '" + textEditNo.Text.ToString().Trim() + "' or e.cname = '" + textEditName.Text.ToString().Trim() + "'";
            }
            else if(comboBoxDept.SelectedValue.ToString() != "")
            {
                strsql = "select cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,from_type 输送类型id,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE  e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where register_date like '" + yyyymm + "%' and dept = '" + comboBoxDept.SelectedValue + "' and isnull(status,'在职')= case when '" + comboBoxStatus.Text + "' = '全部' then isnull(status,'在职') else '" + comboBoxStatus.Text + "' end";
            }
            else if(comboBoxDept3.SelectedValue.ToString() != "")
            {
                strsql = "select cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,from_type 输送类型id,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where register_date like '" + yyyymm + "%' and dept3 = '" + comboBoxDept3.SelectedValue + "' and dept2 = '" + comboBoxDept2.SelectedValue + "' and dept1 = '" + comboBoxDept1.SelectedValue  + "' and isnull(status,'在职')= case when '" + comboBoxStatus.Text + "' = '全部' then isnull(status,'在职') else '" + comboBoxStatus.Text + "' end";
            }
            else if (comboBoxDept2.SelectedValue.ToString() != "")
            {
                strsql = "select cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,from_type 输送类型id,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where register_date like '" + yyyymm + "%' and dept2 = '" + comboBoxDept2.SelectedValue + "' and dept1 = '" + comboBoxDept1.SelectedValue + "' and isnull(status,'在职')= case when '" + comboBoxStatus.Text + "' = '全部' then isnull(status,'在职') else '" + comboBoxStatus.Text + "' end";
            }
            else if (comboBoxDept1.SelectedValue.ToString() != "")
            {
                strsql = "select cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,from_type 输送类型id,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where register_date like '" + yyyymm + "%' and dept1 = '" + comboBoxDept1.SelectedValue + "' and isnull(status,'在职')= case when '" + comboBoxStatus.Text + "' = '全部' then isnull(status,'在职') else '" + comboBoxStatus.Text + "' end";
            }
            else 
            {
                strsql = "select cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,from_type 输送类型id,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where register_date like '" + yyyymm + "%' and isnull(status,'在职')= case when '" + comboBoxStatus.Text + "' = '全部' then isnull(status,'在职') else '" + comboBoxStatus.Text + "' end";
            }

            tods = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = tods.Tables[0].DefaultView;
            gridView1.Columns[6].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[4].OptionsColumn.ReadOnly = true;
            gridView1.Columns[5].OptionsColumn.ReadOnly = true;
            gridView1.Columns[6].OptionsColumn.ReadOnly = true;
            gridView1.Columns[7].OptionsColumn.ReadOnly = true;
            gridView1.Columns[8].OptionsColumn.ReadOnly = true;
            gridView1.Columns[9].OptionsColumn.ReadOnly = true;
            gridView1.Columns[10].OptionsColumn.ReadOnly = true;
            gridView1.Columns[11].OptionsColumn.ReadOnly = true;
            gridView1.Columns[12].OptionsColumn.ReadOnly = true;
            gridView1.Columns[13].OptionsColumn.ReadOnly = true;
            gridView1.Columns[14].OptionsColumn.ReadOnly = true;
            gridView1.Columns[15].OptionsColumn.ReadOnly = true;

            //表头设置
            gridView1.ColumnPanelRowHeight = 35;
            gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //表头及行内容居中显示
            //gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            conn.Close();
            isall = false;
        }
        private void showDetailAll()
        {
            ConnDB conn = new ConnDB();
            string strsql, yyyymm;
            yyyymm = dateTimePicker1.Text.ToString();
            if ( comboBoxDept.SelectedValue.ToString() != "")
            {
                strsql = "select cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,from_type 输送类型id,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where dept = '" + comboBoxDept.SelectedValue + "' and isnull(status,'在职')= case when '" + comboBoxStatus.Text + "' = '全部' then isnull(status,'在职') else '" + comboBoxStatus.Text + "' end";
            }
            else if (comboBoxDept3.SelectedValue.ToString() != "")
            {
                strsql = "select cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,from_type 输送类型id,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where dept3 = '" + comboBoxDept3.SelectedValue + "' and dept2 = '" + comboBoxDept2.SelectedValue + "' and dept1 = '" + comboBoxDept1.SelectedValue + "' and isnull(status,'在职')= case when '" + comboBoxStatus.Text + "' = '全部' then isnull(status,'在职') else '" + comboBoxStatus.Text + "' end";
            }
            else if (comboBoxDept2.SelectedValue.ToString() != "")
            {
                strsql = "select cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,from_type 输送类型id,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where dept2 = '" + comboBoxDept2.SelectedValue + "' and dept1 = '" + comboBoxDept1.SelectedValue + "' and isnull(status,'在职')= case when '" + comboBoxStatus.Text + "' = '全部' then isnull(status,'在职') else '" + comboBoxStatus.Text + "' end";
            }
            else if (comboBoxDept1.SelectedValue.ToString() != "")
            {
                strsql = "select cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,from_type 输送类型id,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where dept1 = '" + comboBoxDept1.SelectedValue + "' and isnull(status,'在职')= case when '" + comboBoxStatus.Text + "' = '全部' then isnull(status,'在职') else '" + comboBoxStatus.Text + "' end";
            }
            else
            {
                strsql = "select cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,from_type 输送类型id,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where isnull(status,'在职')= case when '" + comboBoxStatus.Text + "' = '全部' then isnull(status,'在职') else '" + comboBoxStatus.Text + "' end";
            }

            tods = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = tods.Tables[0].DefaultView;
            gridView1.Columns[6].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[4].OptionsColumn.ReadOnly = true;
            gridView1.Columns[5].OptionsColumn.ReadOnly = true;
            gridView1.Columns[6].OptionsColumn.ReadOnly = true;
            gridView1.Columns[7].OptionsColumn.ReadOnly = true;
            gridView1.Columns[8].OptionsColumn.ReadOnly = true;
            gridView1.Columns[9].OptionsColumn.ReadOnly = true;
            gridView1.Columns[10].OptionsColumn.ReadOnly = true;
            gridView1.Columns[11].OptionsColumn.ReadOnly = true;
            gridView1.Columns[12].OptionsColumn.ReadOnly = true;
            gridView1.Columns[13].OptionsColumn.ReadOnly = true;
            gridView1.Columns[14].OptionsColumn.ReadOnly = true;
            gridView1.Columns[15].OptionsColumn.ReadOnly = true;
            //表头设置
            gridView1.ColumnPanelRowHeight = 35;
            gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //表头及行内容居中显示
            //gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            conn.Close();
            isall = true;
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

        private void TempEmpQuery_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            BindDept1();
            BindDept2();
            //绑定人员状态
            Dictionary<string, string> kvDictonary = new Dictionary<string, string>();
            kvDictonary.Add("在职", "在职");
            kvDictonary.Add("离职", "离职");
            kvDictonary.Add("转正", "转正");
            kvDictonary.Add("全部", "全部");

            BindingSource bs = new BindingSource();
            bs.DataSource = kvDictonary;
            comboBoxStatus.DataSource = bs;
            comboBoxStatus.ValueMember = "Key";
            comboBoxStatus.DisplayMember = "Value";
            comboBoxStatus.SelectedIndex = 0;

            //BindDept3();
            //BindDept();
            showDetail();
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePicker1.Value = startMonth;
            dateTimePicker1.Focus();
            SendKeys.Send("{RIGHT} ");


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

        private void simpleButton所有时间段_Click(object sender, EventArgs e)
        {
            showDetailAll();
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

        private void simpleButtonExport_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, yyyymm;
            yyyymm = dateTimePicker1.Text.ToString();
            simpleButtonExport.Enabled = false;
            if(!isall)
            {
                if (textEditNo.Text.ToString().Trim() != "" || textEditName.Text.ToString().Trim() != "")
                {
                    strsql = "select '''' + cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,'''' + id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where cno = '" + textEditNo.Text.ToString().Trim() + "' or e.cname = '" + textEditName.Text.ToString().Trim() + "'";
                }
                else if (comboBoxDept.SelectedValue.ToString() != "0" && comboBoxDept.SelectedValue.ToString() != "")
                {
                    strsql = "select '''' + cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,'''' + id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where register_date like '" + yyyymm + "%' and dept = '" + comboBoxDept.SelectedValue + "' and isnull(status,'在职')= case when '" + comboBoxStatus.Text + "' = '全部' then isnull(status,'在职') else '" + comboBoxStatus.Text + "' end";
                }
                else if (comboBoxDept3.SelectedValue.ToString() != "0" && comboBoxDept3.SelectedValue.ToString() != "")
                {
                    strsql = "select '''' + cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,'''' + id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where register_date like '" + yyyymm + "%' and dept3 = '" + comboBoxDept3.SelectedValue + "' and dept2 = '" + comboBoxDept2.SelectedValue + "' and dept1 = '" + comboBoxDept1.SelectedValue + "' and isnull(status,'在职')= case when '" + comboBoxStatus.Text + "' = '全部' then isnull(status,'在职') else '" + comboBoxStatus.Text + "' end";
                }
                else if (comboBoxDept2.SelectedValue.ToString() != "0" && comboBoxDept2.SelectedValue.ToString() != "")
                {
                    strsql = "select '''' + cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,'''' + id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where register_date like '" + yyyymm + "%' and dept2 = '" + comboBoxDept2.SelectedValue + "' and dept1 = '" + comboBoxDept1.SelectedValue + "' and isnull(status,'在职')= case when '" + comboBoxStatus.Text + "' = '全部' then isnull(status,'在职') else '" + comboBoxStatus.Text + "' end";
                }
                else if (comboBoxDept1.SelectedValue.ToString() != "0" && comboBoxDept1.SelectedValue.ToString() != "")
                {
                    strsql = "select '''' + cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,'''' + id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where register_date like '" + yyyymm + "%' and dept1 = '" + comboBoxDept1.SelectedValue + "' and isnull(status,'在职')= case when '" + comboBoxStatus.Text + "' = '全部' then isnull(status,'在职') else '" + comboBoxStatus.Text + "' end";
                }
                else
                {
                    strsql = "select '''' + cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,'''' + id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where register_date like '" + yyyymm + "%' and isnull(status,'在职')= case when '" + comboBoxStatus.Text + "' = '全部' then isnull(status,'在职') else '" + comboBoxStatus.Text + "' end";
                }
            }
            else
            {
                if (comboBoxDept.SelectedValue.ToString() != "")
                {
                    strsql = "select '''' + cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,'''' + id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where dept = '" + comboBoxDept.SelectedValue + "' and isnull(status,'在职')= case when '" + comboBoxStatus.Text + "' = '全部' then isnull(status,'在职') else '" + comboBoxStatus.Text + "' end";
                }
                else if (comboBoxDept3.SelectedValue.ToString() != "")
                {
                    strsql = "select '''' + cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,'''' + id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where dept3 = '" + comboBoxDept3.SelectedValue + "' and dept2 = '" + comboBoxDept2.SelectedValue + "' and dept1 = '" + comboBoxDept1.SelectedValue + "' and isnull(status,'在职')= case when '" + comboBoxStatus.Text + "' = '全部' then isnull(status,'在职') else '" + comboBoxStatus.Text + "' end";
                }
                else if (comboBoxDept2.SelectedValue.ToString() != "")
                {
                    strsql = "select '''' + cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,'''' + id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where dept2 = '" + comboBoxDept2.SelectedValue + "' and dept1 = '" + comboBoxDept1.SelectedValue + "' and isnull(status,'在职')= case when '" + comboBoxStatus.Text + "' = '全部' then isnull(status,'在职') else '" + comboBoxStatus.Text + "' end";
                }
                else if (comboBoxDept1.SelectedValue.ToString() != "")
                {
                    strsql = "select '''' + cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,'''' + id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where dept1 = '" + comboBoxDept1.SelectedValue + "' and isnull(status,'在职')= case when '" + comboBoxStatus.Text + "' = '全部' then isnull(status,'在职') else '" + comboBoxStatus.Text + "' end";
                }
                else
                {
                    strsql = "select '''' + cno 临时工号,e.cname 姓名,sex 性别,register_date 报道日期,leave_date 离职日期,cfrom 输送渠道,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,dept 部门,'''' + id_number 身份证号,phone_no 手机号码,shift 班次,status 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where isnull(status,'在职')= case when '" + comboBoxStatus.Text + "' = '全部' then isnull(status,'在职') else '" + comboBoxStatus.Text + "' end";
                }
            }
            

            DataSet ds = conn.ReturnDataSet(strsql);
            bool isok = Common.DataSetToExcel(ds, true);
            if (isok)
            {
                simpleButtonExport.Enabled = true;
                MessageBox.Show("导出完成！");
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString().Trim();
            }
        }
    }
}