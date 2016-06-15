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
    internal static BrokeredMessage ToMessage(this IPacket packet)
    {
      var result = new BrokeredMessage(packet.Payload.GetBodyStream(), true)
      {
        CorrelationId = packet.CorrelationId,
        ContentType = packet.Payload.ContentType,
        Label = packet.Payload.Type,
        MessageId = packet.Id,
        ReplyTo = packet.ReplyTo,
      };

      foreach (var keyValuePair in packet.Headers)
      {
        result.Properties.Add(keyValuePair);
      }

      return result;
    }
  }
}
