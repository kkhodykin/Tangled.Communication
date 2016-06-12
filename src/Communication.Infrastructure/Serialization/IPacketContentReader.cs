using System;
using System.IO;
using System.Diagnostics.Contracts;

namespace Tangled.Communication.Infrastructure.Serialization
{
  [ContractClass(typeof(IPacketContentReaderContract))]
  public interface IPacketContentReader
  {
    object Read();
  }
}