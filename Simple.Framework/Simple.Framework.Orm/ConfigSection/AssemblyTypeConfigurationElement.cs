using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Simple.Framework.Orm
{
    public class AssemblyTypeConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("assemblyDllFileName", IsRequired = true)]
        public string AssemblyDllFileName
        {
            get { return this["assemblyDllFileName"] as string; }
        }
    }
}
