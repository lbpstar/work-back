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
    public partial class LineTypeUpdate : DevExpress.XtraEditors.XtraForm
    {
        public LineTypeUpdate()
        {
            InitializeComponent();
        }

        private void LineTypeUpdate_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            string cname = "", cnamemes = "", saletypename = "", workshop ="";
            int id = 0, saletypeid = 0, workshopid =0;
            ConnDB conn = new ConnDB();
            LineTypeQuery.GetInfo(ref id, ref cname, ref cnamemes, ref saletypeid, ref saletypename, ref workshopid, ref workshop);
            string sql = "select * from cost_saletype where cid = " + saletypeid + " and isnull(forbidden,'false') = 'true'";
            int rows = conn.ReturnRecordCount(sql);
            Common.BasicDataBind("cost_saletype", comboBoxSaleType);
            BindWorkShop();
            if (cname != "" || cnamemes !="")
            {
                textEditName.Text = cname;
                textEditNameMes.Text = cnamemes;
                if (rows == 0)
                {
                    comboBoxSaleType.SelectedIndex = -1;
                    comboBoxSaleType.SelectedValue = saletypeid;
                }
                comboBoxWorkShop.SelectedIndex = -1;
                comboBoxWorkShop.SelectedValue = workshopid;
                textEditID.Text = id.ToString();
                textEditSaleTypeID.Text = saletypeid.ToString();
            }
        }
        public void BindWorkShop()
        {
            ConnDB conn = new ConnDB();
            string sql = "select sub_id cid,cname value from cost_base_data where module_id = 3";
            DataSet ds = conn.ReturnDataSet(sql);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = DBNull.Value;
            dr[1] = "请选择";
            //插在第一位

            ds.Tables[0].Rows.InsertAt(dr, 0);
            comboBoxWorkShop.DataSource = ds.Tables[0];
            comboBoxWorkShop.DisplayMember = "value";
            comboBoxWorkShop.ValueMember = "CID";
            conn.Close();
        }
        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string sql = "update cost_linetype set cname = '" + textEditName.Text.ToString().Trim() + "',cname_mes = '" + textEditNameMes.Text.ToString().Trim() + "', saletype_id =" + comboBoxSaleType.SelectedValue.ToString() + ",WORK_SHOP =" + comboBoxWorkShop.SelectedValue.ToString();
            sql = sql + " where cid = " + textEditID.Text.ToString();
            string sql2 = "select cname from cost_linetype where cname = '" + textEditName.Text.ToString().Trim() + "' and cname_mes = '" + textEditName.Text.ToString().Trim() + "' and saletype_id = " + comboBoxSaleType.SelectedValue.ToString();
            if ((textEditName.Text.ToString().Trim() != "" || textEditNameMes.Text.ToString().Trim() != "") && comboBoxSaleType.SelectedValue.ToString() != "0" && comboBoxWorkShop.SelectedValue.ToString() != "0")
            {
                int rows = conn.ReturnRecordCount(sql2);
                if (rows > 0)
                {
                    MessageBox.Show("该线体已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(sql);
                    if (isok)
                    {
                        MessageBox.Show("修改成功！");
                        LineTypeQuery.RefreshEX();
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
    }
}