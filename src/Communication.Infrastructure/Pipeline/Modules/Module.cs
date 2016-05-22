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

    private readonly Func<IPacketListenerContext, TModule> _factory;

    public Module(Func<IPacketListenerContext, TModule> factory)
    {
      _factory = factory;
    }

    public override Task<object> Invoke(IPacketListenerContext context)
    {
      var action = Descriptor.GetAction(context.Payload.Type);
      if (action == null) return Next.Invoke(context);

      context.Request = context.GetBody();
      context.PayloadType = action.RequestType;

      return action.CallAction(_factory(context), context.Request);
    }
  }
}
