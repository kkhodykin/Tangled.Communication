using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Tangled.Communication.Infrastructure.Extensions;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Tangled.Communication.Infrastructure.Pipeline
{
  [ContractClass(typeof(PacketProcessingMiddlewareContract))]
  public abstract class PacketProcessingMiddleware
  {
    private readonly AppFunc next;

    protected AppFunc Next
    {
      get
      {
        Contract.Ensures(Contract.Result<AppFunc>() != null);
        return this.next;
      }
    }

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(Next != null);
      Contract.Invariant(this.next != null);
    }

    protected PacketProcessingMiddleware(AppFunc next)
    {
      this.next = next;
    }

    protected abstract Task Invoke(IPacketListenerContext context);

    public Task Invoke(IDictionary<string, object> environment)
    {
      Contract.Requires<ArgumentNullException>(environment != null);

      var packet = environment.Get<IPacketListenerContext>(typeof(IPacketListenerContext).FullName);
      return Invoke(packet);
    }
  }
}
