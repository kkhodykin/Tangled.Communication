namespace Tangled.Communication.Infrastructure.Serialization
{
  internal class PacketContentReaderProvider : IPacketContentReaderProvider {
    public IPacketContentReader GetReader(string contentType)
    {
      return contentType == "application/octet-stream"
        ? (IPacketContentReader) new BinaryContentReader()
        : (contentType == "applicaton/json" ? new JsonContentReader() : null);
    }
  }
}
