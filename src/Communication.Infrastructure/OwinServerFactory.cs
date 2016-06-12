using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Tangled.Communication.Infrastructure.Extensions;
using Tangled.Communication.Transport.Abstractions;

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

    public static void Create(Func<IDictionary<string, object>, Task> app, IDictionary<string, object> properties)
    {
      Contract.Requires<ArgumentNullException>(properties != null);

      var gateway = properties.Get<Gateway>(typeof(Gateway).FullName) ?? new Gateway();
      var listeners = properties.Get<IEnumerable<IListener>>("communication.Listeners") ?? Enumerable.Empty<IListener>();
      gateway.AddListeners(listeners);
      gateway.Start(app);
    }
  }
}
