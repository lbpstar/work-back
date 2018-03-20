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
    public partial class LineTypeInsert : DevExpress.XtraEditors.XtraForm
    {
        public LineTypeInsert()
        {
            InitializeComponent();
        }
        private static LineTypeInsert ltiform = null;

        public static LineTypeInsert GetInstance()
        {
            if (ltiform == null || ltiform.IsDisposed)
            {
                ltiform = new LineTypeInsert();
            }
            return ltiform;
        }
        private void LineTypeInsert_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            Common.BasicDataBind("cost_saletype", comboBoxSaleType);
            BindWorkShop();
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
            string strsql, strsql2;
            int rows;
            strsql = "insert into cost_linetype(CNAME,CNAME_MES,SALETYPE_ID,WORK_SHOP) values'" + textEditName.Text.ToString().Trim() + "','" + textEditNameMes.Text.ToString().Trim() + "'," + comboBoxSaleType.SelectedValue.ToString() + "," + comboBoxWorkShop.SelectedValue.ToString() + ")";
            strsql2 = "select cname from cost_linetype where cname = '" + textEditName.Text.ToString().Trim() + "'";
            if (textEditName.Text.ToString().Trim() != "" || textEditNameMes.Text.ToString().Trim() != "")
            {
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("该线体已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
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
                MessageBox.Show("名称不能为空！");
            }
            conn.Close();
        }
    }
}