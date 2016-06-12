using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Tangled.Communication.Transport.Abstractions
{
  [ContractClassFor(typeof(IPacket))]
  internal abstract class IPacketContract : IPacket
  {
    public IPacketContent Payload
    {
      get
      {
        Contract.Ensures(Contract.Result<IPacketContent>() != null);
        return default(IPacketContent);
      }
    }

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(Payload != null);
    }

    public HeaderCollection Headers
    {
      get
      {
        //Code contract checks here...
        return default(HeaderCollection);
      }
    }
    public string Id
    {
      get
      {
        //Code contract checks here...
        return default(string);
      }
    }
    public string ReplyTo
    {
      get
      {
        //Code contract checks here...
        return default(string);
      }
    }
    public string CorrelationId
    {
      get
      {
        //Code contract checks here...
        return default(string);
      }
    }
    public Task Complete()
    {
      //Code contract checks here...
      return default(Task);
    }
    public Task Abandon()
    {
      //Code contract checks here...
      return default(Task);
    }
    public Task DeadLetter()
    {
      //Code contract checks here...
      return default(Task);
    }
  }
}