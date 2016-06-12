using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace Tangled.Communication.Infrastructure.Pipeline.Modules
{
  public sealed class ModuleDescriptor<TModule>
  {
    private readonly ConcurrentDictionary<string, ActionDescriptor<TModule>> actions;

    public ModuleDescriptor(Type type)
    {
      Contract.Requires<ArgumentNullException>(type != null);

      this.actions =
        new ConcurrentDictionary<string, ActionDescriptor<TModule>>(FindActions(type), StringComparer.OrdinalIgnoreCase);
    }

    private static IDictionary<string, ActionDescriptor<TModule>> FindActions(Type type)
    {
      return type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
        .Where(m => !m.IsGenericMethod && !m.IsGenericMethodDefinition)
        .Select(m => new {Method = m, Arguments = m.GetParameters()})
        .Where(m => m.Arguments.Length == 1)
        .Select(m=>
          new
          {
            m.Method,
            Argument = m.Arguments.First(),
            ArgumentType = m.Arguments.First().ParameterType
          })
        .Where(m=>!m.ArgumentType.IsGenericType && !m.ArgumentType.IsGenericTypeDefinition)
        .Select(m => new { Action = new ActionDescriptor<TModule>(m.Method, m.Argument), Type = m.ArgumentType.FullName})
        .ToDictionary(a=>a.Type, a=>a.Action);
    }

    public ActionDescriptor<TModule> GetAction(string type)
    {
      Contract.Requires<ArgumentNullException>(type != null);

      ActionDescriptor<TModule> result;
      return this.actions.TryGetValue(type, out result) ? result : null;
    }
  }
}
