using System.IO;
using Leverate.LXCRM.Communication.Transport.Abstractions;
using Microsoft.ServiceBus.Messaging;

namespace Tangled.Communication.Transport.Azure
{
  internal class PacketContent : IPacketContent
  {
    private readonly BrokeredMessage _message;
    public string ContentType { get; }
    public string Type { get; }
    public string QualifiedType { get; }

    public PacketContent(BrokeredMessage message)
    {
      _message = message;
      ContentType = message.ContentType;
      QualifiedType = _message.Properties["X-Type"] as string;
      Type = message.CorrelationId;
    }

    public Stream GetBodyStream()
    {
      return _message.GetBody<Stream>();
    }
  }
}
