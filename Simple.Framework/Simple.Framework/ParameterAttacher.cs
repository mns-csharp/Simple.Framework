using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection;

namespace Simple.Framework
{
    public class ParameterAttacher
    {        

        /// <summary>
        /// Attaches multiple parameters to a command object.
        /// </summary>
        /// <param name="tm">The transaction manager object which represents a transaction context.</param>
        /// <param name="queryString">The SQL query string.</param>
        /// <param name="parameters">The parameters to be attached to the IDbCommand object.</param>
        /// <returns>IDbCommand</returns>
        public static IDbCommand AttachSaveParameters(ITransactionManager tm, string queryString, IList<object> argumentsList, IList<DbType> dbTypes)
        {
            IDbCommand command = DbObjectInstantiator.GetCommandInstance(tm.ProviderName);
            command.Connection = tm.Connection;
            command.Transaction = tm.Transaction;

            IList<string> parameterNamesList = new List<string>(ParameterParser.Parse(queryString));

            if (parameterNamesList.Count > 0 && argumentsList.Count == argumentsList.Count)
            {
                int i = 0;

                foreach (string paramName in parameterNamesList)
                {
                    Attach(command, paramName, argumentsList[i], dbTypes[i]);
                    
                    ++i;
                }
            }
            
            return command;
        }

        /// <summary>
        /// Attaches a single parameter to a command object.
        /// </summary>
        /// <param name="command">The command object to which parameters are added.</param>
        /// <param name="paramName">The name of the parameter to be added.</param>
        /// <param name="value">The value of the parameter to be added.</param>
        public static void Attach(IDbCommand command, string paramName, object dbParam, DbType dbType)
        {
            IDbDataParameter param = command.CreateParameter();

            param.ParameterName = paramName;
            param.Value = (dbParam==null) ? ((object)DBNull.Value) : dbParam;
            param.DbType = dbType;

            command.Parameters.Add(param);
        }
    }
}
