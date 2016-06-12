using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangled.Communication.Transport.Abstractions;

namespace Tangled.Communication.Infrastructure.Extensions
{
  public static class ObjectExtensions
  {
    public static IPacket Pack(this object content)
    {
      throw new NotImplementedException();
    }

    public static IPacket Pack(this Exception exception)
    {
      throw new NotImplementedException();
    }
  }
}
