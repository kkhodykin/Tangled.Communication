using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
      Contract.Requires<ArgumentNullException>(sender != null);
      Contract.Requires<ArgumentNullException>(payload != null);

      return sender.Send(payload.Pack());
    }

    public static Task Send(this ISender sender, Exception exception)
    {
      Contract.Requires<ArgumentNullException>(sender != null);
      Contract.Requires<ArgumentNullException>(exception != null);

      return sender.Send(exception.Pack());
    }
  }
}
