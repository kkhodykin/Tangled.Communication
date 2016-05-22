using System;
using System.IO;

namespace Tangled.Communication.Infrastructure.Serialization
{
  public interface IPacketContentReader
  {
    object Read(Stream bodyStream, Type payloadType);
  }
}
