using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Simple.Framework.Orm
{
    internal static class DataReaderMapper
    {
        #region Map Single Item
        public static Tcom MapSingleItem<Tcom>(ISafeDataReader dataReader, Mapping mapping)
        {
            Type pocoT = typeof(Tcom);

            Tcom pocoObj = default(Tcom);            

            try
            {
                IList<Property> properties = MappingDataExtractor.GetProperties(mapping);
                
                if (pocoObj == null)
                {
                    pocoObj = (Tcom) Activator.CreateInstance(pocoT);
                }

                Object id = dataReader.GetInt32(MappingDataExtractor.GetIdColumnName(mapping));
                PropertyInfo propInfo = pocoT.GetProperty(MappingDataExtractor.GetIdName(mapping));
                propInfo.SetValue(pocoObj, id, null);
                foreach (Property p in properties)
                {
                    Type innerT = null;

                    propInfo = pocoT.GetProperty(PropertyDataExtractor.GetName(p));
                    if (propInfo == null)
                    {
                        throw new Exception("Property [" + PropertyDataExtractor.GetName(p) + "] was not found in [" + pocoT.FullName + "].");
                    }                    

                    if (propInfo.PropertyType.IsGenericType == false)
                    {
                        innerT = propInfo.PropertyType;
                    }
                    else
                    {
                        innerT = propInfo.PropertyType.GetGenericArguments()[0];
                    }

                    if (!innerT.IsEnum)
                    {
                        Object value = GetBasicTypeValue(dataReader, p, innerT.Name);

                        propInfo.SetValue(pocoObj, value, null);
                    }
                    else
                    {
                        Object enumValue = GetEnumTypeValue<Tcom>(dataReader, p, innerT.BaseType.Name);

                        propInfo.SetValue(pocoObj, enumValue, null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return pocoObj;
        }
        #endregion

        #region GetBasicTypeValue()
        private static Object GetBasicTypeValue(ISafeDataReader dataReader, Property prop, string innerTypeName)
        {
            Object value = dataReader.GetObject( PropertyDataExtractor.GetColumnName(prop));

            #region Commented Out
            /*
            switch (innerTypeName)
            {
                case "Boolean":
                    value = dataReader.GetBoolean(PropertyDataExtractor.GetColumnName(prop));
                    if (value == null)
                    {
                        if (prop.IsNullable)
                        {
                            value = Convert.ToBoolean(prop.DefaultNullValue);
                        }
                    }
                    break;
                case "SByte":
                    value = dataReader.GetSByte(PropertyDataExtractor.GetColumnName(prop));
                    if (value == null)
                    {
                        if (prop.IsNullable)
                        {
                            value = Convert.ToSByte(prop.DefaultNullValue);
                        }
                    }
                    break;
                case "Byte":
                    value = dataReader.GetByte(PropertyDataExtractor.GetColumnName(prop));
                    if (value == null)
                    {
                        if (prop.IsNullable)
                        {
                            value = Convert.ToByte(prop.DefaultNullValue);
                        }
                    }
                    break;
                case "Byte[]":
                    value = dataReader.GetByteArray(PropertyDataExtractor.GetColumnName(prop));
                    break;
                case "DateTime":
                    value = dataReader.GetDateTime(PropertyDataExtractor.GetColumnName(prop));
                    if (value == null)
                    {
                        if (prop.IsNullable)
                        {
                            value = DateTime.MinValue;
                        }
                    }                    
                    break;
                case "Int16":
                    value = dataReader.GetInt16(PropertyDataExtractor.GetColumnName(prop));
                    if (value == null)
                    {
                        if (prop.IsNullable)
                        {
                            value = Convert.ToInt16(prop.DefaultNullValue);
                        }
                    }
                    break;
                case "Int32":
                    value = dataReader.GetInt32(PropertyDataExtractor.GetColumnName(prop));
                    if (value == null)
                    {
                        if (prop.IsNullable)
                        {
                            value = Convert.ToInt32(prop.DefaultNullValue);
                        }
                    }
                    break;
                case "Int64":
                    value = dataReader.GetInt64(PropertyDataExtractor.GetColumnName(prop));
                    if (value == null)
                    {
                        if (prop.IsNullable)
                        {
                            value = Convert.ToInt64(prop.DefaultNullValue);
                        }
                    }
                    break;
                case "Single":
                    value = dataReader.GetFloat(PropertyDataExtractor.GetColumnName(prop));
                    if (value == null)
                    {
                        if (prop.IsNullable)
                        {
                            value = Convert.ToSingle(prop.DefaultNullValue);
                        }
                    }
                    break;
                case "Double":
                    value = dataReader.GetDouble(PropertyDataExtractor.GetColumnName(prop));
                    if (value == null)
                    {
                        if (prop.IsNullable)
                        {
                            value = Convert.ToDouble(prop.DefaultNullValue);
                        }
                    }
                    break;
                case "Decimal":
                    value = dataReader.GetDecimal(PropertyDataExtractor.GetColumnName(prop));
                    if (value == null)
                    {
                        if (prop.IsNullable)
                        {
                            value = Convert.ToDecimal(prop.DefaultNullValue);
                        }
                    }
                    break;
                case "Char":
                    value = dataReader.GetChar(PropertyDataExtractor.GetColumnName(prop));
                    if (value == null)
                    {
                        if (prop.IsNullable)
                        {
                            value = Convert.ToChar(prop.DefaultNullValue);
                        }
                    }
                    break;
                case "String":
                    value = dataReader.GetString(PropertyDataExtractor.GetColumnName(prop));
                    break;
                case "Object":
                    value = dataReader.GetObject(PropertyDataExtractor.GetColumnName(prop));
                    break;
                case "Guid":
                    value = dataReader.GetGuid(PropertyDataExtractor.GetColumnName(prop));
                    break;
            }*/
            
            #endregion

            return value;
        } 
        #endregion

        private static object GetEnumTypeValue<TCom>(ISafeDataReader dataReader, Property property, string baseTypeName)
        {
            object enumValue = null;

            switch (baseTypeName)
            {
                case "Enum":                    
                    Type enumPropertyT = PropertyDataExtractor.GetType(property);
                    
                    Object value = dataReader.GetInt16(PropertyDataExtractor.GetColumnName(property));

                    bool isNullable = PropertyDataExtractor.IsNullable(property);

                    if (value == null)
                    {
                        if (!isNullable)
                        {
                            throw new Exception("Could not assign null-value to a non-nullable property [" + PropertyDataExtractor.GetName(property) + "] in class [" + typeof(TCom).FullName + "].");
                        }
                        else
                        {
                            enumValue = Enum.ToObject(enumPropertyT, value);
                        }
                    }
                    else
                    {
                        enumValue = Enum.ToObject(enumPropertyT, value);
                    }
                    break;
            }

            return enumValue;
        }

        #region Map Multiple Items
        public static IList<Tcom> MapMultipleItems<Tcom>(ISafeDataReader dataReader, Mapping hbm)
        {
            IList<Tcom> list = null;
            Tcom obj = default(Tcom);

            try
            {
                while (dataReader.Read())
                {
                    if (list == null)
                    {
                        list = new List<Tcom>();
                    }

                    obj = MapSingleItem<Tcom>(dataReader, hbm);

                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }
        #endregion
    }
}
