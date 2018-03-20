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
    public partial class InDirectLabourLevelPriceInsert : DevExpress.XtraEditors.XtraForm
    {
        public InDirectLabourLevelPriceInsert()
        {
            InitializeComponent();
        }
        private static InDirectLabourLevelPriceInsert ilpiform = null;
        private bool ischange = false;
        public static InDirectLabourLevelPriceInsert GetInstance()
        {
            if (ilpiform == null || ilpiform.IsDisposed)
            {
                ilpiform = new InDirectLabourLevelPriceInsert();
            }
            return ilpiform;
        }
        public static void Save()
        {
            if (ilpiform == null || ilpiform.IsDisposed)
            { }
            else
            {
                ConnDB conn = new ConnDB();
                string strsql;
                bool isok = false;
                DateTime dt1 = Convert.ToDateTime(ilpiform.dateTimePicker1.Text);
                DateTime dt2 = System.DateTime.Now;
                int month = (dt2.Year - dt1.Year) * 12 + (dt2.Month - dt1.Month);
                if (ilpiform.textEditLevelBegin.Text.ToString().Trim() == "" || ilpiform.textEditLevelEnd.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("员工等级不能为空！");
                }
                else if (!ilpiform.Exist())
                {
                    if (month < -1)
                    {
                        MessageBox.Show("月份错误！");
                    }
                    else
                    {
                        ilpiform.gridView1.FocusInvalidRow();
                        for (int i = 0; i < ilpiform.gridView1.RowCount; i++)
                        {
                            strsql = "insert into COST_INDIRECT_LABOUR_LEVEL_PRICE(yyyymm,level_begin,level_end,work_type,price) values('" + ilpiform.dateTimePicker1.Text.ToString() + "'," + ilpiform.textEditLevelBegin.Text.ToString().Trim() + "," + ilpiform.textEditLevelEnd.Text.ToString().Trim() + "," + ilpiform.gridView1.GetDataRow(i).ItemArray[0].ToString() + "," + Common.IsNull(ilpiform.gridView1.GetDataRow(i).ItemArray[2].ToString()) + ")";
                            isok = conn.EditDatabase(strsql);
                        }
                        if (isok)
                        {
                            MessageBox.Show("保存成功！");
                            InDirectLabourLevelPriceQuery.RefreshEX();
                            ilpiform.Close();
                        }
                        else
                        {
                            MessageBox.Show("失败！");
                        }
                    }

                }
                else
                {
                    MessageBox.Show("该数据已经存在！");
                }
                ilpiform.ischange = false;
                conn.Close();
            }
                
        }

        private bool Exist()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            int rows;
            strsql = "select * from COST_INDIRECT_LABOUR_LEVEL_PRICE where YYYYMM = '" + dateTimePicker1.Text.ToString() + "' and level_begin = " + Common.IsNull(ilpiform.textEditLevelBegin.Text.ToString().Trim()) + " and level_end = " + Common.IsNull(ilpiform.textEditLevelEnd.Text.ToString().Trim());
            rows = conn.ReturnRecordCount(strsql);
            if (rows > 0)
            {
                labelControlCheck.Text = "该数据已经存在！";
                gridControl1.Enabled = false;
                return true;
            }
            else
            {
                labelControlCheck.Text = "";
                gridControl1.Enabled = true;
                return false;
            }
        }
        private void ShowDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql, yyyymm;
            yyyymm = dateTimePicker1.Text.ToString();
            strsql = "select CID,CNAME 上班类型,'0' as '费率(元/小时)'  from COST_WORK_TYPE where forbidden = 'false'";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            conn.Close();
        }

        private void simpleButtonExit_Click(object sender, EventArgs e)
        {
            if (ischange)
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("没有保存，确认退出吗?", "退出", messButton);
                if (dr == DialogResult.OK)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Exist();
            ShowDetail();
        }

        private void simpleButton复制_Click(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("将按最近的月度数据自动新增当前选择月份的数据，确认复制吗?", "复制最近月份数据", messButton);
            if (dr == DialogResult.OK)
            {
                ConnDB conn = new ConnDB();
                string strsql;
                int rows;
                bool isok = false;
                DateTime dt1 = Convert.ToDateTime(ilpiform.dateTimePicker1.Text);
                DateTime dt2 = System.DateTime.Now;
                int month = (dt2.Year - dt1.Year) * 12 + (dt2.Month - dt1.Month);
                strsql = "select * from COST_INDIRECT_LABOUR_LEVEL_PRICE where YYYYMM = '" + dateTimePicker1.Text.ToString() + "'";
                rows = conn.ReturnRecordCount(strsql);
                if (rows > 0)
                {
                    MessageBox.Show("该月度间接人工费率已经存在！");
                }
                else if (month < -1)
                {
                    MessageBox.Show("月份错误！");
                }
                else
                {
                    strsql = "insert into COST_INDIRECT_LABOUR_LEVEL_PRICE(yyyymm,level_begin,level_end,work_type,price) select '" + ilpiform.dateTimePicker1.Text + "',level_begin,level_end,work_type,price from COST_INDIRECT_LABOUR_LEVEL_PRICE where YYYYMM = (select Max(yyyymm) from COST_INDIRECT_LABOUR_LEVEL_PRICE) ";
                    isok = conn.EditDatabase(strsql);
                    if (isok)
                    {
                        MessageBox.Show("复制成功！");
                        InDirectLabourLevelPriceQuery.RefreshEX();
                        this.Close();
                    }
                }
                conn.Close();
            }
            //Exist();
            //ShowDetail();
        }

        private void DirectLabourPriceInsert_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            //Exist();
            ShowDetail();
            InDirectLabourLevelPrice.savetype = "insert";
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePicker1.Value = startMonth;

            dateTimePicker1.Focus();
            SendKeys.Send("{RIGHT} ");
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            ischange = true;
        }
        public static void MyClose()
        {
            ilpiform.Close();
        }
      }
}