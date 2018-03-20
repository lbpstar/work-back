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
    public partial class ChannelQuantityQuery : DevExpress.XtraEditors.XtraForm
    {
        public ChannelQuantityQuery()
        {
            InitializeComponent();
        }
        private static ChannelQuantityQuery ehqform = null;
        private bool ischange = false;
        public static ChannelQuantityQuery GetInstance()
        {
            if (ehqform == null || ehqform.IsDisposed)
            {
                ehqform = new ChannelQuantityQuery();
            }
            return ehqform;
        }
        public static void RefreshEX()
        {
            if (ehqform == null || ehqform.IsDisposed)
            {

            }
            else
            {
                ehqform.showDetail();
            }
        }
        public static void Delete()
        {
            string sql;
            bool isdone = true;
            ConnDB conn = new ConnDB();
            if (ehqform == null || ehqform.IsDisposed)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }
            else if (ehqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除吗?", "删除", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < ehqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "delete from cost_product_quantity where cdate = '" + ehqform.gridView1.GetDataRow(ehqform.gridView1.GetSelectedRows()[i]).ItemArray[0].ToString() + "' and type = 2";
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

        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql,month;
            month = dateTimePicker1.Text.ToString(); 
            strsql = "select i.cdate 日期,i.product_quantity 信道数量 from cost_product_quantity i where i.cdate like '" + month + "%' and type = 2";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            //gridView1.Columns[0].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            //gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            conn.Close();
        }
      
        
        private void simpleButton查询_Click(object sender, EventArgs e)
        {
            showDetail();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            showDetail();
        }

        private void simpleButtonSubmit_Click(object sender, EventArgs e)
        {
            if (ischange == true)
            {
                ConnDB conn = new ConnDB();
                string strsql;
                bool isok = false;

                //gridView1.FocusInvalidRow();
                for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                {
                    strsql = "update i set i.product_quantity = " + gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[1].ToString() + " from COST_product_quantity i where type = 2 and i.cdate = " + gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[0].ToString();
                    isok = conn.EditDatabase(strsql);
                }

                if (isok)
                {
                    MessageBox.Show("提交成功！");
                    showDetail();
                }
                else
                {
                    MessageBox.Show("失败！");
                }
                conn.Close();
                ischange = false;
            }
            else
            {
                MessageBox.Show("没有可更新的数据！");
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            ischange = true;
            gridView1.SelectRow(e.RowHandle);
        }

        private void ChannelQuantityQuery_Load(object sender, EventArgs e)
        {
            showDetail();
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePicker1.Value = startMonth;

            dateTimePicker1.Focus();
            SendKeys.Send("{RIGHT} ");
        }
    }
}