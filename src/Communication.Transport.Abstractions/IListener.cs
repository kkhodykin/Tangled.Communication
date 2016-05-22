using System;

namespace Tangled.Communication.Transport.Abstractions
{
  /// <summary>
  /// Listens for incoming packets. Implements <see cref="IDisposable" />. Dispose object to stop listening.
  /// </summary>
  public interface IListener : IDisposable
  {
    /// <summary>
    /// Call to subscribe for incoming packets.
    /// </summary>
    /// <param name="callback"></param>
    void OnPacket(PacketReceivedCallback callback);
  }
}
