using System;
using System.IO;
using System.Diagnostics.Contracts;

namespace Tangled.Communication.Infrastructure.Serialization
{
  [ContractClassFor(typeof(IPacketContentReader))]
  internal abstract class IPacketContentReaderContract : IPacketContentReader
  {
    public object Read()
    {
      Contract.Ensures(Contract.Result<object>() != null);
      return default(object);
    }
  }
}