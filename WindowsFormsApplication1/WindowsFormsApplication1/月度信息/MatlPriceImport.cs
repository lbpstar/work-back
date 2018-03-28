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
using System.Data.OleDb;
using System.Data.SqlClient;

namespace SMTCost
{
    public partial class MatlPriceImport : DevExpress.XtraEditors.XtraForm
    {
        public delegate bool MethodCaller(string file,string sheet);
        public MatlPriceImport()
        {
            InitializeComponent();
        }
        private static MatlPriceImport weqform = null;

        public static MatlPriceImport GetInstance()
        {
            if (weqform == null || weqform.IsDisposed)
            {
                weqform = new MatlPriceImport();
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

        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            string begin_date, end_date;
            begin_date = Common.FirstDayOfMonth(dateTimePickerMonth.Value).ToString("d");
            end_date = Common.LastDayOfMonth(dateTimePickerMonth.Value).ToString("d");
            strsql = "select MATL_NO 料号,MATL_NAME 物料名称,PRICE 单价,BEGIN_DATE 价格开始日期,END_DATE 价格结束日期 from COST_MATL_PRICE where not (begin_date > '" + end_date + "' or end_date <'" + begin_date + "')";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[4].OptionsColumn.ReadOnly = true;
            conn.Close();
        }
        private void showDetail(DataSet ds)
        {
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[4].OptionsColumn.ReadOnly = true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            showDetail();
        }

        private void simpleButton导入_Click(object sender, EventArgs e)
        {
            simpleButton导入.Enabled = false;
            simpleButton清空.Enabled = false;
            ConnDB conn = new ConnDB();
            string file = textEditFile.Text.ToString().Trim();
            string sheet = comboBoxSheet.Text.ToString();
            //string sql = "select * from COST_HH_STANDARD_HOURS where cmonth = '" + dateTimePickerMonth.Text + "'";
            //int rows = conn.ReturnRecordCount(sql);
            //if (rows > 0)
            //{
            //    MessageBox.Show("该月标工已经存在，要重新导入，请先清空该月数据！");
            //    simpleButton导入.Enabled = true;
            //    simpleButton清空.Enabled = true;
            //}
            //else 
            if (file == "")
            {
                MessageBox.Show("没有选择文件", "提示信息", MessageBoxButtons.OK);
                simpleButton导入.Enabled = true;
                simpleButton清空.Enabled = true;

            }
            else if (sheet == "")
            {
                MessageBox.Show("请选择EXCEL表", "提示信息", MessageBoxButtons.OK);
                simpleButton导入.Enabled = true;
                simpleButton清空.Enabled = true;
            }
            else
            {
                MethodCaller mc = new MethodCaller(Import);
                IAsyncResult result = mc.BeginInvoke(file, sheet, AsyncShowDetail, mc);
            }

        }
        private bool Import(string file, string sheet)
        {
            ConnDB conn = new ConnDB();
            bool success = true;
            int i= 0;
            try
            {
                DataTable dt;
                dt = GetDataFromExcel(file, sheet);
                i=InsertData(dt);
            }
            catch
            {
                MessageBox.Show("失败！");
                success = false;
            }
            if (success && i > 0)
            {
                MessageBox.Show("导入成功！");
            }
            conn.Close();
            return success;
        }
        /// <summary>
        /// 从System.Data.DataTable导入数据到数据库
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private int InsertData(DataTable dt)
        {
            int i = 0;
            string cmonth = dateTimePickerMonth.Text;
            string mat_code = "",mat_name = "";
            string sql, sql2,msg;
            decimal price = 0;
            DateTime begin_date, end_date;
            ConnDB conn = new ConnDB();
            DataSet ds;
            bool err = false;

            foreach (DataRow dr in dt.Rows)
            {
                sql2 = "select price from COST_MATL_PRICE where matl_no ='" + dr["料号"].ToString().Trim() + "' and not (begin_date > '" + dr["价格结束日期"].ToString().Trim() + "' or end_date <'" + dr["价格开始日期"].ToString().Trim() + "')";
                //rows = conn.ReturnRecordCount(sql2);
                ds = conn.ReturnDataSet(sql2);
                if (dr["料号"].ToString().Trim() == null || dr["料号"].ToString().Trim() == "")
                {
                    MessageBox.Show("料号格式有错误！");
                    err = true;
                    break;
                }
                else if(dr["价格开始日期"].ToString().Trim() == null || dr["价格开始日期"].ToString().Trim() == "")
                {
                    MessageBox.Show("价格开始日期格式有错误！");
                    err = true;
                    break;
                }
                else if (dr["价格结束日期"].ToString().Trim() == null || dr["价格结束日期"].ToString().Trim() == "")
                {
                    MessageBox.Show("价格结束日期格式有错误！");
                    err = true;
                    break;
                }
                else if(ds.Tables[0].Rows.Count >0)
                {
                    msg = dr["料号"].ToString().Trim() + "已存在重叠时间范围的价格信息！";
                    MessageBox.Show(msg);
                    showDetail(ds);
                    err = true;
                    break;
                }
            }
            if(!err)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    mat_code = dr["料号"].ToString().Trim();
                    mat_name = dr["物料名称"].ToString().Trim();
                    price = Common.StrToDecimal(dr["单价"].ToString().Trim());
                    begin_date = Convert.ToDateTime(dr["价格开始日期"].ToString().Trim());
                    end_date = Convert.ToDateTime(dr["价格结束日期"].ToString().Trim());
                    sql = string.Format("Insert into COST_MATL_PRICE(MATL_NO,MATL_NAME,PRICE,BEGIN_DATE,END_DATE) Values ('{0}','{1}','{2}','{3}',{4})",  mat_code,mat_name,price,begin_date,end_date);
                    conn.EditDatabase(sql);
                    i++;
                }
            }
            
