using System.Threading.Tasks;

namespace Tangled.Communication.Transport.Abstractions
{
  public interface ISender
  {
    Task Send(IPacket packet);
  }
}
