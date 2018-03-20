﻿using System;
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
    public partial class SpecialDealQuery : DevExpress.XtraEditors.XtraForm
    {
        public SpecialDealQuery()
        {
            InitializeComponent();
        }
        private static SpecialDealQuery spqform = null;

        public static SpecialDealQuery GetInstance()
        {
            if (spqform == null || spqform.IsDisposed)
            {
                spqform = new SpecialDealQuery();
            }
            return spqform;
        }
        public static void RefreshEX()
        {
            if (spqform == null || spqform.IsDisposed)
            {

            }
            else
            {
                spqform.showDetail();
            }
        }
        public static void Delete()
        {
            string sql;
            bool isdone = true;
            ConnDB conn = new ConnDB();
            if (spqform == null || spqform.IsDisposed)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }
            else if (spqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要删除的记录！");
            }

            else
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除吗?", "删除", messButton);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < spqform.gridView1.SelectedRowsCount; i++)
                    {
                        sql = "delete from cost_special_deal where cid = '" + spqform.gridView1.GetDataRow(spqform.gridView1.GetSelectedRows()[i]).ItemArray[0].ToString() + "'";
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
        public static void GetInfo(ref int id, ref string yyyymm,ref string taskname,ref int organizationid,ref int toorganizationid)
        {
            if (spqform == null || spqform.IsDisposed)
            {
                MessageBox.Show("没有选中要修改的记录！");
            }
            else if (spqform.gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中要修改的记录！");
            }
            else
            {
                id =Convert.ToInt32(Common.IsNull(spqform.gridView1.GetDataRow(spqform.gridView1.GetSelectedRows()[0]).ItemArray[0].ToString()));
                yyyymm = spqform.gridView1.GetDataRow(spqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString();
                taskname = spqform.gridView1.GetDataRow(spqform.gridView1.GetSelectedRows()[0]).ItemArray[2].ToString();
                organizationid = Convert.ToInt32(Common.IsNull(spqform.gridView1.GetDataRow(spqform.gridView1.GetSelectedRows()[0]).ItemArray[3].ToString()));
                toorganizationid = Convert.ToInt32(Common.IsNull(spqform.gridView1.GetDataRow(spqform.gridView1.GetSelectedRows()[0]).ItemArray[5].ToString()));
            }
        }
        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql,yyyymm;
            yyyymm = dateTimePicker1.Text.ToString(); 
            strsql = "select i.cid,i.yyyymm 年月,i.task_name 工单包含,j.cid,j.code 目前属于组织,j2.cid,j2.code 归集到组织 from cost_special_deal i left join cost_organization j on i.organization_id = j.cid left join cost_organization j2 on i.to_organization_id = j2.cid where i.yyyymm = '" + yyyymm + "'";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[3].Visible = false;
            gridView1.Columns[5].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[4].OptionsColumn.ReadOnly = true;
            gridView1.Columns[5].OptionsColumn.ReadOnly = true;
            gridView1.Columns[6].OptionsColumn.ReadOnly = true;
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

        private void SpecialDealQuery_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
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