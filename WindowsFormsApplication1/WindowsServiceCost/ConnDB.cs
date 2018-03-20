using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

/// <summary>
/// 数据处理通用类
/// </summary>
public class ConnDB
{
    protected SqlConnection Connection;
    private string connectionString;

    /// <summary>
    /// 默认构造函数,使用web.config文件的链接参数
    /// </summary>
    public ConnDB()
    {
        string decryptStr;
        connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
        decryptStr = Decrypt(connectionString);
        Connection = new SqlConnection(decryptStr);
    }
    //public ConnDB()
    //{
    //    connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
    //    Connection = new SqlConnection(connectionString);
    //}

    /// <summary>
    /// 带参数的构造函数
    /// </summary>
    /// <param name="newConnectionString">数据库联接字符串</param>
    public ConnDB(string newConnectionString)
    {
        connectionString = newConnectionString;
        Connection = new SqlConnection(connectionString);
    }
    public static string Decrypt(string connectString)
    {
        return  WindowsServiceCost.EncryptUtility.DesDecrypt(connectString, "12345678");
    }

    /// <summary>
    /// 完成SqlCommand对象的实例化
    /// </summary>
    /// <param name="storedProcName"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    private SqlCommand BuildCommand(string storedProcName, IDataParameter[] parameters)
    {
        SqlCommand command = BuildQueryCommand(storedProcName, parameters);
        command.CommandTimeout = 600;
        command.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
        return command;
    }


    /// <summary>
    /// 创建新的SQL命令对象(存储过程)
    /// </summary>
    /// <param name="storedProcName"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    private SqlCommand BuildQueryCommand(string storedProcName, IDataParameter[] parameters)
    {
        SqlCommand command = new SqlCommand(storedProcName, Connection);
        command.CommandTimeout = 600;
        command.CommandType = CommandType.StoredProcedure;
        foreach (SqlParameter parameter in parameters)
        {
            command.Parameters.Add(parameter);
        }
        return command;
    }


    /// <summary>
    /// 执行存储过程,无返回值
    /// </summary>
    /// <param name="storedProcName"></param>
    /// <param name="parameters"></param>
    public void ExecuteProcedure(string storedProcName, IDataParameter[] parameters)
    {
        Connection.Open();
        SqlCommand command;
        command = BuildQueryCommand(storedProcName, parameters);
        command.ExecuteNonQuery();
        Connection.Close();
    }


    /// <summary>
    /// 执行存储过程，返回执行操作影响的行数目
    /// </summary>
    /// <param name="storedProcName"></param>
    /// <param name="parameters"></param>
    /// <param name="rowsAffected"></param>
    /// <returns></returns>
    public int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
    {
        int result;
        Connection.Open();
        SqlCommand command = BuildCommand(storedProcName, parameters);
        rowsAffected = command.ExecuteNonQuery();
        result = (int)command.Parameters["ReturnValue"].Value;
        Connection.Close();

        return result;
    }


    /// <summary>
    /// 重载RunProcedure把执行存储过程的结果放在SqlDataReader中
    /// </summary>
    /// <param name="storedProcName"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
    {
        SqlDataReader returnReader;
        Connection.Open();
        SqlCommand command = BuildQueryCommand(storedProcName, parameters);
        command.CommandType = CommandType.StoredProcedure;
        returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
        return returnReader;
    }


    /// <summary>
    /// 重载RunProcedure把执行存储过程的结果存储在DataSet中和表tableName为可选参数
    /// </summary>
    /// <param name="storedProcName"></param>
    /// <param name="parameters"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, params string[] tableName)
    {
        DataSet dataSet = new DataSet();
        Connection.Open();
        SqlDataAdapter sqlDA = new SqlDataAdapter();
        sqlDA.SelectCommand = BuildQueryCommand(storedProcName, parameters);
        string flag;
        flag = "";
        for (int i = 0; i < tableName.Length; i++)
            flag = tableName[i];
        if (flag != "")
            sqlDA.Fill(dataSet, tableName[0]);
        else
            sqlDA.Fill(dataSet);
        Connection.Close();
        return dataSet;
    }


    /// <summary>
    /// 执行SQL语句，返回数据到DataSet中
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public DataSet ReturnDataSet(string sql)
    {
        DataSet dataSet = new DataSet();
        Connection.Open();
        SqlDataAdapter sqlDA = new SqlDataAdapter(sql, Connection);
        sqlDA.Fill(dataSet, "objDataSet");
        Connection.Close();
        return dataSet;
    }


    /// <summary>
    /// 执行SQL语句，返回 DataReader
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public SqlDataReader ReturnDataReader(String sql)
    {
        Connection.Open();
        SqlCommand command = new SqlCommand(sql, Connection);
        command.CommandTimeout = 600;
        SqlDataReader dataReader = command.ExecuteReader();

        return dataReader;
    }


    /// <summary>
    /// 执行SQL语句，返回记录数
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public int ReturnRecordCount(string sql)
    {
        int recordCount = 0;

        Connection.Open();
        SqlCommand command = new SqlCommand(sql, Connection);
        command.CommandTimeout = 600;
        SqlDataReader dataReader = command.ExecuteReader();

        while (dataReader.Read())
        {
            recordCount++;
        }
        dataReader.Close();
        Connection.Close();

        return recordCount;
    }


    /// <summary>
    /// 执行SQL语句
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public bool EditDatabase(string sql)
    {
        bool successState = false;

        Connection.Open();
        SqlTransaction myTrans = Connection.BeginTransaction();
        SqlCommand command = new SqlCommand(sql, Connection, myTrans);
        command.CommandTimeout = 600;
        try
        {
            command.ExecuteNonQuery();
            myTrans.Commit();
            successState = true;
        }
        catch
        {
            myTrans.Rollback();
        }
        finally
        {
            Connection.Close();
        }

        return successState;
        
    }
    /// <summary>
    /// 执行delete语句
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public bool DeleteDatabase(string sql)
    {
        bool successState = false;

        Connection.Open();
        SqlTransaction myTrans = Connection.BeginTransaction();
        SqlCommand command = new SqlCommand(sql, Connection, myTrans);
        command.CommandTimeout = 600;
        try
        {
            command.ExecuteNonQuery();
            myTrans.Commit();
            successState = true;
        }
        catch
        {
            MessageBox.Show("删除的记录可能已经被其它模块使用，请先删除其它模块相关记录！");
            myTrans.Rollback();
        }
        finally
        {
            Connection.Close();
        }

        return successState;

    }

    /// <summary>
    /// 关闭数据库联接
    /// </summary>
    public void Close()
    {
        Connection.Close();
    }


}//end class
