using System;
using System.Collections.Generic;
using Leverate.LXCRM.Communication.Transport.Abstractions;
using Microsoft.Extensions.Logging;

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
