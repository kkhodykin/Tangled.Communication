using System;
using Tangled.Communication.Transport.Abstractions;
using System.Diagnostics.Contracts;

namespace Tangled.Communication.Infrastructure.Serialization
{
  [ContractClassFor(typeof(IPacketContentReaderProvider))]
  internal abstract class IPacketContentReaderProviderContract : IPacketContentReaderProvider
  {
    public IPacketContentReader GetReader(IPacketContent content, Type actualType)
    {
      Contract.Requires<ArgumentNullException>(content != null);
      Contract.Requires<ArgumentNullException>(actualType != null);
      Contract.Ensures(Contract.Result<IPacketContentReader>() != null);

      return default(IPacketContentReader);
    }
  }
}