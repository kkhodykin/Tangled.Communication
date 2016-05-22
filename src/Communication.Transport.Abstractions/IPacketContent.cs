using System.IO;

namespace Tangled.Communication.Transport.Abstractions
{
  public interface IPacketContent
  {
    string ContentType { get; }
    string Type { get; }
    string QualifiedType { get; }
    Stream GetBodyStream();
  }
}
