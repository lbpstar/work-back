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
    public partial class TempEmpStandardPriceInsert : DevExpress.XtraEditors.XtraForm
    {
        public TempEmpStandardPriceInsert()
        {
            InitializeComponent();
        }
        private static TempEmpStandardPriceInsert weiform = null;

        public static TempEmpStandardPriceInsert GetInstance()
        {
            if (weiform == null || weiform.IsDisposed)
            {
                weiform = new TempEmpStandardPriceInsert();
            }
            return weiform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            strsql = "insert into COST_TEMP_EMPLOYEE_PRICE(TYPE,BEGIN_DATE,END_DATE,PRICE,INSURANCE_PRICE) values(2,'"  + dateTimePickerBegin.Text.ToString() + "','" + dateTimePickerEnd.Text.ToString() + "','"  + textEditPrice.Text.ToString().Trim() + "','" + textEditInsurance.Text.ToString().Trim() + "')";
            strsql2 = "select price from COST_TEMP_EMPLOYEE_PRICE where  not (begin_date > '" + dateTimePickerEnd.Text.ToString() + "' or end_date <'" + dateTimePickerBegin.Text.ToString() + "') and type = 2";
            if (textEditPrice.Text.ToString().Trim() != "" || textEditInsurance.Text.ToString().Trim() != "")
            {
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("此时间范围的记录已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
                        TempEmpStandardPriceQuery.RefreshEX();
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
                MessageBox.Show("关键数据不能为空！");
            }
            conn.Close();
        }

        private void TempEmpInsert_Load(object sender, EventArgs e)
        {
            //DateTime dt = DateTime.Now;
            //DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            //this.dateTimePicker1.Value = startMonth;

            //dateTimePicker1.Focus();
            //SendKeys.Send("{RIGHT} ");
        }

    }
}