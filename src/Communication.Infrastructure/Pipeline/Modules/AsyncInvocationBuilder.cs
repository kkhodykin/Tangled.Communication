using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Tangled.Communication.Infrastructure.Pipeline.Modules
{
  internal class AsyncInvocationBuilder : InvocationBuilder
  {
    private readonly MethodCallExpression convertCall;

    public static Task<object> Convert<T>(Task<T> task)
    {
      return (task ?? Task.FromResult(default(T))).ContinueWith(t => (object) t.Result);
    }

    public AsyncInvocationBuilder(MethodInfo method, Type parameterType, Type moduleType) 
      : base(method, parameterType, moduleType)
    {
      Contract.Requires<ArgumentNullException>(method != null);
      Contract.Requires<ArgumentNullException>(parameterType != null);
      Contract.Requires<ArgumentNullException>(moduleType != null);

      this.convertCall = GetConvertCall(method.ReturnType);
    }

    public override Func<TModule, object, Task<object>> GetHandlerInvokation<TModule>(Type returnType)
    {
      if (!returnType.IsGenericType)
      {
        return (m, r) => Expression.Lambda<Func<TModule, object, Task>>(Call, ModuleParam, ArgParam)
          .Compile()(m, r)
          .ContinueWith(t => (object)null);
      }

      return Expression.Lambda<Func<TModule, object, Task<object>>>(this.convertCall, ModuleParam, ArgParam).Compile();
    }

    private MethodCallExpression GetConvertCall(Type returnType)
    {
      var innerReturnType = returnType.GetGenericArguments().SingleOrDefault();
      var convertTask = GetType().GetMethod("Convert").MakeGenericMethod(innerReturnType);
      return Expression.Call(convertTask, Call);
    }
  }
}