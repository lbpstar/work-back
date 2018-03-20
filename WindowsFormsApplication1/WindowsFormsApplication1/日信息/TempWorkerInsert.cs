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
    public partial class TempWorkerInsert : DevExpress.XtraEditors.XtraForm
    {
        public TempWorkerInsert()
        {
            InitializeComponent();
        }
        private static TempWorkerInsert ehiform = null;

        public static TempWorkerInsert GetInstance()
        {
            if (ehiform == null || ehiform.IsDisposed)
            {
                ehiform = new TempWorkerInsert();
            }
            return ehiform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            strsql = "insert into cost_temp_worker(cdate,sale_type_id,dept_id,labour_num,hours) values('" + dateEditDate.Text.ToString() + "','" + Common.IsZero(comboBoxSaleType.SelectedValue.ToString()) + "','" + Common.IsZero(comboBoxDept.SelectedValue.ToString()) + "'," + Common.IsNull(textEditLabourNum.Text.ToString().Trim()) + "," + textEditHours.Text.ToString().Trim() + ")";
            strsql2 = "select cdate from cost_temp_worker where cdate ='" + dateEditDate.Text.ToString() + "' and sale_type_id = " + comboBoxSaleType.SelectedValue.ToString() + " and dept_id = " + comboBoxDept.SelectedValue.ToString();
            if (textEditHours.Text.ToString() != "")
            {
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("此日期记录已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
                        TempWorkerQuery.RefreshEX();
                        this.Close();
                    }
                }

            }
            else
            {
                MessageBox.Show("投入工时不能为空！");
            }
            conn.Close();
        }
        public void BindDept()
        {
            ConnDB conn = new ConnDB();
            string sql = "select * from  cost_dept where isnull(forbidden,'false') != 'true' and saletype_id = " + Common.IsZero(comboBoxSaleType.SelectedValue.ToString());
            DataSet ds = conn.ReturnDataSet(sql);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = "0";
            dr[1] = "请选择";
            //插在第一位
            ds.Tables[0].Rows.InsertAt(dr, 0);
            comboBoxDept.DataSource = ds.Tables[0];
            comboBoxDept.DisplayMember = "CNAME";
            comboBoxDept.ValueMember = "CID";
            conn.Close();

        }
        private void TempWorkerInsert_Load(object sender, EventArgs e)
        {
            dateEditDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Common.BasicDataBind("cost_saletype", comboBoxSaleType);
            BindDept();
        }

        private void comboBoxSaleType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BindDept();
        }
    }
}