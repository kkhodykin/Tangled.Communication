using RabbitMQ.Client.Events;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Transport.Rabbit
{
  class Packet:IPacket
  {
    public IPacketContent Payload { get; }
    public HeaderCollection Headers { get; }
    public string Id { get; }
    public string ReplyTo { get; }
    public string To { get; }

    public Packet(BasicDeliverEventArgs deliverEventArgs)
    {
      Headers = new HeaderCollection(deliverEventArgs.BasicProperties.Headers);
      Id = deliverEventArgs.BasicProperties.MessageId;
      ReplyTo = deliverEventArgs.BasicProperties.ReplyTo;
      To = deliverEventArgs.BasicProperties.CorrelationId;
    }
  }
}
