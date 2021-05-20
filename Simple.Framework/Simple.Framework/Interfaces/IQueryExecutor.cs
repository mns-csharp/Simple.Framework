using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Simple.Framework
{
    public interface IQueryExecutor<T>
    {
        void CreateSqlCommand(string sql);
        void AddParameter(object param, DbType paramType);
        int ExecuteNonQuery();
        ISafeDataReader ExecuteReader();
        int ExecuteScalar();
        DataSet ExecuteDataSet();
    }
}
