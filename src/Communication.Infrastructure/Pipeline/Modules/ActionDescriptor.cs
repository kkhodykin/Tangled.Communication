using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Tangled.Communication.Infrastructure.Pipeline.Modules
{
  public class ActionDescriptor<TModule>
  {
    private readonly MethodInfo _convertTask;

    private readonly Func<TModule, object, Task<object>> _action;
    public Type RequestType { get; }


    public ActionDescriptor(MethodInfo method, ParameterInfo argument)
    {
      RequestType = argument.ParameterType;
      _action = BuildActionIvocation(method, RequestType);

      if (typeof(Task).IsAssignableFrom(method.ReturnType) && method.ReturnType.IsGenericType)
      {
        var returnType = method.ReturnType.GetGenericArguments().SingleOrDefault();
        _convertTask = typeof(TaskConvertor).GetMethod("Convert").MakeGenericMethod(returnType);
      }
    }

    public Task<object> CallAction(TModule module, object request)
    {
      return _action(module, request);
    }

    private Func<TModule, object, Task<object>> BuildActionIvocation(MethodInfo method, Type parameterType)
    {
      var moduleParam = Expression.Parameter(typeof(TModule), "m");
      var argParam = Expression.Parameter(typeof(object), "a");
      var call = Expression.Call(moduleParam, method, Expression.Convert(argParam, parameterType));

      if (method.ReturnType == typeof(void))
      {
        var lambda = Expression.Lambda<Action<TModule, object>>(call, moduleParam, argParam).Compile();
        return (m, r) =>
        {
          lambda(m, r);
          return Task.FromResult((object)null);
        };
      }
      else if (typeof(Task).IsAssignableFrom(method.ReturnType) && !method.ReturnType.IsGenericType)
      {
        var lambda = Expression.Lambda<Func<TModule, object, Task>>(call, moduleParam, argParam).Compile();
        return (m, r) => lambda(m, r).ContinueWith(t => (object)null);
      }
      else if (typeof(Task).IsAssignableFrom(method.ReturnType) && method.ReturnType.IsGenericType)
      {
        var convertedCall = Expression.Call(_convertTask, call);
        var lambda = Expression.Lambda<Func<TModule, object, Task<object>>>(convertedCall, moduleParam, argParam);
        return lambda.Compile();
      }
      else
      {
        var lambda = Expression.Lambda<Func<TModule, object, object>>(call, moduleParam, argParam).Compile();
        return (m, r) => Task.FromResult(lambda(m, r));
      }
    }

    public static class TaskConvertor
    {
      public static Task<object> Convert<T>(Task<T> task)
      {
        return task.ContinueWith(t => (object)t.Result);
      }
    }
  }
}
