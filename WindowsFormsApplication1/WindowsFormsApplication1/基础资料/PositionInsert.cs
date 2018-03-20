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
    public partial class PositionInsert : DevExpress.XtraEditors.XtraForm
    {
        public PositionInsert()
        {
            InitializeComponent();
        }
        private static PositionInsert diform = null;

        public static PositionInsert GetInstance()
        {
            if (diform == null || diform.IsDisposed)
            {
                diform = new PositionInsert();
            }
            return diform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            strsql = "insert into cost_Position(CNAME,person_type_id) values(LTRIM(rtrim('" + textEditName.Text.ToString() + "'))," + comboBoxPersonType.SelectedValue.ToString() + ")";
            strsql2 = "select cname from cost_Position where cname = LTRIM(rtrim('" + textEditName.Text.ToString() + "'))";
            if (textEditName.Text.ToString().Trim() != "")
            {
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("该职位已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
                        PositionQuery.RefreshEX();
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

        private void PositionInsert_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            Common.BasicDataBind("cost_person_type", comboBoxPersonType);
        }
    }
}