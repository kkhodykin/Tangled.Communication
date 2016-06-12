using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace Tangled.Communication.Infrastructure.Pipeline
{
  [ContractClassFor(typeof(PacketProcessingMiddleware))]
  public abstract class PacketProcessingMiddlewareContract : PacketProcessingMiddleware
  {
    protected override Task Invoke(IPacketListenerContext context)
    {
      Contract.Requires<ArgumentNullException>(context != null);
      return default(Task);
    }

    protected PacketProcessingMiddlewareContract(Func<IDictionary<string, object>, Task> next) : base(next)
    {
      Contract.Requires<ArgumentNullException>(next != null);
    }
  }
}