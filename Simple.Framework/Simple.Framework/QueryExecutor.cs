using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Framework
{
    public class QueryExecutor<T> : AQueryExecutor<T>, IQueryExecutor<T>
    {
        public QueryExecutor(ITransactionManager tm) : base(tm) { }
    }
}
