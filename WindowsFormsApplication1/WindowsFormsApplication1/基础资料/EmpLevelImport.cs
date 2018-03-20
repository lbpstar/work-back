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
    public partial class EmpLevelImport : DevExpress.XtraEditors.XtraForm
    {
        public delegate bool MethodCaller(string file,string sheet);
        public EmpLevelImport()
        {
            InitializeComponent();
        }
        private static EmpLevelImport weqform = null;

        public static EmpLevelImport GetInstance()
        {
            if (weqform == null || weqform.IsDisposed)
            {
                weqform = new EmpLevelImport();
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
            strsql = "select employee_id_ 工号,name_ 姓名,DEPARTMENT_ 部门,员工等级 = case when cast(isnull(e_band,'0') as int)>0 then '*' else '' end  from OPENQUERY (BARCODE, 'SELECT employee_id_ ,name_ ,DEPARTMENT_ ,e_band FROM IHPS_ID_USER_PROFILE where department_ like ''制造中心%''')";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            //gridView1.Columns[0].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            conn.Close();
        }
        private void showDetailNull()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            strsql = "select employee_id_ 工号,name_ 姓名,DEPARTMENT_ 部门,e_band 员工等级  from OPENQUERY (BARCODE, 'SELECT employee_id_ ,name_ ,DEPARTMENT_ ,e_band FROM IHPS_ID_USER_PROFILE where isnull(e_band,'''') ='''' and department_ like ''制造中心%''')";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            //gridView1.Columns[0].Visible = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            conn.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            showDetail();
        }

        private void simpleButton导入_Click(object sender, EventArgs e)
        {
            simpleButton导入.Enabled = false;
            ConnDB conn = new ConnDB();
            string file = textEditFile.Text.ToString().Trim();
            string sheet = comboBoxSheet.Text.ToString(); 
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
                Clear();
                labeMsg.Text = "正在导入，请等待";
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
            string cno = "";
            string cname = "";
            string person_level = "";
            ConnDB conn = new ConnDB();
            bool err = false;
            foreach (DataRow dr in dt.Rows)
            {
                if(dr["工号"].ToString().Trim() == null || dr["工号"].ToString().Trim() == "")
                {
                    MessageBox.Show("工号格式有错误！");
                    err = true;
                    break;
                }
            }
            if(!err)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    cno = dr["工号"].ToString().Trim();
                    cname = dr["姓名"].ToString().Trim();
                    person_level = dr["职级"].ToString().Trim();
                    if(person_level == "操作" || person_level == "操作族")
                    {
                        person_level = "4";
                    }
                    string sql = string.Format("Insert into COST_DIRECT_LABOUR(CNO,CNAME,PERSON_LEVEL) Values ('{0}','{1}','{2}')", cno, cname, person_level);
                    conn.EditDatabase(sql);
                    i++;
                }
                //更新到IHPS_ID_USER_PROFILE,直接执行sql语句，提示分布式错误
                //string strsql = "update i set i.e_band = d.PERSON_LEVEL  from OPENQUERY (BARCODE, 'SELECT employee_id_,e_band FROM IHPS_ID_USER_PROFILE where department_ like ''制造中心%''') i left join COST_DIRECT_LABOUR d on i.employee_id_ = d.CNO";
                IDataParameter[] parameters = new IDataParameter[] { };
                conn.RunProcedure("COST_UPDATE_USER_LEVEL", parameters, out i);
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
                    strsql = "select employee_id_ 工号,name_ 姓名,DEPARTMENT_ 部门,员工等级 = case when cast(isnull(e_band,'0') as int)>0 then '*' else '' end from OPENQUERY (BARCODE, 'SELECT employee_id_ ,name_ ,DEPARTMENT_ ,e_band FROM IHPS_ID_USER_PROFILE where department_ like ''制造中心%''')";
                    DataSet ds = conn.ReturnDataSet(strsql);

                    Action<DataSet> action = (data) =>
                    {
                        gridControl1.DataSource = data.Tables[0].DefaultView;
                        //gridView1.Columns[0].Visible = false;
                        gridView1.Columns[0].OptionsColumn.ReadOnly = true;
                        gridView1.Columns[1].OptionsColumn.ReadOnly = true;
                        gridView1.Columns[2].OptionsColumn.ReadOnly = true;
                        gridView1.Columns[3].OptionsColumn.ReadOnly = true;
                        simpleButton导入.Enabled = true;
                        labeMsg.Text = "";
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
                string commandText = String.Format("SELECT 工号,姓名,职级 FROM [{0}] where ltrim(rtrim('工号')) <> '' or ltrim(rtrim('姓名')) <> ''", sheet);
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
            showDetailNull();
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
            Clear();
        }
        private void Clear()
        {
            ConnDB conn = new ConnDB();
            string strsql;
            strsql = "delete from COST_DIRECT_LABOUR";
            conn.EditDatabase(strsql);
            conn.Close();
        }

        private void EmpLevelImport_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            showDetail();
            this.Height = ParentForm.Height;
            this.Width = ParentForm.Width;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
        }

        private void simpleButtonAll_Click(object sender, EventArgs e)
        {
            showDetail();
        }
    }
}