using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Simple.Framework.Orm
{
    public class MappingValidator
    {
        public static void Validate(string fileName, Mapping mapping, Assembly assembly)
        {
            if (mapping == null)
            {
                throw new Exception("Please supply an XML Mapping!");
            }
            else 
            {
                if (string.IsNullOrEmpty(mapping.NamespaceName))
                {
                    throw new Exception("Missing \"Mapping.Namespace\" in file ["+ fileName +"].");
                }

                if (mapping.Class == null)
                {
                    throw new Exception("Missing \"Mapping.Class\" in file [" + fileName + "].");
                }
                else
                {
                    if (string.IsNullOrEmpty(mapping.Class.Name))
                    {
                        throw new Exception("Missing \"Mapping.Class.Name\" in file [" + fileName + "].");
                    }

                    if (mapping.Class.Id == null)
                    {
                        throw new Exception("Missing \"Mapping.Class.Id\" in file [" + fileName + "].");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(mapping.Class.Id.Name))
                        {
                            throw new Exception("Missing \"Mapping.Class.Id.Name\" in file [" + fileName + "].");
                        }
                        /*
                         * if(mapping.Class.Id.Generator==null)
                         * {
                         *      .......use......PivotTable......
                         * }
                         */
                        if (mapping.Class.Id.Generator != null)
                        {
                            if(string.IsNullOrEmpty(mapping.Class.Id.Generator.Mechanism))
                            {
                                throw new Exception("Missing \"Mapping.Class.Id.Generator.Mechanism\" in file [" + fileName + "].");
                            }
                        }

                        if (mapping.Class.Properties == null)
                        {
                            throw new Exception("Please supply at least one Property in file [" + fileName + "].");
                        }
                        else if(mapping.Class.Properties.Length > 0)
                        {
                            foreach (Property p in mapping.Class.Properties)
                            {
                                if (string.IsNullOrEmpty(p.Name))
                                {
                                    throw new Exception("Please supply a Property-name in file [" + fileName + "].");
                                }

                                if (string.IsNullOrEmpty(p.TypeName))
                                {
                                    throw new Exception("Please supply a Type-name in file [" + fileName + "].");
                                }
                                else
                                {
                                    Type t = Type.GetType(p.TypeName);
                                    
                                    if (t == null)
                                    {
                                        t = assembly.GetType(p.TypeName);

                                        if (t == null)
                                        {
                                            throw new Exception("Type [" + p.TypeName + "] was not found in [" + mapping.Class.Name + "." + p.Name + "] in file [" + fileName + "].");
                                        }
                                        else
                                        {
                                            if (!t.IsEnum)
                                            {
                                                throw new Exception("Type [" + p.TypeName + "] must be a CLR type or Enum in [" + mapping.Class.Name + "." + p.Name + "] in file [" + fileName + "].");
                                            }
                                        }
                                    }
                                }

                                if (p.Min != null)
                                {
                                    if (string.IsNullOrEmpty(p.Min.Value))
                                    {
                                        throw new Exception("Please supply a Min-value in property [" + p.Name + "] in file [" + fileName+"].");
                                    }
                                }

                                if (p.Max != null)
                                {
                                    if (string.IsNullOrEmpty(p.Max.Value))
                                    {
                                        throw new Exception("Please supply a Max-value in property [" + p.Name + "] in file [" + fileName + "].");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
