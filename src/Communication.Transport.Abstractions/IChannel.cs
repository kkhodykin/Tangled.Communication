using System.Threading.Tasks;

namespace Tangled.Communication.Transport.Abstractions
{
  public interface IChannel
  {
    Task Reply(object payload);
    Task Complete(IPacket packet);
    Task Abandon(IPacket packet);
    Task DeadLetter(IPacket packet);
  }
}
