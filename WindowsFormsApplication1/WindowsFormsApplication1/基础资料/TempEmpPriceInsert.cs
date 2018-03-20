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
    public partial class TempEmpPriceInsert : DevExpress.XtraEditors.XtraForm
    {
        public TempEmpPriceInsert()
        {
            InitializeComponent();
        }
        private static TempEmpPriceInsert weiform = null;

        public static TempEmpPriceInsert GetInstance()
        {
            if (weiform == null || weiform.IsDisposed)
            {
                weiform = new TempEmpPriceInsert();
            }
            return weiform;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2;
            int rows;
            strsql = "insert into COST_TEMP_EMPLOYEE_PRICE(TYPE,SUPPLIER,BEGIN_DATE,END_DATE,FROM_TYPE,PRICE,MEAL_PRICE,NIGHT_PRICE,TRAVEL_PRICE,REGULAR_PRICE) values(1,'" + textEditSupplier.Text.ToString().Trim() + "','" + dateTimePickerBegin.Text.ToString() + "','" + dateTimePickerEnd.Text.ToString() + "','" + comboBoxType.SelectedValue.ToString() + "','" + Common.IsNull(textEditPrice.Text.ToString().Trim()) + "','" + Common.IsNull(textEditMeal.Text.ToString().Trim()) + "','" + Common.IsNull(textEditNight.Text.ToString().Trim()) + "','" + Common.IsNull(textEditTravel.Text.ToString().Trim()) + "','" + Common.IsNull(textEditRegular.Text.ToString().Trim()) + "')";
            strsql2 = "select price from COST_TEMP_EMPLOYEE_PRICE where SUPPLIER ='" + textEditSupplier.Text.ToString().Trim() + "' and not (begin_date > '" + dateTimePickerEnd.Text.ToString() + "' or end_date <'" + dateTimePickerBegin.Text.ToString() + "') and from_type = '" + comboBoxType.SelectedValue.ToString() + "' and type = 1";
            if (textEditSupplier.Text.ToString().Trim() != "")
            {
                rows = conn.ReturnRecordCount(strsql2);
                if (rows > 0)
                {
                    MessageBox.Show("此时间范围的价格信息已经存在！");
                }
                else
                {
                    bool isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("添加成功！");
                        TempEmpPriceQuery.RefreshEX();
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
                MessageBox.Show("供应商不能为空！");
            }
            conn.Close();
        }
        public void BindType()
        {
            ConnDB conn = new ConnDB();
            string sql = "select sub_id cid,cname value from cost_base_data where module_id = 1";
            DataSet ds = conn.ReturnDataSet(sql);
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = 0;
            dr[1] = "请选择";
            //插在第一位

            ds.Tables[0].Rows.InsertAt(dr, 0);
            comboBoxType.DataSource = ds.Tables[0];
            comboBoxType.DisplayMember = "value";
            comboBoxType.ValueMember = "CID";
            conn.Close();
        }
        private void TempEmpInsert_Load(object sender, EventArgs e)
        {

            BindType();
            //DateTime dt = DateTime.Now;
            //DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            //this.dateTimePicker1.Value = startMonth;

            //dateTimePicker1.Focus();
            //SendKeys.Send("{RIGHT} ");
        }

    }
}