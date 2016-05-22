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
    public Type PayloadType { get; set; }
    public IEnumerable<object> HandlerResponseCollection { get; set; }

    public ILogger Logger => this.GetService<ILogger>();
    public IChannel Channel => this.GetService<IChannel>();

    public PacketListenerContext()
    {
      var dictionary = new Dictionary<string, object>(StringComparer.Ordinal)
      {
        [typeof(IPacketListenerContext).FullName] = this
      };
      Environment = dictionary;
    }

    public PacketListenerContext(IPacket packet, IChannel channel) 
      : this()
    {
      Payload = packet.Payload;
      Headers = packet.Headers;
      Id = packet.Id;
      ReplyTo = packet.ReplyTo;
      Environment[typeof(IPacket).FullName] = packet;
      Environment[typeof(IChannel).FullName] = channel;
    }

    public IPacketContent Payload
    {
      get { return Environment.Get<IPacketContent>("packet.Payload"); }
      private set { Environment["packet.Payload"] = value; }
    }

    public HeaderCollection Headers
    {

      get { return Environment.Get<HeaderCollection>("packet.Headers"); }
      private set { Environment["packet.Headers"] = value; }
    }

    public string Id
    {
      get { return Environment.Get<string>("packet.Id"); }
      private set { Environment["packet.Id"] = value; }
    }

    public string ReplyTo
    {
      get { return Environment.Get<string>("packet.ReplyTo"); }
      private set { Environment["packet.ReplyTo"] = value; }
    }

    public string To
    {
      get { return Environment.Get<string>("packet.To"); }
      private set { Environment["packet.To"] = value; }
    }

    public object Request
    {
      get { return Environment.Get<object>("packet.Request"); }
      set { Environment["packet.Reaquest"] = value; }
    }

    public object Response
    {
      get { return Environment.Get<object>("packet.Response"); }
      set { Environment["packet.Response"] = value; }
    }

    public object GetService(Type type)
    {
      Contract.Requires<ArgumentNullException>(type != null);

      object value;
      return Environment.TryGetValue(type.FullName, out value)
        ? value
        : null;
    }
  }
}
