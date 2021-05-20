using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Framework.Orm
{
    public class KeyMaker
    {
        public static string GetFullyQualifiedName(Mapping mapping)
        {
            StringBuilder sb = new StringBuilder(mapping.NamespaceName);
            sb.Append(".");
            sb.Append(mapping.Class.Name);

            return sb.ToString();
        }

        public static string GetKey(Mapping mapping, string queryName)
        {
            return GetFullyQualifiedName(mapping) + "." + queryName;
        }

        public static string GetKey(Mapping mapping, string queryName, string fieldName)
        {
            return GetFullyQualifiedName(mapping) + "." + queryName + fieldName;
        }
    }
}
