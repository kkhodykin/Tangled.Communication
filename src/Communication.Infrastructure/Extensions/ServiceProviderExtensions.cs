﻿using System;

namespace Tangled.Communication.Infrastructure.Extensions
{
  public static class ServiceProviderExtensions
  {
    /// <summary>
    /// Retrieve a service of type T from the IServiceProvider.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services"></param>
    /// <returns></returns>
    public static T GetService<T>(this IServiceProvider services)
    {
      if (services == null)
        throw new ArgumentNullException(nameof(services));
      return (T) services.GetService(typeof (T));
    }
  }
}
