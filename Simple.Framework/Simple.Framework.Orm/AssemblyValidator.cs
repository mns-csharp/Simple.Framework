using System;
using System.Collections.Generic;
using System.Text;
using Simple.Framework.Orm;
using System.Reflection;

namespace Simple.Framework
{
    public class AssemblyValidator
    {
        public static void ValidateManifest(Assembly assembly)
        {
            List<Type> types = new List<Type>(assembly.GetTypes());
            List<string> manifestResourceNames = new List<string>(assembly.GetManifestResourceNames());

            if (manifestResourceNames.Count <= 0)
            {
                throw new Exception("There are no XML \"Mapping\"s available for the ORM Engine to work on.");
            }

            foreach (Type t in types)
            {
                //List the interfaces implemented by t.
                List<Type> interfaceTypes = new List<Type>(t.GetInterfaces());

                //If t implements IBaseClassInterface, it is a candidate
                //for having a corresponding xml file.
                if (interfaceTypes.Contains(typeof(IBaseClassInterface)))
                {
                    string xmlFileName = t.Name + ".xml";

                    bool contains = false;

                    foreach (string man in manifestResourceNames)
                    {
                        contains = man.Contains(xmlFileName);

                        if (contains)
                        {
                            break;
                        }
                    }

                    if (!contains)
                    {
                        throw new Exception("Type [" + t.FullName + "] doesn't have any corresponding XML configuration!");
                    }
                }
            }
        }

        public static void ValidateDataType(Mapping mapping, Assembly assembly)
        {
            string fullName = MappingDataExtractor.GetNamespace(mapping) +"."+ MappingDataExtractor.GetClassName(mapping);

            Type type = assembly.GetType(fullName);

            if (type == null)
            {
                throw new Exception("Type ["+ MappingDataExtractor.GetClassName(mapping) + "] was not found in the assembly [" + assembly.FullName + "].");
            }
            else
            {
                foreach (Property prop in MappingDataExtractor.GetProperties(mapping))
                {
                    PropertyInfo pi = type.GetProperty(PropertyDataExtractor.GetName(prop));

                    if (pi == null)
                    {
                        throw new Exception("Property [" + PropertyDataExtractor.GetName(prop) + "] was not found in class [" + type.FullName + "].");
                    }

                    if (PropertyDataExtractor.GetType(prop).FullName != "System.String")
                    {
                        if (PropertyDataExtractor.IsNullable(prop) != NullProcessor.IsNullableType(pi.PropertyType))
                        {
                            throw new Exception("Property [" + PropertyDataExtractor.GetName(prop) + "] in class [" + type.FullName + "] should be [nullable=" + !PropertyDataExtractor.IsNullable(prop) + "].");
                        }
                    }

                    Type propT = null;

                    if (!pi.PropertyType.IsGenericType)
                    {
                        propT = pi.PropertyType;
                    }
                    else
                    {
                        propT = pi.PropertyType.GetGenericArguments()[0];
                    }

                    if (propT.FullName != PropertyDataExtractor.GetType(prop).FullName)
                    {
                        throw new Exception("Type mismatch of property [" + PropertyDataExtractor.GetName(prop) + "] in class [" + type.FullName + "].");
                    }
                }
            }
        }
    }
}
