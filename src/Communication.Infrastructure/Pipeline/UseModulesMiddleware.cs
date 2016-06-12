using System.Threading.Tasks;
using Tangled.Communication.Infrastructure.Extensions;
using Tangled.Communication.Infrastructure.Pipeline.Modules;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Tangled.Communication.Infrastructure.Pipeline
{
  internal class UseModulesMiddleware : PacketProcessingMiddleware
  {
    private readonly ModuleRegistry modules;

    public UseModulesMiddleware(AppFunc next, ModuleRegistry modules)
      : base(next)
    {
        this.modules = modules;
    }

    protected override async Task Invoke(IPacketListenerContext context)
    {
      context.Response = (await this.modules.Invoke(context).ConfigureAwait(false)).Pack();
      await Next(context.Environment).ConfigureAwait(false);
    }
  }
}
