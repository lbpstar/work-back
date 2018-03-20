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
    public partial class DeptUpdate : DevExpress.XtraEditors.XtraForm
    {
        public DeptUpdate()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql = "update cost_dept set cname = '" + textEditName.Text.ToString().Trim()  + "',saletype_id =" + comboBoxSaleType.SelectedValue.ToString();
            sql = sql + " where cid = " + textEditID.Text.ToString();
            string sql2 = "select cname from cost_dept where cname = '" + textEditName.Text.ToString() + "' and saletype_id = " + comboBoxSaleType.SelectedValue.ToString();
            if (textEditName.Text.ToString().Trim() != "" && comboBoxSaleType.SelectedValue.ToString() != "0")
            {
                int rows = conn.ReturnRecordCount(sql2);
                if (rows > 0)
                {
                    MessageBox.Show("该部门已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(sql);
                    if (isok)
                    {
                        MessageBox.Show("修改成功！");
                        DeptQuery.RefreshEX();
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

        private void DeptUpdate_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            string cname = "", saletypename = "";
            int id = 0, saletypeid = 0;
            ConnDB conn = new ConnDB();
            DeptQuery.GetInfo(ref id, ref cname,ref saletypeid, ref saletypename);
            string sql = "select * from cost_saletype where cid = " + saletypeid + " and isnull(forbidden,'false') = 'true'";
            int rows = conn.ReturnRecordCount(sql);
            Common.BasicDataBind("cost_saletype", comboBoxSaleType);
            if (cname != "")
            {
                textEditName.Text = cname;
                if (rows == 0)
                {
                    comboBoxSaleType.SelectedIndex = -1;
                    comboBoxSaleType.SelectedValue = saletypeid;
                }
                textEditID.Text = id.ToString();
                textEditSaleTypeID.Text = saletypeid.ToString();
            }
        }
    }
}