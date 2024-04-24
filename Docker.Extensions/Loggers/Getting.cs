
using static Docker.Extensions.Messages;

namespace Docker.Extensions;

static partial class Loggers
{
  internal static Action<JSONMessage> GetDockerLogger (Action<string>? writeMessage = default)
  {
    var writer = writeMessage ?? Console.WriteLine;
    return (message) => {
      if(message.Error is null)
        writer(FormatDockerMessage(message));
    };
  }

  internal static Action<JSONMessage> GetDockerErrorLogger (Action<string>? writeMessage = default)
  {
    var writer = writeMessage ?? Console.WriteLine;
    return (message) => {
      if(message.Error is not null)
        writer(FormatDockerErrorMessage(message));
    };
  }
}