using System.Threading.Tasks;

namespace Tangled.Communication.Transport.Abstractions
{
  public delegate Task PacketReceivedCallback(PacketReceivedCallbackArgs args);
}