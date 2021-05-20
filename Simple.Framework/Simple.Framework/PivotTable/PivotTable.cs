/*------------------------------------------------------------------------------
 * Created     : 10.05.2011 
 * Programmer  : Md. Nazmul Saqib
 * E-mail      : edurazee@yahoo.com
 * 
 * Usage	   : This class is intended to manage data values 
 *               to and form pivot table.
 *------------------------------------------------------------------------------
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Simple.Framework;

namespace Simple.Framework
{
    public class PivotTable
    {
        public string TableName { get; set; }
        public int? CurrentMaxID { get; set; }

        private static bool CreatePivotTable(ITransactionManager tm)
        {
            bool success = false;

            try
            {
                QueryExecutor<PivotTable> queryExecutor = new QueryExecutor<PivotTable>(tm);

                queryExecutor.CreateSqlCommand(@"CREATE TABLE PivotTable(TableName varchar(50) NOT NULL PRIMARY KEY, CurrentMaxID int NOT NULL);");
                queryExecutor.ExecuteNonQuery();

                success = true;
            }
            catch (Exception)
            {
                throw;
            }

            return success;
        }


        private static bool PivotTableAlreadyCreated(ITransactionManager tm)
        {
            bool exists;
            // ANSI SQL way.  Works in PostgreSQL, MSSQL, MySQL.  
            IQueryExecutor<PivotTable> queryExecutor = new QueryExecutor<PivotTable>(tm);
            try
            {
                queryExecutor.CreateSqlCommand(
                  "select case when exists((select * from information_schema.tables where table_name = @table_name)) then 1 else 0 end");
                queryExecutor.AddParameter("PivotTable", System.Data.DbType.String);

                exists = queryExecutor.ExecuteScalar() == 1;
            }
            catch
            {
                try
                {
                    // Other RDBMS.  Graceful degradation
                    exists = true;
                    queryExecutor.CreateSqlCommand("select 1 from @table_name where 1 = 0");
                    queryExecutor.AddParameter("PivotTable", System.Data.DbType.String);
                    queryExecutor.ExecuteNonQuery();
                }
                catch
                {
                    exists = false;
                }
            }

            return exists;
        }

        #region Private Methods
        /// <summary>
        /// Gets PivotTable-data from the database by a tableName.
        /// </summary>
        /// <param name="tm">The transaction manager object which represents a transaction context.</param>
        /// <param name="tableName">The table name of which we want to retrieve data.</param>
        /// <returns>PivotTable</returns>
        private static PivotTable Get(ITransactionManager tm, string tableName)
        {
            PivotTable pivotTable = null;

            IQueryExecutor<PivotTable> queryExecutor = new QueryExecutor<PivotTable>(tm);

            queryExecutor.CreateSqlCommand(@"SELECT TableName, CurrentMaxID FROM PivotTable WHERE TableName=@TableName");
            queryExecutor.AddParameter(tableName, System.Data.DbType.String);


            ISafeDataReader dataReader = queryExecutor.ExecuteReader();

            try
            {
                while (dataReader.Read())
                {
                    if (pivotTable == null)
                    {
                        pivotTable = new PivotTable();
                    }

                    pivotTable.TableName = dataReader.GetString("TableName");
                    pivotTable.CurrentMaxID = dataReader.GetInt32("CurrentMaxID");
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

            return pivotTable;
        }

        /// <summary>
        /// Saves a PivotTable-data to the database.
        /// </summary>
        /// <param name="tm">The transaction manager object which represents a transaction context.</param>
        /// <param name="item">The PivotTable object of which we want to save data.</param>
        /// <returns>int</returns>
        private static int Save(ITransactionManager tm, PivotTable item)
        {
            int count = -1;

            try
            {
                QueryExecutor<PivotTable> queryExecutor = new QueryExecutor<PivotTable>(tm);

                queryExecutor.CreateSqlCommand(@"INSERT INTO PivotTable(TableName, CurrentMaxID) VALUES(@TableName, @CurrentMaxID)");
                queryExecutor.AddParameter(item.TableName, System.Data.DbType.String);
                queryExecutor.AddParameter(item.CurrentMaxID, System.Data.DbType.Int32);

                queryExecutor.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

            return count;
        }

        /// <summary>
        /// Updates the CurrentMaxID-value for a database table.
        /// </summary>
        /// <param name="tm">The transaction manager object which represents a transaction context.</param>
        /// <param name="item">The PivotTable object of which we want to update data.</param>
        /// <returns>int</returns>
        private static int Update(ITransactionManager tm, PivotTable item)
        {
            int count = -1;

            try
            {
                QueryExecutor<PivotTable> queryExecutor = new QueryExecutor<PivotTable>(tm);

                queryExecutor.CreateSqlCommand(@"UPDATE PivotTable
                                            SET 
                                        CurrentMaxID = @CurrentMaxID
                                            WHERE 
                                        TableName = @TableName");
                queryExecutor.AddParameter(item.CurrentMaxID, System.Data.DbType.Int32);
                queryExecutor.AddParameter(item.TableName, System.Data.DbType.String);

                count = queryExecutor.ExecuteNonQuery();
                                            //new DbParameter(item.CurrentMaxID, System.Data.DbType.Int32),
                                            //new DbParameter(item.TableName, System.Data.DbType.String));
            }
            catch (Exception)
            {
                throw;
            }

            return count;
        }

        /// <summary>
        /// Determines if the entry for a table already exists in the pivot-table.
        /// </summary>
        /// <param name="tm">The transaction manager object which represents a transaction context.</param>
        /// <param name="tableName">The table name of which we want to test the existence of data.</param>
        /// <returns>bool</returns>
        private static bool EntryExists(ITransactionManager tm, string tableName)
        {
            bool exists = true;

            try
            {
                QueryExecutor<PivotTable> queryExecutor = new QueryExecutor<PivotTable>(tm);
                queryExecutor.CreateSqlCommand(@"select count(TableName) from PivotTable where TableName=@TableName");
                queryExecutor.AddParameter(tableName, System.Data.DbType.String);
                
                int count = queryExecutor.ExecuteScalar();
                

                if (count == 0)
                {
                    exists = false;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return exists;
        } 
        #endregion

        public static int? GetNextID(ITransactionManager tm, string tableName)
        {
            int? nextId = 0;

            bool created = PivotTable.PivotTableAlreadyCreated(tm);

            if (!created)
            {
                created = PivotTable.CreatePivotTable(tm);
            }

            PivotTable pvtTable;

            bool entryExists = PivotTable.EntryExists(tm, tableName);

            if (!entryExists)
            {
                pvtTable = new PivotTable { TableName = tableName, CurrentMaxID = 0};

                PivotTable.Save(tm, pvtTable);
            }
                
            return ++nextId;
        }

        public static int UpdateNewID(ITransactionManager tm, string tableName, int? id)
        {
            PivotTable pvtTable = new PivotTable();
            pvtTable.CurrentMaxID = id;
            pvtTable.TableName = tableName;
            
            return PivotTable.Update(tm, pvtTable);
        }

        public static int DeleteEntries(ITransactionManager tm)
        {
            int returns = -1;

            try
            {
                QueryExecutor<PivotTable> queryExecutor = new QueryExecutor<PivotTable>(tm);

                queryExecutor.CreateSqlCommand(@"DELETE FROM PivotTable");
                returns = queryExecutor.ExecuteNonQuery();
            }
            catch (Exception)
            {
                returns = -1;
            }

            return returns;
        }

        public static int DeleteTable(ITransactionManager tm)
        {
            int returns = -1;

            try
            {
                QueryExecutor<PivotTable> queryExecutor = new QueryExecutor<PivotTable>(tm);

                queryExecutor.CreateSqlCommand(@"DROP TABLE PivotTable");
                returns = queryExecutor.ExecuteNonQuery();
            }
            catch (Exception)
            {
                returns = -1;
            }

            return returns;
        }
    }
}
