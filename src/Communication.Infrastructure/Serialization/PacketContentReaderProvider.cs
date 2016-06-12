using System;
using System.Diagnostics.Contracts;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Infrastructure.Serialization
{
  internal class PacketContentReaderProvider : IPacketContentReaderProvider {
    public IPacketContentReader GetReader(IPacketContent content, Type actualType)
    {
      return content.ContentType == "application/octet-stream"
        ? (IPacketContentReader) new BinaryContentReader(content.GetBodyStream(), actualType)
        : new JsonContentReader(content.GetBodyStream(), actualType);
    }
  }
}
