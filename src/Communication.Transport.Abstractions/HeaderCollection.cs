using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Tangled.Communication.Transport.Abstractions
{
  /// <summary>
  /// <see cref="IPacket"/> header collection.
  /// </summary>
  public class HeaderCollection : IEnumerable<KeyValuePair<string, object>>
  {
    /// <summary>
    /// The headers
    /// </summary>
    private readonly IDictionary<string, object> headers;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeaderCollection"/> class. 
    /// </summary>
    public HeaderCollection() : this(null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HeaderCollection"/> class. 
    /// </summary>
    /// <param name="headers">
    /// Headers dictionary
    /// </param>
    public HeaderCollection(IDictionary<string, object> headers)
    {
      this.headers = headers ?? new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Indexer property
    /// </summary>
    /// <param name="key">
    /// The <paramref name="key"/> used to retrieve header by name.
    /// </param>
    /// <returns>
    /// The <see cref="object"/> header.
    /// </returns>
    public object this[string key]
    {
      get
      {
        Contract.Requires<ArgumentNullException>(key != null);
        return this.headers.ContainsKey(key) ? this.headers[key] : null;
      }
      set
      {
        Contract.Requires<ArgumentNullException>(key != null);
        this.headers[key] = value;
      }


    }

    /// <summary>
    /// Used to check the header's presence.
    /// </summary>
    /// <param name="header">The <c>header</c> name.</param>
    /// <returns>Whether the <c>header</c> is present in the collection</returns>
    public bool Has(string header)
    {
      Contract.Requires<ArgumentNullException>(header != null);
      return this.headers.ContainsKey(header);
    }

    /// <summary>Returns an enumerator that iterates through the collection.</summary>
    /// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
    /// <filterpriority>1</filterpriority>
    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      return this.headers.GetEnumerator();
    }

    /// <summary>Returns an enumerator that iterates through a collection.</summary>
    /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
    /// <filterpriority>2</filterpriority>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }
}