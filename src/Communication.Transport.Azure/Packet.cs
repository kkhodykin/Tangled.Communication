using System;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Transport.Azure
{
  internal class Packet : IPacket
  {
    private readonly BrokeredMessage message;

    public Packet(BrokeredMessage message)
    {
      this.message = message;
      Payload = new PacketContent(message);
      Headers = new HeaderCollection(message.Properties);
    }

    public IPacketContent Payload { get; }
    public HeaderCollection Headers { get; }

    public string Id => this.message.MessageId;
    public string ReplyTo => this.message.ReplyTo;
    public string CorrelationId => this.message.CorrelationId;

    public Task Complete() => this.message.CompleteAsync();

    public Task Abandon() => this.message.AbandonAsync();

    public Task DeadLetter() => this.message.DeadLetterAsync();
  }
}
