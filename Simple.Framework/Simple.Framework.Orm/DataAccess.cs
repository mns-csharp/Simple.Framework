using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;

namespace Simple.Framework.Orm
{
    public class DataAccess<T>
    {        
        #region IList<T> Get(ITransactionManager tm)
        public IList<T> Get(ITransactionManager tm)
        {
            IList<T> list = null;

            ISafeDataReader dataReader = null;

            try
            {
                string fullClassName = typeof(T).FullName;
                Mapping mapping = OrmEngine.GetMapping(fullClassName);
                
                string sqlKey = fullClassName + "." + QueryNameConst.GetAll;
                string sqlQueryString = MappingDataExtractor.GetSqlQuery(mapping, sqlKey);
                
                QueryExecutor<T> executor = new QueryExecutor<T>(tm);
                executor.CreateSqlCommand(sqlQueryString);
                
                dataReader = executor.ExecuteReader();

                list = DataReaderMapper.MapMultipleItems<T>(dataReader, mapping);

                dataReader.Close();
            }
            catch
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader = null;
                }

                throw;
            }

            return list;
        }
        #endregion

        #region T Get(ITransactionManager tm, int id)
        public T Get(ITransactionManager tm, int id)
        {
            T item = default(T);

            ISafeDataReader dataReader = null;

            try
            {
                string fullClassName = typeof(T).FullName;
                Mapping mapping = OrmEngine.GetMapping(fullClassName);
                //if (mapping == null)
                //{
                //    throw new Exception(fullClassName + ErrorMessages.WasNotFound);
                //}

                string sqlKey = fullClassName + "." + QueryNameConst.GetByID;
                string sqlQueryString = MappingDataExtractor.GetSqlQuery(mapping, sqlKey);
                //if (string.IsNullOrEmpty(sqlQueryString))
                //{
                //    throw new Exception(sqlKey + ErrorMessages.WasNotFound);
                //}

                QueryExecutor<T> queryExecutor = new QueryExecutor<T>(tm);
                queryExecutor.CreateSqlCommand(sqlQueryString);
                queryExecutor.AddParameter(id, DbType.Int32);

                dataReader = queryExecutor.ExecuteReader();

                while (dataReader.Read())
                {
                    item = DataReaderMapper.MapSingleItem<T>(dataReader, mapping);
                }

                dataReader.Close();
            }
            catch (Exception ex)
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader = null;
                }

                throw ex;
            }

            return item;
        }
        #endregion

        #region int Save(ITransactionManager tm, T item)
        public int Save(ITransactionManager tm, T item)
        {
            int newID = -1;

            try
            {
                Type pocoT = typeof(T);
                string fullClassName = pocoT.FullName;

                Mapping mapping = OrmEngine.GetMapping(fullClassName);

                string key = fullClassName + "." + QueryNameConst.Save;
                string SQL = MappingDataExtractor.GetSqlQuery(mapping, key);

                IList<Property> propertyList = MappingDataExtractor.GetProperties(mapping);

                newID = PivotTable.GetNextID(tm, pocoT.Name).Value;

                QueryExecutor<T> executor = new QueryExecutor<T>(tm);
                executor.CreateSqlCommand(SQL);
                executor.AddParameter(newID, DbType.Int32);

                foreach (Property p in propertyList)
                {
                    string propName = PropertyDataExtractor.GetName(p);
                    PropertyInfo propInfo = pocoT.GetProperty(propName);
                    Object value = propInfo.GetValue(item, null);
                    
                    if (value == null)
                    {
                        if (PropertyDataExtractor.IsNullable(p))
                        {
                            value = PropertyDataExtractor.GetDefaultNullvalue(p);
                        }
                    }
                    
                    DbType dbType = PropertyDataExtractor.GetDbTypeEnum(p);

                    PropertyValidator.ValidateMinMax(p, value);
                    PropertyValidator.ValidateNullable(p, value);

                    executor.AddParameter(value, dbType);
                }

                executor.ExecuteNonQuery();

                PivotTable.UpdateNewID(tm, pocoT.Name, newID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newID;
        }
        #endregion

        #region int Update(ITransactionManager tm, T item)
        public int Update(ITransactionManager tm, T item)
        {
            int count = -1;

            try
            {
                Type pocoT = typeof(T);
                string fullClassName = pocoT.FullName;

                Mapping mapping = OrmEngine.GetMapping(fullClassName);
                //if (mapping == null)
                //{
                //    throw new Exception(fullClassName + ErrorMessages.WasNotFound);
                //}

                string key = KeyMaker.GetKey(mapping, QueryNameConst.UpdateByID);
                string SQL =  MappingDataExtractor.GetSqlQuery(mapping, key);
                //if (string.IsNullOrEmpty(SQL))
                //{
                //    throw new Exception(key + ErrorMessages.WasNotFound);
                //}

                IList<Property> pocoProperties = MappingDataExtractor.GetProperties(mapping);

                PropertyInfo propInfo = pocoT.GetProperty(MappingDataExtractor.GetIdName(mapping));
                object id = propInfo.GetValue(item, null);

                QueryExecutor<T> executor = new QueryExecutor<T>(tm);
                executor.CreateSqlCommand(SQL);

                foreach (Property p in pocoProperties)
                {
                    string propName = PropertyDataExtractor.GetName(p);
                    propInfo = pocoT.GetProperty(propName);
                    object value = propInfo.GetValue(item, null);
                    if (value == null)
                    {
                        if (PropertyDataExtractor.IsNullable(p))
                        {
                            value = PropertyDataExtractor.GetDefaultNullvalue(p);
                        }
                    }
                    DbType dbType = PropertyDataExtractor.GetDbTypeEnum(p);

                    PropertyValidator.ValidateMinMax(p, value);

                    executor.AddParameter(value, dbType); 
                }

                executor.AddParameter(id, DbType.Int32);

                count = executor.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return count;
        }
        #endregion
        
        #region int Delete(ITransactionManager tm, T item)
        public int Delete(ITransactionManager tm, T item)
        {
            int delCount = -1;

            try
            {
                Type pocoT = typeof(T);
                string fullClassName = pocoT.FullName;
                Mapping mapping = OrmEngine.GetMapping(fullClassName);
                //if (mapping == null)
                //{
                //    throw new Exception(fullClassName + ErrorMessages.WasNotFound);
                //}

                string key = fullClassName + "." + QueryNameConst.DeleteByID;
                string SQL = MappingDataExtractor.GetSqlQuery(mapping, key);
                //if (string.IsNullOrEmpty(SQL))
                //{
                //    throw new Exception(key + ErrorMessages.WasNotFound);
                //}

                string colName = MappingDataExtractor.GetIdColumnName(mapping);
                PropertyInfo propInfo = pocoT.GetProperty(colName);
                object id = propInfo.GetValue(item, null);

                QueryExecutor<T> executor = new QueryExecutor<T>(tm);
                executor.CreateSqlCommand(SQL);
                executor.AddParameter(id, DbType.Int32);
                
                delCount = executor.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return delCount;
        }
        #endregion

        #region IList<Tcom> Get<Tcom>(ITransactionManager tm, string SQL, params object[] parameters)
        public IList<Tcom> Get<Tcom>(ITransactionManager tm, string SQL, params object[] parameters)
        {
            IList<Tcom> list = null;

            ISafeDataReader dataReader = null;

            try
            {
                QueryExecutor<Tcom> executor = new QueryExecutor<Tcom>(tm);
                executor.CreateSqlCommand(SQL);
                foreach(Object o in parameters)
                {
                    executor.AddParameter(o, DbType.Object);
                }

                dataReader = executor.ExecuteReader();

                while (dataReader.Read())
                {
                    Tcom tempO = Activator.CreateInstance<Tcom>();

                    Type tcom = tempO.GetType();

                    foreach (PropertyInfo pi in tcom.GetProperties())
                    {
                        if (PrimitiveTypeChecker.IsSystemType(pi.PropertyType))
                        {
                            try
                            {
                                Object objValue = dataReader.GetObject(pi.Name);

                                pi.SetValue(tempO, objValue, null);
                            }
                            catch { }
                        }
                    }

                    if (list == null)
                    {
                        list = new List<Tcom>();
                    }

                    list.Add(tempO);
                }

                dataReader.Close();
            }
            catch (Exception ex)
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader = null;
                }

                throw ex;
            }

            return list;
        }
        #endregion
    }
}
