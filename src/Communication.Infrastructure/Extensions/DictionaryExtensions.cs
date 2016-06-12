using System.Collections.Generic;

namespace Tangled.Communication.Infrastructure.Extensions
{
  internal static class DictionaryExtensions
  {
    internal static T Get<T>(this IDictionary<string, object> dictionary, string key)
    {
      object obj;
      if (!dictionary.TryGetValue(key, out obj))
        return default (T);
      return (T) obj;
    }

    internal static T Get<T>(this IDictionary<string, object> dictionary, string subDictionaryKey, string key)
    {
      var dictionary1 = dictionary.Get<IDictionary<string, object>>(subDictionaryKey);
      if (dictionary1 == null)
        return default (T);
      return dictionary1.Get<T>(key);
    }
  }
}