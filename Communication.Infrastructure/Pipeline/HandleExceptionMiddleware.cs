using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Tangled.Communication.Infrastructure.Pipeline
{
  public class HandleExceptionMiddleware
  {
    private readonly AppFunc _next;
    private readonly ILogger _logger;

    public HandleExceptionMiddleware(AppFunc next, ILogger logger)
    {
      _next = next;
      _logger = logger;
    }

    public async Task Invoke(IDictionary<string, object> environment)
    {
      try
      {
        await _next(environment).ConfigureAwait(false);
      }
      catch (Exception e)
      {
        _logger.LogCritical(e.Message, e);
      }
    }
  }
}
