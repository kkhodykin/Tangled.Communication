using System;
using System.Threading.Tasks;
using Tangled.Communication.Infrastructure.Extensions;

namespace Tangled.Communication.Infrastructure.Pipeline.Modules
{
  public abstract class Module
  {
    public Module Next { get; set; }
    public abstract Task<object> Invoke(IPacketListenerContext context);
  }

  internal sealed class Module<TModule> : Module
  {
    static readonly ModuleDescriptor<TModule> Descriptor = new ModuleDescriptor<TModule>(typeof(TModule));

    private readonly Func<IPacketListenerContext, TModule> factory;

    public Module(Func<IPacketListenerContext, TModule> factory)
    {
      this.factory = factory;
    }

    public override Task<object> Invoke(IPacketListenerContext context)
    {
      var action = Descriptor.GetAction(context.Request.Payload.Type);
      if (action == null)
        return Next.Invoke(context);

      var module = this.factory(context);

      if (module == null)
        throw new InvalidOperationException($"Can't create module {typeof(TModule).FullName} using provided factory");

      var body = context.GetBody(action.RequestType);

      return action.CallAction(module, body);
    }
  }
}
