using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlTypes;

namespace Simple.Framework
{
    #region MyRegion
    internal class DBNullContainer
    {
        private Dictionary<string, INullable> nullableDistionary { get; set; }

        public DBNullContainer()
        {
            nullableDistionary = new Dictionary<string, INullable>();
        }

        public void AddDBNull(KeyValuePair<string, INullable> keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue.Key))
            {
                if (!nullableDistionary.ContainsKey(keyValue.Key))
                {
                    nullableDistionary.Add(keyValue.Key, keyValue.Value);
                }
            }
        }

        public INullable GetDBNull(string key)
        {
            INullable t = null;

            if (nullableDistionary.ContainsKey(key))
            {
                t = nullableDistionary[key];
            }

            return t;
        }

        public void ClearDBNulls()
        {
            if (nullableDistionary != null)
            {
                if (nullableDistionary.Count > 0)
                {
                    nullableDistionary.Clear();
                }
            }
        }

        public void ClearAll()
        {
            ClearDBNulls();
        }
    }
    #endregion
}
