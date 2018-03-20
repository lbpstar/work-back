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
    public partial class TempEmpImport : DevExpress.XtraEditors.XtraForm
    {
        public delegate bool MethodCaller(string file,string sheet);
        public TempEmpImport()
        {
            InitializeComponent();
        }
        private static TempEmpImport weqform = null;

        public static TempEmpImport GetInstance()
        {
            if (weqform == null || weqform.IsDisposed)
            {
                weqform = new TempEmpImport();
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
            strsql = "select CNO 临时工号,e.CNAME 姓名,SEX 性别,REGISTER_DATE 报到日期,CFROM 输送渠道,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,DEPT 部门,ID_NUMBER 身份证号,phone_no 手机号码,shift 班次,STATUS 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where status = '在职'";
            DataSet ds = conn.ReturnDataSet(strsql);
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[6].Visible = false;
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
            gridView1.Columns[11].OptionsColumn.ReadOnly = true;
            gridView1.Columns[12].OptionsColumn.ReadOnly = true;
            gridView1.Columns[13].OptionsColumn.ReadOnly = true;
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
            string cno = "", cname = "",sex = "", register_date = "",cfrom = "", dept1 = "", dept2 = "", dept3 = "", dept = "",id_number = "",phone_no = "",shift = "";
            int fromtype;
            //DateTime register_date;
            ConnDB conn = new ConnDB();
            bool err = false;
            foreach (DataRow dr in dt.Rows)
            {
                if(dr["姓名"].ToString().Trim() == null || dr["姓名"].ToString().Trim() == "")
                {
                    MessageBox.Show("姓名格式有错误！");
                    err = true;
                    break;
                }
                string sql = "select * from COST_SHIFT where cname = '" + dr["班次"].ToString().Trim() + "'";
                int count = conn.ReturnRecordCount(sql);
                if(count == 0)
                {
                    string msg = "班次：" + dr["班次"].ToString().Trim() + "，与考勤班次中现有的班次格式不一致！";
                    MessageBox.Show(msg);
                    err = true;
                    break;
                }
            }
            if(!err)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    cno = GenerateNo();
                    cname = dr["姓名"].ToString().Trim();
                    sex = dr["性别"].ToString().Trim();
                    register_date = dr["报到日期"].ToString().Trim();
                    cfrom = dr["输送渠道"].ToString().Trim();
                    dept1 = "制造中心";
                    dept2 = dr["二级部门"].ToString().Trim();
                    dept3 = dr["三级部门"].ToString().Trim();
                    dept = dr["部门"].ToString().Trim();
                    id_number = dr["身份证号"].ToString().Trim();
                    phone_no = dr["手机号码"].ToString().Trim();
                    shift = dr["班次"].ToString().Trim();
                    string sqltype = "select isnull(sub_id,0) from cost_base_data where module_id =1 and cname = '" + dr["输送类型"].ToString().Trim() + "'";
                    DataSet ds = conn.ReturnDataSet(sqltype);
                    fromtype = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    string strsql = "select * from cost_temp_employee where cno = '" + cno + "'";
                    int j = conn.ReturnRecordCount(strsql);
                    if(j==0)
                    {
                        string sql = string.Format("Insert into COST_TEMP_EMPLOYEE(CNO,CNAME,SEX,REGISTER_DATE,CFROM,from_type,dept1,dept2,dept3,DEPT,ID_NUMBER,phone_no,shift,STATUS) Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", cno, cname, sex, register_date, cfrom, fromtype, dept1,dept2,dept3,dept, id_number,phone_no, shift,"在职");
                        conn.EditDatabase(sql);
                        i++;
                    }
                    else
                    {
                        string err2 = "工号" + id_number + "已经存在！";
                        MessageBox.Show(err2);
                    }
                    
                }
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
                    strsql = "select CNO 临时工号,e.CNAME 姓名,SEX 性别,REGISTER_DATE 报到日期,CFROM 输送渠道,b.cname 输送类型,dept1 一级部门,dept2 二级部门,dept3 三级部门,DEPT 部门,ID_NUMBER 身份证号,phone_no 手机号码,shift 班次,STATUS 状态 from COST_TEMP_EMPLOYEE e left join cost_base_data b on e.from_type = b.sub_id and module_id = 1 where status = '在职'";
                    DataSet ds = conn.ReturnDataSet(strsql);

                    Action<DataSet> action = (data) =>
                    {
                        gridControl1.DataSource = data.Tables[0].DefaultView;
                        gridView1.Columns[6].Visible = false;
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
                        gridView1.Columns[11].OptionsColumn.ReadOnly = true;
                        gridView1.Columns[12].OptionsColumn.ReadOnly = true;
                        gridView1.Columns[13].OptionsColumn.ReadOnly = true;
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
                string commandText = String.Format("SELECT  姓名,性别,报到日期,输送渠道,输送类型,二级部门,三级部门,部门,身份证号,手机号码,班次 FROM [{0}] where ltrim(rtrim(姓名)) <> '' and 姓名 is not null", sheet);
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
            Clear();
        }
        private void Clear()
        {
            //ConnDB conn = new ConnDB();
            //string strsql;
            //strsql = "delete from COST_DIRECT_LABOUR";
            //conn.EditDatabase(strsql);
            //conn.Close();
        }
        private string GenerateNo()
        {
            string maxno, strbegin, num, sql;
            int i;
            strbegin = DateTime.Now.ToString("yyyy");
            sql = "select maxno = max(cno) from COST_TEMP_EMPLOYEE where cno like '" + strbegin + "%'";
            ConnDB conn = new ConnDB();
            DataSet ds = conn.ReturnDataSet(sql);
            if (ds.Tables[0].Rows[0][0].ToString() != "")
            {
                maxno = ds.Tables[0].Rows[0][0].ToString();
                num = maxno.Substring(maxno.Length - 5, 5);
                i = Convert.ToInt32(num) + 1;
                return maxno.Substring(0, maxno.Length - 5) + i.ToString().PadLeft(5, '0');
            }
            else
            {
                return strbegin + "00001";
            }
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
    }
}