using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Infrastructure
{
  public interface IPacketListenerContext : IPacket, IServiceProvider
  {
    IDictionary<string, object> Environment { get; }
    Type PayloadType { get; set; }
    IChannel Channel { get; }
    object Request { get; set; }
    object Response { get; set; }
    ILogger Logger { get; }
  }
}
