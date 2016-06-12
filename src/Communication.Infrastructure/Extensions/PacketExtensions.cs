using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Tangled.Communication.Infrastructure.Serialization;
using Tangled.Communication.Transport.Abstractions;

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

    public static IPacketContentReaderProvider GetReaderProvider(this IPacketListenerContext context)
    {
      Contract.Requires<ArgumentNullException>(context != null);
      Contract.Ensures(Contract.Result<IPacketContentReaderProvider>() != null);

      return context.GetService<IPacketContentReaderProvider>();
    }

    public static object GetBody(this IPacketListenerContext context, Type type)
    {
      Contract.Requires<ArgumentNullException>(context != null);
      Contract.Requires<ArgumentNullException>(type != null);
      Contract.Ensures(Contract.Result<object>() != null);

      var reader = context.GetReaderProvider()
        .GetReader(context.Request.Payload, type);
      var body = reader.Read();
      return body;
    }
  }
}
