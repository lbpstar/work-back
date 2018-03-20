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
    public partial class IndirectLabourPriceInsert : DevExpress.XtraEditors.XtraForm
    {
        public IndirectLabourPriceInsert()
        {
            InitializeComponent();
        }
        private static IndirectLabourPriceInsert ilpiform = null;
        private bool ischange = false;
        public static IndirectLabourPriceInsert GetInstance()
        {
            if (ilpiform == null || ilpiform.IsDisposed)
            {
                ilpiform = new IndirectLabourPriceInsert();
            }
            return ilpiform;
        }
        public static void Save()
        {
            if (ilpiform == null || ilpiform.IsDisposed)
            { }
            else
            {
                //ilpiform.gridView1.UpdateCurrentRow();
                ConnDB conn = new ConnDB();
                string strsql;
                bool isok = false;
                DateTime dt1 = Convert.ToDateTime(ilpiform.dateTimePicker1.Text);
                DateTime dt2 = System.DateTime.Now;
                int month = (dt2.Year - dt1.Year) * 12 + (dt2.Month - dt1.Month);
                if (!ilpiform.Exist())
                {
                    if (month < 0)
                    {
                        MessageBox.Show("不能新增以后的月份！");
                    }
                    else if(ilpiform.comboBoxDept.SelectedValue.ToString()=="0")
                    {
                        MessageBox.Show("请选择部门！");
                    }
                    else if (ilpiform.comboBoxIndirectLabour.SelectedValue.ToString() == "0")
                    {
                        MessageBox.Show("请选择人员！");
                    }
                    else
                    {
                        ilpiform.gridView1.FocusInvalidRow();
                        for (int i = 0; i < ilpiform.gridView1.RowCount; i++)
                        {
                            strsql = "insert into cost_indirect_labour_price(yyyymm,indirect_labour_id,work_type_id,price) values('" + ilpiform.dateTimePicker1.Text.ToString() + "'," + ilpiform.comboBoxIndirectLabour.SelectedValue.ToString() + "," +  ilpiform.gridView1.GetDataRow(i).ItemArray[2].ToString() + "," + ilpiform.gridView1.GetDataRow(i).ItemArray[4].ToString() + ")";
                            isok = conn.EditDatabase(strsql);
                            //MessageBox.Show(ilpiform.gridView1.GetRowCellValue(i, "费率").ToString()); 
                        }
                        if (isok)
                        {
                            MessageBox.Show("保存成功！");
                            IndirectLabourPriceQuery.RefreshEX();
                        }
                    }

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
            strsql = "select * from cost_indirect_labour_price i left join cost_direct_labour j on i.indirect_labour_id = j.cid where isnull(YYYYMM,'') ='" + dateTimePicker1.Text.ToString() + "' and j.dept_id = " + comboBoxDept.SelectedValue.ToString() + " and indirect_labour_id = " + comboBoxIndirectLabour.SelectedValue.ToString();
            rows = conn.ReturnRecordCount(strsql);
            if (rows > 0)
            {
                labelControlCheck.Text = "该人员数据已经存在！";
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
            strsql = "select '" + comboBoxIndirectLabour.SelectedValue.ToString() + "'as labour_id,'" + ((DataRowView)comboBoxIndirectLabour.SelectedItem).Row["CNAME"].ToString() + "' as 人员,j.CID,j.CNAME 上班类型,i.price '费率(元/小时)' from (select i.* from cost_indirect_labour_price i left join cost_direct_labour j on i.indirect_labour_id = j.cid where isnull(YYYYMM,'') ='" + yyyymm + "' and j.dept_id = " + comboBoxDept.SelectedValue.ToString() + " and indirect_labour_id = " + comboBoxIndirectLabour.SelectedValue.ToString() + ") i right join (select * from COST_WORK_TYPE where forbidden = 'false') j on i.WORK_TYPE_id = j.cid  ";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[2].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].Width = 130;
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

        private void simpleButton复制_Click(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("将按最近的月度数据自动新增当前选择月份的数据，确认复制吗?", "复制最近月份数据", messButton);
            if (dr == DialogResult.OK)
            {
                ConnDB conn = new ConnDB();
                string strsql;
                bool isok = false;
                DateTime dt1 = Convert.ToDateTime(ilpiform.dateTimePicker1.Text);
                DateTime dt2 = System.DateTime.Now;
                int month = (dt2.Year - dt1.Year) * 12 + (dt2.Month - dt1.Month);
                if (!ilpiform.Exist())
                {
                    if (month < 0)
                    {
                        MessageBox.Show("不能新增以后的月份！");
                    }
                    else
                    {
                        strsql = "insert into cost_indirect_labour_price(yyyymm,indirect_labour_id,work_type_id,price) select '" + ilpiform.dateTimePicker1.Text + "',indirect_labour_id,work_type_id,price from cost_indirect_labour_price where YYYYMM = (select Max(yyyymm) from cost_indirect_labour_price) ";
                        isok = conn.EditDatabase(strsql);
                        if (isok)
                        {
                            MessageBox.Show("复制成功！");
                            IndirectLabourPriceQuery.RefreshEX();
                            this.Close();
                        }
                    }

                }
                conn.Close();
            }
        }

        private void BindIndirectLabour()
        {
            if(comboBoxDept.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                ConnDB conn = new ConnDB();
                string sql = "select CID,CNO + ' ' + CNAME as CNAME from cost_direct_labour where person_type_id = 4 and isnull(forbidden,'false') != 'true' and dept_id = " + comboBoxDept.SelectedValue.ToString();
                DataSet ds = conn.ReturnDataSet(sql);
                DataRow dr = ds.Tables[0].NewRow();
                dr[0] = "0";
                dr[1] = "请选择";
                //插在第一位
                ds.Tables[0].Rows.InsertAt(dr, 0);
                comboBoxIndirectLabour.DataSource = ds.Tables[0];
                comboBoxIndirectLabour.DisplayMember = "CNAME";
                comboBoxIndirectLabour.ValueMember = "CID";
                conn.Close();
            }
            
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            ischange = true;
        }

        private void comboBoxDept_SelectedValueChanged(object sender, EventArgs e)
        {
            BindIndirectLabour();
            if (comboBoxDept.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                ShowDetail();
            }
        }

        private void comboBoxIndirectLabour_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxIndirectLabour.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                Exist();
                ShowDetail();
            }
        }

        private void IndirectLabourPriceInsert_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            IndirectLabourPrice.savetype = "insert";
            Common.BasicDataBind("cost_dept", comboBoxDept);
            BindIndirectLabour();
            Exist();
            ShowDetail();
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePicker1.Value = startMonth;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Exist();
            ShowDetail();
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }
        public static void MyClose()
        {
            ilpiform.Close();
        }
    }
}