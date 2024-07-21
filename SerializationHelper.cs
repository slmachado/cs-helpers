using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Helpers;

public static class SerializationHelper
{
    public static XmlDocument SerializeToXml<T>(T obj)
    {
        var xmlDoc = new XmlDocument();
        using var xmlWriter = xmlDoc.CreateNavigator()?.AppendChild();
        var xmlSerializer = new XmlSerializer(typeof(T));
        if (xmlWriter != null) xmlSerializer.Serialize(xmlWriter, obj);
        return xmlDoc;
    }


    public static T DeserializeFromXml<T>(XmlReader xml)
    {
        var xmlSerializer = new XmlSerializer(typeof(T));
        return (T)xmlSerializer.Deserialize(xml)!;
    }


    // ReSharper disable once MemberCanBePrivate.Global
    public static string SerializeDataContract<T>(T obj)
    {
        var stream = new MemoryStream();
        var serializer = new DataContractSerializer(typeof(T));
        serializer.WriteObject(stream, obj);
        return new UTF8Encoding().GetString(stream.ToArray());
    }


    public static XmlDocument SerializeDataContractToXml<T>(T obj)
    {
        var data = SerializeDataContract(obj);
        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(data);
        return xmlDoc;
    }


    private static T DeserializeDataContract<T>(string data)
    {
        var stream = new MemoryStream(new UTF8Encoding().GetBytes(data));
        var serializer = new DataContractSerializer(typeof(T));
        return (T)serializer.ReadObject(stream)!;
    }

    
    public static T DeserializeDataContractFromStream<T>(Stream stream)
    {
        var serializer = new DataContractSerializer(typeof(T));
        return (T)serializer.ReadObject(stream)!;
    }


    /// <summary>
    /// Deserialize json stream
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static T DeserializeDataContractFromJson<T>(Stream stream)
    {
        var serializer = new DataContractJsonSerializer(typeof(T));
        return (T)serializer.ReadObject(stream)!;
    }


    public static T DeserializeDataContractFromXml<T>(XmlReader xml)
    {
        var xmlDoc = new XmlDocument();
        xmlDoc.Load(xml);
        return DeserializeDataContract<T>(xmlDoc.OuterXml);
    }
}