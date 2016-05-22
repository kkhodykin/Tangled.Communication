using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leverate.LXCRM.Communication.Transport.Abstractions
{
  public class ConnectionSettings
  {
    public string ConnectionString { get; set; }
    public string EntityPath { get; set; }
    public bool ReentryAllowed { get; set; }
    public bool Private { get; set; }
  }
}
