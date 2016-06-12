using System;
using System.Diagnostics.Contracts;

namespace Tangled.Communication.Transport.Abstractions
{
  [ContractClassFor(typeof(IConnection))]
  internal abstract class IConnectionContract : IConnection
  {
    public string ConnectionString
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(ConnectionString != null);
    }

    public IListener CreateListener(string path)
    {
      Contract.Requires<ArgumentNullException>(path != null);
      return default(IListener);
    }
    public IListener CreateListener(string path, bool allowRetries)
    {
      Contract.Requires<ArgumentNullException>(path != null);
      return default(IListener);
    }
    public IListener CreateListener(string path, bool allowRetries, bool @private)
    {
      Contract.Requires<ArgumentNullException>(path != null);
      return default(IListener);
    }
    public ISender CreateSender(string path)
    {
      Contract.Requires<ArgumentNullException>(path != null);
      return default(ISender);
    }
  }
}