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
    public partial class SteelNetRateQuery : DevExpress.XtraEditors.XtraForm
    {
        public SteelNetRateQuery()
        {
            InitializeComponent();
        }
        private static SteelNetRateQuery weqform = null;

        public static SteelNetRateQuery GetInstance()
        {
            if (weqform == null || weqform.IsDisposed)
            {
                weqform = new SteelNetRateQuery();
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
                        sql = "delete from cost_steel_net_rate where cid = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[0].ToString() + "'";
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
        public static void GetInfo(ref int id, ref string yyyymm,ref decimal steelnetrate)
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
                id =Convert.ToInt32(Common.IsNull(weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[0].ToString()));
                yyyymm = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString();
                steelnetrate = Convert.ToDecimal(Common.IsNull(weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[2].ToString()));
            }
        }
        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql,yyyy;
            yyyy = dateTimePicker1.Text.ToString(); 
            strsql = "select i.cid,i.yyyymm 年月,i.steel_net_rate 钢网占比 from cost_steel_net_rate i where i.yyyymm like '" + yyyy + "%'";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
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

        private void WaterElectricityQuery_Load(object sender, EventArgs e)
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