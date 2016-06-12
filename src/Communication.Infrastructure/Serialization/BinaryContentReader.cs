using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Tangled.Communication.Infrastructure.Serialization
{
  internal class BinaryContentReader : XmlObjectSerializer, IPacketContentReader {
    private readonly Stream contentStream;
    private readonly Type type;
    private DataContractSerializer serializer;

    public BinaryContentReader(Stream contentStream, Type type)
    {
      Contract.Requires<ArgumentNullException>(contentStream != null);
      Contract.Requires<ArgumentNullException>(type != null);

      this.contentStream = contentStream;
      this.type = type;
    }

    public object Read()
    {
        this.serializer = new DataContractSerializer(this.type);
      return ReadObject(XmlDictionaryReader.CreateBinaryReader(this.contentStream, XmlDictionaryReaderQuotas.Max));
    }

    public override object ReadObject(Stream stream)
    {
      return ReadObject(XmlDictionaryReader.CreateBinaryReader(stream, XmlDictionaryReaderQuotas.Max));
    }

    public override void WriteStartObject(XmlDictionaryWriter writer, object graph)
    {
        this.serializer.WriteStartObject(writer, graph);
    }

    public override void WriteObjectContent(XmlDictionaryWriter writer, object graph)
    {
        this.serializer.WriteObjectContent(writer, graph);
    }

    public override void WriteEndObject(XmlDictionaryWriter writer)
    {
        this.serializer.WriteEndObject(writer);
    }

    public override object ReadObject(XmlDictionaryReader reader, bool verifyObjectName)
    {
      return this.serializer.ReadObject(reader, verifyObjectName);
    }

    public override bool IsStartObject(XmlDictionaryReader reader)
    {
      return this.serializer.IsStartObject(reader);
    }
  }
}
