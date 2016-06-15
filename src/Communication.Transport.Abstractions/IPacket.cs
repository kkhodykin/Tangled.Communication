using System.Diagnostics.Contracts;

namespace Tangled.Communication.Transport.Abstractions
{
  /// <summary>
  /// Packet as it is.
  /// </summary>
  [ContractClass(typeof(IPacketContract))]
  public interface IPacket
  {
    /// <summary>
    /// The payload carried by the packet.
    /// </summary>
    IPacketContent Payload { get; }

    /// <summary>
    /// Arbitrary metadata headers that can be sent along with the packet.
    /// </summary>
    HeaderCollection Headers { get; }

    /// <summary>
    /// Packet identifier.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Identifier of the endpoint for replies.
    /// </summary>
    string ReplyTo { get; }

    /// <summary>
    /// The correlation identifier. Should contain source packet <see cref = "Id"/> for the packet sent in reply.
    /// </summary>
    string CorrelationId { get; }
  }
}