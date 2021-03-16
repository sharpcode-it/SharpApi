using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using SharpApi.Helpers.ValueTypeExtensions;

namespace SharpApi.Helpers.ObjectExtensions.Serialization
{
    public static partial class Serialize
    {
        public static string ToXml<T>(this T value,XmlWriterSettings xmlWriterSettings=null!)
        {
            if (value == null) return string.Empty;
            var typeObject = typeof(T);

            if (!typeObject.IsSerializable) return string.Empty;

            var xmlserializer = new XmlSerializer(typeof(T));
            var stringWriter = new StringWriter();
            
            using var writer = XmlWriter.Create(stringWriter, xmlWriterSettings);
            xmlserializer.Serialize(writer, value);
            return stringWriter.ToString();
        }

        public static XmlDocument ToXmlDocument<T>(this T value)
        {
            var xmlString = value.ToXml();
            if (xmlString.IsNullOrEmpty()) return new XmlDocument();

            var xmlDocument =  new XmlDocument();
            xmlDocument.LoadXml(xmlString);
            return xmlDocument;
        }
    }
}
