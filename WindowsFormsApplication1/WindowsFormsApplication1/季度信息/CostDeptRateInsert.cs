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
    public partial class CostDeptRateInsert : DevExpress.XtraEditors.XtraForm
    {
        public CostDeptRateInsert()
        {
            InitializeComponent();
        }
        private static CostDeptRateInsert criform = null;

        public static CostDeptRateInsert GetInstance()
        {
            if (criform == null || criform.IsDisposed)
            {
                criform = new CostDeptRateInsert();
            }
            return criform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            strsql = "insert into cost_rate(yyyy,quarter_id,sale_type_id,dept_id,cost_rate) values('" + dateTimePicker1.Text.ToString() + "'," + comboBoxQuarter.SelectedValue.ToString() + "," + comboBoxSaleType.SelectedValue.ToString() + "," + comboBoxDept.SelectedValue.ToString()  + ",ltrim(rtrim(" + Common.IsNull(textEditCostRate.Text.ToString()) + ")))";
            strsql2 = "select * from cost_rate where yyyy ='" + dateTimePicker1.Text.ToString() + "' and sale_type_id = " + Common.IsZero(comboBoxSaleType.SelectedValue.ToString()) + " and quarter_id = " + Common.IsZero(comboBoxQuarter.SelectedValue.ToString()) + " and dept_id = " + comboBoxDept.SelectedValue;
            if(comboBoxQuarter.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("请选择季度");
            }
            else if (comboBoxSaleType.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("请选择营业类型");
            }
            else if (comboBoxDept.SelectedValue.ToString() == "0")
            {

                MessageBox.Show("部门不能为空！");
            }
            else if (textEditCostRate.Text.ToString() == "")
            {

                MessageBox.Show("成本比率不能为空！");
            }
            else
            {
                
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("该部门成本比率已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
                        CostDeptRateQuery.RefreshEX();
                        this.Close();
                    }
                }
            }
            conn.Close();
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

        private void simpleButton复制_Click(object sender, EventArgs e)
        {
            if (comboBoxQuarter.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("请选择季度");
            }
            else
            { 
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("将按最近的季度数据自动新增当前选择季度的数据，确认复制吗?", "复制最近季度数据", messButton);
                if (dr == DialogResult.OK)
                {
                    ConnDB conn = new ConnDB();
                    string strsql,strsql2;
                    int rows;
                    bool isok = false;
                    strsql2 = "select * from cost_rate where yyyy ='" + dateTimePicker1.Text.ToString() + "' and dept_id >0 and quarter_id = " + Common.IsZero(comboBoxQuarter.SelectedValue.ToString());
                    rows = conn.ReturnRecordCount(strsql2);
                    if (rows > 0)
                    {
                        MessageBox.Show("该季度部门成本比率已经存在！");
                    }
                    else
                    {
                        strsql = "insert into cost_rate(yyyy,quarter_id,sale_type_id,dept_id,cost_rate) select '" + dateTimePicker1.Text + "','" + Common.IsZero(comboBoxQuarter.SelectedValue.ToString()) + "',sale_type_id,dept_id,cost_rate from cost_rate where YYYY + cast(quarter_id as varchar(10)) = (select Max(yyyy+cast(quarter_id as varchar(10))) from cost_rate where dept_id >0) and dept_id >0";
                        isok = conn.EditDatabase(strsql);
                        if (isok)
                        {
                            MessageBox.Show("复制成功！");
                            CostDeptRateQuery.RefreshEX();
                            this.Close();
                        }
                    }      
                    conn.Close();
                }
            }
        }

        private void CostRateInsert_Load(object sender, EventArgs e)
        {
            Common.BasicDataBind("cost_saletype", comboBoxSaleType);
            Common.BasicDataBind("cost_quarter", comboBoxQuarter);
            BindDept();
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePicker1.Value = startMonth;
        }

        private void comboBoxSaleType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BindDept();
        }
    }
}