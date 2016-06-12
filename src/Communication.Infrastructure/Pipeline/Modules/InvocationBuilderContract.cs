using System;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Threading.Tasks;

namespace Tangled.Communication.Infrastructure.Pipeline.Modules
{
  [ContractClassFor(typeof(InvocationBuilder))]
  internal abstract class InvocationBuilderContract : InvocationBuilder
  {
    protected InvocationBuilderContract(MethodInfo method, Type parameterType, Type moduleType) : base(method, parameterType, moduleType)
    {
      Contract.Requires<ArgumentNullException>(method != null);
      Contract.Requires<ArgumentNullException>(parameterType != null);
      Contract.Requires<ArgumentNullException>(moduleType != null);
    }

    public override Func<TModule, object, Task<object>> GetHandlerInvokation<TModule>(Type returnType)
    {
      Contract.Requires<ArgumentNullException>(returnType != null);
      Contract.Ensures(Contract.Result<Func<TModule, object, Task<object>>>() != null);

      return default(Func<TModule, object, Task<object>>);
    }
  }
}