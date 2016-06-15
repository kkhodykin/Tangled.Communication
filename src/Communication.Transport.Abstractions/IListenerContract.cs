using System;
using System.Diagnostics.Contracts;

namespace Tangled.Communication.Transport.Abstractions
{
  [ContractClassFor(typeof(IListener))]
  public abstract class IListenerContract : IListener
  {
    public void Dispose()
    {
    }

    public IDisposable OnPacket(PacketReceivedCallback receivedCallback)
    {
      Contract.Requires<ArgumentNullException>(receivedCallback != null);
      return default(IDisposable);
    }
  }
}