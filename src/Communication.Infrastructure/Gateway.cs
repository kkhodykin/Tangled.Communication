using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Leverate.LXCRM.Communication.Transport.Abstractions;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Tangled.Communication.Infrastructure
{
  ///<summary>The gateway.</summary>
  public class Gateway : IDisposable
  {
    private AppFunc _app;

    private readonly List<IListener> _listeners = new List<IListener>();

    public void AddListener(IListener listener)
    {
      _listeners.Add(listener);
    }

    public void AddListeners(IEnumerable<IListener> listeners)
    {
      _listeners.AddRange(listeners);
    }

    internal void Start(AppFunc app)
    {
      _app = app;
      _listeners.ForEach(l => l.OnPacket(OnPacket));
    }

    private Task OnPacket(PacketReceivedCallbackArgs args)
    {
      var context = new PacketListenerContext(args.Packet, args.Channel);
      return _app(context.Environment);
    }

    void IDisposable.Dispose()
    {
      _listeners.ForEach(l=>l.Dispose());
    }
  }
}
