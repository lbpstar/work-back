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
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;

namespace SMTCost
{
    public partial class IndirectLabourPriceUpdate : DevExpress.XtraEditors.XtraForm
    {
        public IndirectLabourPriceUpdate()
        {
            InitializeComponent();
        }
        private static IndirectLabourPriceUpdate ilpuform = null;
        private bool ischange = false;

        public static IndirectLabourPriceUpdate GetInstance()
        {
            if (ilpuform == null || ilpuform.IsDisposed)
            {
                ilpuform = new IndirectLabourPriceUpdate();
            }
            ilpuform.ShowDetail();
            return ilpuform;
        }
        public static void Save()
        {
            ConnDB conn = new ConnDB();
            string sql;
            bool isok = false;
            if (ilpuform.gridView1.RowCount > 0)
            {
                ilpuform.gridView1.FocusInvalidRow();
                for (int i = 0; i < ilpuform.gridView1.RowCount; i++)
                {
                    sql = "update cost_indirect_labour_price set price = " + ilpuform.gridView1.GetDataRow(i).ItemArray[7].ToString() + " where yyyymm = '" + ilpuform.gridView1.GetDataRow(i).ItemArray[1].ToString() + "' and work_type_id = " + ilpuform.gridView1.GetDataRow(i).ItemArray[5].ToString() + " and indirect_labour_id = " + ilpuform.gridView1.GetDataRow(i).ItemArray[0].ToString();
                    isok = conn.EditDatabase(sql);
                }
            }
            if (isok)
            {
                MessageBox.Show("修改成功！");
                IndirectLabourPriceQuery.RefreshEX();
                ilpuform.Close();
            }
            else
            {
                MessageBox.Show("失败！");
            }
            ilpuform.ischange = false;
            conn.Close();
            ilpuform.Close();
            IndirectLabourPrice.savetype = "insert";
        }

        private void DirectLabourPriceUpdate_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            ShowDetail();
            IndirectLabourPrice.savetype = "update";
        }

        private void ShowDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            string yyyymm = "", deptid = "", indirectlabourid = "";
            IndirectLabourPriceQuery.GetInfo(ref yyyymm, ref deptid, ref indirectlabourid);
            if (deptid == "0")
            {
                strsql = "select i.indirect_labour_id 员工id,i.yyyymm 年月,i.deptname 部门,i.cno 工号,i.cname 姓名,j.CID,j.CNAME 上班类型,i.price '费率(元/小时)' from (select i.*,j.cno,j.cname,d.cid as deptid,d.cname as deptname from cost_indirect_labour_price i left join cost_direct_labour j on i.indirect_labour_id = j.cid left join cost_dept d on j.dept_id = d.cid where isnull(YYYYMM,'') ='" + yyyymm + "') i left join (select * from COST_WORK_TYPE where forbidden = 'false') j on i.WORK_TYPE_id = j.cid  ";

            }
            else if (indirectlabourid == "0")
            {
                strsql = "select i.indirect_labour_id 员工id,i.yyyymm 年月,i.deptname 部门,i.cno 工号,i.cname 姓名,j.CID,j.CNAME 上班类型,i.price '费率(元/小时)' from (select i.*,j.cno,j.cname,d.cid as deptid,d.cname as deptname from cost_indirect_labour_price i left join cost_direct_labour j on i.indirect_labour_id = j.cid left join cost_dept d on j.dept_id = d.cid where isnull(YYYYMM,'') ='" + yyyymm + "' and j.dept_id = " + deptid + ") i left join (select * from COST_WORK_TYPE where forbidden = 'false') j on i.WORK_TYPE_id = j.cid  ";

            }
            else
            {
                strsql = "select i.indirect_labour_id 员工id,i.yyyymm 年月,i.deptname 部门,i.cno 工号,i.cname 姓名,j.CID,j.CNAME 上班类型,i.price '费率(元/小时)' from (select i.*,j.cno,j.cname,d.cid as deptid,d.cname as deptname from cost_indirect_labour_price i left join cost_direct_labour j on i.indirect_labour_id = j.cid left join cost_dept d on j.dept_id = d.cid where isnull(YYYYMM,'') ='" + yyyymm + "' and j.dept_id = " + deptid + " and indirect_labour_id = " + indirectlabourid + ") i left join (select * from COST_WORK_TYPE where forbidden = 'false') j on i.WORK_TYPE_id = j.cid  ";

            }
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;

            gridView1.Columns[0].Visible = false;
            gridView1.Columns[5].Visible = false;

            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[4].OptionsColumn.ReadOnly = true;
            gridView1.Columns[5].OptionsColumn.ReadOnly = true;
            gridView1.Columns[6].OptionsColumn.ReadOnly = true;
            conn.Close();
        }

        private void simpleButtonExit_Click(object sender, EventArgs e)
        {
            if(ischange)
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("没有保存，确认退出吗?", "退出", messButton);
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

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            ischange = true;
        }
        public static void MyClose()
        {
            ilpuform.Close();
        }
    }
}