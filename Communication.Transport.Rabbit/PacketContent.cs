using System.IO;
using Leverate.LXCRM.Communication.Transport.Abstractions;
using RabbitMQ.Client.Events;

namespace Tangled.Communication.Transport.Rabbit
{
  class PacketContent:IPacketContent
  {
    private readonly Stream _stream;

    public string ContentType { get; }
    public string Type { get; }
    public string QualifiedType { get; }

    public PacketContent(BasicDeliverEventArgs deliverEventArgs)
    {
      _stream = new MemoryStream(deliverEventArgs.Body);
      ContentType = deliverEventArgs.BasicProperties.ContentType;
      Type = deliverEventArgs.BasicProperties.Type;
      QualifiedType = new HeaderCollection(deliverEventArgs.BasicProperties?.Headers)["X-Type"]?.ToString();
    }

    public Stream GetBodyStream()
    {
      return _stream;
    }
  }
}
