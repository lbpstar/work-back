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
    public partial class TempEmpPriceQuery : DevExpress.XtraEditors.XtraForm
    {
        public TempEmpPriceQuery()
        {
            InitializeComponent();
        }
        private static TempEmpPriceQuery weqform = null;

        public static TempEmpPriceQuery GetInstance()
        {
            if (weqform == null || weqform.IsDisposed)
            {
                weqform = new TempEmpPriceQuery();
            }
            return weqform;
        }
        public static void RefreshEX()
        {
            if (weqform == null || weqform.IsDisposed)
            {

            }
            else
            {
                weqform.showDetail();
            }
        }
        public static void Delete()
        {
            string sql;
            bool isdone = true;
            ConnDB conn = new ConnDB();
            if (weqform == null || weqform.IsDisposed)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }
            else if (weqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除吗?", "删除", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < weqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "delete from  COST_TEMP_EMPLOYEE_price where cid = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[0].ToString() + "'";
                        isdone = conn.DeleteDatabase(sql);
                        if (!isdone)
                            break;
                    }
                    if (isdone)
                    {
                        MessageBox.Show("删除成功！");
                    }
                }
            }
            conn.Close();
        }
        public static void GetInfo(ref string id,ref string supplier,ref string begin_date,ref string end_date,ref string from_type,ref string price,ref string meal_price,ref string night_price,ref string travel_price, ref string regular_price)
        {
            if (weqform == null || weqform.IsDisposed)
            {
                MessageBox.Show("没有选中要修改的记录！");
            }
            else if (weqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要修改的记录！");
            }
            else
            {
                id = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[0].ToString();
                supplier = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString();
                begin_date = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[2].ToString();
                end_date = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[3].ToString();
                from_type = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[4].ToString();
                price = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[6].ToString();
                meal_price = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[7].ToString();
                night_price = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[8].ToString();
                travel_price = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[9].ToString();
                regular_price = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[10].ToString();
            }
        }
        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql, yyyymm;
            yyyymm = dateTimePickerBegin.Text.ToString();
            if (textEditSupplier.Text.ToString().Trim() != "")
            {
                strsql = "select cid,supplier 供应商,begin_date 价格开始日期,end_date 价格结束日期,from_type 输送类型id,b.cname 输送类型,price '出勤价格（元/时）',meal_price '餐补（元/天）',night_price '夜班补助（元/天）',travel_price '车补（元/人）',regular_price '转正费（元/人）' from COST_TEMP_EMPLOYEE_price p  left join cost_base_data b on p.from_type = b.sub_id and module_id = 1 where type = 1 and supplier = '" + textEditSupplier.Text.ToString().Trim() + "' and not (begin_date > '" + dateTimePickerEnd.Text + "' or end_date <'" + dateTimePickerBegin.Text + "')";
            }
            else
            {
                strsql = "select cid,supplier 供应商,begin_date 价格开始日期,end_date 价格结束日期,from_type 输送类型id,b.cname 输送类型,price '出勤价格（元/时）',meal_price '餐补（元/天）',night_price '夜班补助（元/天）',travel_price '车补（元/人）',regular_price '转正费（元/人）' from COST_TEMP_EMPLOYEE_price p  left join cost_base_data b on p.from_type = b.sub_id and module_id = 1 where  type = 1 and not (begin_date > '" + dateTimePickerEnd.Text + "' or end_date <'" + dateTimePickerBegin.Text + "')";
            }

            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[4].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[4].OptionsColumn.ReadOnly = true;
            gridView1.Columns[5].OptionsColumn.ReadOnly = true;
            gridView1.Columns[6].OptionsColumn.ReadOnly = true;
            gridView1.Columns[7].OptionsColumn.ReadOnly = true;
            gridView1.Columns[8].OptionsColumn.ReadOnly = true;
            gridView1.Columns[9].OptionsColumn.ReadOnly = true;
            gridView1.Columns[10].OptionsColumn.ReadOnly = true;
            ////表头设置
            //gridView1.ColumnPanelRowHeight = 35;
            //gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            //gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            ////表头及行内容居中显示
            ////gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            conn.Close();
        }

        private void simpleButton查询_Click(object sender, EventArgs e)
        {
            showDetail();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //showDetail();
        }
       
        private void TempEmpQuery_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePickerBegin.Value = startMonth;
            dateTimePickerBegin.Focus();
            SendKeys.Send("{RIGHT} ");
            showDetail();
        }

    }
}