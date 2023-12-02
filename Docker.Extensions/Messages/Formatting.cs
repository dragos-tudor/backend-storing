namespace Docker.Extensions;

partial class Messages
{
  static string FormatMessage(JSONMessage message) =>
    $"{message.Status} {message.ProgressMessage}";
}