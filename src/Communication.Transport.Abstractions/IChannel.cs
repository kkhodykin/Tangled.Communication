using System.Threading.Tasks;

namespace Tangled.Communication.Transport.Abstractions
{
  /// <summary>
  /// Communication channel used to deliver the packet. Implements <see cref="ISender"/> in order to allow packet handling code to send the response.
  /// </summary>
  public interface IChannel : ISender
  {
    /// <summary>
    /// Called to notify underlying transport layer that the packet processing has completed.
    /// </summary>
    /// <param name="packet">The packet being processed.</param>
    /// <returns></returns>
    Task Complete(IPacket packet);

    /// <summary>
    /// Called to notify underlying transport layer that the packet can't be processed by the handling code.
    /// </summary>
    /// <param name="packet">The packet being processed.</param>
    /// <returns></returns>
    Task Abandon(IPacket packet);

    /// <summary>
    /// Called to notify underlying transport layer that the packet is poisonous.
    /// </summary>
    /// <param name="packet">The packet being processed.</param>
    /// <returns></returns>
    Task DeadLetter(IPacket packet);
  }
}
