using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

/// <summary>
/// ���ݴ���ͨ����
/// </summary>
public class ConnDB
{
    protected SqlConnection Connection;
    private string connectionString;

    /// <summary>
    /// Ĭ�Ϲ��캯��,ʹ��web.config�ļ������Ӳ���
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
    /// �������Ĺ��캯��
    /// </summary>
    /// <param name="newConnectionString">���ݿ������ַ���</param>
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
    /// ���SqlCommand�����ʵ����
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
    /// �����µ�SQL�������(�洢����)
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
    /// ִ�д洢����,�޷���ֵ
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
    /// ִ�д洢���̣�����ִ�в���Ӱ�������Ŀ
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
    /// ����RunProcedure��ִ�д洢���̵Ľ������SqlDataReader��
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
    /// ����RunProcedure��ִ�д洢���̵Ľ���洢��DataSet�кͱ�tableNameΪ��ѡ����
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
    /// ִ��SQL��䣬�������ݵ�DataSet��
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
    /// ִ��SQL��䣬���� DataReader
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
    /// ִ��SQL��䣬���ؼ�¼��
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
    /// ִ��SQL���
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
    /// ִ��delete���
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
            MessageBox.Show("ɾ���ļ�¼�����Ѿ�������ģ��ʹ�ã�����ɾ������ģ����ؼ�¼��");
            myTrans.Rollback();
        }
        finally
        {
            Connection.Close();
        }

        return successState;

    }

    /// <summary>
    /// �ر����ݿ�����
    /// </summary>
    public void Close()
    {
        Connection.Close();
    }


}//end class
