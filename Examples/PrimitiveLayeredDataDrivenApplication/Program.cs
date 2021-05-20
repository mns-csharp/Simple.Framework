using Simple.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimitiveLayeredDataDrivenApplication
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

    class MyTestTableDAO
    {
        public static MyTestTable GetByID(TransactionManager tm, int ID)
        {
            MyTestTable item = null;

            QueryExecutor<MyTestTable> query = new QueryExecutor<MyTestTable>(tm);
            query.CreateSqlCommand("select * from MyTestTable");
            ISafeDataReader reader = query.ExecuteReader();

            while (reader.Read())
            {
                if (item == null)
                {
                    item = new MyTestTable();
                }
                item.ID = (int) reader.GetInt32("ID");
                item.TableName = reader.GetString("TableName");
            }

            return item;
        }
    }

    class MyTestTableBO
    {
        TransactionManager tm;

        public MyTestTableBO()
        {
            string connString = @"Data Source =.\;Initial Catalog=sql11413365;Integrated Security=True";
            string providerName = "System.Data.SqlClient";
            tm = new TransactionManager(connString, providerName);
        }

        public MyTestTable Get(int ID)
        {
            MyTestTable item;

            tm.BeginTransaction();

            item = MyTestTableDAO.GetByID(tm, ID);

            tm.CommitTransaction();

            return item;
        }
    }
    

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
