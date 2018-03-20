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
    public partial class InDirectLabourLevelPriceUpdate : DevExpress.XtraEditors.XtraForm
    {
        public InDirectLabourLevelPriceUpdate()
        {
            InitializeComponent();
        }
        private static InDirectLabourLevelPriceUpdate dlpuform = null;
        private bool ischange = false;

        public static InDirectLabourLevelPriceUpdate GetInstance()
        {
            if (dlpuform == null || dlpuform.IsDisposed)
            {
                dlpuform = new InDirectLabourLevelPriceUpdate();
            }
            dlpuform.ShowDetail();
            return dlpuform;
        }
        public static void Save()
        {
            ConnDB conn = new ConnDB();
            string sql;
            bool isok = false;
            string yyyymm = "";
            int id = 0,level_begin = 0, level_end = 0;
            InDirectLabourLevelPriceQuery.GetInfo(ref id,ref yyyymm, ref level_begin, ref level_end);
            if (yyyymm != "")
            {
                dlpuform.gridView1.FocusInvalidRow();
                for (int i = 0; i < dlpuform.gridView1.RowCount; i++)
                {
                    sql = "update cost_indirect_labour_level_price set price = " + dlpuform.gridView1.GetDataRow(i).ItemArray[6].ToString() + " where cid = " + dlpuform.gridView1.GetDataRow(i).ItemArray[0].ToString();
                    isok = conn.EditDatabase(sql);
                }
            }
            if (isok)
            {
                MessageBox.Show("修改成功！");
                InDirectLabourLevelPriceQuery.RefreshEX();
                dlpuform.Close();
            }
            else
            {
                MessageBox.Show("失败！");
            }
            dlpuform.ischange = false;
            conn.Close();
            dlpuform.Close();
            InDirectLabourLevelPrice.savetype = "insert";
        }
        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            
        }

        private void DirectLabourPriceUpdate_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            ShowDetail();
            InDirectLabourLevelPrice.savetype = "update";
        }
        private void ShowDetail()
        {
            string yyyymm = "";
            int id = 0,level_begin = 0, level_end = 0;
            InDirectLabourLevelPriceQuery.GetInfo(ref id,ref yyyymm, ref level_begin, ref level_end);
            string strsql;
            if (yyyymm != "")
            {
                ConnDB conn = new ConnDB();
                strsql = "select i.cid id,i.yyyymm 年月,i.level_begin '员工等级（起）',i.level_end '员工等级（止）',j.CID,j.CNAME 上班类型,i.price '费率(元/小时)' from (select * from cost_indirect_labour_level_price where isnull(YYYYMM,'') ='" + yyyymm + "' and level_begin = " + level_begin + " and level_end = " + level_end + ") i left join (select * from COST_WORK_TYPE where forbidden = 'false') j on i.WORK_TYPE = j.cid  ";
                DataSet ds = conn.ReturnDataSet(strsql);
                gridControl1.DataSource = ds.Tables[0].DefaultView;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[4].Visible = false;

                gridView1.Columns[0].OptionsColumn.ReadOnly = true;
                gridView1.Columns[1].OptionsColumn.ReadOnly = true;
                gridView1.Columns[2].OptionsColumn.ReadOnly = true;
                gridView1.Columns[3].OptionsColumn.ReadOnly = true;
                gridView1.Columns[4].OptionsColumn.ReadOnly = true;
                gridView1.Columns[5].OptionsColumn.ReadOnly = true;
                conn.Close();
            }
            
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
            dlpuform.Close();
        }
    }
}