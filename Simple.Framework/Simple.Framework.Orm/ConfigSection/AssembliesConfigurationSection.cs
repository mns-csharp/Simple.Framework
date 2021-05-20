using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Simple.Framework.Orm
{
    public class AssembliesConfigurationSection : ConfigurationSection
    {
        #region must be here
        private static string _rootElement = "assemblies";

        public static AssembliesConfigurationSection GetAssemblyDllFileNamesFromAppConfig()
        {
            AssembliesConfigurationSection su = (AssembliesConfigurationSection)ConfigurationManager.GetSection(AssembliesConfigurationSection._rootElement) ?? new AssembliesConfigurationSection();

            return su;
        }
        #endregion

        [ConfigurationProperty("mappingHbmAssembly", IsRequired = true)]
        public AssemblyTypeConfigurationElement MappingHbmAssembly
        {
            get { return this["mappingHbmAssembly"] as AssemblyTypeConfigurationElement; }
        }

        [ConfigurationProperty("repositoryAssembly", IsRequired = true)]
        public AssemblyTypeConfigurationElement RepositoryAssembly
        {
            get { return this["repositoryAssembly"] as AssemblyTypeConfigurationElement; }
        }
    }
}
