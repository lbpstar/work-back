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
    public partial class DirectLabourPriceQuery : DevExpress.XtraEditors.XtraForm
    {
        public DirectLabourPriceQuery()
        {
            InitializeComponent();
        }
        private static DirectLabourPriceQuery dlpqform = null;

        public static DirectLabourPriceQuery GetInstance()
        {
            if (dlpqform == null || dlpqform.IsDisposed)
            {
                dlpqform = new DirectLabourPriceQuery();
            }
            return dlpqform;
        }
        public static void RefreshEX()
        {
            if (dlpqform == null || dlpqform.IsDisposed)
            {

            }
            else
            {
                dlpqform.ShowDetail();
            }
        }
        public static void Delete()
        {
            string sql;
            bool isdone= true;
            ConnDB conn = new ConnDB();
            if (dlpqform == null || dlpqform.IsDisposed)
            {
                MessageBox.Show("没有要删除的数据！");
            }
            else if (dlpqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有要删除的数据！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                string ms = "确定要删除月份" + dlpqform.dateTimePicker1.Text.ToString() + "数据吗？";
                DialogResult dr = MessageBox.Show(ms, "直接人工费率删除", messButton);
                if (dr == DialogResult.OK)
                {
                    sql = "delete from cost_direct_labour_price where YYYYMM = '" + dlpqform.dateTimePicker1.Text.ToString() + "'";
                    isdone = conn.DeleteDatabase(sql);
                    if (isdone)
                    {
                        MessageBox.Show("删除成功！");
                    }
                }
            }
            conn.Close();
        }
        
        public static string GetMonth()
        {
            if (dlpqform == null || dlpqform.IsDisposed)
            {
                MessageBox.Show("没有选中要修改的月份！");
                return "";
            }
            else if (dlpqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要修改的月份！");
                return "";
            }
            else
            {
                //return dlpqform.dateTimePicker1.Text.ToString();
                return dlpqform.gridView1.GetDataRow(dlpqform.gridView1.GetSelectedRows()[0]).ItemArray[0].ToString();

            }
        }

        private void ShowDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql, yyyymm;
            yyyymm = dateTimePicker1.Text.ToString();
            strsql = "select i.yyyymm 年月,j.CID,j.CNAME 上班类型,i.price '费率(元/小时)' from (select * from cost_direct_labour_price where isnull(YYYYMM,'') ='" + yyyymm + "') i left join (select * from COST_WORK_TYPE where forbidden = 'false') j on i.WORK_TYPE = j.cid  ";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[1].Visible = false;

            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            conn.Close();
        }
        private void ShowAll()
        {
            ConnDB conn = new ConnDB();
            string strsql, yyyymm;
            yyyymm = dateTimePicker1.Text.ToString();
            strsql = "select i.yyyymm 年月,j.CID,j.CNAME 上班类型,i.price '费率(元/小时)' from cost_direct_labour_price i left join (select * from COST_WORK_TYPE where forbidden = 'false') j on i.WORK_TYPE = j.cid  ";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[1].Visible = false;

            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            conn.Close();
        }

        private void simpleButton查询_Click(object sender, EventArgs e)
        {
            ShowDetail();
        }


        private void simpleButton全部_Click(object sender, EventArgs e)
        {
            ShowAll();
        }

        private void DirectLabourPriceQuery_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            ShowDetail();
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePicker1.Value = startMonth;

            dateTimePicker1.Focus();
            SendKeys.Send("{RIGHT} ");
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ShowDetail();
        }
    }
}