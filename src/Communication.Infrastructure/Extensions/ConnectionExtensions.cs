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
  }
}
