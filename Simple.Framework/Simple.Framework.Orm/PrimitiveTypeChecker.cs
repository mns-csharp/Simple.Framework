using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Framework
{
    public class PrimitiveTypeChecker
    {
        private static readonly List<Type> PrimitiveDataTypes;

        static PrimitiveTypeChecker()
        {
            PrimitiveDataTypes = new List<Type>()
            {
                typeof(System.Int16),
                typeof(System.Int32),
                typeof(System.Int64),
                typeof(System.String),
                typeof(System.Byte),
                typeof(System.Char),
                typeof(System.DateTime),
                typeof(System.Decimal),
                typeof(System.Double),
                typeof(System.Enum),
                typeof(System.SByte),
                typeof(System.Single),
                typeof(System.String),
                typeof(System.UInt16),
                typeof(System.UInt32),
                typeof(System.UInt64)
            };            
        }

        public static bool IsSystemType(Type type)
        {
            bool isSystemType = false;

            Type innerT = null;

            if (type.IsGenericType == false)
            {
                innerT = type;
            }
            else
            {
                innerT = type.GetGenericArguments()[0];
            }

            if (innerT.IsEnum)
            {
                isSystemType = true;
            }
            else
            {
                if (PrimitiveDataTypes.Contains(innerT))
                {
                    isSystemType = true;
                }
                else
                {
                    isSystemType = false;
                }
            }

            return isSystemType;
        }
    }
}
