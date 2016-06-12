using System;
using Owin;
using Tangled.Communication.Infrastructure.Pipeline;
using Tangled.Communication.Infrastructure.Pipeline.Modules;

namespace Tangled.Communication.Infrastructure.Extensions
{
  public static class AppBuilderExtensions
  {
    public static IAppBuilder UseModules(this IAppBuilder app, Action<ModuleRegistry> modules)
    {
      if (app == null)
        throw new ArgumentNullException(nameof(app));

      if (modules == null) throw new ArgumentNullException(nameof(modules));

      var registry = new ModuleRegistry();
      modules(registry);

      return app.Use<HandleExceptionMiddleware>()
        .Use<HandlePacketProcessingExceptionMiddleware>()
        .Use<UseModulesMiddleware>(registry)
        .Use<DispatchResponseMiddleware>();
    }
  }
}
