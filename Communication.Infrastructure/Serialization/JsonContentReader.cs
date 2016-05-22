using System;
using System.IO;
using Newtonsoft.Json;

namespace Tangled.Communication.Infrastructure.Serialization
{
  internal class JsonContentReader : IPacketContentReader {
    public object Read(Stream bodyStream, Type payloadType)
    {
      using (var reader = new StreamReader(bodyStream))
      {
        return JsonConvert.DeserializeObject(reader.ReadToEnd(), payloadType);
      }
    }
  }
}
