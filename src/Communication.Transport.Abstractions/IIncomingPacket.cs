using System.Threading.Tasks;

namespace Tangled.Communication.Transport.Abstractions
{
  /// <summary>
  /// Represents incoming packet.
  /// </summary>
  public interface IIncomingPacket : IPacket {
    /// <summary>
    /// Called to notify underlying transport layer that the packet processing has completed.
    /// </summary>
    /// <returns></returns>
    Task Complete();

    /// <summary>
    /// Called to notify underlying transport layer that the packet can't be processed by the handling code.
    /// </summary>
    /// <returns></returns>
    Task Abandon();

    /// <summary>
    /// Called to notify underlying transport layer that the packet is poisonous.
    /// </summary>
    /// <returns></returns>
    Task DeadLetter();
  }
}