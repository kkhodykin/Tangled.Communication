using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Leverate.LXCRM.Communication.Transport.Abstractions;
using Microsoft.ServiceBus.Messaging;

namespace Tangled.Communication.Transport.Azure
{
  class Channel : IChannel
  {
    private static readonly ConcurrentDictionary<string, MessageSender> SenderCache = new ConcurrentDictionary<string, MessageSender>();
    private readonly MessagingFactory _factory;
    private readonly MessageReceiver _receiver;
    private readonly BrokeredMessage _message;
    private readonly ResponseMessageBuilder _responseBuilder;
    private readonly Lazy<MessageSender> _sender;

    public Channel(MessagingFactory factory, MessageReceiver receiver, BrokeredMessage message)
    {
      _factory = factory;
      _receiver = receiver;
      _message = message;
      _responseBuilder = new ResponseMessageBuilder(message);

      _sender =
        new Lazy<MessageSender>(
          () => SenderCache.GetOrAdd(
            _message.ReplyTo, 
            dest => _factory.CreateMessageSender(dest)));
    }

    public Task Reply(object payload)
    {
      if (_factory == null || string.IsNullOrWhiteSpace(_message.ReplyTo))
        return Task.FromResult(0);

      var message = _responseBuilder.BuildResponse(payload);
      return message == null ? Task.FromResult(0) : _sender.Value.SendAsync(message);
    }

    public Task Complete(IPacket packet)
    {
      return TryUpdatePacketState(packet, (r, p) => r.CompleteAsync(p.LockToken));
    }

    public Task Abandon(IPacket packet)
    {
      return TryUpdatePacketState(packet, (r, p) => r.AbandonAsync(p.LockToken));
    }

    public Task DeadLetter(IPacket packet)
    {
      return TryUpdatePacketState(packet, (r, p) => r.DeadLetterAsync(p.LockToken));
    }

    private async Task TryUpdatePacketState(IPacket packet, Func<MessageReceiver, Packet, Task> action)
    {
      var localPacket = packet as Packet;
      if (_receiver == null || localPacket == null || localPacket.LockToken == default(Guid)) return;

      await action(_receiver, localPacket).ConfigureAwait(false);
    }
  }
}
