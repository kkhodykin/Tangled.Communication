using System.IO;
using RabbitMQ.Client.Events;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Transport.Rabbit
{
  class PacketContent : IPacketContent
  {
    private readonly Stream stream;

    public string ContentType { get; }
    public string Type { get; }

    public PacketContent(BasicDeliverEventArgs deliverEventArgs)
    {
        this.stream = new MemoryStream(deliverEventArgs.Body);
      ContentType = deliverEventArgs.BasicProperties.ContentType;
      Type = deliverEventArgs.BasicProperties.Type;
    }

    public Stream GetBodyStream()
    {
      return this.stream;
    }
  }
}
