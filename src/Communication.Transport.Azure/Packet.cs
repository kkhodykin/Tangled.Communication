using System;
using Leverate.LXCRM.Communication.Transport.Abstractions;
using Microsoft.ServiceBus.Messaging;

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
      To = message.To;
    }

    public IPacketContent Payload { get; }
    public HeaderCollection Headers { get; }
    public string Id { get; }
    public string ReplyTo { get; }
    public string To { get; }
    public Guid LockToken { get; }
  }
}
