using System.Threading.Tasks;

namespace Leverate.LXCRM.Communication.Transport.Abstractions
{
  public interface IChannel
  {
    Task Reply(object payload);
    Task Complete(IPacket packet);
    Task Abandon(IPacket packet);
    Task DeadLetter(IPacket packet);
  }
}
