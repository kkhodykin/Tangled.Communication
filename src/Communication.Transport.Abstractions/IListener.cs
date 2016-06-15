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
    IDisposable OnPacket(PacketReceivedCallback receivedCallback);
  }
}
