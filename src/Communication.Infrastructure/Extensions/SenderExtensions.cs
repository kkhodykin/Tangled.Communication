using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Infrastructure.Extensions
{
  public static class SenderExtensions
  {
    public static Task Send(this ISender sender, object payload)
    {
      throw new NotImplementedException();
    }

    public static Task Send(this ISender sender, Exception exception)
    {
      throw new NotImplementedException();
    }
  }
}
