using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Tangled.Communication.Infrastructure.Extensions;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Infrastructure.Pipeline
{
  public class HandlePacketProcessingExceptionMiddleware : PacketProcessingMiddleware
  {
    public HandlePacketProcessingExceptionMiddleware(Func<IDictionary<string, object>, Task> next)
      : base(next) {}

    protected override async Task Invoke(IPacketListenerContext context)
    {
      try
      {
        await Next(context.Environment).ConfigureAwait(false);
      }
      catch (OperationCanceledException e)
      {
        context.Logger.LogWarning(e.Message, e);
        await context.Channel.Abandon(context.GetService<IPacket>()).ConfigureAwait(false);
      }
      catch (Exception e)
      {
        await context.Channel.Send(e).ConfigureAwait(false);
        await context.Channel.DeadLetter(context.GetService<IPacket>()).ConfigureAwait(false);
        context.Logger.LogError(e.Message, e);
      }
    }
  }
}
