using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Transport.Azure.Extensions
{
  internal static class PacketExtensions
  {
    public static BrokeredMessage ToMessage(this IPacket packet)
    {
      throw new NotImplementedException();
    }
  }
}
