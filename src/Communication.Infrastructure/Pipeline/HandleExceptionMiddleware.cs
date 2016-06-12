using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Tangled.Communication.Infrastructure.Pipeline
{
  public class HandleExceptionMiddleware
  {
    private readonly AppFunc next;
    private readonly ILogger logger;

    public HandleExceptionMiddleware(AppFunc next, ILogger logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(IDictionary<string, object> environment)
    {
      Contract.Ensures(Contract.Result<Task>() != null);

      try
      {
        await this.next(environment).ConfigureAwait(false);
      }
      catch (Exception e)
      {
          this.logger.LogCritical(e.Message, e);
      }
    }
  }
}
