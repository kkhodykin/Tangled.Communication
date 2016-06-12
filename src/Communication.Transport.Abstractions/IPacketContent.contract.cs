using System.IO;
using System.Diagnostics.Contracts;

namespace Tangled.Communication.Transport.Abstractions
{
  [ContractClassFor(typeof(IPacketContent))]
  internal abstract class IPacketContentContract : IPacketContent
  {
    public string ContentType
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    public string Type
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    public Stream GetBodyStream()
    {
      Contract.Ensures(Contract.Result<Stream>() != null);
      return default(Stream);
    }

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(ContentType != null);
      Contract.Invariant(Type != null);
    }

  }
}