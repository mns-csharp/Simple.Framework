/*------------------------------------------------------------------------------
 * Created     : 10.05.2011 
 * Programmer  : Md. Nazmul Saqib
 * E-mail      : edurazee@yahoo.com
 * 
 * Usage	   : This class is intended to check if the value of each of the class-
 *               properties is null or not.
 *               
 *               The method GetNullValue() returns an appropriate DBNull value 
 *               to be stored in the database table.
 *------------------------------------------------------------------------------
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlTypes;
using System.Collections;
using System.Reflection;

namespace Simple.Framework
{
    
    public class NullProcessor
    {
        public static bool IsNullableType(Type type)
        {
            if (type.IsClass)
            {
                return true;
            }

            if (!type.IsValueType)
            {
                return true;
            }

            if (!type.IsGenericType)
            {
                return false;
            }

            if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return true;
            }
            else
            {
                return false;
            }
        }    

        #region IsNullableType<T>(T obj)
        /// <summary>
        /// Tests a value to see if its type is Nullable.
        /// </summary>
        /// <typeparam name="T">Type of the argument value.</typeparam>
        /// <param name="obj">Value whose type is to be tested.</param>
        /// <returns>bool</returns>
        public static bool IsNullableType<T>(T obj)
        {
            if (obj == null)
            {
                return true; // obvious
            }

            Type type = typeof(T);

            if (!type.IsValueType)
            {
                return true; // ref-type
            }

            if (Nullable.GetUnderlyingType(type) != null)
            {
                return true; // Nullable<T>
            }

            return false; // value-type
        } 
        #endregion

        #region MyRegion
		/// <summary>
        /// Returns an appropriate database null value for a datatype.
        /// </summary>
        /// <param name="value">Value to be tested.</param>
        /// <returns>object</returns>
        //public static object GetDBNullValue<T>(object obj)
        //{
        //    object returns = obj;

        //    string name;// = VarDecToVarName.GetName<T>(obj);

        //    if (obj == null)
        //    {
        //        string typeName = typeof(T).Name;

        //        returns = DBNullValuesContext.GetDBNullValue(typeName);
        //    }

        //    return returns;
        //}

        //public static object GetNullValue(object value)
        //{
        //    object returnValue = value;
                        
        //    //if (!NullChecker.IsNullableType<object>(value))
        //    //{
        //    //    throw new Exception(CustomErrorMessage.DataTypeMustBeNullable);
        //    //}

        //    if (returnValue != null)
        //    {                 
        //        Type dataType = value.GetType();            

        //        if (!dataType.IsEnum)
        //        {
        //            switch (dataType.Name)
        //            {
        //                case "String":
        //                    returnValue = SqlString.Null;
        //                    break;

        //                case "Char":
        //                    returnValue = SqlChars.Null;
        //                    break;

        //                case "Double":
        //                    returnValue = SqlDouble.Null;
        //                    break;

        //                case "Deciaml":
        //                    returnValue = SqlDecimal.Null;
        //                    break;

        //                case "Float":
        //                    returnValue = SqlSingle.Null;
        //                    break;

        //                case "Int16":
        //                    returnValue = SqlInt16.Null;
        //                    break;

        //                case "Int32":
        //                    returnValue = SqlInt32.Null;
        //                    break;

        //                case "Int64":
        //                    returnValue = SqlInt64.Null;
        //                    break;

        //                case "UInt16":
        //                    returnValue = SqlInt32.Null;
        //                    break;

        //                case "UInt32":
        //                    returnValue = SqlInt64.Null;
        //                    break;

        //                case "UInt64":
        //                    returnValue = SqlInt64.Null;
        //                    break;

        //                case "Byte":
        //                    returnValue = SqlByte.Null;
        //                    break;

        //                case "Byte[]":
        //                    returnValue = SqlBinary.Null;
        //                    break;

        //                case "Boolean":
        //                    returnValue = SqlBoolean.Null;
        //                    break;

        //                case "DateTime":
        //                    returnValue = SqlDateTime.Null;
        //                    break;

        //                default:
        //                    returnValue = DBNull.Value;
        //                    break;
        //            }
        //        }
        //    }

        //    return returnValue;
        //} 
	#endregion
    }
}
