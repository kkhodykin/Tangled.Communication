using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Microsoft.Extensions.Logging;
using Tangled.Communication.Infrastructure.Serialization;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Infrastructure
{
  [ContractClass(typeof(IPacketListenerContextContract))]
  public interface IPacketListenerContext : IServiceProvider
  {
    IDictionary<string, object> Environment { get; }
    IIncomingPacket Request { get; set; }
    IPacket Response { get; set; }
    ILogger Logger { get; }
    ISender ReplyChannel { get; }
  }
}
