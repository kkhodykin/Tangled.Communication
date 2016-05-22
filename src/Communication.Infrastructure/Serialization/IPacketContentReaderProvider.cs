namespace Tangled.Communication.Infrastructure.Serialization
{
  public interface IPacketContentReaderProvider
  {
    IPacketContentReader GetReader(string contentType);
  }
}
