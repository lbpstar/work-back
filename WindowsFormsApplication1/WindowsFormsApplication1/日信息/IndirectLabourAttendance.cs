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
    public partial class IndirectLabourAttendance : DevExpress.XtraEditors.XtraForm
    {
        public IndirectLabourAttendance()
        {
            InitializeComponent();
        }
        private static IndirectLabourAttendance ilaform = null;
        private bool ischange = false;

        public static IndirectLabourAttendance GetInstance()
        {
            if (ilaform == null || ilaform.IsDisposed)
            {
                ilaform = new IndirectLabourAttendance();
            }
            return ilaform;
        }
        private void simpleButtonQuery_Click(object sender, EventArgs e)
        {
            showDetail();
        }
        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            int deptid;
            deptid = (int)comboBoxDept.SelectedValue;
            if (deptid > 0)
            {
                strsql = "select isnull(i.cid,0) id,'" + dateEditDate.Text + "' 日期,d.cid 部门id,d.cname 部门,j.cid 直接人工id,j.cno 工号,j.cname 姓名,i.work_type_id 上班类型id,w.cname 上班类型,i.hours 小时数 from (select * from COST_DIRECT_LABOUR_ATTENDANCE where cdate = '" + dateEditDate.Text + "') i right join COST_DIRECT_LABOUR j on i.DIRECT_LABOUR_ID = j.CID left join COST_DEPT D on j.DEPT_ID = D.CID left join cost_work_type w on i.work_type_id = w.cid where j.person_type_id = 4 and D.cid = " + Common.IsZero(deptid.ToString()) + " order by d.cname";
            }
            else
            {
                strsql = "select isnull(i.cid,0) id,'" + dateEditDate.Text + "' 日期,d.cid 部门id,d.cname 部门,j.cid 直接人工id,j.cno 工号,j.cname 姓名,i.work_type_id 上班类型id,w.cname 上班类型,i.hours 小时数 from(select * from COST_DIRECT_LABOUR_ATTENDANCE where cdate = '" + dateEditDate.Text + "') i right join COST_DIRECT_LABOUR j on i.DIRECT_LABOUR_ID = j.CID left join COST_DEPT D on j.DEPT_ID = D.CID left join cost_work_type w on i.work_type_id = w.cid where j.person_type_id = 4 order by d.cname";
            }

            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[2].Visible = false;
            gridView1.Columns[4].Visible = false;
            gridView1.Columns[7].Visible = false;        

            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[4].OptionsColumn.ReadOnly = true;
            gridView1.Columns[5].OptionsColumn.ReadOnly = true;
            gridView1.Columns[6].OptionsColumn.ReadOnly = true;
            gridView1.Columns[7].OptionsColumn.ReadOnly = true;
            gridView1.Columns[8].OptionsColumn.ReadOnly = true;


            if (gridView1.GetDataRow(0) != null && gridView1.GetDataRow(0).ItemArray[7].ToString() != "0" && gridView1.GetDataRow(0).ItemArray[7].ToString() != "")
            {
                comboBoxWorkType.SelectedIndex = -1;
                comboBoxWorkType.SelectedValue = Convert.ToInt32(gridView1.GetDataRow(0).ItemArray[7].ToString());
            }
            conn.Close();
        }
        private void showDetail2()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            int deptid;
            deptid = (int)comboBoxDept.SelectedValue;
            if (deptid > 0)
            {
                strsql = "select isnull(i.cid,0) id,'" + dateEditDate.Text + "' 日期,d.cid 部门id,d.cname 部门,j.cid 直接人工id,j.cno 工号,j.cname 姓名,i.work_type_id 上班类型id,w.cname 上班类型,'" + Common.IsNull(textEditHours.Text.ToString()) + "' 小时数 from (select * from COST_DIRECT_LABOUR_ATTENDANCE where cdate = '" + dateEditDate.Text + "') i right join COST_DIRECT_LABOUR j on i.DIRECT_LABOUR_ID = j.CID left join COST_DEPT D on j.DEPT_ID = D.CID left join cost_work_type w on i.work_type_id = w.cid where j.person_type_id = 4 and D.cid = " + Common.IsZero(deptid.ToString()) + " order by d.cname";
            }
            else
            {
                strsql = "select isnull(i.cid,0) id,'" + dateEditDate.Text + "' 日期,d.cid 部门id,d.cname 部门,j.cid 直接人工id,j.cno 工号,j.cname 姓名,i.work_type_id 上班类型id,w.cname 上班类型,'" + Common.IsNull(textEditHours.Text.ToString()) + "' 小时数 from(select * from COST_DIRECT_LABOUR_ATTENDANCE where cdate = '" + dateEditDate.Text + "') i right join COST_DIRECT_LABOUR j on i.DIRECT_LABOUR_ID = j.CID left join COST_DEPT D on j.DEPT_ID = D.CID left join cost_work_type w on i.work_type_id = w.cid where j.person_type_id = 4 order by d.cname";
            }

            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[2].Visible = false;
            gridView1.Columns[4].Visible = false;
            gridView1.Columns[7].Visible = false;

            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[4].OptionsColumn.ReadOnly = true;
            gridView1.Columns[5].OptionsColumn.ReadOnly = true;
            gridView1.Columns[6].OptionsColumn.ReadOnly = true;
            gridView1.Columns[7].OptionsColumn.ReadOnly = true;
            gridView1.Columns[8].OptionsColumn.ReadOnly = true;
            conn.Close();
        }

        private void DirectLabourAttendance_Load(object sender, EventArgs e)
        {
            dateEditDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Common.BasicDataBind("cost_dept", comboBoxDept);
            Common.BasicDataBind("cost_work_type", comboBoxWorkType);
            showDetail();
        }

        private void simpleButtonSubmit_Click(object sender, EventArgs e)
        {
            if(comboBoxWorkType.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("上班类型没有设置！");
            }
            else if(ischange == true)
            {
                ConnDB conn = new ConnDB();
                string strsql, strsql2;
                int deptid, worktypeid;

                bool isok = false;
                deptid = (int)comboBoxDept.SelectedValue;
                worktypeid = (int)comboBoxWorkType.SelectedValue;
                if (deptid > 0)
                {
                    strsql = "select i.* from COST_DIRECT_LABOUR_ATTENDANCE i left join COST_DIRECT_LABOUR j on i.DIRECT_LABOUR_ID = j.CID left join COST_DEPT D on j.DEPT_ID = D.CID where j.person_type_id = 4 and j.DEPT_ID = " + Common.IsZero(deptid.ToString()) + " and cdate = '" + dateEditDate.Text + "'";
                }
                else
                {
                    strsql = "select i.* from COST_DIRECT_LABOUR_ATTENDANCE i left join COST_DIRECT_LABOUR j on i.DIRECT_LABOUR_ID = j.CID left join COST_DEPT D on j.DEPT_ID = D.CID where j.person_type_id = 4 and cdate = '" + dateEditDate.Text + "'";
                }
                DataSet ds = conn.ReturnDataSet(strsql);

                gridView1.FocusInvalidRow();
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    bool exist = false;
                    decimal h;
                    if(gridView1.GetDataRow(i).ItemArray[9].ToString()=="")
                    {
                        h = 0;
                    }
                    else
                    {
                        h = Convert.ToDecimal(gridView1.GetDataRow(i).ItemArray[9]);
                    }
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        if (gridView1.GetDataRow(i).ItemArray[0].ToString() == ds.Tables[0].Rows[j][0].ToString())
                        {
                            strsql2 = "update i set i.work_type_id = " + worktypeid + ",i.hours = " + h + " from COST_DIRECT_LABOUR_ATTENDANCE i where i.cid = " + gridView1.GetDataRow(i).ItemArray[0].ToString();
                            isok = conn.EditDatabase(strsql2);
                            exist = true;
                            break;
                        }
                    }
                    if (!exist)
                    {
                        strsql2 = "insert into COST_DIRECT_LABOUR_ATTENDANCE(cdate,direct_labour_id,work_type_id,hours) values('" + gridView1.GetDataRow(i).ItemArray[1].ToString() + "'," + gridView1.GetDataRow(i).ItemArray[4].ToString() + "," + worktypeid + "," + h + ")";
                        isok = conn.EditDatabase(strsql2);
                    }
                }

                if (isok)
                {
                    MessageBox.Show("提交成功！");
                    comboBoxWorkType.SelectedIndex = -1;
                    comboBoxWorkType.SelectedValue = 0;
                    textEditHours.Text = "0";
                    showDetail();
                }
                else
                {
                    MessageBox.Show("失败！");
                }
                //dlpuform.ischange = false;
                conn.Close();
                ischange = false;
            }
            else
            {
                MessageBox.Show("没有可更新的数据！");
            }      

        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            ischange = true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (ischange)
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("有修改的数据没有提交，确认退出吗?", "退出", messButton);
                if (dr == DialogResult.OK)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void comboBoxLineType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            showDetail();
            //comboBoxWorkType.SelectedIndex = -1;
            //comboBoxWorkType.SelectedValue = 0;
            //textEditHours.Text = "0";
        }

        private void dateEditDate_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxWorkType.SelectedValue != null && comboBoxWorkType.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                showDetail();
            }
        }

        private void comboBoxWorkType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ischange = true;
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (gridView1.GetDataRow(e.RowHandle) == null) return;
            if (gridView1.GetDataRow(e.RowHandle)[9] == null || gridView1.GetDataRow(e.RowHandle)[9].ToString() == "") return;
            if (Convert.ToDecimal(gridView1.GetDataRow(e.RowHandle)[9]) > 8)
            {
                e.Appearance.BackColor = Color.Pink;
            }
            else
            {
                e.Appearance.BackColor = Color.White;
            }
        }

        private void textEditHours_EditValueChanged(object sender, EventArgs e)
        {
            showDetail2();
            ischange = true;
        }
    }
}