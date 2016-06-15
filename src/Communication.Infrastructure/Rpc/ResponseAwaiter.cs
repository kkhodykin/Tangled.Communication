using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Infrastructure.Rpc
{
  internal class ResponseAwaiter : IDisposable
  {
    private readonly IDisposable subscription;

    private readonly BlockingCollection<IPacket> packetQueue = new BlockingCollection<IPacket>();

    public ResponseAwaiter(IListener listener)
    {
      this.subscription = listener.OnPacket(args => Task.Run(() => this.packetQueue.Add(args.Packet) ));
    }

    public Task<IPacket> AwaitCompletion(IPacket packet, CancellationToken cancellationToken)
    {
      return Task.Run(() =>
      {
        while (true)
        {
          var responsePacket = this.packetQueue.Take();
          if (responsePacket.CorrelationId != packet.CorrelationId) continue;
          return responsePacket;
        }
      }, cancellationToken);

    }

    public void Dispose()
    {
      this.subscription.Dispose();
    }
  }
}
