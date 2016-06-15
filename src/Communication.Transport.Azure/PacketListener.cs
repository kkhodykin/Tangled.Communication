using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Transport.Azure
{
  public class PacketListener : IListener
  {
    private readonly MessageReceiver receiver;
    private readonly IConnection connection;
    private PacketReceivedCallback callback;

    internal PacketListener(MessageReceiver receiver, IConnection connection)
    {
      this.receiver = receiver;
      this.connection = connection;
    }

    public IDisposable OnPacket(PacketReceivedCallback receivedCallback)
    {
      this.callback += receivedCallback;
      return new DisposeAction(() => this.callback -= receivedCallback);
    }

    private async Task Loop()
    {
      while (!this.receiver.IsClosed)
      {
        var message = await this.receiver.ReceiveAsync().ConfigureAwait(false);
        await OnMessageReceived(message).ConfigureAwait(false);
      }
    }

    private PacketReceivedCallbackArgs CreateCallbackArgs(BrokeredMessage message)
    {
      return new PacketReceivedCallbackArgs(new Packet(message), this.connection);
    }

    private Task OnMessageReceived(BrokeredMessage message)
    {
      if (this.callback == null) return Task.FromResult(0);

      var args = CreateCallbackArgs(message);
      var callbacks = this.callback.GetInvocationList().OfType<PacketReceivedCallback>();
      var tasks = callbacks.Select(c => c(args));

      return Task.WhenAll(tasks);
    }

    public Task Start()
    {
      Contract.Ensures(Contract.Result<Task>() != null);

      return Loop();
    }

    public void Dispose()
    {
      this.callback = null;
      this.receiver.Close();
    }

    private class DisposeAction : IDisposable
    {
      private readonly Action action;

      public DisposeAction(Action action)
      {
        this.action = action;
      }

      public void Dispose() => this.action();
    }
  }
}
