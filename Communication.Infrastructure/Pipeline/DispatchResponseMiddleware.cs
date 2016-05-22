using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tangled.Communication.Infrastructure.Pipeline
{
  public class DispatchResponseMiddleware : PacketProcessingMiddleware
  {
    public DispatchResponseMiddleware(Func<IDictionary<string, object>, Task> next)
      : base(next) { }

    protected override async Task Invoke(IPacketListenerContext context)
    {
      await context.Channel.Reply(context.Response).ConfigureAwait(false);
    }
  }
}
