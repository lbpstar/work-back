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
    public partial class CostTransferInsert : DevExpress.XtraEditors.XtraForm
    {
        public CostTransferInsert()
        {
            InitializeComponent();
        }
        private static CostTransferInsert ehiform = null;

        public static CostTransferInsert GetInstance()
        {
            if (ehiform == null || ehiform.IsDisposed)
            {
                ehiform = new CostTransferInsert();
            }
            return ehiform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            strsql = "insert into cost_transfer(cdate,sale_type_id,transfer_type,hours) values('" + dateEditDate.Text.ToString() + "','" + Common.IsZero(comboBoxSaleType.SelectedValue.ToString()) + "','" +  Common.IsZero(comboBoxTransferType.SelectedValue.ToString()) + "'," + textEditHours.Text.ToString().Trim() + ")";
            strsql2 = "select cdate from cost_transfer where cdate ='" + dateEditDate.Text.ToString() + "' and sale_type_id = " + comboBoxSaleType.SelectedValue.ToString() + " and transfer_type = " + comboBoxTransferType.SelectedValue.ToString();
            if (textEditHours.Text.ToString() != "")
            {
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("此转嫁工时已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
                        CostTransferQuery.RefreshEX();
                        this.Close();
                    }
                }

            }
            else
            {
                MessageBox.Show("转嫁工时不能为空！");
            }
            conn.Close();
        }

        private void CostTransferInsert_Load(object sender, EventArgs e)
        {
            dateEditDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Common.BasicDataBind("cost_saletype", comboBoxSaleType);
            Common.BasicDataBind("cost_transfer_type", comboBoxTransferType);
        }
    }
}