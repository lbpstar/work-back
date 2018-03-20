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
    public partial class IndirectLabourUpdate : DevExpress.XtraEditors.XtraForm
    {
        public IndirectLabourUpdate()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql = "update cost_direct_labour set cno = ltrim(rtrim('" + textEditNo.Text.ToString() + "')),cname = ltrim(rtrim('" + textEditName.Text.ToString() + "')),position_id =" + Common.IsZero(comboBoxPosition.SelectedValue.ToString()) + ",person_level = " + textEditLevel.Text.ToString() + ",dept_id =" + comboBoxDept.SelectedValue.ToString();
            sql = sql + " where cid = " + textEditID.Text.ToString();
            string sql2 = "select cname from cost_direct_labour where cno = '" + textEditNo.Text.ToString().Trim() + "' and cid <> " + textEditID.Text;
            string sql3 = "select * from cost_direct_labour where cno = '" + textEditNo.Text.ToString().Trim() + "' and cname ='" + textEditName.Text.ToString().Trim() + "' and position_id = " + comboBoxPosition.SelectedValue.ToString()  + " and dept_id =" + comboBoxDept.SelectedValue.ToString() + " and person_level = " + Common.IsNull(textEditLevel.Text.ToString().Trim());
            if (textEditNo.Text.ToString().Trim() != "" && textEditName.Text.ToString().Trim() != "" && comboBoxPosition.SelectedValue.ToString() != "0" && comboBoxDept.SelectedValue.ToString() != "0" && textEditLevel.Text.ToString().Trim() != "")
            {
                int rows = conn.ReturnRecordCount(sql2);
                int rows2 = conn.ReturnRecordCount(sql3);
                if (rows > 0)
                {
                    MessageBox.Show("该工号已经存在！");
                }
                else if (rows2 > 0)
                {
                    MessageBox.Show("该人员已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(sql);
                    if (isok)
                    {
                        MessageBox.Show("修改成功！");
                        IndirectLabourQuery.RefreshEX();
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


        private void IndirectLabourUpdate_Load(object sender, EventArgs e)
        {
            string cno = "", cname = "", positionname = "", deptname = "";
            int id = 0, positionid = 0, deptid = 0,person_level = 0;
            IndirectLabourQuery.GetInfo(ref id, ref cno, ref cname, ref positionid,ref positionname, ref person_level, ref deptid, ref deptname);
            PositionBind();
            Common.BasicDataBind("cost_dept", comboBoxDept);
            ConnDB conn = new ConnDB();
            string sql = "select * from cost_dept where cid = " + deptid + " and isnull(forbidden,'false') = 'true'";
            int rows = conn.ReturnRecordCount(sql);
            string sql2 = "select * from cost_position where cid = " + positionid + " and isnull(forbidden,'false') = 'true'";
            int rows2 = conn.ReturnRecordCount(sql);
            if (cno != "")
            {
                textEditNo.Text = cno;
                textEditName.Text = cname;
                textEditLevel.Text = person_level.ToString();
                if (rows == 0)
                {
                    comboBoxDept.SelectedIndex = -1;
                    comboBoxDept.SelectedValue = deptid;
                }
                if (rows2 == 0)
                {
                    comboBoxPosition.SelectedIndex = -1;
                    comboBoxPosition.SelectedValue = positionid;
                }
                textEditID.Text = id.ToString();

            }
        }
        private void PositionBind()
        {
            ConnDB conn = new ConnDB();
            string sql = "select * from cost_position i left join cost_person_type j on i.person_type_id = j.cid where isnull(i.forbidden,'false') != 'true' and j.cname = '间接人工'";
            DataSet ds = conn.ReturnDataSet(sql);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = "0";
            dr[1] = "请选择";
            //插在第一位
            ds.Tables[0].Rows.InsertAt(dr, 0);
            comboBoxPosition.DataSource = ds.Tables[0];
            comboBoxPosition.DisplayMember = "CNAME";
            comboBoxPosition.ValueMember = "CID";
            conn.Close();
        }
    }
}