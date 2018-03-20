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
    public partial class FestivalQuery : DevExpress.XtraEditors.XtraForm
    {
        public FestivalQuery()
        {
            InitializeComponent();
        }
        private static FestivalQuery wtqform = null;

        public static FestivalQuery GetInstance()
        {
            if (wtqform == null || wtqform.IsDisposed)
            {
                wtqform = new FestivalQuery();
            }
            return wtqform;
        }
        public static void RefreshEX()
        {
            if (wtqform == null || wtqform.IsDisposed)
            {

            }
            else
            {
                wtqform.showDetail();
            }
        }
        public static void Delete()
        {
            string sql;
            bool isdone= true;
            ConnDB conn = new ConnDB();
            if (wtqform == null || wtqform.IsDisposed)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }
            else if (wtqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除吗?", "考勤班次删除", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < wtqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "delete from cost_festival where festival_date   = '" + wtqform.gridView1.GetDataRow(wtqform.gridView1.GetSelectedRows()[i]).ItemArray[0].ToString() + "'";
                        isdone = conn.DeleteDatabase(sql);
                        if (!isdone)
                            break;
                    }
               
                    if(isdone)
                    {
                        MessageBox.Show("删除成功！");
                    }
                }
            }
            conn.Close();
        }
        public static void GetInfo(ref string cdate,ref int ctype,ref string cnote)
        {
            if (wtqform == null || wtqform.IsDisposed)
            {
                MessageBox.Show("没有选中要修改的记录！");
                cdate =  "";
            }
            else if (wtqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要修改的记录！");
                cdate = "";
            }
            else
            {
                cdate = wtqform.gridView1.GetDataRow(wtqform.gridView1.GetSelectedRows()[0]).ItemArray[0].ToString();
                ctype = Convert.ToInt32(wtqform.gridView1.GetDataRow(wtqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString());
                cnote = wtqform.gridView1.GetDataRow(wtqform.gridView1.GetSelectedRows()[0]).ItemArray[3].ToString();
            }
            //return "";
        }

        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            strsql = "select CONVERT(varchar(100), festival_date, 23) 日期,festival_type,类型 = case when festival_type = 0 then '日常班' when festival_type = 1 then '周末' else '节假日' end,festival_note 备注 from cost_festival where festival_date like '" + dateTimePickerDate.Text + "%'";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[1].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            conn.Close();
        }

        private void SaleTypeQuery_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            showDetail();
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePickerDate.Value = startMonth;
        }

        private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
        {
            showDetail();
        }
    }
}