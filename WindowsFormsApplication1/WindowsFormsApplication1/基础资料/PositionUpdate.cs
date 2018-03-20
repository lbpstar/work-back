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
    public partial class PositionUpdate : DevExpress.XtraEditors.XtraForm
    {
        public PositionUpdate()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql = "update cost_position set cname = ltrim(rtrim('" + textEditName.Text.ToString() + "')),person_type_id =" + comboBoxPersonType.SelectedValue.ToString();
            sql = sql + " where cid = " + textEditID.Text.ToString();
            string sql2 = "select cname from cost_Position where cname = '" + textEditName.Text.ToString().Trim() + "' and person_type_id = " + comboBoxPersonType.SelectedValue.ToString();
            if (textEditName.Text.ToString().Trim() != "" && comboBoxPersonType.SelectedValue.ToString() != "0")
            {
                int rows = conn.ReturnRecordCount(sql2);
                if (rows > 0)
                {
                    MessageBox.Show("该职位已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(sql);
                    if (isok)
                    {
                        MessageBox.Show("修改成功！");
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
                MessageBox.Show("不能为空值！");
            }
            conn.Close();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PositionUpdate_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            string cname = "", persontypename = "";
            int id = 0, persontypeid = 0;
            PositionQuery.GetInfo(ref id, ref cname, ref persontypeid, ref persontypename);
            Common.BasicDataBind("cost_person_type", comboBoxPersonType);
            if (cname != "")
            {
                textEditName.Text = cname;
                comboBoxPersonType.SelectedIndex = -1;
                comboBoxPersonType.SelectedValue = persontypeid;
                textEditID.Text = id.ToString();
                textEditPersonTypeID.Text = persontypeid.ToString();
            }
           
        }
    }
}