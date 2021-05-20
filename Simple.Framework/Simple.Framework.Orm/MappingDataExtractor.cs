using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Simple.Framework.Orm
{
    public enum Mechanism
    {
        AutoIncrement,
        PivotTable
    }

    public static class MappingDataExtractor
    {
        public static string GetNamespace(Mapping mapping)
        {
            return mapping.NamespaceName;
        }

        public static string GetClassName(Mapping mapping)
        {
            return mapping.Class.Name;
        }

        public static string GetTableName(Mapping mapping)
        {
            if (string.IsNullOrEmpty(mapping.Class.TableName))
            {
                return mapping.Class.Name;
            }
            else
            {
                return mapping.Class.TableName;
            }
        }

        public static string GetIdName(Mapping mapping)
        {
            return mapping.Class.Id.Name;
        }

        public static string GetIdColumnName(Mapping mapping)
        {
            if (string.IsNullOrEmpty(mapping.Class.Id.ColumnName))
            {
                return mapping.Class.Id.Name;
            }
            else
            {
                return mapping.Class.Id.ColumnName;
            }
        }

        public static Mechanism GetGeneratorMechanism(Mapping mapping)
        {
            if (mapping.Class.Id.Generator == null)
            {
                return Mechanism.PivotTable;
            }
            else
            {
                return (Mechanism) Enum.Parse(typeof(Mechanism), mapping.Class.Id.Generator.Mechanism);
            }
        }

        public static IList<Property> GetProperties(Mapping mapping)
        {
            return new List<Property>(mapping.Class.Properties);
        }

        public static string GetSqlQuery(Mapping mapping, string key)
        {
            return mapping.MappingSqlContainer.GetSql(key);
        }
    }

    public static class PropertyDataExtractor
    {
        public static string GetName(Property property)
        {
            return property.Name;
        }

        public static string GetColumnName(Property property)
        {
            if (string.IsNullOrEmpty(property.ColumnName))
            {
                return property.Name;
            }
            else
            {
                return property.ColumnName;
            }
        }

        public static Type GetType(Property property)
        {
            Type t = Type.GetType(property.TypeName);

            if (t == null)
            {
                t = OrmEngine.MappingAssembly.GetType(property.TypeName);
            }

            return t;
        }

        public static DbType GetDbTypeEnum(Property property)
        {
            DbType dbType = DbType.Object;

            Type t = GetType(property);

            if (!t.IsEnum)
            {
                switch (t.FullName)
                {
                    case "System.Byte[]":
                        dbType = DbType.Binary;
                        break;

                    case "System.Char":
                    case "System.String":
                        dbType = DbType.String;
                        break;

                    case "System.Guid":
                        dbType = DbType.Guid;
                        break;

                    case "System.Boolean":
                        dbType = DbType.Boolean;
                        break;

                    case "System.Byte":
                        dbType = DbType.Byte;
                        break;

                    case "System.Int16":
                        dbType = DbType.Int16;
                        break;

                    case "System.Int32":
                        dbType = DbType.Int32;
                        break;

                    case "System.Int64":
                        dbType = DbType.Int64;
                        break;

                    case "System.Single":
                        dbType = DbType.Single;
                        break;

                    case "System.Double":
                        dbType = DbType.Double;
                        break;

                    case "System.Decimal":
                        dbType = DbType.Decimal;
                        break;

                    case "System.DateTime":
                        dbType = DbType.DateTime;
                        break;

                    case "System.Object":
                        dbType = DbType.Object;
                        break;

                    case "System.UInt16":
                        dbType = DbType.UInt16;
                        break;

                    case "System.UInt32":
                        dbType = DbType.UInt32;
                        break;

                    case "System.UInt64":
                        dbType = DbType.UInt64;
                        break;
                }
            }
            else
            {
                dbType = DbType.Int16;
            }

            return dbType;
        }

        public static bool IsNullable(Property property)
        {
            return property.IsNullable;
        }

        public static object GetDefaultNullvalue(Property property)
        {
            TypeCode tCode = Type.GetTypeCode(GetType(property));
            
            object obj = null;

            if (property.DefaultNullValue != null)
            {
                obj = Convert.ChangeType(property.DefaultNullValue, tCode);
            }

            return obj;
        }

        public static object GetMinValue(Property property)
        {
            TypeCode typeCode = Type.GetTypeCode(GetType(property));
            
            Object value = null;

            if (property.Min != null)
            {
                if (typeCode == TypeCode.String)
                {
                    value = Convert.ChangeType(property.Min.Value, TypeCode.Int32);
                }
                else
                {
                    value = Convert.ChangeType(property.Min.Value, typeCode);
                }
            }

            return value;
        }

        public static string GetMinValueErrorMessage(Property property)
        {
            string msg = string.Empty;

            if (property.Min != null)
            {
                msg = property.Min.ErrorMessage;
            }

            return msg;
        }

        public static object GetMaxValue(Property property)
        {
            TypeCode typeCode = Type.GetTypeCode(GetType(property));
            
            Object value = null;

            if (property.Max != null)
            {
                if (typeCode == TypeCode.String)
                {
                    value = Convert.ChangeType(property.Max.Value, TypeCode.Int32);
                }
                else
                {
                    value = Convert.ChangeType(property.Max.Value, typeCode);
                }
            }

            return value;
        }

        public static string GetMaxValueErrorMessage(Property property)
        {
            string msg = string.Empty;

            if (property.Max != null)
            {
                msg = property.Max.ErrorMessage;
            }

            return msg;
        }

        //public static Object GetTag(Property property)
        //{
        //    return property.Tag;
        //}
    }
}
