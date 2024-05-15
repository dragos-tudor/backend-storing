
namespace Docker.Extensions;

partial class DockerFuncs
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