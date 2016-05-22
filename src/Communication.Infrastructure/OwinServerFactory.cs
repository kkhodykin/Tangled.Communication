using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leverate.LXCRM.Communication.Transport.Abstractions;
using Tangled.Communication.Infrastructure.Extensions;

namespace Tangled.Communication.Infrastructure
{
  public static class OwinServerFactory
  {
    public static void Initialize(IDictionary<string, object> properties)
    {
      if (properties == null)
        throw new ArgumentNullException(nameof(properties));
      properties["owin.Version"] = (object) "1.0";
      var dictionary = properties.Get<IDictionary<string, object>>("server.Capabilities") ?? new Dictionary<string, object>();
      properties["server.Capabilities"] = (object) dictionary;
      properties[typeof(Gateway).FullName] = new Gateway();
    }

    public static IDisposable Create(Func<IDictionary<string, object>, Task> app, IDictionary<string, object> properties)
    {
      var gateway = properties.Get<Gateway>(typeof(Gateway).FullName) ?? new Gateway();
      var listeners = properties.Get<IEnumerable<IListener>>("communication.Listeners") ?? Enumerable.Empty<IListener>();
      gateway.AddListeners(listeners);
      gateway.Start(app);
      return gateway;
    }
  }
}
