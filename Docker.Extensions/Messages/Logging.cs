namespace Docker.Extensions;

static partial class Messages
{
  internal static void LogMessage (
    JSONMessage message,
    int messagesCounter,
    byte throttleThreshold,
    Func<JSONMessage, string> formatMesage,
    Action<string> writeMessage)
  {
    if((messagesCounter % throttleThreshold) != 0) return;

    writeMessage(formatMesage(message));
  }

  internal static Action<JSONMessage> LogMessages (
    byte throttleThreshold = 10,
    Func<JSONMessage, string>? formatMesage = default,
    Action<string>? writeMessage = default)
  {
    int messagesCounter = 0;
    return (message) =>
      LogMessage(
        message,
        messagesCounter++,
        throttleThreshold,
        formatMesage ?? FormatMessage,
        writeMessage ?? Console.WriteLine);
  }
}