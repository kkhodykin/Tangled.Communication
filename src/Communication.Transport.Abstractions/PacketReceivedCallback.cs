using System.Threading.Tasks;

namespace Tangled.Communication.Transport.Abstractions
{
  /// <summary>
  /// Delegate of the method invoked by the <see cref="IListener"/> when the incomming <see cref="IPacket"/> was received.
  /// </summary>
  /// <param name="args">The <see cref="PacketReceivedCallbackArgs"/> passed to the callback.</param>
  /// <returns></returns>
  public delegate Task PacketReceivedCallback(PacketReceivedCallbackArgs args);
}