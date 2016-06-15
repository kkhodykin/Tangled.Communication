using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;
using Tangled.Communication.Infrastructure.Extensions;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Infrastructure.Rpc
{
  public static class SenderExtensions
  {
    public static async Task<IPacket> ExecuteRequest(this ISender sender, IPacket request, CancellationToken cancellationToken)
    {
      Contract.Ensures(Contract.Result<Task<IPacket>>() != null);

      var packet = request.Pack();
      var listener = sender.Connection.GetDefaultListener();
      using (var awaiter = new ResponseAwaiter(listener))
      {
        await sender.Send(packet).ConfigureAwait(false);
        return await awaiter.AwaitCompletion(request, cancellationToken).ConfigureAwait(false);
      }
    }
  }
}