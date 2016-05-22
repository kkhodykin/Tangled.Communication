using System;

namespace Tangled.Communication.Transport.Abstractions
{
  public interface IListener : IDisposable
  {
    void OnPacket(PacketReceivedCallback callback);
  }
}
