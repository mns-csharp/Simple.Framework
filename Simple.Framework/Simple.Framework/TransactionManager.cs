using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Simple.Framework
{
    public class TransactionManager : ATransactionManager, ITransactionManager
    {
        public TransactionManager(string connectionStringConfigName)
            : base(connectionStringConfigName)
        {
        }

        public TransactionManager(string connectionString, string provider)
            : base(connectionString, provider)
        {
        }
    }
}