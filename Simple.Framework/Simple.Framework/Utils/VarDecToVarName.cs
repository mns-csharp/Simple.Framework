using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Simple.Framework
{
    public class VarDecToVarName
    {
        public static string GetName<T>(T item) where T : class
        {
            string name = string.Empty;

            PropertyInfo [] properties = typeof(T).GetProperties();

            if (properties.Length == 1)
            {
                name = properties[0].Name;
            }
            
            return name;
        }
    }
}
