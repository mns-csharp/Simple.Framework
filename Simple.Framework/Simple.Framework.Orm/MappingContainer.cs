using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;

namespace Simple.Framework.Orm
{
    class MappingContainer
    {
        private Dictionary<string, Mapping> mapingsDictionary { get; set; }

        public MappingContainer()
        {
            mapingsDictionary = new Dictionary<string, Mapping>();
        }

        public void AddMapping(KeyValuePair<string, Mapping> keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue.Key))
            {
                if (!mapingsDictionary.ContainsKey(keyValue.Key))
                {
                    mapingsDictionary.Add(keyValue.Key, keyValue.Value);
                }
            }
        }

        public Mapping GetMapping(string key)
        {
            Mapping mapping = null;

            if (mapingsDictionary.ContainsKey(key))
            {
                mapping = mapingsDictionary[key];
            }

            return mapping;
        }

        public void ClearMappings()
        {
            if (mapingsDictionary != null)
            {
                if (mapingsDictionary.Count > 0)
                {
                    mapingsDictionary.Clear();
                }
            }
        }
    }
}