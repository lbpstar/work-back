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
    public partial class DeptMapUpdate : DevExpress.XtraEditors.XtraForm
    {
        public DeptMapUpdate()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            int deptid = 0;
            string cname = "", attendance = "", attendance3rd = "", attendance4th = "";
            DeptMapQuery.GetInfo(ref deptid, ref cname, ref attendance,ref attendance3rd,ref attendance4th);
            ConnDB conn = new ConnDB();
            string sql = "update cost_dept_map set DEPT2 = '" + textEdit2nd.Text.ToString().Trim() + "',dept_id =" + comboBoxDept.SelectedValue.ToString() + ",DEPT3 = '" + textEdit3rd.Text.ToString().Trim() + "',DEPT4 = '" + textEdit4th.Text.ToString().Trim() + "'";
            sql = sql + " where dept_id = " + deptid + " and isnull(DEPT2,'') = '" + attendance + "' and isnull(DEPT3,'') = '" + attendance3rd + "' and isnull(DEPT4,'') = '" + attendance4th + "'";
            string sql2 = "select * from cost_dept_map where isnull(DEPT2,'') = '" + textEdit2nd.Text.ToString().Trim() + "' and dept_id = " + comboBoxDept.SelectedValue.ToString() + " and isnull(DEPT3,'') = '" + textEdit3rd.Text.ToString().Trim() + "' and isnull(DEPT4,'') = '" + textEdit4th.Text.ToString().Trim() + "'";
            if (textEdit4th.Text.ToString().Trim() != "" && comboBoxDept.SelectedValue.ToString() != "0")
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
                        DeptMapQuery.RefreshEX();
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

        private void DeptMapUpdate_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            int deptid = 0;
            string cname = "", attendance = "", attendance3rd = "", attendance4th ="";
            DeptMapQuery.GetInfo(ref deptid, ref cname, ref attendance,ref attendance3rd, ref attendance4th);
            Common.BasicDataBind("cost_dept", comboBoxDept);
            if (cname != "")
            {
                textEdit2nd.Text = attendance;
                textEdit3rd.Text = attendance3rd;
                textEdit4th.Text = attendance4th;
                comboBoxDept.SelectedIndex = -1;
                comboBoxDept.SelectedValue = deptid;
                textEditID.Text = deptid.ToString();
            }
        }
    }
}