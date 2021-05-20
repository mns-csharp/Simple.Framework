using System;
using System.Data;
using Simple.Framework;

namespace SimpleDataDrivenApplication
{
    class MyTestTable
    {
        public int ID { get; set; }
        public string TableName { get; set; }
        public DateTime TableDate { get; set; }
        public byte[] TablePicture { get; set; }

        public static MyTestTable GetTestObject()
        {
            MyTestTable newObj = new MyTestTable();
            newObj.TableName = "Mohammad Saqib";
            newObj.TableDate = DateTime.Now;
            newObj.TablePicture = ConvertImage.ToByteArray("MyPicture.png");
            return newObj;
        }
    }

    public class MainClass
    {
        public static void Main(string [] args)
        {
            MyTestTable newObject = MyTestTable.GetTestObject();

            //string connString = @"Data Source=sql11.freesqldatabase.com,3306;Network Library=DBMSSOCN;Initial Catalog=sql11413365;User ID=sql11413365;Password=ep4MzxVN3W;";
            string connString = @"Data Source =.\;Initial Catalog=sql11413365;Integrated Security=True";
            string providerName = "System.Data.SqlClient";
            TransactionManager tm = new TransactionManager(connString, providerName);
            

            try
            {
                tm.BeginTransaction();
                PivotTable.DeleteEntries(tm);
                //PivotTable.DeleteTable(tm);
                QueryExecutor<MyTestTable> deleteRowsQuery = new QueryExecutor<MyTestTable>(tm);
                deleteRowsQuery.CreateSqlCommand(@"delete from MyTestTable");
                deleteRowsQuery.ExecuteNonQuery();
                tm.CommitTransaction();

                tm.BeginTransaction();                
                int? newID = PivotTable.GetNextID(tm, "MyTestTable");
                newObject.ID = (int) newID;               

                QueryExecutor<MyTestTable> insertQuery = new QueryExecutor<MyTestTable>(tm);
                insertQuery.CreateSqlCommand(@"INSERT INTO MyTestTable(ID, TableName, TableDate, TablePicture) 
                                            VALUES (@ID, @TableName, @TableDate, @TablePicture)");
                insertQuery.AddParameter(newObject.ID, DbType.Int16);
                insertQuery.AddParameter(newObject.TableName, DbType.String);
                insertQuery.AddParameter(newObject.TableDate, DbType.DateTime);
                insertQuery.AddParameter(newObject.TablePicture, DbType.Binary);
                insertQuery.ExecuteNonQuery();                
                PivotTable.UpdateNewID(tm, "MyTestTable", newID);                
                tm.CommitTransaction();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //Console.ReadLine();
        }
    }
}