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
    public partial class DeptStandardPointInsert : DevExpress.XtraEditors.XtraForm
    {
        public DeptStandardPointInsert()
        {
            InitializeComponent();
        }
        private static DeptStandardPointInsert spiform = null;

        public static DeptStandardPointInsert GetInstance()
        {
            if (spiform == null || spiform.IsDisposed)
            {
                spiform = new DeptStandardPointInsert();
            }
            return spiform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            strsql = "insert into cost_dept_standard_point(yyyymm,sale_type_id,dept_id,dept_standard_point) values('" + dateTimePicker1.Text.ToString() + "'," + comboBoxSaleType.SelectedValue.ToString() + "," + comboBoxDept.SelectedValue.ToString() + ",ltrim(rtrim(" + textEditDeptStandardPoint.Text.ToString() + ")))";
            strsql2 = "select * from cost_dept_standard_point where yyyymm ='" + dateTimePicker1.Text.ToString() + "' and sale_type_id = " + Common.IsZero(comboBoxSaleType.SelectedValue.ToString()) + " and dept_id = " + Common.IsZero(comboBoxDept.SelectedValue.ToString());
            if(comboBoxSaleType.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("请选择营业分类！");
            }
            else if(comboBoxDept.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("请选择部门！");
            }
            else if(textEditDeptStandardPoint.Text.ToString() == "")
            {
                MessageBox.Show("部门标准单点成本不能为空！");
            }
            else
            {
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("该单点标准成本已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
                        DeptStandardPointQuery.RefreshEX();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("失败！");
                    }
                }
            }
            conn.Close();
        }

        private void simpleButton复制_Click(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("将按最近的月度数据自动新增当前选择月份的数据，确认复制吗?", "复制最近月份数据", messButton);
            if (dr == DialogResult.OK)
            {
                ConnDB conn = new ConnDB();
                string strsql,strsql2;
                int rows;
                bool isok = false;
                DateTime dt1 = Convert.ToDateTime(dateTimePicker1.Text);
                DateTime dt2 = System.DateTime.Now;
                int month = (dt2.Year - dt1.Year) * 12 + (dt2.Month - dt1.Month);
                strsql2 = "select * from cost_dept_standard_point where yyyymm ='" + dateTimePicker1.Text.ToString() + "'";
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("该月度部门标准单点成本已经存在！");
                }
                else if(month < -1)
                {
                    MessageBox.Show("月份错误！");
                }
                else
                {
                    strsql = "insert into cost_dept_standard_point(yyyymm,sale_type_id,dept_id,dept_standard_point) select '" + dateTimePicker1.Text + "',sale_type_id,dept_id,dept_standard_point from cost_dept_standard_point where YYYYMM = (select Max(yyyymm) from cost_dept_standard_point) ";
                    isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("复制成功！");
                        DeptStandardPointQuery.RefreshEX();
                        this.Close();
                    }
                }
                conn.Close();
            }
        }

        private void DeptStandardPointInsert_Load(object sender, EventArgs e)
        {
            Common.BasicDataBind("cost_saletype", comboBoxSaleType);
            BindDept();
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePicker1.Value = startMonth;

            dateTimePicker1.Focus();
            SendKeys.Send("{RIGHT} ");
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

        private void comboBoxSaleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (comboBoxSaleType.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                BindDept();
            }

        }
    }
}