using System.Diagnostics.Contracts;

namespace Tangled.Communication.Transport.Abstractions
{
  /// <summary>
  /// Mainteins connectivity to the underlayung transport layer.
  /// </summary>
  [ContractClass(typeof(IConnectionContract))]
  public interface IConnection
  {
    /// <summary>
    /// Connection string used to connect to the underlayung transport layer.
    /// </summary>
    string ConnectionString { get; }

    /// <summary>
    /// Creates the <see cref = "IListener"/>
    /// </summary>
    /// <param name = "path">Communication layer entity path to listen to.</param>
    /// <returns></returns>
    IListener CreateListener(string path);
    /// <summary>
    /// Creates the <see cref = "IListener"/>
    /// </summary>
    /// <param name = "path">Communication layer entity path to listen to.</param>
    /// <param name = "allowRetries">Indicates if the <see cref = "IListener"/> expects the packet to be retried after <see cref = "IPacket.Abandon"/> was called.</param>
    /// <returns></returns>
    IListener CreateListener(string path, bool allowRetries);
    /// <summary>
    /// Creates the <see cref = "IListener"/>
    /// </summary>
    /// <param name = "path">Communication layer entity path to listen to.</param>
    /// <param name = "allowRetries">Indicates if the <see cref = "IListener"/> expects the packet to be retried after <see cref = "IPacket.Abandon"/> was called.</param>
    /// <param name = "private">Indicates if the <see cref = "IListener"/> should create new subscription when connecting to the communication gateway or reuse existing one.</param>
    /// <returns></returns>
    IListener CreateListener(string path, bool allowRetries, bool @private);
    /// <summary>
    /// Creates the <see cref = "ISender"/>
    /// </summary>
    /// <param name = "path">Communication layer entity path to send packets to.</param>
    /// <returns></returns>
    ISender CreateSender(string path);
  }
}