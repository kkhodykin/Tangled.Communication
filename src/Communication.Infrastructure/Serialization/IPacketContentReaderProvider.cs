using System;
using Tangled.Communication.Transport.Abstractions;
using System.Diagnostics.Contracts;

namespace Tangled.Communication.Infrastructure.Serialization
{
  [ContractClass(typeof(IPacketContentReaderProviderContract))]
  public interface IPacketContentReaderProvider
  {
    IPacketContentReader GetReader(IPacketContent content, Type actualType);
  }
}