            return i;
        }
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
                    DateTime begin_date, end_date;
                    begin_date = Common.FirstDayOfMonth(dateTimePickerMonth.Value);
                    end_date = Common.LastDayOfMonth(dateTimePickerMonth.Value);
                    strsql = "select MATL_NO 料号,MATL_NAME 物料名称,PRICE 单价,BEGIN_DATE 价格开始日期,END_DATE 价格结束日期 from COST_MATL_PRICE where not (begin_date > '" + end_date + "' or end_date <'" + begin_date + "')";
                    DataSet ds = conn.ReturnDataSet(strsql);

                    Action<DataSet> action = (data) =>
                    {
                        gridControl1.DataSource = data.Tables[0].DefaultView;
                        gridView1.Columns[0].Visible = false;
                        gridView1.Columns[0].OptionsColumn.ReadOnly = true;
                        gridView1.Columns[1].OptionsColumn.ReadOnly = true;
                        gridView1.Columns[2].OptionsColumn.ReadOnly = true;
                        gridView1.Columns[3].OptionsColumn.ReadOnly = true;
                        gridView1.Columns[4].OptionsColumn.ReadOnly = true;
                        simpleButton导入.Enabled = true;
                        simpleButton清空.Enabled = true;
                    };
                    Invoke(action, ds);
                    conn.Close();
                }

            }
        }
        private void GetExcelSheetName(String fileName)
        {
            try
            {
                string connectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\";", fileName);
                OleDbConnection conn = new OleDbConnection(connectionString);
                string name;
                conn.Open();
                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                for (int i = 0; i < schemaTable.Rows.Count; i++)
                {
                    name = schemaTable.Rows[i]["TABLE_NAME"].ToString();
                    comboBoxSheet.Items.Insert(i, name);
                    //comboBox2.DataSource = comboBox2.Items;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        /// <summary>
        /// 获取指定 Excel 文件中工作表的数据
        /// </summary>
        /// <param name="fileName">Excel 的文件名</param>
        /// <returns></returns>
        private DataTable GetDataFromExcel(String fileName,String sheet)
        {
            if (!String.IsNullOrEmpty(sheet))
            {
                string commandText = String.Format("SELECT 料号,物料名称,单价,价格开始日期,价格结束日期 FROM [{0}] where ltrim(rtrim('料号')) <> ''", sheet);
                return this.ExecuteDataTable(fileName, commandText);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取指定 Excel 文件中工作表的数据。
        /// </summary>
        /// <param name="fileName">Excel 的文件名</param>
        /// <param name="commandText">查询 SQL </param>
        private DataTable ExecuteDataTable(String fileName, String commandText)
        {
            string connectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\";", fileName);
            using (OleDbDataAdapter da = new OleDbDataAdapter(commandText, connectionString))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
        }

        private void simpleButton查询_Click(object sender, EventArgs e)
        {
            showDetail();
        }

        private void simpleButtonchose_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Excel文件(*.xlsx)|*.xlsx|Excel文件(*.xls)|*.xls";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textEditFile.Text = openFileDialog1.FileName.ToString();
            }

            if (textEditFile.Text.ToString() != "")
            {
                comboBoxSheet.Items.Clear();
                GetExcelSheetName(textEditFile.Text.ToString());
            }
        }

        private void simpleButton清空_Click(object sender, EventArgs e)
        {
            Delete();
        }
        private void Delete()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            bool isok=false;
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                strsql = "delete from cost_matl_price where matl_no = '" + gridView1.GetDataRow(gridView1.GetSelectedRows()[i]).ItemArray[0].ToString() + "'";
                isok = conn.EditDatabase(strsql);
            }
            if (isok)
            {
                MessageBox.Show("已删除选定行！");
                showDetail();
            }
            else
            {
                MessageBox.Show("失败！");
            }

            conn.Close();

            
        }
        private void MatlPriceImport_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            showDetail();
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            this.dateTimePickerMonth.Value = startMonth;

            dateTimePickerMonth.Focus();
            SendKeys.Send("{RIGHT} ");
        }
    }
}