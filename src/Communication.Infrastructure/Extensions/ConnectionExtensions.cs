using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Infrastructure.Extensions
{
  public static class ConnectionExtensions
  {
    public static TService GetServiceProxy<TService>(this IConnection connection)
      where TService : class 
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Returns default service instance listener.
    /// </summary>
    /// <returns></returns>
    /// <remarks>The default listener used to receive replies from other services and for system notifications.</remarks>
    public static IListener GetDefaultListener(this IConnection connection)
    {
      throw new NotImplementedException();
    }
  }
}
