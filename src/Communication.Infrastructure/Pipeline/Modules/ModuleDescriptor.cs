using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tangled.Communication.Infrastructure.Pipeline.Modules
{
  public sealed class ModuleDescriptor<TModule>
  {
    private readonly ConcurrentDictionary<string, ActionDescriptor<TModule>> _actions;

    public ModuleDescriptor(Type type)
    {
      _actions =
        new ConcurrentDictionary<string, ActionDescriptor<TModule>>(FindActions(type), StringComparer.OrdinalIgnoreCase);
    }

    private IDictionary<string, ActionDescriptor<TModule>> FindActions(Type type)
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
      ActionDescriptor<TModule> result;
      return _actions.TryGetValue(type, out result) ? result : null;
    }
  }
}
