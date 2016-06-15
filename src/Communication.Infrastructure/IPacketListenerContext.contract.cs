using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Tangled.Communication.Transport.Abstractions;
using System.Diagnostics.Contracts;

namespace Tangled.Communication.Infrastructure
{
  [ContractClassFor(typeof(IPacketListenerContext))]
  internal abstract class IPacketListenerContextContract : IPacketListenerContext
  {
    public IDictionary<string, object> Environment
    {
      get
      {
        Contract.Ensures(Contract.Result<IDictionary<string, object>>() != null);
        return new Dictionary<string, object>();
      }
    }

    public IIncomingPacket Request
    {
      get
      {
        Contract.Ensures(Contract.Result<IPacket>() != null);
        return (IIncomingPacket)new object();
      }
      set { }
    }

    public IPacket Response { get { return default(IPacket); } set { } }
    public ILogger Logger => default(ILogger);

    public ISender ReplyChannel => default(ISender);

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(Environment != null);
      Contract.Invariant(Request != null);
    }

    public object GetService(Type serviceType)
    {
      return default(Type);
    }
  }
}