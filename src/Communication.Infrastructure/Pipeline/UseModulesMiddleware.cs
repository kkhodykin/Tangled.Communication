using System.Threading.Tasks;
using Tangled.Communication.Infrastructure.Pipeline.Modules;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Tangled.Communication.Infrastructure.Pipeline
{
  class UseModulesMiddleware : PacketProcessingMiddleware
  {
    private readonly ModuleRegistry _modules;

    public UseModulesMiddleware(AppFunc next, ModuleRegistry modules)
      : base(next)
    {
      _modules = modules;
    }

    protected override async Task Invoke(IPacketListenerContext context)
    {
      context.Response = await _modules.Invoke(context).ConfigureAwait(false);
      await Next(context.Environment).ConfigureAwait(false);
    }
  }
}
