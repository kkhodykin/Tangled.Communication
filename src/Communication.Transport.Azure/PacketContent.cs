using System.IO;
using Microsoft.ServiceBus.Messaging;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Transport.Azure
{
  internal class PacketContent : IPacketContent
  {
    private readonly BrokeredMessage message;
    public string ContentType { get; }
    public string Type { get; }

    public PacketContent(BrokeredMessage message)
    {
      this.message = message;
      ContentType = message.ContentType;
      Type = message.CorrelationId;
    }

    public Stream GetBodyStream()
    {
      return this.message.GetBody<Stream>();
    }
  }
}
