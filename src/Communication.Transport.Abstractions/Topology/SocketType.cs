using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangled.Communication.Transport.Abstractions.Topology
{
  /// <summary>
  /// Combination of flags that controls socket lifetime and availability.
  /// </summary>
  [Flags]
  public enum SocketType
  {
    Permanent = 1,
    Ephemeral = 2,
    Exclusive = 4,
    Shared = 8
  }
}
