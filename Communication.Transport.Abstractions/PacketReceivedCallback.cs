using System.Threading.Tasks;

namespace Leverate.LXCRM.Communication.Transport.Abstractions
{
  public delegate Task PacketReceivedCallback(PacketReceivedCallbackArgs args);
}