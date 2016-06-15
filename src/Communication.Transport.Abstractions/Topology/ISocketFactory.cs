namespace Tangled.Communication.Transport.Abstractions.Topology
{
  /// <summary>
  /// Implementation knows how to create the socket on the specific transport provider.
  /// </summary>
  public interface ISocketFactory
  {
    /// <summary>
    /// Creates the socket.
    /// </summary>
    /// <param name="descriptor">Setting used to create socket.</param>
    void Create(SocketDescriptor descriptor);
  }
}