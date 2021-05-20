/*------------------------------------------------------------------------------
 * Created     : 10.05.2011 
 * Programmer  : Md. Nazmul Saqib
 * E-mail      : edurazee@yahoo.com
 * 
 * Usage	   : This class is intended to execute SQL queries and return appropriate 
 *               values after execution.
 *               
 *               This is intended to be used in DAL layer.
 *------------------------------------------------------------------------------
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Simple.Framework
{
    /// <summary>
    /// This class is intended to execute SQL queries and 
    /// return appropriate values after execution.
    /// 
    /// This is intended to be used in the DAL layer.
    /// </summary>
    public abstract class AQueryExecutor<T> : IQueryExecutor<T>
    {        
        private ITransactionManager TransactionManager { get; set; }
        private IDbCommand Command { get; set; }

        public AQueryExecutor(ITransactionManager tm)
        {
            TransactionManager = tm;
        }

        private IList<string> parameterNamesList { get; set; }
        private int i = 0;

        public void CreateSqlCommand(string queryString)
        {
            Command = DbObjectInstantiator.GetCommandInstance(TransactionManager.ProviderName);
            Command.CommandText = queryString;
            Command.Connection = TransactionManager.Connection;
            Command.Transaction = TransactionManager.Transaction;

            if (parameterNamesList != null)
            {
                parameterNamesList.Clear();
                parameterNamesList = null;
                i = 0;
            }
            parameterNamesList = new List<string>(ParameterParser.Parse(queryString));
        }

        public void AddParameter(object dbParam, DbType paramType)
        {
            if (parameterNamesList.Count > 0)
            {
                if(i <= parameterNamesList.Count)
                {
                    IDbDataParameter param = Command.CreateParameter();

                    param.ParameterName = parameterNamesList[i++];

                    if (dbParam != null)
                    {
                        param.Value = dbParam;
                    }
                    else
                    {
                        param.Value = DBNullValue.GetNullValue(paramType);
                    }
                    
                    param.DbType = paramType;
                    Command.Parameters.Add(param);
                }
            }
        }

        /// <summary>
        /// Executes an SQL statement against the TransactionManager 
        /// object and returns the number of rows affected.
        /// </summary>
        /// <param name="queryString">The SQL query string.</param>
        /// <param name="parameters">The parameters to be attached to the IDbCommand object.</param>
        /// <returns>int</returns>
        public virtual int ExecuteNonQuery()
        {
            int count = -1;

            try
            {
                Command.Connection = TransactionManager.Connection;
                Command.Transaction = TransactionManager.Transaction;
                count = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return count;
        }        

        /// <summary>
        /// Executes an SQL statement against the TransactionManager 
        /// object and returns a SafeDataReader object.
        /// </summary>
        /// <param name="queryString">The SQL query string.</param>
        /// <param name="parameters">The parameters to be attached to the IDbCommand object.</param>
        /// <returns>SafeDataReader</returns>
        public virtual ISafeDataReader ExecuteReader()
        {
            ISafeDataReader reader = null;

            try
            {
                Command.Connection = TransactionManager.Connection;
                Command.Transaction = TransactionManager.Transaction;
                reader = new SafeDataReader(Command.ExecuteReader());
            }
            catch (Exception)
            {
                throw ;
            }

            return reader;
        }

        /// <summary>
        /// Executes an SQL statement against the TransactionManager 
        /// object and returns count value.
        /// </summary>
        /// <param name="queryString">The SQL query string.</param>
        /// <param name="parameters">The parameters to be attached to the IDbCommand object.</param>
        /// <returns>int</returns>
        public virtual int ExecuteScalar()
        {
            int value = -1;

            try
            {
                Command.Connection = TransactionManager.Connection;
                Command.Transaction = TransactionManager.Transaction;
                object obj = Command.ExecuteScalar();

                if (obj == DBNull.Value)
                {
                    value = -1;
                }
                else
                {
                    value = Convert.ToInt32(obj);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return value;
        }

        public virtual DataSet ExecuteDataSet()
        {
            DataSet dataSet = null;

            try
            {
                IDbDataAdapter dataAdapter = DbObjectInstantiator.GetDataAdapterInstance(TransactionManager.ProviderName);
                Command.Connection = TransactionManager.Connection;
                Command.Transaction = TransactionManager.Transaction;
                dataAdapter.SelectCommand = Command;

                dataSet = new DataSet();

                dataAdapter.Fill(dataSet);
            }
            catch (Exception)
            {
                throw;
            }

            return dataSet;
        }        
    }
}
