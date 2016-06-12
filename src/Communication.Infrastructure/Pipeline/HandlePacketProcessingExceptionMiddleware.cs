using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Tangled.Communication.Infrastructure.Extensions;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Infrastructure.Pipeline
{
  public class HandlePacketProcessingExceptionMiddleware : PacketProcessingMiddleware
  {
    public HandlePacketProcessingExceptionMiddleware(Func<IDictionary<string, object>, Task> next)
      : base(next)
    {
      Contract.Requires<ArgumentNullException>(next != null);
    }

    protected override Task Invoke(IPacketListenerContext context)
    {
      try
      {
        context.Logger.LogTrace($"Start processing message {context.Request.Id}");
        var result = Next(context.Environment);
        context.Logger.LogTrace($"Finished processing message {context.Request.Id}");
        return result;
      }
      catch (OperationCanceledException e)
      {
        context.Logger.LogWarning(e.Message, e);
        return context.Request.Abandon();
      }
      catch (Exception e)
      {
        context.Logger.LogError(e.Message, e);
        return Task.WhenAll(context.ReplyChannel.Send(e), context.Request.DeadLetter());
      }
    }
  }
}
