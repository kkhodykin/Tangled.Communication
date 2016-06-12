using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Transport.Azure
{
  public class PacketListener : IListener
  {
    private MessageReceiver _receiver;
    private PacketReceivedCallback _callback;

    internal PacketListener(MessageReceiver receiver)
    {
      _receiver = receiver;
    }

    private async Task Loop()
    {
      while (!_receiver.IsClosed)
      {
        var message = await _receiver.ReceiveAsync().ConfigureAwait(false);
        await OnMessageReceived(message).ConfigureAwait(false);
      }
    }

    private PacketReceivedCallbackArgs CreateCallbackArgs(BrokeredMessage message)
    {
      return new PacketReceivedCallbackArgs(new Packet(message), CreateChannel(message));
    }

    private Channel CreateChannel(BrokeredMessage message)
    {
      throw new NotImplementedException(message.ToString());
    }

    private Task OnMessageReceived(BrokeredMessage message)
    {
      var callback = _callback;
      if (callback == null) return Task.FromResult(0);

      var args = CreateCallbackArgs(message);
      var callbacks = callback.GetInvocationList().OfType<PacketReceivedCallback>();
      var tasks = callbacks.Select(c => c(args));

      return Task.WhenAll(tasks);
    }

    public Task Start()
    {
      return Loop();
    }

    public void Dispose()
    {
      _callback = null;
      _receiver.Close();
    }

    public void OnPacket(PacketReceivedCallback callback)
    {
      _callback += callback;
    }
  }
}
