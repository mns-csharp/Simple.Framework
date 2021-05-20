using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Framework.Orm
{
    public class SQLContainer
    {
        private Dictionary<string, string> dictionary { get; set; }

        public SQLContainer()
        {
            dictionary = new Dictionary<string, string>();
        }

        public void AddSql(KeyValuePair<string, string> keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue.Key))
            {
                if (!dictionary.ContainsKey(keyValue.Key))
                {
                    dictionary.Add(keyValue.Key, keyValue.Value);
                }
            }
        }

        public string GetSql(string key)
        {
            string sql = string.Empty;

            if (dictionary.ContainsKey(key))
            {
                sql = dictionary[key];
            }

            return sql;
        }

        public void ClearSqlQueries()
        {
            if (dictionary != null)
            {
                if (dictionary.Count > 0)
                {
                    dictionary.Clear();
                }
            }
        }
    }
}
