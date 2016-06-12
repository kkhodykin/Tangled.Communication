using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using static System.Diagnostics.Contracts.Contract;

namespace Tangled.Communication.Infrastructure.Pipeline.Modules
{
  [ContractClass(typeof(InvocationBuilderContract))]
  internal abstract class InvocationBuilder
  {
    protected ParameterExpression ArgParam { get; }
    protected MethodCallExpression Call { get; }
    protected ParameterExpression ModuleParam { get; }

    public static Func<TModule, object, Task<object>> Create<TModule>(MethodInfo method, Type parameterType)
    {
      Requires<ArgumentNullException>(method != null);
      Requires<ArgumentNullException>(parameterType != null);

      var builder = typeof(Task).IsAssignableFrom(method.ReturnType)
        ? (InvocationBuilder)new AsyncInvocationBuilder(method, parameterType, typeof(TModule))
        : new SyncInvocationBuilder(method, parameterType, typeof(TModule));

      return builder.GetHandlerInvokation<TModule>(method.ReturnType);
    }

    protected InvocationBuilder(MethodInfo method, Type parameterType, Type moduleType)
    {
      ModuleParam = Expression.Parameter(moduleType, "m");
      ArgParam = Expression.Parameter(typeof(object), "a");
      var convert = Expression.Convert(ArgParam, parameterType);

      Call = Expression.Call(ModuleParam, method, convert);
    }

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Invariant(ArgParam != null);
    }

    public abstract Func<TModule, object, Task<object>> GetHandlerInvokation<TModule>(Type returnType);
  }
}