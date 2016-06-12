using System;
using System.Diagnostics.Contracts;

namespace Tangled.Communication.Transport.Abstractions
{
  /// <summary>
  /// Listens for incoming packets. Implements <see cref="IDisposable" />. Dispose object to stop listening.
  /// </summary>
  [ContractClass(typeof(IListenerContract))]
  public interface IListener : IDisposable
  {
    /// <summary>
    /// Call to subscribe for incoming packets.
    /// </summary>
    /// <param name="receivedCallback"></param>
    void OnPacket(PacketReceivedCallback receivedCallback);
  }

  [ContractClassFor(typeof(IListener))]
  public abstract class IListenerContract : IListener
  {
    public void Dispose()
    {
    }

    public void OnPacket(PacketReceivedCallback receivedCallback)
    {
      Contract.Requires<ArgumentNullException>(receivedCallback != null);
    }
  }
}
