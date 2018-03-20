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
    public partial class InDirectLabourLevelPriceQuery : DevExpress.XtraEditors.XtraForm
    {
        public InDirectLabourLevelPriceQuery()
        {
            InitializeComponent();
        }
        private static InDirectLabourLevelPriceQuery dlpqform = null;

        public static InDirectLabourLevelPriceQuery GetInstance()
        {
            if (dlpqform == null || dlpqform.IsDisposed)
            {
                dlpqform = new InDirectLabourLevelPriceQuery();
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
                string ms = "确定要删除对应月份和员工等级的数据吗？";
                DialogResult dr = MessageBox.Show(ms, "间接人工费率删除", messButton);
                if (dr == DialogResult.OK)
                {
                    sql = "delete from cost_indirect_labour_level_price where YYYYMM = '" + dlpqform.dateTimePicker1.Text.ToString() + "' and level_begin = " + dlpqform.gridView1.GetDataRow(dlpqform.gridView1.GetSelectedRows()[0]).ItemArray[2].ToString() + " and level_end = " + dlpqform.gridView1.GetDataRow(dlpqform.gridView1.GetSelectedRows()[0]).ItemArray[3].ToString();
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
                return dlpqform.gridView1.GetDataRow(dlpqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString();

            }
        }
        public static void GetInfo(ref int id,ref string month, ref int level_begin, ref int level_end)
        {
            if (dlpqform == null || dlpqform.IsDisposed)
            {
                MessageBox.Show("没有选中要修改的记录！");
                month = "";
            }
            else if (dlpqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要修改的记录！");
                month = "";
            }
            else
            {
                id = int.Parse(Common.IsNull(dlpqform.gridView1.GetDataRow(dlpqform.gridView1.GetSelectedRows()[0]).ItemArray[0].ToString()));
                month = dlpqform.gridView1.GetDataRow(dlpqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString();
                level_begin = int.Parse(Common.IsNull(dlpqform.gridView1.GetDataRow(dlpqform.gridView1.GetSelectedRows()[0]).ItemArray[2].ToString()));
                level_end = int.Parse(Common.IsNull(dlpqform.gridView1.GetDataRow(dlpqform.gridView1.GetSelectedRows()[0]).ItemArray[3].ToString()));

            }
        }

        private void ShowDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql, yyyymm;
            yyyymm = dateTimePicker1.Text.ToString();
            strsql = "select i.cid,i.yyyymm 年月,i.level_begin '员工等级（起）',i.level_end '员工等级（止）',j.CID,j.CNAME 上班类型,i.price '费率(元/小时)' from (select * from cost_indirect_labour_level_price where isnull(YYYYMM,'') ='" + yyyymm + "') i left join (select * from COST_WORK_TYPE where forbidden = 'false') j on i.WORK_TYPE = j.cid  ";
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
            gridView1.Columns[6].OptionsColumn.ReadOnly = true;
            conn.Close();
        }

        private void simpleButton查询_Click(object sender, EventArgs e)
        {
            ShowDetail();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ShowDetail();
        }

        private void InDirectLabourLevelPriceQuery_Load(object sender, EventArgs e)
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
    }
}