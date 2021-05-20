using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;

namespace Simple.Framework.Orm
{
    public static class OrmEngine
    {
        private static MappingContainer container = null;
        public static Assembly MappingAssembly { get; set; }

        public static void Initalize()
        {
            try
            {
                //Step 1. Gathering <assemblies> config section from App.config.
                AssembliesConfigurationSection assembliesConSec = AssembliesConfigurationSection.GetAssemblyDllFileNamesFromAppConfig(); //"LoadHbmsToDictionary";//"How_To_Deserialize_a_Hbm_File";

                //Step 2. Collecting assembly name form the config-section-object.
                string assemblyFileName = assembliesConSec.MappingHbmAssembly.AssemblyDllFileName;

                //Step 3. Loading assembly-file into an object.
                Assembly assembly = Assembly.LoadFrom(assemblyFileName);
                MappingAssembly = assembly;

                string[] manifestResourceNames = assembly.GetManifestResourceNames();

                //Tests to see if there are xml files for 
                //every datatype.
                AssemblyValidator.ValidateManifest(assembly);

                Array.Sort(manifestResourceNames);
                //Array.Reverse(manifestResourceNames);

                KeyValuePair<string, string> keyValue;
                foreach (string mrn in manifestResourceNames)
                {
                    if (container == null)
                    {
                        container = new MappingContainer();
                    }

                    Stream stream = assembly.GetManifestResourceStream(mrn);
                    XmlSerializer serializer = new XmlSerializer(typeof(Mapping));

                    Mapping mapping = (Mapping)serializer.Deserialize(stream);                    
                    
                    //Test the xml so that it contains the required fields.
                    MappingValidator.Validate(mrn, mapping, assembly);
                    
                    //Test a dataType so that it conforms to a mapping
                    AssemblyValidator.ValidateDataType(mapping, assembly);

                    //Make the mapping Memory-resident.
                    container.AddMapping(new KeyValuePair<string, Mapping>(mapping.NamespaceName + "." + mapping.Class.Name, mapping));

                    SQLGenerator sqlGenerator = new SQLGenerator(mapping);

                    //SelectAll query
                    keyValue = sqlGenerator.GetAll();
                    mapping.MappingSqlContainer.AddSql(keyValue);

                    //SelectByID query
                    keyValue = sqlGenerator.GetByID();
                    mapping.MappingSqlContainer.AddSql(keyValue);

                    //Insert query
                    keyValue = sqlGenerator.InsertByID();
                    mapping.MappingSqlContainer.AddSql(keyValue);

                    //Update query
                    keyValue = sqlGenerator.UpdateByID();
                    mapping.MappingSqlContainer.AddSql(keyValue);

                    //Delete query
                    keyValue = sqlGenerator.DeleteByID();
                    mapping.MappingSqlContainer.AddSql(keyValue);

                    //XXXXBy.'fieldValue' queries
                    IList<Property> properties = MappingDataExtractor.GetProperties(mapping);
                    foreach (Property prop in properties)
                    {
                        //GetBy.'fieldValue' queries
                        keyValue = sqlGenerator.GetByFieldValue(PropertyDataExtractor.GetColumnName(prop));
                        prop.PropertySqlContainer.AddSql(keyValue);

                        //UpdateBy.'fieldValue' queries
                        keyValue = sqlGenerator.UpdateByFieldValue(PropertyDataExtractor.GetColumnName(prop));
                        prop.PropertySqlContainer.AddSql(keyValue);

                        //DeleteBy.'fieldValue' queries
                        keyValue = sqlGenerator.DeleteByFieldValue(PropertyDataExtractor.GetColumnName(prop));
                        prop.PropertySqlContainer.AddSql(keyValue);
                    }
                }
            }
            catch(Exception ex)
            {
                if (container != null)
                {
                    container.ClearMappings();
                }
                throw ex;
            }
        }

        public static Mapping GetMapping(string key)
        {
            return container.GetMapping(key);
        }
    }
}
