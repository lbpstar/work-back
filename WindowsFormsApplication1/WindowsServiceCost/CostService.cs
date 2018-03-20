using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace WindowsServiceCost
{
    public partial class CostService : ServiceBase
    {
        System.Timers.Timer timer1;//计时器 
        public CostService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string month = DateTime.Now.ToString("yyyy-MM");
            DateTime begin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            if(DateTime.Now.ToString("yyyy-MM-dd")== begin.ToString("yyyy-MM-dd"))
            {
                month = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");
            }
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\costlog.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "获取数据.");
                ImportAttendance(month);
                ImportPointRaw(month);
                ImportPointSum(month);
                ImportHH(month);
                ImportProduct(month);
                UpdateDept();
                CopyData(month);
                CalculateSMT(month);
                CalculateEMS(month);
                CalculateASY(month);
                CalculateProduct(month);
                CalculateSys(month);
            }
            timer1 = new System.Timers.Timer();
            timer1.Interval = 1000;//执行时间间隔1s  
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(GetData);
            timer1.Enabled = true;
        }
        private void GetData(object sender, System.Timers.ElapsedEventArgs e)
        {
            string now = DateTime.Now.ToString("HH:mm:ss");
            string month = DateTime.Now.ToString("yyyy-MM");
            DateTime begin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            if (DateTime.Now.ToString("yyyy-MM-dd") == begin.ToString("yyyy-MM-dd"))
            {
                month = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");
            }
            if (now == "08:40:00")
            {
                ConnDB conn = new ConnDB();
                string strsql;
                int rows;
                strsql = "select * from COST_DIRECT_LABOUR_ATTENDANCE where cdate = convert(varchar(20),getdate()-1,23)";
                rows = conn.ReturnRecordCount(strsql);
                if (rows == 0)
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\costlog.txt", true))
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "获取数据.");
                        ImportAttendance(month);
                        ImportPointRaw(month);
                        ImportPointSum(month);
                        ImportHH(month);
                        ImportProduct(month);
                        UpdateDept();
                        CopyData(month);
                        CalculateSMT(month);
                        CalculateEMS(month);
                        CalculateASY(month);
                        CalculateProduct(month);
                        CalculateSys(month);
                    }
                }
                conn.Close();
            }
            else if (now == "08:50:00")
            {
                ConnDB conn = new ConnDB();
                string strsql;
                int rows;
                strsql = "select * from COST_DIRECT_LABOUR_ATTENDANCE where cdate = convert(varchar(20),getdate()-1,23)";
                rows = conn.ReturnRecordCount(strsql);
                if (rows == 0)
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\costlog.txt", true))
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "获取数据.");
                        ImportAttendance(month);
                        ImportPointRaw(month);
                        ImportPointSum(month);
                        ImportHH(month);
                        ImportProduct(month);
                        UpdateDept();
                        CopyData(month);
                        CalculateSMT(month);
                        CalculateEMS(month);
                        CalculateASY(month);
                        CalculateProduct(month);
                        CalculateSys(month);
                    }
                }
                conn.Close();     
            }

        }
        /// <summary>
        /// 导入考勤
        /// </summary>
        private void ImportAttendance(string month)
        {
            ConnDB conn = new ConnDB();
            string strsql,sqldel;
            int rows, i;
            //string month = DateTime.Now.ToString("yyyy-MM");
            //string month = "2017-10";
            strsql = "select * from COST_DIRECT_LABOUR_ATTENDANCE where cdate like '%" + month + "%'";
            sqldel = "delete from COST_DIRECT_LABOUR_ATTENDANCE where cdate like '%" + month + "%'";
            IDataParameter[] parameters = new IDataParameter[] { new SqlParameter("@cmonth", month) };

            rows = conn.ReturnRecordCount(strsql);
            if (rows > 0)
            {
                conn.EditDatabase(sqldel);
                conn.RunProcedure("COST_ATTENDANCE_IMPORT", parameters, out i);
            }
            else
            {
                conn.RunProcedure("COST_ATTENDANCE_IMPORT", parameters, out i);
            }
            conn.Close();
        }
        /// <summary>
        /// 拉别点数
        /// </summary>
        private void ImportPointRaw(string month)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2,sqldel;
            int rows;
            //string month = DateTime.Now.ToString("yyyy-MM");
            //string month = "2017-10";
            strsql = "insert into cost_pointcount(cdate,line_type_name,pointcount) select CONVERT(varchar(100), cdate, 23) as cdate,linetype,pointcount from openquery (LINKERPNEW, 'select 日期 as cdate, 生产线别 as linetype,sum(生产总点数) as pointcount from CUX_SMT_PROD_RP where  to_char(日期,''yyyy-mm'')=''" + month + "'' group by 日期, 生产线别 order by 日期')";
            strsql2 = "select * from cost_pointcount where cdate like '%" + month + "%'";
            sqldel = "delete from cost_pointcount where cdate like '%" + month + "%'";
            rows = conn.ReturnRecordCount(strsql2);
            if (rows > 0)
            {
                conn.EditDatabase(sqldel);
                conn.EditDatabase(strsql);
            }
            else
            {
                conn.EditDatabase(strsql);
            }
            conn.Close();
        }
        /// <summary>
        /// 营业类型点数
        /// </summary>
        private void ImportPointSum(string month)
        {
            ConnDB conn = new ConnDB();
            string strsql, strsql2,sqldel;
            int rows;
            //string month = DateTime.Now.ToString("yyyy-MM");
            //string month = "2017-10";
            strsql = "insert into cost_pointcount_sum(cdate, saletype_id, pointcount) ";
            strsql = strsql + "select CONVERT(varchar(100), cdate, 23) as cdate, saletypeid, sum(cast(生产总点数 as decimal(18, 2)))  pointcount from ";
            strsql = strsql + "(select cdate,工单号,saletype, 装配件描述, 生产总点数 =case when saletype = 'shl' and 工单号 like '%-ws' then cast(生产总点数 as decimal(18,2))*3.56 else 生产总点数 end, saletypeid = case when(saletype = 'HCL' AND CODE2 IS null and 装配件描述 not like '【HBTG专用%') OR CODE2 = 'HCL' then 2 else 10 end from ";
            strsql = strsql + "(select cdate,工单号,saletype, 装配件描述, 生产总点数 from openquery (LINKERPNEW, 'select 日期 as cdate,工单号, organization_code as saletype,装配件描述,生产总点数  from CUX_SMT_PROD_RP where (装配件描述  not like ''%F1专用%'' and   装配件描述 not like ''%F1 TB%'') and to_char(日期,''yyyy-mm'')=''" + month + "'' ')) i ";
            strsql = strsql + "left join (select d.*, o.CODE, O2.CODE CODE2 from COST_SPECIAL_DEAL d left join cost_organization o on d.ORGANIZATION_ID = o.CID left join cost_organization o2 on d.TO_ORGANIZATION_ID = o2.CID where d.YYYYMM = '" + month + "') j ";
            strsql = strsql + "on i.装配件描述 like '%' + j.TASK_NAME + '%' and i.saletype = j.CODE) t ";
            strsql = strsql + "group by cdate, saletypeid order by cdate";

            strsql2 = "select * from cost_pointcount_sum where cdate like '%" + month + "%'";
            sqldel = "delete from cost_pointcount_sum where cdate like '%" + month + "%'";
            rows = conn.ReturnRecordCount(strsql2);
            if (rows > 0)
            {
                conn.EditDatabase(sqldel);
                conn.EditDatabase(strsql);
            }
            else
            {
                conn.EditDatabase(strsql);
            }
            conn.Close();
        }
        /// <summary>
        /// 后焊工时
        /// </summary>
        private void ImportHH(string month)
        {
            ConnDB conn = new ConnDB();
            string strsql,sqldel;
            int rows, i;
            //string month = DateTime.Now.ToString("yyyy-MM");
            //string month = "2017-10";
            strsql = "select cdate from COST_EMS_HH_HOURS where cdate like '" + month + "%'";
            IDataParameter[] parameters = new IDataParameter[] { new SqlParameter("@cmonth", month), new SqlParameter("@type", 2) };
            sqldel = "delete from COST_EMS_HH_HOURS where  cdate like '" + month + "%'";
            rows = conn.ReturnRecordCount(strsql);
            if (rows > 0)
            {
                conn.EditDatabase(sqldel);
                conn.RunProcedure("COST_PRODUCT_IMPORT", parameters, out i);
            }
            else
            {
                conn.RunProcedure("COST_PRODUCT_IMPORT", parameters, out i);
            }
            conn.Close();
        }
        /// <summary>
        /// 终端台数
        /// </summary>
        private void ImportProduct(string month)
        {
            ConnDB conn = new ConnDB();
            string strsql,sqldel;
            int rows, i;
            //string month = DateTime.Now.ToString("yyyy-MM");
            //string month = "2017-10";
            strsql = "select cdate from cost_product_quantity where cdate like '" + month + "%'  and type = 1";
            IDataParameter[] parameters = new IDataParameter[] { new SqlParameter("@cmonth", month), new SqlParameter("@type", "1") };
            sqldel = "delete from cost_product_quantity where  cdate like '" + month + "%'  and type = 1";
            rows = conn.ReturnRecordCount(strsql);
            if (rows > 0)
            {
                conn.EditDatabase(sqldel);
                conn.RunProcedure("COST_PRODUCT_IMPORT", parameters, out i); 
            }
            else
            {
                conn.RunProcedure("COST_PRODUCT_IMPORT", parameters, out i);
            }
            conn.Close();
        }
        /// <summary>
        /// 更新部门
        /// </summary>
        private void UpdateDept()
        {
            ConnDB conn = new ConnDB();
            IDataParameter[] parameters = new IDataParameter[] {  };
            conn.RunProcedure("COST_DEPT_IMPORT", parameters);
            conn.Close();
        }
        /// <summary>
        /// smt主营成本计算
        /// </summary>
        private void CalculateSMT(string month)
        {
            ConnDB conn = new ConnDB();
            //string month = DateTime.Now.ToString("yyyy-MM");
            string sql;
            int i, rows;
            bool isok;
            isok = CheckData1(month);
            if (isok)
            {
                sql = "select * from COST_MONTH_CALCULATE where cmonth ='" + month + "' and sale_type_id =2";
                IDataParameter[] parameters = new IDataParameter[] { new SqlParameter("@cmonth", month), new SqlParameter("@saletypeid", "2") };

                rows = conn.ReturnRecordCount(sql);
                if (rows > 0)
                {
                    string sql1, sql2, sql3, sql4;
                    sql1 = "delete from COST_DAY_CALCULATE where cdate like '" + month + "%' and sale_type_id =2";
                    sql2 = "delete from COST_MONTH_CALCULATE where cmonth ='" + month + "' and sale_type_id =2";
                    sql3 = "delete from COST_DEPT_CALCULATE where cdate like '" + month + "%' and sale_type_id =2";
                    sql4 = "delete from COST_DEPT_MONTH_CALCULATE where cmonth ='" + month + "' and sale_type_id =2";
                    conn.EditDatabase(sql1);
                    conn.EditDatabase(sql2);
                    conn.EditDatabase(sql3);
                    conn.EditDatabase(sql4);
                    conn.RunProcedure("costing", parameters, out i);
                }
                else
                {  
                    conn.RunProcedure("costing", parameters, out i);
                }
            }

            conn.Close();
        }
        /// <summary>
        /// smt主营数据检查
        /// </summary>
        /// <returns></returns>

        private bool CheckData1(string month)
        {
            ConnDB conn = new ConnDB();
            string sql;
            string  yyyy = "";
            int quarter = 0;
            int count;
            GetQuarter(month, ref yyyy, ref quarter);
            sql = "select top 1 * from COST_DIRECT_LABOUR_ATTENDANCE where cdate like '" + month + "%'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }

            sql = "select top 1 * from cost_pointcount_sum where saletype_id = 2 and cdate like '" + month + "%'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from cost_depreciation where YYYYMM = '" + month + "' and SALE_TYPE_ID = 2";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_RENT_EXPENSE where YYYYMM = '" + month + "' and SALE_TYPE_ID = 2";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_WATER_ELECTRICITY where YYYYMM = '" + month + "' and SALE_TYPE_ID = 2";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_STANDARD_POINT where YYYYMM = '" + month + "' and SALE_TYPE_ID = 2";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_DEPT_STANDARD_POINT where YYYYMM = '" + month + "' and SALE_TYPE_ID = 2";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }

            sql = "select top 1 * from COST_DIRECT_LABOUR_PRICE where YYYYMM = '" + month + "'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_INDIRECT_LABOUR_LEVEL_PRICE where YYYYMM = '" + month + "'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from cost_rate where yyyy ='" + yyyy + "' and isnull(dept_id,0) >0 and quarter_id = " + quarter + " and sale_type_id = 15";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_RATE where yyyy ='" + yyyy + "' and isnull(dept_id,0) =0 and QUARTER_ID = " + quarter + " and sale_type_id = 15";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            conn.Close();
            return true;

        }
        /// <summary>
        /// EMS成本计算
        /// </summary>
        private void CalculateEMS(string month)
        {
            ConnDB conn = new ConnDB();
            //string month = DateTime.Now.ToString("yyyy-MM");
            string sql;
            int i, rows;
            bool isok;
            isok = CheckData2(month);
            if (isok)
            {
                sql = "select * from COST_MONTH_CALCULATE where cmonth ='" + month + "' and sale_type_id = 10";
                IDataParameter[] parameters = new IDataParameter[] { new SqlParameter("@cmonth", month), new SqlParameter("@saletypeid", "10") };

                rows = conn.ReturnRecordCount(sql);
                if (rows > 0)
                {
                    string sql1, sql2, sql3, sql4;
                    sql1 = "delete from COST_DAY_CALCULATE where cdate like '" + month + "%' and sale_type_id = 10";
                    sql2 = "delete from COST_MONTH_CALCULATE where cmonth ='" + month + "' and sale_type_id = 10";
                    sql3 = "delete from COST_DEPT_CALCULATE where cdate like '" + month + "%' and sale_type_id = 10";
                    sql4 = "delete from COST_DEPT_MONTH_CALCULATE where cmonth ='" + month + "' and sale_type_id = 10";
                    conn.EditDatabase(sql1);
                    conn.EditDatabase(sql2);
                    conn.EditDatabase(sql3);
                    conn.EditDatabase(sql4);
                    conn.RunProcedure("costing", parameters, out i);
                }
                else
                {
                    conn.RunProcedure("costing", parameters, out i);
                }
            }

            conn.Close();
        }
        /// <summary>
        /// ems数据检查
        /// </summary>
        /// <returns></returns>
        private bool CheckData2(string month)
        {
            ConnDB conn = new ConnDB();
            string sql;
            string yyyy = "";
            int quarter = 0;
            int count;
            GetQuarter(month, ref yyyy, ref quarter);
            sql = "select top 1 * from COST_DIRECT_LABOUR_ATTENDANCE where cdate like '" + month + "%'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }

            sql = "select top 1 * from cost_pointcount_sum where saletype_id = 10 and cdate like '" + month + "%'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from cost_depreciation where YYYYMM = '" + month + "' and SALE_TYPE_ID = 10";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_RENT_EXPENSE where YYYYMM = '" + month + "' and SALE_TYPE_ID = 10";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_WATER_ELECTRICITY where YYYYMM = '" + month + "' and SALE_TYPE_ID = 10";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_STANDARD_POINT where YYYYMM = '" + month + "' and SALE_TYPE_ID = 10";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_DEPT_STANDARD_POINT where YYYYMM = '" + month + "' and SALE_TYPE_ID = 10";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_STEEL_NET_RATE where YYYYMM = '" + month + "'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_DIRECT_LABOUR_PRICE where YYYYMM = '" + month + "'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_INDIRECT_LABOUR_LEVEL_PRICE where YYYYMM = '" + month + "'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from cost_rate where yyyy ='" + yyyy + "' and isnull(dept_id,0) >0 and quarter_id = " + quarter + " and sale_type_id = 15";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_RATE where yyyy ='" + yyyy + "' and isnull(dept_id,0) =0 and QUARTER_ID = " + quarter + " and sale_type_id = 15";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            conn.Close();
            return true;

        }
        /// <summary>
        /// asy成本计算
        /// </summary>
        private void CalculateASY(string month)
        {
            ConnDB conn = new ConnDB();
            //string month = DateTime.Now.ToString("yyyy-MM");
            string sql;
            int i, rows;
            bool isok;
            isok = CheckData3(month);
            if (isok)
            {
                sql = "select * from COST_MONTH_CALCULATE where cmonth ='" + month + "' and sale_type_id =13";
                IDataParameter[] parameters = new IDataParameter[] { new SqlParameter("@cmonth", month), new SqlParameter("@saletypeid", "13") };

                rows = conn.ReturnRecordCount(sql);
                if (rows > 0)
                {
                    string sql1, sql2, sql3, sql4;
                    sql1 = "delete from COST_DAY_CALCULATE where cdate like '" + month + "%' and sale_type_id =13";
                    sql2 = "delete from COST_MONTH_CALCULATE where cmonth ='" + month + "' and sale_type_id =13";
                    sql3 = "delete from COST_DEPT_CALCULATE where cdate like '" + month + "%' and sale_type_id =13";
                    sql4 = "delete from COST_DEPT_MONTH_CALCULATE where cmonth ='" + month + "' and sale_type_id =13";
                    conn.EditDatabase(sql1);
                    conn.EditDatabase(sql2);
                    conn.EditDatabase(sql3);
                    conn.EditDatabase(sql4);
                    conn.RunProcedure("costing", parameters, out i);
                }
                else
                {
                    conn.RunProcedure("costing", parameters, out i);
                }
            }

            conn.Close();
        }
      /// <summary>
      /// asy数据检查
      /// </summary>
      /// <returns></returns>
        private bool CheckData3(string month)
        {
            ConnDB conn = new ConnDB();
            string sql;
            string yyyy = "";
            int quarter = 0;
            int count;
            GetQuarter(month, ref yyyy, ref quarter);
            sql = "select top 1 * from COST_DIRECT_LABOUR_ATTENDANCE where cdate like '" + month + "%'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }

            sql = "select top 1 * from COST_EMS_HH_HOURS where cdate like '" + month + "%'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_STANDARD_POINT where YYYYMM = '" + month + "' and SALE_TYPE_ID = 13";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_DEPT_STANDARD_POINT where YYYYMM = '" + month + "' and SALE_TYPE_ID = 13";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }

            sql = "select top 1 * from COST_DIRECT_LABOUR_PRICE where YYYYMM = '" + month + "'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_INDIRECT_LABOUR_LEVEL_PRICE where YYYYMM = '" + month + "'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from cost_rate where yyyy ='" + yyyy + "' and isnull(dept_id,0) >0 and quarter_id = " + quarter + " and sale_type_id = 15";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_RATE where yyyy ='" + yyyy + "' and isnull(dept_id,0) =0 and QUARTER_ID = " + quarter + " and sale_type_id = 15";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            conn.Close();
            return true;
        }
        /// <summary>
        /// 终端成本计算
        /// </summary>
        private void CalculateProduct(string month)
        {
            ConnDB conn = new ConnDB();
            //string month = DateTime.Now.ToString("yyyy-MM");
            string sql;
            int i, rows;
            bool isok;
            isok = CheckData4(month);
            if (isok)
            {
                sql = "select * from COST_MONTH_CALCULATE where cmonth ='" + month + "' and sale_type_id =14";
                IDataParameter[] parameters = new IDataParameter[] { new SqlParameter("@cmonth", month), new SqlParameter("@saletypeid", "14") };

                rows = conn.ReturnRecordCount(sql);
                if (rows > 0)
                {
                    string sql1, sql2, sql3, sql4;
                    sql1 = "delete from COST_DAY_CALCULATE where cdate like '" + month + "%' and sale_type_id =14";
                    sql2 = "delete from COST_MONTH_CALCULATE where cmonth ='" + month + "' and sale_type_id =14";
                    sql3 = "delete from COST_DEPT_CALCULATE where cdate like '" + month + "%' and sale_type_id =14";
                    sql4 = "delete from COST_DEPT_MONTH_CALCULATE where cmonth ='" + month + "' and sale_type_id =14";
                    conn.EditDatabase(sql1);
                    conn.EditDatabase(sql2);
                    conn.EditDatabase(sql3);
                    conn.EditDatabase(sql4);
                    conn.RunProcedure("costing", parameters, out i);
                }
                else
                {
                    conn.RunProcedure("costing", parameters, out i);
                }
            }

            conn.Close();
        }
        /// <summary>
        /// 终端数据检查
        /// </summary>
        /// <returns></returns>
        private bool CheckData4(string month)
        {
            ConnDB conn = new ConnDB();
            string sql;
            string yyyy = "";
            int quarter = 0;
            int count;
            GetQuarter(month, ref yyyy, ref quarter);
            sql = "select top 1 * from COST_DIRECT_LABOUR_ATTENDANCE where cdate like '" + month + "%'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }

            sql = "select top 1 * from COST_PRODUCT_QUANTITY where type = 1 and PRODUCT_QUANTITY >0 and cdate like '" + month + "%'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from cost_depreciation where YYYYMM = '" + month + "' and SALE_TYPE_ID = 14";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_STANDARD_POINT where YYYYMM = '" + month + "' and SALE_TYPE_ID = 14";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_DEPT_STANDARD_POINT where YYYYMM = '" + month + "' and SALE_TYPE_ID = 14";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }

            sql = "select top 1 * from COST_DIRECT_LABOUR_PRICE where YYYYMM = '" + month + "'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_INDIRECT_LABOUR_LEVEL_PRICE where YYYYMM = '" + month + "'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_QUANTITY_RATE where yyyy ='" + yyyy + "' and quarter_id = " + quarter;
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from cost_rate where yyyy ='" + yyyy + "' and isnull(dept_id,0) >0 and quarter_id = " + quarter + " and sale_type_id = 14";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_RATE where yyyy ='" + yyyy + "' and isnull(dept_id,0) =0 and QUARTER_ID = " + quarter + " and sale_type_id = 14";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            conn.Close();
            return true;
        }
        /// <summary>
        /// 系统成本计算
        /// </summary>
        private void CalculateSys(string month)
        {
            ConnDB conn = new ConnDB();
            //string month = DateTime.Now.ToString("yyyy-MM");
            string sql;
            int i, rows;
            bool isok;
            isok = CheckData5(month);
            if (isok)
            {
                sql = "select * from COST_MONTH_CALCULATE where cmonth ='" + month + "' and sale_type_id =15";
                IDataParameter[] parameters = new IDataParameter[] { new SqlParameter("@cmonth", month), new SqlParameter("@saletypeid", "15") };

                rows = conn.ReturnRecordCount(sql);
                if (rows > 0)
                {
                    string sql1, sql2, sql3, sql4;
                    sql1 = "delete from COST_DAY_CALCULATE where cdate like '" + month + "%' and sale_type_id =15";
                    sql2 = "delete from COST_MONTH_CALCULATE where cmonth ='" + month + "' and sale_type_id =15";
                    sql3 = "delete from COST_DEPT_CALCULATE where cdate like '" + month + "%' and sale_type_id =15";
                    sql4 = "delete from COST_DEPT_MONTH_CALCULATE where cmonth ='" + month + "' and sale_type_id =15";
                    conn.EditDatabase(sql1);
                    conn.EditDatabase(sql2);
                    conn.EditDatabase(sql3);
                    conn.EditDatabase(sql4);
                    conn.RunProcedure("costing", parameters, out i);
                }
                else
                {
                    conn.RunProcedure("costing", parameters, out i);
                }
            }

            conn.Close();
        }
        /// <summary>
        /// 系统数据检查
        /// </summary>
        /// <returns></returns>
        private bool CheckData5(string month)
        {
            ConnDB conn = new ConnDB();
            string sql;
            string yyyy = "";
            int quarter = 0;
            int count;
            GetQuarter(month, ref yyyy, ref quarter);

            sql = "select top 1 * from COST_DIRECT_LABOUR_ATTENDANCE where cdate like '" + month + "%'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }

            sql = "select top 1 * from COST_PRODUCT_QUANTITY where type = 2 and PRODUCT_QUANTITY >0 and cdate like '" + month + "%'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from cost_depreciation where YYYYMM = '" + month + "' and SALE_TYPE_ID = 15";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_STANDARD_POINT where YYYYMM = '" + month + "' and SALE_TYPE_ID = 15";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_DEPT_STANDARD_POINT where YYYYMM = '" + month + "' and SALE_TYPE_ID = 15";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }

            sql = "select top 1 * from COST_DIRECT_LABOUR_PRICE where YYYYMM = '" + month + "'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_INDIRECT_LABOUR_LEVEL_PRICE where YYYYMM = '" + month + "'";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from cost_rate where yyyy ='" + yyyy + "' and isnull(dept_id,0) >0 and quarter_id = " + quarter + " and sale_type_id = 15";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            sql = "select top 1 * from COST_RATE where yyyy ='" + yyyy + "' and isnull(dept_id,0) =0 and QUARTER_ID = " + quarter + " and sale_type_id = 15";
            count = conn.ReturnRecordCount(sql);
            if (count == 0)
            {
                return false;
            }
            conn.Close();
            return true;

        }
        public void GetQuarter(string yyyymm, ref string yyyy, ref int quarter)
        {
            yyyy = yyyymm.Substring(0, 4);
            int mm = Convert.ToInt32(yyyymm.Substring(5, 2));
            if (mm < 4)
                quarter = 1;
            else if (mm >= 4 && mm < 7)
                quarter = 2;
            else if (mm >= 7 && mm < 10)
                quarter = 3;
            else
                quarter = 4;
        }
        private void CopyData(string month)
        {
            ConnDB conn = new ConnDB();
            string strsql, sqlcheck;
            string  yyyy = "";
            int quarter = 0;
            GetQuarter(month, ref yyyy, ref quarter);
            int rows;
            //月数据：主营综合费用
            sqlcheck = "select yyyymm from COST_COMPOSITE_EXPENSE where yyyymm ='" + month + "'";
            rows = conn.ReturnRecordCount(sqlcheck);
            if (rows == 0)
            {
                strsql = "insert into COST_COMPOSITE_EXPENSE(yyyymm,sale_type_id,expense) select '" + month + "',sale_type_id,expense from COST_COMPOSITE_EXPENSE where YYYYMM = (select Max(yyyymm) from COST_COMPOSITE_EXPENSE) ";
                conn.EditDatabase(strsql);  
            }
            //月数据：折旧费用
            sqlcheck = "select yyyymm from cost_depreciation where yyyymm ='" + month + "'";
            rows = conn.ReturnRecordCount(sqlcheck);
            if (rows == 0)
            {
                strsql = "insert into cost_depreciation(yyyymm,sale_type_id,depreciation) select '" + month + "',sale_type_id,depreciation from cost_depreciation where YYYYMM = (select Max(yyyymm) from cost_depreciation) ";
                conn.EditDatabase(strsql);
            }
            //月数据：部门标准单点成本
            sqlcheck = "select * from cost_dept_standard_point where yyyymm ='" + month + "'";
            rows = conn.ReturnRecordCount(sqlcheck);
            if (rows == 0)
            {
                strsql = "insert into cost_dept_standard_point(yyyymm,sale_type_id,dept_id,dept_standard_point) select '" + month + "',sale_type_id,dept_id,dept_standard_point from cost_dept_standard_point where YYYYMM = (select Max(yyyymm) from cost_dept_standard_point) ";
                conn.EditDatabase(strsql);
            }
            //月数据：直接人工费率
            sqlcheck = "select * from cost_direct_labour_price where YYYYMM = '" + month + "'";
            rows = conn.ReturnRecordCount(sqlcheck);
            if (rows == 0)
            {
                strsql = "insert into cost_direct_labour_price(yyyymm,work_type,price) select '" + month + "',work_type,price from cost_direct_labour_price where YYYYMM = (select Max(yyyymm) from cost_direct_labour_price) ";
                conn.EditDatabase(strsql);
            }
            //月数据：间接人工费率
            sqlcheck = "select * from COST_INDIRECT_LABOUR_LEVEL_PRICE where YYYYMM = '" + month + "'";
            rows = conn.ReturnRecordCount(sqlcheck);
            if (rows == 0)
            {
                strsql = "insert into COST_INDIRECT_LABOUR_LEVEL_PRICE(yyyymm,level_begin,level_end,work_type,price) select '" + month + "',level_begin,level_end,work_type,price from COST_INDIRECT_LABOUR_LEVEL_PRICE where YYYYMM = (select Max(yyyymm) from COST_INDIRECT_LABOUR_LEVEL_PRICE) ";
                conn.EditDatabase(strsql);
            }
            //月数据：租赁费用
            sqlcheck = "select yyyymm from cost_rent_expense where yyyymm ='" + month + "'";
            rows = conn.ReturnRecordCount(sqlcheck);
            if (rows == 0)
            {
                strsql = "insert into cost_rent_expense(yyyymm,sale_type_id,rent_expense) select '" + month + "',sale_type_id,rent_expense from cost_rent_expense where YYYYMM = (select Max(yyyymm) from cost_rent_expense) ";
                conn.EditDatabase(strsql);
            }
            //月数据：标准单点成本
            sqlcheck = "select yyyymm from cost_standard_point where yyyymm ='" + month + "'";
            rows = conn.ReturnRecordCount(sqlcheck);
            if (rows == 0)
            {
                strsql = "insert into cost_standard_point(yyyymm,sale_type_id,standard_point) select '" + month + "',sale_type_id,standard_point from cost_standard_point where YYYYMM = (select Max(yyyymm) from cost_standard_point) ";
                conn.EditDatabase(strsql);
               
            }
            //月数据：钢网成本占比
            sqlcheck = "select yyyymm from cost_steel_net_rate where yyyymm ='" + month + "'";
            rows = conn.ReturnRecordCount(sqlcheck);
            if (rows == 0)
            {
                strsql = "insert into cost_steel_net_rate(yyyymm,steel_net_rate) select '" + month + "',steel_net_rate from cost_steel_net_rate where YYYYMM = (select Max(yyyymm) from cost_steel_net_rate) ";
                conn.EditDatabase(strsql);  
            }
            //月数据：临时工费率
            sqlcheck = "select yyyymm from COST_TEMP_WORKER_PRICE where yyyymm ='" + month + "'";
            rows = conn.ReturnRecordCount(sqlcheck);
            if (rows == 0)
            {
                strsql = "insert into COST_TEMP_WORKER_PRICE(yyyymm,sale_type_id,price) select '" + month + "',sale_type_id,price from COST_TEMP_WORKER_PRICE where YYYYMM = (select Max(yyyymm) from COST_TEMP_WORKER_PRICE) ";
                conn.EditDatabase(strsql);
            }
            //月数据：水电费
            sqlcheck = "select yyyymm from cost_water_electricity where yyyymm ='" + month + "'";
            rows = conn.ReturnRecordCount(sqlcheck);
            if (rows == 0)
            {
                strsql = "insert into cost_water_electricity(yyyymm,sale_type_id,water_electricity) select '" + month + "',sale_type_id,water_electricity from cost_water_electricity where YYYYMM = (select Max(yyyymm) from cost_water_electricity) ";
                conn.EditDatabase(strsql);
            }
            //季度数据：部门成本比率
            sqlcheck = "select * from cost_rate where yyyy ='" + yyyy + "' and dept_id >0 and quarter_id = " + quarter;
            rows = conn.ReturnRecordCount(sqlcheck);
            if (rows == 0)
            {
                strsql = "insert into cost_rate(yyyy,quarter_id,sale_type_id,dept_id,cost_rate) select '" + yyyy + "','" + quarter + "',sale_type_id,dept_id,cost_rate from cost_rate where YYYY + cast(quarter_id as varchar(10)) = (select Max(yyyy+cast(quarter_id as varchar(10))) from cost_rate where dept_id >0) and dept_id >0";
                conn.EditDatabase(strsql);
            }
            //季度数据：成本比率
            sqlcheck = "select * from cost_rate where yyyy ='" + yyyy + "' and isnull(dept_id,0) = 0 and quarter_id = " + quarter;
            rows = conn.ReturnRecordCount(sqlcheck);
            if (rows == 0)
            {
                strsql = "insert into cost_rate(yyyy,quarter_id,sale_type_id,cost_rate) select '" + yyyy + "','" + quarter + "',sale_type_id,cost_rate from cost_rate where YYYY + cast(quarter_id as varchar(10)) = (select Max(yyyy+cast(quarter_id as varchar(10))) from cost_rate where isnull(dept_id,0) = 0) and isnull(dept_id,0) = 0";
                conn.EditDatabase(strsql);
            }
            //季度数据：终端台数比率
            sqlcheck = "select * from COST_QUANTITY_RATE where yyyy ='" + yyyy + "' and quarter_id = " + quarter;
            rows = conn.ReturnRecordCount(sqlcheck);
            if (rows == 0)
            {
                strsql = "insert into COST_QUANTITY_RATE(yyyy,quarter_id,rate) select '" + yyyy + "','" + quarter + "',rate from COST_QUANTITY_RATE where YYYY + cast(quarter_id as varchar(10)) = (select Max(yyyy+cast(quarter_id as varchar(10))) from COST_QUANTITY_RATE) ";
                conn.EditDatabase(strsql);
            }
            //季度数据：费用转嫁费率
            sqlcheck = "select * from cost_transfer_price where yyyy ='" + yyyy + "' and quarter_id = " + quarter;
            rows = conn.ReturnRecordCount(sqlcheck);
            if (rows == 0)
            {
                strsql = "insert into cost_transfer_price(yyyy,quarter_id,sale_type_id,price) select '" + yyyy + "','" + quarter + "',sale_type_id,price from cost_transfer_price where YYYY + cast(quarter_id as varchar(10)) = (select Max(yyyy+cast(quarter_id as varchar(10))) from cost_transfer_price) ";
                conn.EditDatabase(strsql);
            }

            conn.Close();
        }
        protected override void OnStop()
        {
            this.timer1.Enabled = false;          
        }
    }
}
