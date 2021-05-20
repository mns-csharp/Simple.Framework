/*------------------------------------------------------------------------------
 * Created     : 10.05.2011 
 * Programmer  : Md. Nazmul Saqib
 * E-mail      : edurazee@yahoo.com
 * 
 * Usage	   : This class is intended to manage database transactions.
 * 
 *               This is intended to be used in BLL layer.
 *------------------------------------------------------------------------------
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Simple.Framework
{
    public abstract class ATransactionManager : ITransactionManager
    {
        public virtual IDbConnection Connection { get; private set; }
        public virtual IDbTransaction Transaction { get; private set; }

        /*
         * The ConnectionStringSettings information is kept so that
         * we can get the providerName and other information and 
         * used during any database object instantiation in the DAO.
         */
        private ConnectionStringSettings ConnectionStringSettings { get; set; }
        public virtual string ConnectionStringName { get; private set; }
        public virtual string ConnectionString { get; private set; }
        public virtual string ProviderName { get; private set; }

        public ATransactionManager(string connectionStringName)
        {
            // Read config from App.config
            ConnectionStringSettings = ApplicationContext.Get(connectionStringName);

            // Polulate this object
            ConnectionStringName = ConnectionStringSettings.Name;
            ConnectionString = ConnectionStringSettings.ConnectionString;
            ProviderName = ConnectionStringSettings.ProviderName;

            //We are creating our Connection object only one-time.
            Connection = DbObjectInstantiator.GetConnectionInstance(ProviderName);
            Connection.ConnectionString = ConnectionString;
        }

        public ATransactionManager(string connectionString, string providerName)
        {
            // Polulate this object
            ConnectionString = connectionString;
            ProviderName = providerName;

            //We are creating our Connection object only one-time.
            Connection = DbObjectInstantiator.GetConnectionInstance(ProviderName);
            Connection.ConnectionString = ConnectionString;
        }

        public virtual void BeginTransaction()
        {
            Connection.Open();
            Transaction = Connection.BeginTransaction();
        }

        public virtual void CommitTransaction()
        {
            Transaction.Commit();
            Connection.Close();
        }

        public virtual void RollbackTransaction()
        {
            if (Transaction != null)
            {
                Transaction.Rollback();
            }

            if (Connection != null)
            {
                Connection.Close();
            }
        }
    }
}
