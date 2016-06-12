using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Tangled.Communication.Transport.Abstractions;
using Tangled.Communication.Transport.Azure.Extensions;

namespace Tangled.Communication.Transport.Azure
{
    /// <summary>
    /// Can send the <see cref = "IPacket"/> over the underlying transport layer.
    /// </summary>  
  public class PacketSender : ISender
  {
    private readonly MessageSender sender;

    /// <summary>
    /// Creates instance of <see cref="PacketSender"/> class
    /// </summary>
    /// <param name="sender"></param>
    public PacketSender(MessageSender sender)
    {
      this.sender = sender;
    }

    /// <summary>
    /// Send the <see cref = "IPacket"/> over the underlying transport layer. 
    /// </summary>
    /// <param name = "packet">The <see cref = "IPacket"/> to be sent.</param>
    /// <returns></returns>
    public Task Send(IPacket packet)
    {
      return this.sender.SendAsync(packet.ToMessage());
    }
  }
}
