using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tangled.Communication.Infrastructure.Extensions;

namespace Tangled.Communication.Infrastructure.Pipeline
{
  public class DispatchResponseMiddleware : PacketProcessingMiddleware
  {
    public DispatchResponseMiddleware(Func<IDictionary<string, object>, Task> next)
      : base(next) { }

    protected override async Task Invoke(IPacketListenerContext context)
    {
      await context.Channel.Send(context.Response).ConfigureAwait(false);
    }
  }
}
