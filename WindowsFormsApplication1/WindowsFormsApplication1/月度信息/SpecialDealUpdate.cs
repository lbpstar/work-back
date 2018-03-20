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
    public partial class SpecialDealUpdate : DevExpress.XtraEditors.XtraForm
    {
        public SpecialDealUpdate()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql = "update cost_special_deal set task_name = '" + textEditTask.Text.ToString().Trim() + "',organization_id = " + comboBoxOrg.SelectedValue.ToString() + ",to_organization_id = " + comboBoxToOrg.SelectedValue.ToString();
            sql = sql + " where cid = " + textEditID.Text.ToString();
            string sql2 = "select * from cost_special_deal where task_name = '" + textEditTask.Text.ToString().Trim()
                + "' and cid <> " + textEditID.Text.ToString() + " and yyyymm = '" + dateTimePicker1.Text.ToString() + "'";
            if (textEditTask.Text.ToString().Trim() != "" && comboBoxOrg.SelectedValue.ToString() != "0" && comboBoxToOrg.SelectedValue.ToString() != "0")
            {
                int rows = conn.ReturnRecordCount(sql2);
                if (rows > 0)
                {
                    MessageBox.Show("该记录已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(sql);
                    if (isok)
                    {
                        MessageBox.Show("修改成功！");
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
                MessageBox.Show("不能为空值！");
            }         
            conn.Close();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void SpecialDealUpdate_Load(object sender, EventArgs e)
        {
            BindOrg(comboBoxOrg);
            BindOrg(comboBoxToOrg);
            string yyyymm = "", taskname="";
            int id = 0, organizationid = 0, toorganizationid=0;
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePicker1.Value = startMonth;
            SpecialDealQuery.GetInfo(ref id, ref yyyymm, ref taskname, ref organizationid, ref toorganizationid);
            if (id != 0)
            {
                comboBoxOrg.SelectedIndex = -1;
                comboBoxOrg.SelectedValue = organizationid;
                comboBoxToOrg.SelectedIndex = -1;
                comboBoxToOrg.SelectedValue = toorganizationid;
                dateTimePicker1.Text = yyyymm;
                textEditTask.Text = taskname;
                textEditID.Text = id.ToString();

            }

            dateTimePicker1.Focus();
            SendKeys.Send("{RIGHT} ");
        }
    }
}