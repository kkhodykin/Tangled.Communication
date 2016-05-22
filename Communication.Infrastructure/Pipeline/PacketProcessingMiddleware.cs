using System.Collections.Generic;
using System.Threading.Tasks;
using Tangled.Communication.Infrastructure.Extensions;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Tangled.Communication.Infrastructure.Pipeline
{
  public abstract class PacketProcessingMiddleware
  {
    protected AppFunc Next { get; }

    protected PacketProcessingMiddleware(AppFunc next)
    {
      Next = next;
    }

    protected abstract Task Invoke(IPacketListenerContext context);

    public Task Invoke(IDictionary<string, object> environment)
    {
      var packet = environment.Get<IPacketListenerContext>(typeof(IPacketListenerContext).FullName);
      return Invoke(packet);
    }
  }
}
