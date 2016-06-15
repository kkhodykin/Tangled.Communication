namespace Tangled.Communication.Transport.Abstractions
{
  /// <summary>
  /// Implementor is capable of handling 2-way communication accross messaging based transport layer.
  /// </summary>
  public interface IDuplexSession : ISender, IListener
  {
    
  }
}