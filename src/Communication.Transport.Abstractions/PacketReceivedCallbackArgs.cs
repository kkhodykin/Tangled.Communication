namespace Tangled.Communication.Transport.Abstractions
{
  public struct PacketReceivedCallbackArgs
  {
    public IPacket Packet { get; }
    public IChannel Channel { get; }

    public PacketReceivedCallbackArgs(IPacket packet, IChannel channel)
    {
      Packet = packet;
      Channel = channel;
    }
  }
}