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

namespace SMTCost
{
    public partial class TempEmpTravelExpenseQuery : DevExpress.XtraEditors.XtraForm
    {
        public delegate bool MethodCaller(string file, string sheet);
        public TempEmpTravelExpenseQuery()
        {
            InitializeComponent();
        }
        private static TempEmpTravelExpenseQuery weqform = null;

        public static TempEmpTravelExpenseQuery GetInstance()
        {
            if (weqform == null || weqform.IsDisposed)
            {
                weqform = new TempEmpTravelExpenseQuery();
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
                        sql = "delete from  COST_EXPENSE where cid = '" + weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[i]).ItemArray[0].ToString() + "'";
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
        public static void GetInfo(ref string id,ref string empno, ref string name, ref string begin_date,ref string expense)
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
                empno = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[1].ToString();
                name = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[2].ToString();
                begin_date = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[3].ToString();
                expense = weqform.gridView1.GetDataRow(weqform.gridView1.GetSelectedRows()[0]).ItemArray[4].ToString();
            }
        }
        private void showDetail()
        {
            ConnDB conn = new ConnDB();
            string strsql, yyyymm;
            yyyymm = dateTimePickerBegin.Text.ToString();
            strsql = "select cid,e.emp_no 员工工号,t.cname 姓名,begin_date 日期,expense '交通补贴（元）' from COST_EXPENSE  e left join COST_TEMP_EMPLOYEE t on e.emp_no = t.cno where ctype = 2 and begin_date <= '" + dateTimePickerEnd.Text + "' and begin_date >='" + dateTimePickerBegin.Text + "'";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[4].OptionsColumn.ReadOnly = true;

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
            this.dateTimePickerBegin.Value = startMonth.AddMonths(-1).AddDays(25);
            //dateTimePickerBegin.Focus();
            //SendKeys.Send("{RIGHT} ");
            showDetail();
        }
        private bool Import(string file, string sheet)
        {
            ConnDB conn = new ConnDB();
            bool success = true;
            int i = 0;
            try
            {
                DataTable dt;
                dt = GetDataFromExcel(file, sheet);
                i = InsertData(dt);
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
            string sql,empno = "", month = "",d26 ="0", d27 = "0", d28 = "0", d29 = "0", d30 = "0", d31 = "0", d1 = "0", d2 = "0", d3 = "0", d4 = "0", d5 = "0", d6 = "0", d7 = "0", d8 = "0", d9 = "0", d10 = "0", d11 = "0", d12 = "0", d13 = "0", d14 = "0", d15 = "0", d16 = "0", d17 = "0", d18 = "0", d19 = "0", d20 = "0", d21 = "0", d22 = "0", d23 = "0", d24 = "0", d25 = "0";
            ConnDB conn = new ConnDB();
            sql = "delete from cost_expense_temp";
            conn.EditDatabase(sql);
            bool err = false;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["员工工号"].ToString().Trim() == null || dr["员工工号"].ToString().Trim() == "")
                {
                    MessageBox.Show("员工工号格式有错误！");
                    err = true;
                    break;
                }
            }
            if (!err)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    empno = dr["员工工号"].ToString().Trim();
                    month = dr["月份"].ToString().Trim();
                    //date = dr["日期"].ToString().Trim();
                    //expense = dr["交通补贴"].ToString().Trim();
                    d26 = dr["26"].ToString().Trim();
                    d27 = dr["27"].ToString().Trim();
                    d28 = dr["28"].ToString().Trim();
                    d29 = dr["29"].ToString().Trim();
                    d30 = dr["30"].ToString().Trim();
                    d31 = dr["31"].ToString().Trim();
                    d1 = dr["1"].ToString().Trim();
                    d2 = dr["2"].ToString().Trim();
                    d3 = dr["3"].ToString().Trim();
                    d4 = dr["4"].ToString().Trim();
                    d5 = dr["5"].ToString().Trim();
                    d6 = dr["6"].ToString().Trim();
                    d7 = dr["7"].ToString().Trim();
                    d8 = dr["8"].ToString().Trim();
                    d9 = dr["9"].ToString().Trim();
                    d10 = dr["10"].ToString().Trim();
                    d11 = dr["11"].ToString().Trim();
                    d12 = dr["12"].ToString().Trim();
                    d13 = dr["13"].ToString().Trim();
                    d14 = dr["14"].ToString().Trim();
                    d15 = dr["15"].ToString().Trim();
                    d16 = dr["16"].ToString().Trim();
                    d17 = dr["17"].ToString().Trim();
                    d18 = dr["18"].ToString().Trim();
                    d19 = dr["19"].ToString().Trim();
                    d20 = dr["20"].ToString().Trim();
                    d21 = dr["21"].ToString().Trim();
                    d22 = dr["22"].ToString().Trim();
                    d23 = dr["23"].ToString().Trim();
                    d24 = dr["24"].ToString().Trim();
                    d25 = dr["25"].ToString().Trim();
                    sql = string.Format("Insert into COST_EXPENSE_TEMP Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}')", empno, month, d26, d27, d28,d29,d30,d31,d1,d2,d3,d4,d5,d6,d7,d8,d9,d10,d11,d12,d13,d14,d15,d16,d17,d18,d19,d20,d21,d22,d23,d24,d25);
                    conn.EditDatabase(sql);
                    i++;
                }
                sql = "insert into COST_EXPENSE(CTYPE,TYPE_DES,BEGIN_DATE,EMP_NO,EXPENSE) select 2,'交通补贴',cdate,emp_no,MAX(expense) expense from (SELECT emp_no,case when cdate >25 then convert(varchar(10),dateadd(month,-1,cmonth+'-'+cdate),120)else cmonth+'-'+cdate end as cdate,expense ";
                sql += "FROM cost_expense_temp UNPIVOT (expense FOR cdate IN ([26],[27],[28],[29],[30],[31],[01],[02],[03],[04],[05],[06],[07],[08],[09],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25]) ) P) t group by emp_no,cdate";
                conn.EditDatabase(sql);
                sql = "delete from cost_expense_temp";
                conn.EditDatabase(sql);
            }
            
            conn.Close();
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
                    strsql = "select cid,e.emp_no 员工工号,t.cname 姓名,begin_date 日期,expense '交通补贴（元）' from COST_EXPENSE  e left join COST_TEMP_EMPLOYEE t on e.emp_no = t.cno where ctype = 2 and begin_date <= '" + dateTimePickerEnd.Text + "' and begin_date >='" + dateTimePickerBegin.Text + "'";
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
                    comboBoxEditSheet.Properties.Items.Insert(i, name);
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
        private DataTable GetDataFromExcel(String fileName, String sheet)
        {
            if (!String.IsNullOrEmpty(sheet))
            {
                //string commandText = String.Format("SELECT 员工工号,月份 ,日期,交通补贴 FROM  ( SELECT 员工工号,月份,[26], [27],[28],[29],[30],[31],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25] FROM [{0}] where ltrim(rtrim(员工工号)) <> '' and 员工工号 is not null) T UNPIVOT ( 交通补贴 FOR 日期 IN ([26], [27],[28],[29],[30],[31],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25]) ) P", sheet);
                string commandText = String.Format("SELECT 员工工号,月份,[26], [27],[28],[29],[30],[31],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25] FROM [{0}] where ltrim(rtrim(员工工号)) <> '' and 员工工号 is not null", sheet);
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

        private void simpleButton选择_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Excel文件(*.xlsx)|*.xlsx|Excel文件(*.xls)|*.xls";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textEditFile.Text = openFileDialog1.FileName.ToString();
            }

            if (textEditFile.Text.ToString() != "")
            {
                comboBoxEditSheet.Properties.Items.Clear();
                GetExcelSheetName(textEditFile.Text.ToString());
            }
        }

        private void simpleButton导入_Click(object sender, EventArgs e)
        {
            simpleButton导入.Enabled = false;
            ConnDB conn = new ConnDB();
            string file = textEditFile.Text.ToString().Trim();
            string sheet = comboBoxEditSheet.Text.ToString();
            if (file == "")
            {
                MessageBox.Show("没有选择文件", "提示信息", MessageBoxButtons.OK);
                simpleButton导入.Enabled = true;

            }
            else if (sheet == "")
            {
                MessageBox.Show("请选择EXCEL表", "提示信息", MessageBoxButtons.OK);
                simpleButton导入.Enabled = true;
            }
            else
            {
                MethodCaller mc = new MethodCaller(Import);
                IAsyncResult result = mc.BeginInvoke(file, sheet, AsyncShowDetail, mc);
            }
        }
    }
}