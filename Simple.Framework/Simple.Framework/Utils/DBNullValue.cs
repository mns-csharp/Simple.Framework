using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlTypes;
using System.Data;

namespace Simple.Framework
{
    public class DBNullValue
    {
        public static Object GetNullValue(DbType dataType)
        {
            Object returnValue;

            switch (dataType)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.String:
                case DbType.StringFixedLength:
                    returnValue = SqlString.Null;
                    break;

                case DbType.Double:
                    returnValue = SqlDouble.Null;
                    break;

                case DbType.Decimal:
                case DbType.VarNumeric:
                    returnValue = SqlDecimal.Null;
                    break;

                case DbType.Single:
                    returnValue = SqlSingle.Null;
                    break;

                case DbType.Int16:
                    returnValue = SqlInt16.Null;
                    break;

                case DbType.Int32:
                    returnValue = SqlInt32.Null;
                    break;

                case DbType.Int64:
                    returnValue = SqlInt64.Null;
                    break;

                case DbType.UInt16:
                    returnValue = SqlInt32.Null;
                    break;

                case DbType.UInt32:
                    returnValue = SqlInt64.Null;
                    break;

                case DbType.UInt64:
                    returnValue = SqlInt64.Null;
                    break;

                case DbType.Byte:
                case DbType.SByte:
                    returnValue = SqlByte.Null;
                    break;

                case DbType.Binary:
                    returnValue = SqlBinary.Null;
                    break;

                case DbType.Boolean:
                    returnValue = SqlBoolean.Null;
                    break;

                case DbType.DateTime:
                case DbType.Date:
                case DbType.DateTime2:
                case DbType.DateTimeOffset:
                case DbType.Time:
                    returnValue = SqlDateTime.Null;
                    break;

                case DbType.Object:
                default:
                    returnValue = DBNull.Value;
                    break;
            }
            
            return returnValue;
        }
    }
}
