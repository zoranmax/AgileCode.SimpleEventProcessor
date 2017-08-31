using System;
using System.IO;
using System.Xml.Serialization;

namespace AgileCode.SimpleEventProcessor.Tests.Helper
{
    /// <summary>
    /// Helper class to simplify the de-/seralisation of objects.
    /// </summary>
    public class GenericSerializer  
    {
        //---- Members --------------------------------------------
        private readonly XmlAttributeOverrides _attributes;
        private XmlSerializer _xmlSerializer;

        //---------------------------------------------------------
        private GenericSerializer()
        {

        }

        //---------------------------------------------------------
        /// <summary>
        /// Creates an object of type <typeparam name="T">TSourceProperty</typeparam> from
        /// a xml string.
        /// </summary>
        public static T DeserializeFromString<T>(string xml)
            where T : class
        {
            return new GenericSerializer().FromString<T>(xml);
        }


        //---------------------------------------------------------
        /// <summary>
        /// Serializes an object of type <typeparam name="T">TSourceProperty</typeparam> 
        /// to a xml string.
        /// </summary>
        public static string SerializeToString<T>(T value) where T : class
        {
            return new GenericSerializer().ToString(value);
        }


        //---------------------------------------------------------
        /// <summary>
        /// Serializes an object of type <typeparam name="T">TSourceProperty</typeparam> 
        /// to a xml string.
        /// </summary>
        public static string SerializeToXml<T>(T value) where T : class
        {
            return new GenericSerializer().ToString(value);
        }

        //---------------------------------------------------------
        public string ToString<T>(T value) where T : class
        {
            var type = typeof(T);
            if (value == null)
            {
                return "<null>";
            }
            if (typeof(T) == typeof(object))
            {
                type = value.GetType();
            }

            XmlSerializer xmlSerializer = CreateSerializer(type);
            using (var writer = new StringWriter())
            {
                xmlSerializer.Serialize(writer, value);

                return writer.ToString();
            }
        }

        //---------------------------------------------------------
        public T FromString<T>(string xml) where T : class
        {
            XmlSerializer serializer = CreateSerializer<T>();
            using (var sr = new StringReader(xml))
            {
                T currentObject = (T)serializer.Deserialize(sr);
                return currentObject;
            }
        }

        //---------------------------------------------------------
        private XmlSerializer CreateSerializer<T>()
        {
            return CreateSerializer(typeof(T));
        }

        //---------------------------------------------------------
        private XmlSerializer CreateSerializer(Type type)
        {
            if (_xmlSerializer == null)
            {
                if (_attributes == null)
                {
                    _xmlSerializer = new XmlSerializer(type);
                }
                else
                {
                    _xmlSerializer = new XmlSerializer(type, _attributes);
                }
            }
            return _xmlSerializer;
        }

    }
}