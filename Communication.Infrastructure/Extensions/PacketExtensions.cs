using System.Collections.Generic;
using Leverate.LXCRM.Communication.Transport.Abstractions;
using Tangled.Communication.Infrastructure.Serialization;

namespace Tangled.Communication.Infrastructure.Extensions
{
  internal static class PacketExtensions
  {
    public static IDictionary<string, object> ToDictionary(this PacketReceivedCallbackArgs args)
    {
      var result = new Dictionary<string, object>
      {
        [typeof(IPacket).FullName] = args.Packet
      };
      return result;
    }

    public static object GetBody(this IPacketListenerContext context)
    {
      var readerProvider = context.GetService<IPacketContentReaderProvider>()?.GetReader(context.Payload.ContentType);
      return readerProvider?.Read(context.Payload.GetBodyStream(), context.PayloadType);
    }
  }
}
