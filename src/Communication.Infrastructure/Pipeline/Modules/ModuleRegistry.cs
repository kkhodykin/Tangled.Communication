using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace Tangled.Communication.Infrastructure.Pipeline.Modules
{
  public class ModuleRegistry
  {
    private readonly Module head = new HeadModule();
    private Module tail;

    public ModuleRegistry()
    {
      this.tail = this.head;
    }

    public ModuleRegistry Add<TModule>(Func<IPacketListenerContext, TModule> moduleFactory)
    {
      Contract.Requires<ArgumentNullException>(moduleFactory != null);
      Contract.Ensures(Contract.Result<ModuleRegistry>() != null);

      this.tail.Next = new Module<TModule>(moduleFactory);
      this.tail = this.tail.Next;
      return this;
    }

    internal Task<object> Invoke(IPacketListenerContext context)
    {
      Contract.Requires<ArgumentNullException>(context != null);

      this.tail.Next = new TailModule();
      return this.head.Invoke(context);
    }
  }
}
