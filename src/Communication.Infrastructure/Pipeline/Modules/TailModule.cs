using System;
using System.Threading.Tasks;

namespace Tangled.Communication.Infrastructure.Pipeline.Modules
{
  class TailModule : Module
  {
    public override Task<object> Invoke(IPacketListenerContext context)
    {
      throw new OperationCanceledException(
        $"Unable to find module capable of handling the message of type \"{context.Request.Payload.Type}\"");
    }
  }
}