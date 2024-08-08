using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;
using System.IO.Compression;

namespace Helpers
{
    public static class SerializationHelper
    {
        /// <summary>
        /// Serializes an object to an XmlDocument.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>An XmlDocument containing the serialized object.</returns>
        public static XmlDocument SerializeToXml<T>(T obj)
        {
            var xmlDoc = new XmlDocument();
            using var xmlWriter = xmlDoc.CreateNavigator()?.AppendChild();
            var xmlSerializer = new XmlSerializer(typeof(T));
            if (xmlWriter != null) xmlSerializer.Serialize(xmlWriter, obj);
            return xmlDoc;
        }

        /// <summary>
        /// Deserializes an object from an XmlReader.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="xml">The XmlReader containing the serialized object.</param>
        /// <returns>The deserialized object.</returns>
        public static T DeserializeFromXml<T>(XmlReader xml)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            return (T)xmlSerializer.Deserialize(xml)!;
        }

        /// <summary>
        /// Serializes an object to a string using DataContractSerializer.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>A string containing the serialized object.</returns>
        public static string SerializeDataContract<T>(T obj)
        {
            var stream = new MemoryStream();
            var serializer = new DataContractSerializer(typeof(T));
            serializer.WriteObject(stream, obj);
            return new UTF8Encoding().GetString(stream.ToArray());
        }

        /// <summary>
        /// Serializes an object to an XmlDocument using DataContractSerializer.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>An XmlDocument containing the serialized object.</returns>
        public static XmlDocument SerializeDataContractToXml<T>(T obj)
        {
            var data = SerializeDataContract(obj);
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(data);
            return xmlDoc;
        }

        /// <summary>
        /// Deserializes an object from a string using DataContractSerializer.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="data">The string containing the serialized object.</param>
        /// <returns>The deserialized object.</returns>
        private static T DeserializeDataContract<T>(string data)
        {
            var stream = new MemoryStream(new UTF8Encoding().GetBytes(data));
            var serializer = new DataContractSerializer(typeof(T));
            return (T)serializer.ReadObject(stream)!;
        }

        /// <summary>
        /// Deserializes an object from a stream using DataContractSerializer.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="stream">The stream containing the serialized object.</param>
        /// <returns>The deserialized object.</returns>
        public static T DeserializeDataContractFromStream<T>(Stream stream)
        {
            var serializer = new DataContractSerializer(typeof(T));
            return (T)serializer.ReadObject(stream)!;
        }

        /// <summary>
        /// Deserializes an object from a JSON stream using DataContractJsonSerializer.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="stream">The JSON stream containing the serialized object.</param>
        /// <returns>The deserialized object.</returns>
        public static T DeserializeDataContractFromJson<T>(Stream stream)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            return (T)serializer.ReadObject(stream)!;
        }

        /// <summary>
        /// Deserializes an object from an XmlReader using DataContractSerializer.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="xml">The XmlReader containing the serialized object.</param>
        /// <returns>The deserialized object.</returns>
        public static T DeserializeDataContractFromXml<T>(XmlReader xml)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xml);
            return DeserializeDataContract<T>(xmlDoc.OuterXml);
        }

        /// <summary>
        /// Serializes an object to a JSON string using System.Text.Json.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>A JSON string containing the serialized object.</returns>
        public static string SerializeToJson<T>(T obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        /// <summary>
        /// Deserializes an object from a JSON string using System.Text.Json.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="json">The JSON string containing the serialized object.</param>
        /// <returns>The deserialized object.</returns>
        public static T DeserializeFromJson<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json)!;
        }

        /// <summary>
        /// Validates if a string is a well-formed XML.
        /// </summary>
        /// <param name="xml">The XML string to validate.</param>
        /// <returns>True if the string is a well-formed XML; otherwise, false.</returns>
        public static bool IsValidXml(string xml)
        {
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                return true;
            }
            catch (XmlException)
            {
                return false;
            }
        }

        /// <summary>
        /// Validates if a string is a well-formed JSON.
        /// </summary>
        /// <param name="json">The JSON string to validate.</param>
        /// <returns>True if the string is a well-formed JSON; otherwise, false.</returns>
        public static bool IsValidJson(string json)
        {
            try
            {
                JsonDocument.Parse(json);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }

        /// <summary>
        /// Formats an XmlDocument to a pretty-printed string.
        /// </summary>
        /// <param name="xmlDoc">The XmlDocument to format.</param>
        /// <returns>A pretty-printed string representation of the XmlDocument.</returns>
        public static string FormatXml(XmlDocument xmlDoc)
        {
            using var stringWriter = new StringWriter();
            using var xmlTextWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true });
            xmlDoc.WriteTo(xmlTextWriter);
            xmlTextWriter.Flush();
            return stringWriter.GetStringBuilder().ToString();
        }

        /// <summary>
        /// Compresses an object to a byte array using GZip.
        /// </summary>
        /// <typeparam name="T">The type of the object to compress.</typeparam>
        /// <param name="obj">The object to compress.</param>
        /// <returns>A byte array containing the compressed object.</returns>
        public static byte[] Compress<T>(T obj)
        {
            var json = SerializeToJson(obj);
            var bytes = Encoding.UTF8.GetBytes(json);
            using var memoryStream = new MemoryStream();
            using var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress);
            gzipStream.Write(bytes, 0, bytes.Length);
            return memoryStream.ToArray();
        }

        /// <summary>
        /// Decompresses an object from a byte array using GZip.
        /// </summary>
        /// <typeparam name="T">The type of the object to decompress.</typeparam>
        /// <param name="compressedData">The byte array containing the compressed object.</param>
        /// <returns>The decompressed object.</returns>
        public static T Decompress<T>(byte[] compressedData)
        {
            using var compressedStream = new MemoryStream(compressedData);
            using var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress);
            using var resultStream = new MemoryStream();
            gzipStream.CopyTo(resultStream);
            var json = Encoding.UTF8.GetString(resultStream.ToArray());
            return DeserializeFromJson<T>(json);
        }
    }
}
