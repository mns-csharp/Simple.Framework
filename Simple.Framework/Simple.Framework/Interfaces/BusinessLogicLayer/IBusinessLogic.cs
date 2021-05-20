using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Framework
{
    public interface IBusinessLogic<T>
    {
        T Get(int id);
        IList<T> Get();
        int Save(T item);
        int Update(T item);
        int Delete(T item);
    }
}
