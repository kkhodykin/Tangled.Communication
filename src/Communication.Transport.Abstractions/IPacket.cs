using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Leverate.LXCRM.Communication.Transport.Abstractions
{
  public interface IPacket
  {
    IPacketContent Payload { get; }
    HeaderCollection Headers { get; }
    string Id { get; }
    string ReplyTo { get; }
    string To { get; }
  }
}
