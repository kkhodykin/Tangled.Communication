using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Leverate.LXCRM.Communication.Transport.Abstractions;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace Tangled.Communication.Transport.Azure
{
  public class PacketListener : IListener
  {
    private static readonly ConcurrentDictionary<string, NamespaceManager> NamespaceManagers = new ConcurrentDictionary<string, NamespaceManager>();
    private static readonly ConcurrentDictionary<string, MessagingFactory> MessagingFactories = new ConcurrentDictionary<string, MessagingFactory>();
    private readonly MessageReceiver _receiver;
    private PacketReceivedCallback _callback;
    protected NamespaceManager NamespaceManager { get; private set; }
    protected MessagingFactory MessagingFactory { get; private set; }

    public PacketListener(ConnectionSettings settings)
    {
      if (settings == null)
        throw new ArgumentNullException(nameof(settings));
      if (settings.ConnectionString == null)
        throw new ArgumentNullException(nameof(settings.ConnectionString));

      NamespaceManager = NamespaceManagers.GetOrAdd(settings.ConnectionString, NamespaceManager.CreateFromConnectionString);
      MessagingFactory = MessagingFactories.GetOrAdd(settings.ConnectionString, MessagingFactory.CreateFromConnectionString);
      _receiver = MessagingFactory.CreateMessageReceiver(settings.EntityPath,
        settings.ReentryAllowed ? ReceiveMode.PeekLock : ReceiveMode.ReceiveAndDelete);
    }

    private async Task Loop()
    {
      while (!_receiver.IsClosed)
      {
        var message = await _receiver.ReceiveAsync().ConfigureAwait(false);
        await OnMessageReceived(message).ConfigureAwait(false);
      }
    }

    PacketReceivedCallbackArgs CreateCallbackArgs(BrokeredMessage message)
    {
      return new PacketReceivedCallbackArgs(new Packet(message), CreateChannel(message));
    }

    private Channel CreateChannel(BrokeredMessage message)
    {
      return new Channel(MessagingFactory, _receiver.Mode == ReceiveMode.PeekLock? _receiver : null, message);
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

    public void Dispose()
    {
      _callback = null;
      _receiver.Close();
    }

    public void OnPacket(PacketReceivedCallback callback)
    {
      Loop().ContinueWith(t =>
      {

      });
      _callback += callback;
    }
  }
}
