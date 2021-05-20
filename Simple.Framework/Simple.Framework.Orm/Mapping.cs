using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Simple.Framework.Orm
{
    public class Generator
    {
        [XmlAttribute("mechanism")]
        public string Mechanism { get; set; }
    }

    public class Min
    {
        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlAttribute("errMsg")]
        public string ErrorMessage { get; set; }
    }

    public class Max
    {
        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlAttribute("errMsg")]
        public string ErrorMessage { get; set; }
    }

    public class Id
    {
        [XmlAttribute("column")]
        public string ColumnName { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("generator")]
        public Generator Generator { get; set; }
    }

    public class Property
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("column")]
        public string ColumnName { get; set; }

        [XmlAttribute("nullable")]
        public bool IsNullable { get; set; }

        [XmlAttribute("default")]
        public string DefaultNullValue { get; set; }

        [XmlAttribute("type")]
        public string TypeName { get; set; }

        [XmlElement("min")]
        public Min Min { get; set; }

        [XmlElement("max")]
        public Max Max { get; set; }

        public SQLContainer PropertySqlContainer { get; set; }

        public Property()
        {
            PropertySqlContainer = new SQLContainer();
        }
    }

    public class Class
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("table")]
        public string TableName { get; set; }

        [XmlElement("id")]
        public Id Id { get; set; }

        [XmlElement("property")]
        public Property[] Properties { get; set; }
    }

    [XmlRoot("mapping", Namespace = "urn:sform.com")]
    public class Mapping
    {
        [XmlAttribute("namespace")]
        public string NamespaceName { get; set; }

        [XmlElement("class")]
        public Class Class { get; set; }

        public SQLContainer MappingSqlContainer { get; set; }

        public Mapping()
        {
            MappingSqlContainer = new SQLContainer();
        }
    }

    
}
