using System.IO;
using System.Diagnostics.Contracts;

namespace Tangled.Communication.Transport.Abstractions
{
  /// <summary>
  /// The contents of the <see cref = "IPacket"/>
  /// </summary>
  [ContractClass(typeof(IPacketContentContract))]
  public interface IPacketContent
  {
    /// <summary>
    /// The MIME type of the <see cref = "IPacket"/> content (e.g. application/json).
    /// </summary>
    string ContentType { get; }

    /// <summary>
    /// Actual strong type used to represent data transmitted.
    /// </summary>
    string Type { get; }

    /// <summary>
    /// Returns packet contents as a <see cref = "Stream"/>
    /// </summary>
    /// <returns></returns>
    Stream GetBodyStream();
  }
}