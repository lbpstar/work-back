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
    public partial class SpecialDealInsert : DevExpress.XtraEditors.XtraForm
    {
        public SpecialDealInsert()
        {
            InitializeComponent();
        }
        private static SpecialDealInsert spiform = null;

        public static SpecialDealInsert GetInstance()
        {
            if (spiform == null || spiform.IsDisposed)
            {
                spiform = new SpecialDealInsert();
            }
            return spiform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            strsql = "insert into cost_special_deal(yyyymm,task_name,organization_id,to_organization_id) values('" + dateTimePicker1.Text.ToString() + "','" + textEditTask.Text.ToString().Trim() + "'," + comboBoxOrg.SelectedValue + "," + comboBoxToOrg.SelectedValue + ")";
            strsql2 = "select yyyymm from cost_special_deal where yyyymm ='" + dateTimePicker1.Text.ToString() + "' and task_name = '" + textEditTask.Text.ToString().Trim() + "'";
            if (textEditTask.Text.ToString() != "")
            {
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("该记录在本月已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
                        SpecialDealQuery.RefreshEX();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("失败！");
                    }
                }

            }
            else
            {
                MessageBox.Show("工单不能为空！");
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
                strsql2 = "select yyyymm from cost_special_deal where yyyymm ='" + dateTimePicker1.Text.ToString() + "'";
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("该月已经有数据存在，不能复制！");
                }
                else if(month < -1)
                {
                    MessageBox.Show("月份错误！");
                }
                else
                {
                    strsql = "insert into cost_special_deal(yyyymm,task_name,organization_id,to_organization_id) select '" + dateTimePicker1.Text + "',task_name,organization_id,to_organization_id from cost_special_deal where YYYYMM = (select Max(yyyymm) from cost_special_deal) ";
                    isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("复制成功！");
                        SpecialDealQuery.RefreshEX();
                        this.Close();
                    }
                }
                conn.Close();
            }
        }
        public void BindOrg(System.Windows.Forms.ComboBox comboboxname)
        {
            ConnDB conn = new ConnDB();
            string sql = "select cid,CODE from COST_ORGANIZATION";
            DataSet ds = conn.ReturnDataSet(sql);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = "0";
            dr[1] = "请选择";
            //插在第一位

            ds.Tables[0].Rows.InsertAt(dr, 0);
            comboboxname.DataSource = ds.Tables[0];
            comboboxname.DisplayMember = "CODE";
            comboboxname.ValueMember = "CID";
            conn.Close();
        }

        private void SpecialDealInsert_Load(object sender, EventArgs e)
        {
            BindOrg(comboBoxOrg);
            BindOrg(comboBoxToOrg);
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePicker1.Value = startMonth;

            dateTimePicker1.Focus();
            SendKeys.Send("{RIGHT} ");
        }         
    }
}