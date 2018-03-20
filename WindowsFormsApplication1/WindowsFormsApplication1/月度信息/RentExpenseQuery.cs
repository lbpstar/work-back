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
    public partial class RentExpenseQuery : DevExpress.XtraEditors.XtraForm
    {
        public RentExpenseQuery()
        {
            InitializeComponent();
        }
        private static RentExpenseQuery reqform = null;

        public static RentExpenseQuery GetInstance()
        {
            if (reqform == null || reqform.IsDisposed)
            {
                reqform = new RentExpenseQuery();
            }
            return reqform;
        }
        public static void RefreshEX()
        {
            if (reqform == null || reqform.IsDisposed)
            {

            }
            else
            {
                reqform.showDetail();
            }
        }
        public static void Delete()
        {
            string sql;
            bool isdone = true;
            ConnDB conn = new ConnDB();
            if (reqform == null || reqform.IsDisposed)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }
            else if (reqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除吗?", "删除", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < reqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "delete from cost_rent_expense where cid = '" + reqform.gridView1.GetDataRow(reqform.gridView1.GetSelectedRows()[i]).ItemArray[0].ToString() + "'";
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
        public static void GetInfo(ref int id, ref string yyyymm,ref int saletypeid,ref decimal rentexpense)
        {
            if (reqform == null || reqform.IsDisposed)
            {
                MessageBox.Show("没有选中要修改的记录！");
            }
            else if (reqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要修改的记录！");
            }
            else
            {
                id =Convert.ToInt32(Common.IsNull(reqform.gridView1.GetDataRow(reqform.gridView1.GetSelectedRows()[0]).ItemArray[0].ToString()));
                yyyymm = reqform.gridView1.GetDataRow(reqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString();
                saletypeid = Convert.ToInt32(Common.IsNull(reqform.gridView1.GetDataRow(reqform.gridView1.GetSelectedRows()[0]).ItemArray[2].ToString()));
                rentexpense = Convert.ToDecimal(Common.IsNull(reqform.gridView1.GetDataRow(reqform.gridView1.GetSelectedRows()[0]).ItemArray[4].ToString()));
            }
        }
        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql,yyyymm;
            yyyymm = dateTimePicker1.Text.ToString(); 
            strsql = "select i.cid,i.yyyymm 年月,i.sale_type_id 营业类型id,j.cname 营业类型,i.rent_expense '租赁费(元)' from cost_rent_expense i left join cost_saletype j on i.sale_type_id = j.cid where i.yyyymm = '" + yyyymm + "'";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[2].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[4].OptionsColumn.ReadOnly = true;
            conn.Close();
        }
      
        
        private void simpleButton查询_Click(object sender, EventArgs e)
        {
            showDetail();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            showDetail();
        }

        private void RentExpenseQuery_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
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
    }
}