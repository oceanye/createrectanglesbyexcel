using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SQLite;
using System.Configuration;

namespace createrectanglesbyexcel.sqlite.DBUtility
{
    /// <summary>
    /// The SqlHelper class is intended to encapsulate high performance, 
    /// scalable best practices for common uses of SqlClient.
    /// </summary>
    public abstract class SQLiteHelper
    {

        /// <summary>
        /// 数据库地址
        /// </summary>
        private static string _ConnectionString = string.Empty;
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        public static string ConnectionString
        {
            get
            {
                return _ConnectionString;
            }
            set
            {
                _ConnectionString = value;
            }
        }

        #region 缓存参数
        /// <summary>
        /// add parameter array to the cache
        /// </summary>
        /// <param name="cacheKey">Key to the parameter cache</param>
        /// <param name="cmdParms">an array of SqlParamters to be cached</param>
        public static void CacheParameters(string cacheKey, params SQLiteParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// Retrieve cached parameters
        /// </summary>
        /// <param name="cacheKey">key used to lookup parameters</param>
        /// <returns>Cached SqlParamters array</returns>
        public static SQLiteParameter[] GetCachedParameters(string cacheKey)
        {
            SQLiteParameter[] cachedParms = (SQLiteParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SQLiteParameter[] clonedParms = new SQLiteParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SQLiteParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }
        #endregion

        #region ExecuteNonQuery方法
        public static int ExecuteNonQuery(string cmdText, string connectionString = "", CommandType cmdType = CommandType.Text, SQLiteParameter[] commandParameters = null, int connecttimes = 0)
        {
            if (string.IsNullOrEmpty(connectionString))
                connectionString = _ConnectionString;
            return ExecuteNonQuery(new SQLiteConnection(connectionString), cmdText, cmdType, commandParameters, connecttimes);
        }

        public static int ExecuteNonQuery(SQLiteConnection connection, string cmdText, CommandType cmdType = CommandType.Text, SQLiteParameter[] commandParameters = null, int connecttimes = 0, SQLiteTransaction trans = null)
        {

            SQLiteCommand cmd = new SQLiteCommand();
            PrepareCommand(cmd, connection, trans, cmdType, cmdText, connecttimes, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            connection.Close();
            return val;
        }

        public static int ExecuteNonQuery(SQLiteTransaction trans, string cmdText, CommandType cmdType = CommandType.Text, SQLiteParameter[] commandParameters = null, int connecttimes = 0)
        {
            return ExecuteNonQuery(trans.Connection, cmdText, cmdType, commandParameters, connecttimes, trans);
        }
        #endregion

        #region ExecuteScalar方法
        public static object ExecuteScalar(string cmdText, string connectionString = "", CommandType cmdType = CommandType.Text, SQLiteParameter[] commandParameters = null, int connecttimes = 0)
        {
            if (string.IsNullOrEmpty(connectionString))
                connectionString = _ConnectionString;
            return ExecuteScalar(new SQLiteConnection(connectionString), cmdText, cmdType, commandParameters, connecttimes);
        }


        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(SQLiteConnection connection, string cmdText, CommandType cmdType = CommandType.Text, SQLiteParameter[] commandParameters = null, int connecttimes = 0, SQLiteTransaction trans = null)
        {

            SQLiteCommand cmd = new SQLiteCommand();

            PrepareCommand(cmd, connection, trans, cmdType, cmdText, connecttimes, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            connection.Close();
            return val;
        }

        public static object ExecuteScalar(SQLiteTransaction trans, string cmdText, CommandType cmdType = CommandType.Text, SQLiteParameter[] commandParameters = null, int connecttimes = 0)
        {
            return ExecuteScalar(trans.Connection, cmdText, cmdType, commandParameters, connecttimes, trans);
        }
        #endregion

        #region ExecuteReader方法
        public static SQLiteDataReader ExecuteReader(string cmdText, string connectionString = "", CommandType cmdType = CommandType.Text, SQLiteParameter[] commandParameters = null, int connecttimes = 0)
        {
            if (string.IsNullOrEmpty(connectionString))
                connectionString = _ConnectionString;
            return ExecuteReader(new SQLiteConnection(connectionString), cmdText, cmdType, commandParameters, connecttimes);
        }


        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static SQLiteDataReader ExecuteReader(SQLiteConnection connection, string cmdText, CommandType cmdType = CommandType.Text, SQLiteParameter[] commandParameters = null, int connecttimes = 0, SQLiteTransaction trans = null)
        {

            SQLiteCommand cmd = new SQLiteCommand();

            PrepareCommand(cmd, connection, trans, cmdType, cmdText, connecttimes, commandParameters);
            SQLiteDataReader val = cmd.ExecuteReader();
            cmd.Parameters.Clear();
            return val;
        }
        #endregion


        private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, SQLiteTransaction trans, CommandType cmdType, string cmdText, int connecttime = 0, params SQLiteParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (connecttime != 0)
                cmd.CommandTimeout = connecttime;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SQLiteParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }


        public static DataSet Fillds(string cmdText, string tableName = "", string connectionString = "", CommandType cmdType = CommandType.Text, SQLiteParameter[] commandParameters = null, int connecttimes = 0)
        {
            if (string.IsNullOrEmpty(connectionString))
                connectionString = _ConnectionString;
            DataSet ds = null;
            SQLiteCommand cmd = new SQLiteCommand();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                ds = new DataSet();
                PrepareCommand(cmd, connection, null, cmdType, cmdText, connecttimes, commandParameters);
                using (SQLiteDataAdapter SQLiteDataAdapter = new SQLiteDataAdapter(cmd))
                {
                    if (string.IsNullOrEmpty(tableName))
                        SQLiteDataAdapter.Fill(ds);
                    else
                        SQLiteDataAdapter.Fill(ds, tableName);
                }
                cmd.Parameters.Clear();
                connection.Close();
            }
            return ds;
        }
        /// <summary>
        /// 批量插入、修改、删除，并同步更新存储及数据源
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="tableName"></param>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="commandParameters"></param>
        /// <param name="connecttimes"></param>
        /// <returns></returns>
        public static DataTable Fill(string cmdText, string tableName = "", string connectionString = "", CommandType cmdType = CommandType.Text, SQLiteParameter[] commandParameters = null, int connecttimes = 0)
        {
            if (string.IsNullOrEmpty(connectionString))
                connectionString = _ConnectionString;
            DataTable dt = null;
            SQLiteCommand cmd = new SQLiteCommand();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                if (string.IsNullOrEmpty(tableName))
                    dt = new DataTable();
                else
                    dt = new DataTable(tableName.Trim());
                PrepareCommand(cmd, connection, null, cmdType, cmdText, connecttimes, commandParameters);

                using (SQLiteDataAdapter SQLiteDataAdapter = new SQLiteDataAdapter(cmd))
                {
                    SQLiteDataAdapter.Fill(dt);
                }
                cmd.Parameters.Clear();
                connection.Close();
            }
            return dt;
        }

        /// <summary>
        /// 批量插入、修改、删除，并同步更新存储及数据源
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="cmdText"></param>
        /// <param name="tableName"></param>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="commandParameters"></param>
        /// <param name="connecttimes"></param>
        /// <returns></returns>
        public static int BatchUpdate(DataSet ds, string cmdText, string tableName = "", string connectionString = "", CommandType cmdType = CommandType.Text, SQLiteParameter[] commandParameters = null, int connecttimes = 0)
        {
            if (string.IsNullOrEmpty(connectionString))
                connectionString = _ConnectionString;
            int rowNum = 0;
            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, connecttimes, commandParameters);
                using (SQLiteDataAdapter SQLiteDataAdapter = new SQLiteDataAdapter(cmd))
                {

                    SQLiteCommandBuilder builder = new SQLiteCommandBuilder(SQLiteDataAdapter);
                    builder.QuotePrefix = "[";
                    builder.QuoteSuffix = "]";
                    if (string.IsNullOrEmpty(tableName))
                        rowNum = SQLiteDataAdapter.Update(ds);
                    else
                        rowNum = SQLiteDataAdapter.Update(ds, tableName);
                    ds.AcceptChanges();

                }
                cmd.Parameters.Clear();
                connection.Close();
                return rowNum;
            }
        }


        public static int BatchUpdate(DataTable dt, string cmdText, string tableName = "", string connectionString = "", CommandType cmdType = CommandType.Text, SQLiteParameter[] commandParameters = null, int connecttimes = 0)
        {
            if (string.IsNullOrEmpty(connectionString))
                connectionString = _ConnectionString;
            int rowNum = 0;
            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, connecttimes, commandParameters);
                using (SQLiteDataAdapter SQLiteDataAdapter = new SQLiteDataAdapter(cmd))
                {

                    SQLiteCommandBuilder builder = new SQLiteCommandBuilder(SQLiteDataAdapter);
                    builder.QuotePrefix = "[";
                    builder.QuoteSuffix = "]";
                    rowNum = SQLiteDataAdapter.Update(dt);
                    dt.AcceptChanges();
                }
                cmd.Parameters.Clear();
                connection.Close();
                return rowNum;
            }
        }

        public static bool TestConnection(string connectionString = "")
        {
            try
            {
                if (string.IsNullOrEmpty(connectionString))
                    connectionString = _ConnectionString;
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                        return true;
                    }
                }

            }
            catch { }

            return false;
        }
    }
}

