using System.Threading.Tasks;

namespace Tangled.Communication.Infrastructure.Pipeline.Modules
{
  class HeadModule : Module
  {
    public override Task<object> Invoke(IPacketListenerContext context)
    {
      return Next?.Invoke(context);
    }
  }
}