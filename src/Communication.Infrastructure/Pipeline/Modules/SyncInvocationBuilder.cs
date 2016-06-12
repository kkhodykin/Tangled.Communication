using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Tangled.Communication.Infrastructure.Pipeline.Modules
{
  internal class SyncInvocationBuilder : InvocationBuilder
  {
    public SyncInvocationBuilder(MethodInfo method, Type parameterType, Type moduleType) 
      : base(method, parameterType, moduleType)
    {
      Contract.Requires<ArgumentNullException>(method != null);
      Contract.Requires<ArgumentNullException>(parameterType != null);
      Contract.Requires<ArgumentNullException>(moduleType != null);
    }

    public override Func<TModule, object, Task<object>> GetHandlerInvokation<TModule>(Type returnType)
    {
      if (returnType != typeof(void))
      {
        return (m, r) => Task.FromResult(
          Expression.Lambda<Func<TModule, object, object>>(Call, ModuleParam, ArgParam)
            .Compile()(m, r));
      }

      var lambda = Expression.Lambda<Action<TModule, object>>(Call, ModuleParam, ArgParam)
        .Compile();
      return (m, r) =>
      {
        lambda(m, r);
        return Task.FromResult((object)null);
      };
    }
  }
}