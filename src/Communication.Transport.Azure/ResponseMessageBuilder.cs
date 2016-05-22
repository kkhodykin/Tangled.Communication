using System;
using System.IO;
using System.Text;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace Tangled.Communication.Transport.Azure
{
  class ResponseMessageBuilder
  {
    private readonly BrokeredMessage _request;

    public ResponseMessageBuilder(BrokeredMessage request)
    {
      _request = request;
    }

    public BrokeredMessage BuildResponse(object payload)
    {
      var serialized = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(payload));
      var message = new BrokeredMessage(new MemoryStream(serialized), true);
      var type = payload.GetType();
      message.To = _request.ReplyTo;
      message.Properties["X-Type"] = JsonConvert.SerializeObject(type);
      message.CorrelationId = type.FullName;
      if (payload is Exception)
        message.Label = "Fault";

      return message;
    }
  }
}
