using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Transport.Azure
{
  public class Connection : IConnection
  {
    private static readonly ConcurrentDictionary<string, NamespaceManager> NamespaceManagers = new ConcurrentDictionary<string, NamespaceManager>();
    private static readonly ConcurrentDictionary<string, MessagingFactory> MessagingFactories = new ConcurrentDictionary<string, MessagingFactory>();
    protected NamespaceManager NamespaceManager { get; private set; }
    protected MessagingFactory MessagingFactory { get; private set; }

    public string ConnectionString { get; }

    public Connection(string connectionString)
    {
      Contract.Requires<ArgumentNullException>(connectionString != null);

      ConnectionString = connectionString;
      NamespaceManager = NamespaceManagers.GetOrAdd(connectionString, NamespaceManager.CreateFromConnectionString);
      MessagingFactory = MessagingFactories.GetOrAdd(connectionString, MessagingFactory.CreateFromConnectionString);
    }

    public IListener CreateListener(string path)
    {
      return CreateListener(path, false);
    }

    public IListener CreateListener(string path, bool allowRetries)
    {
      var receiver = MessagingFactory.CreateMessageReceiver(path,
        allowRetries ? ReceiveMode.PeekLock : ReceiveMode.ReceiveAndDelete);

      return new PacketListener(receiver, this);
    }

    public IListener CreateListener(string path, bool allowRetries, bool @private)
    {
      throw new NotImplementedException();
    }

    public ISender CreateSender(string path)
    {
      throw new NotImplementedException();
    }
  }
}
