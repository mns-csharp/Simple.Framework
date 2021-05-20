using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Simple.Framework
{
    public class ConnectionStringSettingsContainer
    {
        private Dictionary<string, ConnectionStringSettings> tranMgrDistionary;

        public ConnectionStringSettingsContainer()
        {
            tranMgrDistionary = new Dictionary<string, ConnectionStringSettings>();
        }

        public void Add(KeyValuePair<string, ConnectionStringSettings> keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue.Key))
            {
                if (!tranMgrDistionary.ContainsKey(keyValue.Key))
                {
                    tranMgrDistionary.Add(keyValue.Key, keyValue.Value);
                }
            }
        }

        public ConnectionStringSettings Get(string key)
        {
            ConnectionStringSettings t = null;

            if (tranMgrDistionary.ContainsKey(key))
            {
                t = tranMgrDistionary[key];
            }

            return t;
        }

        public void Clear()
        {
            if (tranMgrDistionary != null)
            {
                if (tranMgrDistionary.Count > 0)
                {
                    tranMgrDistionary.Clear();
                }
            }
        }
    }
}
