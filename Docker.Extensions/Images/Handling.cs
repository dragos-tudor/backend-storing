namespace Docker.Extensions;

partial class Images
{
  internal static void HandleProgressMessage(JSONMessage message)
  {
    if((DateTime.Now.Microsecond % 5) == 0) return;
    Console.WriteLine($"{message.Status} {message.ProgressMessage}");
  }

}