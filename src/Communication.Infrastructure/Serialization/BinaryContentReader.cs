using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Tangled.Communication.Infrastructure.Serialization
{
  internal class BinaryContentReader : XmlObjectSerializer, IPacketContentReader {
    private DataContractSerializer _serializer;

    public object Read(Stream stream, Type type)
    {
      _serializer = new DataContractSerializer(type);
      return ReadObject(XmlDictionaryReader.CreateBinaryReader(stream, XmlDictionaryReaderQuotas.Max));
    }

    public override object ReadObject(Stream stream)
    {
      return ReadObject(XmlDictionaryReader.CreateBinaryReader(stream, XmlDictionaryReaderQuotas.Max));
    }

    public override void WriteStartObject(XmlDictionaryWriter writer, object graph)
    {
      _serializer.WriteStartObject(writer, graph);
    }

    public override void WriteObjectContent(XmlDictionaryWriter writer, object graph)
    {
      _serializer.WriteObjectContent(writer, graph);
    }

    public override void WriteEndObject(XmlDictionaryWriter writer)
    {
      _serializer.WriteEndObject(writer);
    }

    public override object ReadObject(XmlDictionaryReader reader, bool verifyObjectName)
    {
      return _serializer.ReadObject(reader, verifyObjectName);
    }

    public override bool IsStartObject(XmlDictionaryReader reader)
    {
      return _serializer.IsStartObject(reader);
    }
  }
}
