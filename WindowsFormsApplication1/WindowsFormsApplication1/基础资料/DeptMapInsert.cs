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
    public partial class DeptMapInsert : DevExpress.XtraEditors.XtraForm
    {
        public DeptMapInsert()
        {
            InitializeComponent();
        }
        private static DeptMapInsert diform = null;

        public static DeptMapInsert GetInstance()
        {
            if (diform == null || diform.IsDisposed)
            {
                diform = new DeptMapInsert();
            }
            return diform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            strsql = "insert into cost_dept_map(dept_id,DEPT1,DEPT2,DEPT3,DEPT4) values(" + comboBoxDept.SelectedValue.ToString() + ",'制造中心','" + textEdit2nd.Text.ToString().Trim() + "','" + textEdit3rd.Text.ToString().Trim() + "','" + textEdit4th.Text.ToString().Trim()  + "')";
            strsql2 = "select * from cost_dept_map where isnull(DEPT2,'') = '" + textEdit2nd.Text.ToString().Trim() + "' and isnull(DEPT3,'') = '" + textEdit3rd.Text.ToString().Trim() + "' and isnull(DEPT4,'') = '" + textEdit4th.Text.ToString().Trim() + "'";
            if (textEdit4th.Text.ToString().Trim() != "" && comboBoxDept.SelectedValue.ToString() != "0")
            {
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("该考勤部门已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
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
                MessageBox.Show("不能为空！");
            }
            conn.Close();
        }

        private void DeptMapInsert_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            Common.BasicDataBind("cost_dept", comboBoxDept);
        }
    }
}