namespace Tangled.Communication.Transport.Abstractions
{
  /// <summary>
  /// The connection settings used by the underlying transport layer.
  /// </summary>
  public class ConnectionSettings
  {
    /// <summary>
    /// The connection string used to connect to the communication gateway.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// The path of the entity on the communication gateway to connect to 
    /// (usually a <value>queue</value>, or <value>topic/subscription</value>, or <value>exchange/queue</value>)
    /// </summary>
    public string EntityPath { get; set; }

    /// <summary>
    /// Indicates if the <see cref="IListener"/> expects the packet to be retried after <see cref="IChannel.Abandon"/> was called.
    /// </summary>
    public bool ReentryAllowed { get; set; }

    /// <summary>
    /// Indicates if the <see cref="IListener"/> should create new subscription when connecting to the communication gateway or reuse existing one.
    /// </summary>
    public bool Private { get; set; }
  }
}
