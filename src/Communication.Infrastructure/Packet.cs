using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Infrastructure
{
  internal class Packet : IPacket
  {
    public IPacketContent Payload { get; }
    public HeaderCollection Headers { get; } = new HeaderCollection();
    public string Id { get; } = Guid.NewGuid().ToString();
    public string ReplyTo { get; set; }
    public string CorrelationId { get; set; }

    public Packet(object payload)
    {
      Payload = new PacketContent(payload);
    }

    public Packet(Exception exception)
    {
      exception.
    }
  }

  internal class PacketContent : IPacketContent
  {
    private readonly string content;

    public PacketContent(object payload)
    {
      ContentType = "application/json";
      Type = payload.GetType().FullName;
      this.content = JsonConvert.SerializeObject(payload);
    }

    public string ContentType { get; }
    public string Type { get; }
    public Stream GetBodyStream()
    {
      return new MemoryStream(Encoding.UTF8.GetBytes(this.content));
    }
  }
}
