using System;
using System.Collections;
using System.Collections.Generic;

namespace Tangled.Communication.Transport.Abstractions
{
  public class HeaderCollection : IEnumerable<KeyValuePair<string, object>>
  {
    private readonly IDictionary<string, object> _headers;

    public HeaderCollection() : this(null)
    {
    }

    public HeaderCollection(IDictionary<string, object> headers)
    {
      _headers = headers ?? new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
    }

    public object this[string key]
    {
      get { return _headers.ContainsKey(key) ? _headers[key] : null; }
      set { _headers[key] = value; }
    }

    public bool Has(string header)
    {
      return _headers.ContainsKey(header);
    }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      return _headers.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}