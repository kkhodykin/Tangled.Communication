using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Tangled.Communication.Transport.Abstractions;
using Tangled.Communication.Transport.Azure.Extensions;

namespace Tangled.Communication.Transport.Azure
{
  public class PacketSender : ISender
  {
    private readonly MessageSender _sender;

    public PacketSender(MessageSender sender)
    {
      _sender = sender;
    }

    public Task Send(IPacket packet)
    {
      return _sender.SendAsync(packet.ToMessage());
    }
  }
}
