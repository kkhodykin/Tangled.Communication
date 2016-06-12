using System;
using System.Diagnostics.Contracts;
using System.IO;
using Newtonsoft.Json;

namespace Tangled.Communication.Infrastructure.Serialization
{
  internal class JsonContentReader : IPacketContentReader {
    private readonly Stream contentStream;
    private readonly Type type;

    public JsonContentReader(Stream contentStream, Type type)
    {
      this.contentStream = contentStream;
      this.type = type;
      Contract.Requires<ArgumentNullException>(contentStream != null);
      Contract.Requires<ArgumentNullException>(type != null);

    }

    public object Read()
    {
      using (var reader = new StreamReader(this.contentStream))
      {
        return JsonConvert.DeserializeObject(reader.ReadToEnd(), this.type);
      }
    }
  }
}
