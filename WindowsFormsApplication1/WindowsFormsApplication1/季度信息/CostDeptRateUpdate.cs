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
    public partial class CostDeptRateUpdate : DevExpress.XtraEditors.XtraForm
    {
        public CostDeptRateUpdate()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql = "update cost_rate set cost_rate = ltrim(rtrim('" + Common.IsNull(textEditCostRate.Text.ToString()) + "'))";
            sql = sql + " where cid = " + textEditID.Text.ToString();
            bool isok = conn.EditDatabase(sql);
            if (isok)
            {
                MessageBox.Show("修改成功！");
                CostDeptRateQuery.RefreshEX();
                this.Close();
            }
            else
            {
                MessageBox.Show("失败！");
            }
            conn.Close();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CostRateUpdate_Load(object sender, EventArgs e)
        {
            string yyyy = "";
            int id = 0, saletypeid = 0,quarterid = 0,deptid = 0;
            decimal costrate = 0;
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePicker1.Value = startMonth;
            CostDeptRateQuery.GetInfo(ref id, ref yyyy,ref quarterid,ref saletypeid,ref deptid, ref costrate);
            Common.BasicDataBind("cost_saletype", comboBoxSaleType);
            Common.BasicDataBind("cost_quarter", comboBoxQuarter);
            BindDept();
            if (id != 0)
            {
                comboBoxSaleType.SelectedIndex = -1;
                comboBoxSaleType.SelectedValue = saletypeid;
                comboBoxQuarter.SelectedIndex = -1;
                comboBoxQuarter.SelectedValue = quarterid;
                comboBoxDept.SelectedIndex = -1;
                comboBoxDept.SelectedValue = deptid;
                dateTimePicker1.Value = Convert.ToDateTime(yyyy + "-01");
                textEditCostRate.Text = costrate.ToString();
                textEditID.Text = id.ToString();

            }
        }
        public void BindDept()
        {
            ConnDB conn = new ConnDB();
            string sql = "select * from  cost_dept where isnull(forbidden,'false') != 'true' and saletype_id = " + Common.IsZero(comboBoxSaleType.SelectedValue.ToString());
            DataSet ds = conn.ReturnDataSet(sql);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = "0";
            dr[1] = "请选择";
            //插在第一位
            ds.Tables[0].Rows.InsertAt(dr, 0);
            comboBoxDept.DataSource = ds.Tables[0];
            comboBoxDept.DisplayMember = "CNAME";
            comboBoxDept.ValueMember = "CID";
            conn.Close();

        }

        private void comboBoxSaleType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BindDept();
        }
    }
}