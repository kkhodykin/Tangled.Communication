using System;

namespace Tangled.Communication.Transport.Abstractions.Topology
{
  /// <summary>
  /// Describes the socket created on the transport layer.
  /// </summary>
  public class SocketDescriptor
  {
    /// <summary>
    /// Socket path (e.g.Queue or Topic/Subscription, or Exchange/Queue)
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// Flags representing socket behavior and lifetime. See <see cref="SocketType"/>
    /// </summary>
    /// <remarks>
    /// Restricted to the following combinations: 
    ///   <see cref="SocketType.Permanent"/> | <see cref="SocketType.Exclusive"/> - Depending on <see cref="Path"/> value can identify either Queue or Subscription type of socket.
    ///   <see cref="SocketType.Permanent"/> | <see cref="SocketType.Shared"/> - Describes Topic (Exchange) socket.
    ///   <see cref="SocketType.Ephemeral"/> | <see cref="SocketType.Exclusive"/> - Describes subscription type of socket that exists only while socket client is active.
    /// </remarks>
    public SocketType Type { get; set; }

    /// <summary>
    /// Amount of time before orphant the packet discarded by underlaying transport provider.
    /// </summary>
    public TimeSpan PacketTtl { get; set; }
  }
}