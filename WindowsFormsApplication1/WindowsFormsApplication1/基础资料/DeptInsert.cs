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
    public partial class DeptInsert : DevExpress.XtraEditors.XtraForm
    {
        public DeptInsert()
        {
            InitializeComponent();
        }
        private static DeptInsert diform = null;

        public static DeptInsert GetInstance()
        {
            if (diform == null || diform.IsDisposed)
            {
                diform = new DeptInsert();
            }
            return diform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            strsql = "insert into cost_dept(CNAME,SALETYPE_ID) values('" + textEditName.Text.ToString().Trim() + "'," + comboBoxSaleType.SelectedValue.ToString() + ")";
            strsql2 = "select cname from cost_dept where cname = '" + textEditName.Text.ToString().Trim() + "'";
            if (textEditName.Text.ToString().Trim() != "")
            {
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("该部门已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
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
                MessageBox.Show("名称不能为空！");
            }
            conn.Close();
        }

        private void DeptInsert_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            Common.BasicDataBind("cost_saletype", comboBoxSaleType);
        }
    }
}