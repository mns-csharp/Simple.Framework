using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Framework
{
    public interface IDataAccess<T> where T : BaseClass<T>
    {
        T Get(ITransactionManager tm, int? id);
        IList<T> Get(ITransactionManager tm);
        int Save(ITransactionManager tm, T item);
        int Update(ITransactionManager tm, T item);
        int Delete(ITransactionManager tm, T item);        
    }
}
