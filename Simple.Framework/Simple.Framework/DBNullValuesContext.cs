using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlTypes;

namespace Simple.Framework
{
    public class DBNullValuesContext
    {
        private static DBNullContainer nullValueList = null;

        static DBNullValuesContext()
        {
            nullValueList = new DBNullContainer();
            nullValueList.AddDBNull(new KeyValuePair<string, INullable>("String", SqlString.Null));
            nullValueList.AddDBNull(new KeyValuePair<string, INullable>("Char", SqlChars.Null));
            nullValueList.AddDBNull(new KeyValuePair<string, INullable>("Double", SqlDouble.Null));
            nullValueList.AddDBNull(new KeyValuePair<string, INullable>("Decimal", SqlDecimal.Null));
            nullValueList.AddDBNull(new KeyValuePair<string, INullable>("Float", SqlSingle.Null));
            nullValueList.AddDBNull(new KeyValuePair<string, INullable>("Int16", SqlInt16.Null));
            nullValueList.AddDBNull(new KeyValuePair<string, INullable>("Int32", SqlInt32.Null));
            nullValueList.AddDBNull(new KeyValuePair<string, INullable>("Int64", SqlInt64.Null));
            nullValueList.AddDBNull(new KeyValuePair<string, INullable>("UInt16", SqlInt16.Null));
            nullValueList.AddDBNull(new KeyValuePair<string, INullable>("UInt32", SqlInt32.Null));
            nullValueList.AddDBNull(new KeyValuePair<string, INullable>("UInt64", SqlInt64.Null));
            nullValueList.AddDBNull(new KeyValuePair<string, INullable>("Byte", SqlByte.Null));
            nullValueList.AddDBNull(new KeyValuePair<string, INullable>("Byte[]", SqlBinary.Null));
            nullValueList.AddDBNull(new KeyValuePair<string, INullable>("Boolean", SqlBoolean.Null));
            nullValueList.AddDBNull(new KeyValuePair<string, INullable>("DateTime", SqlDateTime.Null));
        }

        public INullable GetDBNullValue(string key)
        {
            return nullValueList.GetDBNull(key);
        }
    }
}
