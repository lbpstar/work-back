﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Configuration;
using System.Threading;

namespace SMTCost
{
    public partial class HHImport : DevExpress.XtraEditors.XtraForm
    {
        public delegate bool MethodCaller();
        public HHImport()
        {
            InitializeComponent();
        }
        private static HHImport ilaform = null;

        public static HHImport GetInstance()
        {
            if (ilaform == null || ilaform.IsDisposed)
            {
                ilaform = new HHImport();
            }
            return ilaform;
        }
        private void simpleButtonQuery_Click(object sender, EventArgs e)
        {
            showDetail();
        }
        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            strsql = "select cdate 日期,hours HH产出工时 from COST_EMS_HH_HOURS where cdate like '" + dateTimePickerMonth.Text + "%'";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            //gridView1.Columns[0].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            conn.Close();
        }
        private bool Import()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            string month = dateTimePickerMonth.Text;
            int rows,i;
            bool success = true;
            strsql = "select cdate from COST_EMS_HH_HOURS where cdate like '" + dateTimePickerMonth.Text + "%'";
            IDataParameter[] parameters = new IDataParameter[] { new SqlParameter("@cmonth", month),new SqlParameter("@type", 2) };

            rows = conn.ReturnRecordCount(strsql);
            if (rows > 0)
            {
                MessageBox.Show("该月HH产出工时已经存在，要重新导入，请先清空该月数据！");
            }
            else
            {
                try
                {
                    conn.RunProcedure("COST_PRODUCT_IMPORT", parameters, out i);
                }
                catch
                {
                    MessageBox.Show("失败！");
                    success = false;
                }
                if (success)
                {
                    MessageBox.Show("导入成功！");
                    //showDetail();
                }
            }
            conn.Close();
            return success;      
        }
        private void Clear()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            strsql = "delete from COST_EMS_HH_HOURS where  cdate like '" + dateTimePickerMonth.Text + "%'";
            bool isok = conn.EditDatabase(strsql);
            if (isok)
            {
                MessageBox.Show("该月HH产出工时已成功清空！");
                showDetail();
            }
            else
            {
                MessageBox.Show("失败！");
            }
            
            conn.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {    
            this.Close();
        }

        private void simpleButtonImport_Click(object sender, EventArgs e)
        {
            //Thread th = new Thread(Import);
            //th.IsBackground = true;
            //th.Start();  
            simpleButtonImport.Enabled = false;
            simpleButtonClear.Enabled = false;
            MethodCaller mc = new MethodCaller(Import);
            IAsyncResult result = mc.BeginInvoke(AsyncShowDetail, mc);
        }
        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="result"></param>
        private void AsyncShowDetail(IAsyncResult result)
        {
            MethodCaller aysnDelegate = result.AsyncState as MethodCaller;
            if (aysnDelegate != null)
            {
                bool success = aysnDelegate.EndInvoke(result);
                if (success)
                {
                    ConnDB conn = new ConnDB();
                    string strsql;
                    strsql = "select cdate 日期,hours HH产出工时 from COST_EMS_HH_HOURS where cdate like '" + dateTimePickerMonth.Text + "%'";
                    DataSet ds = conn.ReturnDataSet(strsql);
                   
                    Action<DataSet> action = (data) =>
                     {
                         gridControl1.DataSource = data.Tables[0].DefaultView;
                         //gridView1.Columns[0].Visible = false;
                         gridView1.Columns[0].OptionsColumn.ReadOnly = true;
                         gridView1.Columns[1].OptionsColumn.ReadOnly = true;
                         simpleButtonImport.Enabled = true;
                         simpleButtonClear.Enabled = true;
                     };
                    Invoke(action, ds);
                    conn.Close();
                }
            }      
        }
        private void simpleButtonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void dateTimePickerMonth_ValueChanged(object sender, EventArgs e)
        {
            showDetail();
        }

        private void ProductQuantityImport_Load(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePickerMonth.Value = startMonth;
            showDetail();
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;

            dateTimePickerMonth.Focus();
            SendKeys.Send("{RIGHT} ");
        }
    }
}