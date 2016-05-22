namespace Tangled.Communication.Transport.Abstractions
{
  /// <summary>
  /// The object passed to the <see cref="PacketReceivedCallback"/> when <see cref="IPacket"/> was received by the <see cref="IListener"/>.
  /// </summary>
  public struct PacketReceivedCallbackArgs
  {
    /// <summary>
    /// The received <see cref="IPacket"/>
    /// </summary>
    public IPacket Packet { get; }

    /// <summary>
    /// The communication <see cref="IChannel"/> used to notify underlying transport layer about packet processing state.
    /// </summary>
    public IChannel Channel { get; }

    public PacketReceivedCallbackArgs(IPacket packet, IChannel channel)
    {
      Packet = packet;
      Channel = channel;
    }
  }
}