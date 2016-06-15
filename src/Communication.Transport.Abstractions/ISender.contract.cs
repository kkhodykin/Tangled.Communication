using System;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Tangled.Communication.Transport.Abstractions
{
  [ContractClassFor(typeof(ISender))]
  internal abstract class ISenderContract : ISender
  {
    public IConnection Connection { get; }

    public Task Send(IPacket packet)
    {
      Contract.Requires<ArgumentNullException>(packet != null);
      return default(Task);
    }
  }
}