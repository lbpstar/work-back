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
    public partial class DirectLabourPriceUpdate : DevExpress.XtraEditors.XtraForm
    {
        public DirectLabourPriceUpdate()
        {
            InitializeComponent();
        }
        private static DirectLabourPriceUpdate dlpuform = null;
        private bool ischange = false;

        public static DirectLabourPriceUpdate GetInstance()
        {
            if (dlpuform == null || dlpuform.IsDisposed)
            {
                dlpuform = new DirectLabourPriceUpdate();
            }
            dlpuform.ShowDetail();
            return dlpuform;
        }
        public static void Save()
        {
            ConnDB conn = new ConnDB();
            string sql;
            bool isok = false;
            string yyyymm = DirectLabourPriceQuery.GetMonth();
            if (yyyymm != "")
            {
                dlpuform.gridView1.FocusInvalidRow();
                for (int i = 0; i < dlpuform.gridView1.RowCount; i++)
                {
                    sql = "update cost_direct_labour_price set price = " + dlpuform.gridView1.GetDataRow(i).ItemArray[3].ToString() + " where yyyymm = '" + yyyymm + "' and work_type = " + dlpuform.gridView1.GetDataRow(i).ItemArray[1].ToString();
                    isok = conn.EditDatabase(sql);
                }
            }
            if (isok)
            {
                MessageBox.Show("修改成功！");
                DirectLabourPriceQuery.RefreshEX();
                dlpuform.Close();
            }
            else
            {
                MessageBox.Show("失败！");
            }
            dlpuform.ischange = false;
            conn.Close();
            dlpuform.Close();
            DirectLabourPrice.savetype = "insert";
        }
        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            
        }

        private void DirectLabourPriceUpdate_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            ShowDetail();
            DirectLabourPrice.savetype = "update";
        }
        private void ShowDetail()
        {
            string yyyymm = DirectLabourPriceQuery.GetMonth();
            string strsql;
            if (yyyymm != "")
            {
                ConnDB conn = new ConnDB();
                strsql = "select i.yyyymm 年月,j.CID,j.CNAME 上班类型,i.price '费率(元/小时)' from (select * from cost_direct_labour_price where isnull(YYYYMM,'') ='" + yyyymm + "') i left join (select * from COST_WORK_TYPE where forbidden = 'false') j on i.WORK_TYPE = j.cid  ";
                DataSet ds = conn.ReturnDataSet(strsql);
                gridControl1.DataSource = ds.Tables[0].DefaultView;
                gridView1.Columns[1].Visible = false;

                gridView1.Columns[0].OptionsColumn.ReadOnly = true;
                gridView1.Columns[1].OptionsColumn.ReadOnly = true;
                gridView1.Columns[2].OptionsColumn.ReadOnly = true;
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