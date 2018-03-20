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
    public partial class IndirectLabourInsert : DevExpress.XtraEditors.XtraForm
    {
        public IndirectLabourInsert()
        {
            InitializeComponent();
        }
        private static IndirectLabourInsert iliform = null;

        public static IndirectLabourInsert GetInstance()
        {
            if (iliform == null || iliform.IsDisposed)
            {
                iliform = new IndirectLabourInsert();
            }
            return iliform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            strsql = "insert into cost_direct_labour(CNO,CNAME,POSITION_ID,DEPT_ID,PERSON_LEVEL,PERSON_TYPE_ID,FORBIDDEN) values(LTRIM(rtrim('" + textEditNo.Text.ToString().Trim() + "')),LTRIM(rtrim('" + textEditName.Text.ToString().Trim() + "'))," + comboBoxPosition.SelectedValue.ToString() + "," + comboBoxDept.SelectedValue.ToString() +"," + textEditLevel.Text.ToString().Trim() + ",4,0)";
            strsql2 = "select cname from cost_direct_labour where cno = LTRIM(rtrim('" + textEditNo.Text.ToString().Trim() + "'))";
            if (textEditLevel.Text.ToString().Trim() == "")
            {
                MessageBox.Show("员工等级不能为空！");
            }
            else if (textEditNo.Text.ToString() != "")
            {
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("该人员已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
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
                MessageBox.Show("工号不能为空！");
            }
            conn.Close();
        }


        private void IndirectLabourInsert_Load(object sender, EventArgs e)
        {
            PositionBind();
            Common.BasicDataBind("cost_dept", comboBoxDept);
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