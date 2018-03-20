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
    public partial class CostRateInsert : DevExpress.XtraEditors.XtraForm
    {
        public CostRateInsert()
        {
            InitializeComponent();
        }
        private static CostRateInsert criform = null;

        public static CostRateInsert GetInstance()
        {
            if (criform == null || criform.IsDisposed)
            {
                criform = new CostRateInsert();
            }
            return criform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            strsql = "insert into cost_rate(yyyy,quarter_id,sale_type_id,cost_rate) values('" + dateTimePicker1.Text.ToString() + "'," + comboBoxQuarter.SelectedValue.ToString() + "," + comboBoxSaleType.SelectedValue.ToString() + ",ltrim(rtrim(" + Common.IsNull(textEditCostRate.Text.ToString()) + ")))";
            strsql2 = "select * from cost_rate where yyyy ='" + dateTimePicker1.Text.ToString() + "' and sale_type_id = " + Common.IsZero(comboBoxSaleType.SelectedValue.ToString()) + " and quarter_id = " + Common.IsZero(comboBoxQuarter.SelectedValue.ToString());
            if(comboBoxQuarter.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("请选择季度");
            }
            else if (comboBoxSaleType.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("请选择营业类型");
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
                    MessageBox.Show("该成本比率已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
                        CostRateQuery.RefreshEX();
                        this.Close();
                    }
                }
            }
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
                    strsql2 = "select * from cost_rate where yyyy ='" + dateTimePicker1.Text.ToString() + "' and quarter_id = " + Common.IsZero(comboBoxQuarter.SelectedValue.ToString());
                    rows = conn.ReturnRecordCount(strsql2);
                    if (rows > 0)
                    {
                        MessageBox.Show("该季度成本比率已经存在！");
                    }
                    else
                    {
                        strsql = "insert into cost_rate(yyyy,quarter_id,sale_type_id,cost_rate) select '" + dateTimePicker1.Text + "','" + Common.IsZero(comboBoxQuarter.SelectedValue.ToString()) + "',sale_type_id,cost_rate from cost_rate where YYYY + cast(quarter_id as varchar(10)) = (select Max(yyyy+cast(quarter_id as varchar(10))) from cost_rate where isnull(dept_id,0) = 0) ";
                        isok = conn.EditDatabase(strsql);
                        if (isok)
                        {
                            MessageBox.Show("复制成功！");
                            CostRateQuery.RefreshEX();
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
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePicker1.Value = startMonth;
        }
    }
}