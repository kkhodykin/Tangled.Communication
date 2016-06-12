using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Transport.Rabbit
{
  class Packet : IPacket
  {
    private readonly IModel channel;
    private readonly BasicDeliverEventArgs deliverEventArgs;
    public IPacketContent Payload { get; }
    public HeaderCollection Headers { get; }
    public string Id { get; }
    public string ReplyTo { get; }
    public string CorrelationId { get; }

    public Packet(IModel channel, BasicDeliverEventArgs deliverEventArgs)
    {
      this.channel = channel;
      this.deliverEventArgs = deliverEventArgs;
      Headers = new HeaderCollection(deliverEventArgs.BasicProperties.Headers);
      Id = deliverEventArgs.BasicProperties.MessageId;
      ReplyTo = deliverEventArgs.BasicProperties.ReplyTo;
      CorrelationId = deliverEventArgs.BasicProperties.CorrelationId;
      Payload = new PacketContent(deliverEventArgs);
    }

    public Task Complete()
    {
      this.channel.BasicAck(this.deliverEventArgs.DeliveryTag, false);
      return Task.FromResult(0);
    }

    public Task Abandon()
    {
      this.channel.BasicReject(this.deliverEventArgs.DeliveryTag, true);
      return Task.FromResult(0);
    }

    public Task DeadLetter()
    {
      this.channel.BasicReject(this.deliverEventArgs.DeliveryTag, false);
      return Task.FromResult(0);
    }
  }
}
