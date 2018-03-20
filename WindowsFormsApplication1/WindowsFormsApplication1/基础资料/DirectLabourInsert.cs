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
    public partial class DirectLabourInsert : DevExpress.XtraEditors.XtraForm
    {
        public DirectLabourInsert()
        {
            InitializeComponent();
        }
        private static DirectLabourInsert dliform = null;

        public static DirectLabourInsert GetInstance()
        {
            if (dliform == null || dliform.IsDisposed)
            {
                dliform = new DirectLabourInsert();
            }
            return dliform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            strsql = "insert into cost_direct_labour(CNO,CNAME,DEPT_ID,POSITION_ID,LINETYPE_ID,PERSON_TYPE_ID,FORBIDDEN) values(LTRIM(rtrim('" + textEditNo.Text.ToString() + "')),LTRIM(rtrim('" + textEditName.Text.ToString() + "'))," + comboBoxDept.SelectedValue.ToString() + "," + comboBoxPosition.SelectedValue.ToString() + "," + Common.IsZero(comboBoxLineType.SelectedValue.ToString()) + ",3,0)";
            strsql2 = "select cname from cost_direct_labour where cno = LTRIM(rtrim('" + textEditNo.Text.ToString().Trim() + "'))";
            if (textEditNo.Text.ToString().Trim() != "")
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
                        DirectLabourQuery.RefreshEX();
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

        private void DirectLabourInsert_Load(object sender, EventArgs e)
        {
            PositionBind();
            Common.BasicDataBind("cost_dept", comboBoxDept);
            LineTypeBind();
            
        }
        private void PositionBind()
        {
            ConnDB conn = new ConnDB();
            string sql = "select * from cost_position i left join cost_person_type j on i.person_type_id = j.cid where isnull(i.forbidden,'false') != 'true' and j.cname = '直接人工'";
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
        private void LineTypeBind()
        {
            ConnDB conn = new ConnDB();
            string sql = "select l.cid,l.cname from COST_LINETYPE l left join COST_SALETYPE s on l.SALETYPE_ID = s.CID left join COST_DEPT d on s.CID = d.SALETYPE_ID where isnull(l.forbidden,'false') != 'true' and d.CID = " + comboBoxDept.SelectedValue.ToString();
            DataSet ds = conn.ReturnDataSet(sql);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = "0";
            dr[1] = "请选择";
            //插在第一位
            ds.Tables[0].Rows.InsertAt(dr, 0);
            comboBoxLineType.DataSource = ds.Tables[0];
            comboBoxLineType.DisplayMember = "CNAME";
            comboBoxLineType.ValueMember = "CID";
            conn.Close();
        }

        private void comboBoxDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDept.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                LineTypeBind();
            }
        }
    }
}