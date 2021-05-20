using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Simple.Framework
{
    public interface ITransactionManager
    {
        string ConnectionStringName { get; }
        string ConnectionString { get; }
        string ProviderName { get; }

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
    }
}
