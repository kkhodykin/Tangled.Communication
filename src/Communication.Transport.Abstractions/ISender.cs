using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Tangled.Communication.Transport.Abstractions
{
  /// <summary>
  /// Can send the <see cref = "IPacket"/> over the underlying transport layer.
  /// </summary>
  [ContractClass(typeof(ISenderContract))]
  public interface ISender
  {
    /// <summary>
    /// Connection object which manages the specific <see cref="ISender"/> instance lifetime.
    /// </summary>
    IConnection Connection { get; }

    /// <summary>
    /// Send the <see cref = "IPacket"/> over the underlying transport layer. 
    /// </summary>
    /// <param name = "packet">The <see cref = "IPacket"/> to be sent.</param>
    /// <returns></returns>
    Task Send(IPacket packet);
  }
}