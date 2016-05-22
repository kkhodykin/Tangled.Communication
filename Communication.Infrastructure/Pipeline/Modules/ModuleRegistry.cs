using System;
using System.Threading.Tasks;

namespace Tangled.Communication.Infrastructure.Pipeline.Modules
{
  public class ModuleRegistry
  {
    private readonly Module _head = new HeadModule();
    private Module _tail;

    public ModuleRegistry()
    {
      _tail = _head;
    }

    public ModuleRegistry Add<TModule>(Func<IPacketListenerContext, TModule> moduleFactory)
    {
      _tail.Next = new Module<TModule>(moduleFactory);
      _tail = _tail.Next;
      return this;
    }

    public Task<object> Invoke(IPacketListenerContext context)
    {
      _tail.Next = new TailModule();
      return _head.Invoke(context);
    }
  }
}
