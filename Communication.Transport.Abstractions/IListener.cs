using System;

namespace Leverate.LXCRM.Communication.Transport.Abstractions
{
  public interface IListener : IDisposable
  {
    void OnPacket(PacketReceivedCallback callback);
  }
}
