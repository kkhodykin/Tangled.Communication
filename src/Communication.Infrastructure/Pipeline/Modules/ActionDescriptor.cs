using System;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Threading.Tasks;

namespace Tangled.Communication.Infrastructure.Pipeline.Modules
{
  public class ActionDescriptor<TModule>
  {
    private readonly Func<TModule, object, Task<object>> action;
    private readonly Type requestType;

    public Type RequestType
    {
      get
      {
        Contract.Ensures(Contract.Result<Type>() != null);
        return this.requestType;
      }
    }

    public ActionDescriptor(MethodInfo method, ParameterInfo argument)
    {
      Contract.Requires<ArgumentNullException>(method != null);
      Contract.Requires<ArgumentNullException>(argument != null);

      this.requestType = argument.ParameterType;
      this.action = InvocationBuilder.Create<TModule>(method, RequestType);
    }

    public Task<object> CallAction(TModule module, object request)
    {
      Contract.Requires<ArgumentNullException>(module != null);
      Contract.Requires<ArgumentNullException>(request != null);

      return this.action(module, request);
    }

  }
}
