using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Tangled.Communication.Transport.Abstractions;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Tangled.Communication.Infrastructure
{
  using System.Configuration;

  /// <summary> 
  /// The gateway.
  /// </summary>
  public class Gateway
  {
    /// <summary>
    /// The listeners collection
    /// </summary>
    private readonly List<IListener> listeners = new List<IListener>();

    /// <summary>
    /// The application
    /// </summary>
    private AppFunc app;

    /// <summary>
    /// Adds the <c>listener</c> to the collection.
    /// </summary>
    /// <param name="listener">The <c>listener</c>.</param>
    public void AddListener(IListener listener)
    {
      Contract.Requires<ArgumentNullException>(listener != null);
      
      this.listeners.Add(listener);
    }

    /// <summary>
    /// Adds the listeners.
    /// </summary>
    /// <param name="listeners">The listeners.</param>
    public void AddListeners(IEnumerable<IListener> listeners)
    {
      Contract.Requires<ArgumentNullException>(listeners != null);

      this.listeners.AddRange(listeners);
    }

    /// <summary>
    /// Starts the specified application.
    /// </summary>
    /// <param name="app">The application.</param>
    internal void Start(AppFunc app)
    {
      Contract.Requires<ArgumentNullException>(app != null);

      this.app = app;
      this.listeners.ForEach(l => l.OnPacket(OnPacket));
    }

    /// <summary>
    /// Called when <see cref="IPacket"/> received.
    /// </summary>
    /// <param name="args">The arguments.</param>
    /// <returns>Task that completes after all handlers has finished.</returns>
    private Task OnPacket(PacketReceivedCallbackArgs args)
    {
      var context = new PacketListenerContext(args.Packet, args.Connection);
      return this.app(context.Environment);
    }
  }
}
