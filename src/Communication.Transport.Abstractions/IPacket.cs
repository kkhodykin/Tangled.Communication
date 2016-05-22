namespace Tangled.Communication.Transport.Abstractions
{
  public interface IPacket
  {
    IPacketContent Payload { get; }
    HeaderCollection Headers { get; }
    string Id { get; }
    string ReplyTo { get; }
    string To { get; }
  }
}
