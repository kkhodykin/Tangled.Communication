using System;
using Microsoft.ServiceBus.Messaging;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Transport.Azure
{
  internal class Packet : IPacket
  {
    public Packet(BrokeredMessage message)
    {
      Payload = new PacketContent(message);
      Headers = new HeaderCollection(message.Properties);
      Id = message.MessageId;
      ReplyTo = message.ReplyTo;
      LockToken = message.LockToken;
      CorrelationId = message.To;
    }

    public IPacketContent Payload { get; }
    public HeaderCollection Headers { get; }
    public string Id { get; }
    public string ReplyTo { get; }
    public string CorrelationId { get; }
    public Guid LockToken { get; }
  }
}
