using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Microsoft.Extensions.Logging;
using Tangled.Communication.Infrastructure.Extensions;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Infrastructure
{
  internal class PacketListenerContext : IPacketListenerContext
  {
    public IDictionary<string, object> Environment { get; set; }
    public IEnumerable<object> HandlerResponseCollection { get; set; }

    public PacketListenerContext()
    {
      var dictionary = new Dictionary<string, object>(StringComparer.Ordinal)
      {
        [typeof(IPacketListenerContext).FullName] = this
      };
      Environment = dictionary;
    }

    public PacketListenerContext(IPacket packet, IConnection connection) 
      : this()
    {
      var replyChannel = connection.CreateSender(packet.ReplyTo);
      Request = packet;
      Environment[typeof(ISender).FullName] = replyChannel;
    }

    public ILogger Logger => this.GetService<ILogger>();
    public ISender ReplyChannel => this.GetService<ISender>();

    public IPacket Request
    {
      get { return Environment.Get<IPacket>("packet.Request"); }
      set { Environment["packet.Reaquest"] = value; }
    }

    public IPacket Response
    {
      get { return Environment.Get<IPacket>("packet.Response"); }
      set { Environment["packet.Response"] = value; }
    }

    public object GetService(Type type)
    {
      object value;
      return Environment.TryGetValue(type.FullName, out value)
        ? value
        : null;
    }
  }
}
