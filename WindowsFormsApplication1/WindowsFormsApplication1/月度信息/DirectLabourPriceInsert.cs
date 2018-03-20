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
    public partial class DirectLabourPriceInsert : DevExpress.XtraEditors.XtraForm
    {
        public DirectLabourPriceInsert()
        {
            InitializeComponent();
        }
        private static DirectLabourPriceInsert dlpiform = null;
        private bool ischange = false;
        public static DirectLabourPriceInsert GetInstance()
        {
            if (dlpiform == null || dlpiform.IsDisposed)
            {
                dlpiform = new DirectLabourPriceInsert();
            }
            return dlpiform;
        }
        public static void Save()
        {
            if (dlpiform == null || dlpiform.IsDisposed)
            { }
            else
            {
                ConnDB conn = new ConnDB();
                string strsql;
                bool isok = false;
                DateTime dt1 = Convert.ToDateTime(dlpiform.dateTimePicker1.Text);
                DateTime dt2 = System.DateTime.Now;
                int month = (dt2.Year - dt1.Year) * 12 + (dt2.Month - dt1.Month);
                if (!dlpiform.Exist())
                {
                    if (month < -1)
                    {
                        MessageBox.Show("月份错误！");
                    }
                    else
                    {
                        dlpiform.gridView1.FocusInvalidRow();
                        for (int i = 0; i < dlpiform.gridView1.RowCount; i++)
                        {
                            strsql = "insert into cost_direct_labour_price(yyyymm,work_type,price) values('" + dlpiform.dateTimePicker1.Text.ToString() + "'," + dlpiform.gridView1.GetDataRow(i).ItemArray[0].ToString() + "," + Common.IsNull(dlpiform.gridView1.GetDataRow(i).ItemArray[2].ToString()) + ")";
                            isok = conn.EditDatabase(strsql);
                        }
                        if (isok)
                        {
                            MessageBox.Show("保存成功！");
                            DirectLabourPriceQuery.RefreshEX();
                        }
                        else
                        {
                            MessageBox.Show("失败！");
                        }
                    }

                }
                dlpiform.ischange = false;
                conn.Close();
            }
                
        }

        private bool Exist()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            int rows;
            strsql = "select * from cost_direct_labour_price where YYYYMM = '" + dateTimePicker1.Text.ToString() + "'";
            rows = conn.ReturnRecordCount(strsql);
            if (rows > 0)
            {
                labelControlCheck.Text = "该月数据已经存在！";
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
            strsql = "select j.CID,j.CNAME 上班类型,i.price '费率(元/小时)' from (select * from cost_direct_labour_price where isnull(YYYYMM,'') ='" + yyyymm + "') i right join (select * from COST_WORK_TYPE where forbidden = 'false') j on i.WORK_TYPE = j.cid  ";
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
                bool isok = false;
                DateTime dt1 = Convert.ToDateTime(dlpiform.dateTimePicker1.Text);
                DateTime dt2 = System.DateTime.Now;
                int month = (dt2.Year - dt1.Year) * 12 + (dt2.Month - dt1.Month);
                if (!dlpiform.Exist())
                {
                    if (month < -1)
                    {
                        MessageBox.Show("月份错误！");
                    }
                    else
                    {
                        strsql = "insert into cost_direct_labour_price(yyyymm,work_type,price) select '" + dlpiform.dateTimePicker1.Text + "',work_type,price from cost_direct_labour_price where YYYYMM = (select Max(yyyymm) from cost_direct_labour_price) ";
                        isok = conn.EditDatabase(strsql);
                        if (isok)
                        {
                            MessageBox.Show("复制成功！");
                            DirectLabourPriceQuery.RefreshEX();
                            this.Close();
                        }
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
            Exist();
            ShowDetail();
            DirectLabourPrice.savetype = "insert";
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
            dlpiform.Close();
        }
      }
}