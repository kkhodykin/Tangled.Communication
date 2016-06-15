namespace Tangled.Communication.Transport.Abstractions
{
  /// <summary>
  /// The object passed to the <see cref="PacketReceivedCallback"/> when <see cref="IIncomingPacket"/> was received by the <see cref="IListener"/>.
  /// </summary>
  public struct PacketReceivedCallbackArgs
  {
    /// <summary>
    /// The received <see cref="IPacket"/>
    /// </summary>
    public IIncomingPacket Packet { get; }

    /// <summary>
    /// The <see cref="IConnection"/> instance used to notify underlying transport layer about packet processing state.
    /// </summary>
    public IConnection Connection { get; }

    public PacketReceivedCallbackArgs(IIncomingPacket packet, IConnection connection)
    {
      Packet = packet;
      Connection = connection;
    }
  }
}