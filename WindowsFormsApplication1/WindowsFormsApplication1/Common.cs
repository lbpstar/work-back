using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Security.Cryptography;

namespace SMTCost
{
    class Common
    {
        public static void BasicDataBind(string tablename, System.Windows.Forms.ComboBox comboboxname)
        {
                ConnDB conn = new ConnDB();
                string sql = "select * from " + tablename + " where isnull(forbidden,'false') != 'true'";
                DataSet ds = conn.ReturnDataSet(sql);
                DataRow dr = ds.Tables[0].NewRow();
                dr[0] = "0";//其实这里也许设置为"null"更好
                dr[1] = "请选择";
                //插在第一位

                ds.Tables[0].Rows.InsertAt(dr, 0);
                comboboxname.DataSource = ds.Tables[0];
                comboboxname.DisplayMember = "CNAME";
                comboboxname.ValueMember = "CID";
                conn.Close();
            
        }
        public static string IsNull(string str)
        {
            if (str == null || str == "")
                str = "0";
            return str;
        }

        public static string IsZero(string i)
        {
            if (i == "0")
                i = "null";
            return i;
        }
        public static string IsEmpty(string i)
        {
            if (i == "")
                i = "NULL";
            return i;
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="strText">需加密的字符串</param>
        /// <returns></returns>
        public static string MD5Encrypt(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strText));
            //return System.Text.Encoding.Unicode.GetString(result);
            return System.Convert.ToBase64String(result);

        }
        //        public class PersonInfo
        //        {
        //            private string _firstName;
        //            private string _lastName;

        //            public PersonInfo(string firstName, string lastName)
        //            {
        //                _firstName = firstName;
        //                _lastName = lastName;
        //            }

        //            public override string ToString()
        //            {
        //                return _firstName + " " + _lastName;
        //            }
        //        }
        //        ComboBoxItemCollection coll = comboboxname.Properties.Items;
        //        coll.BeginUpdate();
        //            try
        //            {
        //                for(int i = 0; i<ds.Tables[0].Rows.Count;i++)
        //                {
        //                    coll.Add(new PersonInfo("Sven", "Petersen"));
        //                }
        //}
        //            finally
        //            {
        //                coll.EndUpdate();
        //            }
        //            comboboxname.SelectedIndex = -1;
        public static Decimal StrToDecimal(object DecimalString)
        {
            try
            {
                Decimal f = (Decimal)Convert.ToDecimal(DecimalString);
                return f;
            }
            catch (FormatException)
            {
                return (Decimal)0.00;
            }
        }
        public static bool DataSetToExcel(DataSet dataSet, bool isShowExcle)
        {
            DataTable dataTable = dataSet.Tables[0];
            int rowNumber = dataTable.Rows.Count;

            int rowIndex = 1;
            int colIndex = 0;


            if (rowNumber == 0)
            {
                return false;
            }

            //建立Excel对象
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(true);
            

            //生成字段名称
            foreach (DataColumn col in dataTable.Columns)
            {
                colIndex++;
                excel.Cells[1, colIndex] = col.ColumnName;
            }

            //填充数据
            foreach (DataRow row in dataTable.Rows)
            {
                rowIndex++;
                colIndex = 0;
                foreach (DataColumn col in dataTable.Columns)
                {
                    colIndex++;
                    excel.Cells[rowIndex, colIndex] = row[col.ColumnName];
                }
            }
            excel.Visible = isShowExcle;
            //列自动适应宽度
            excel.Columns.AutoFit();
            return true;
        }
    }
    
}
