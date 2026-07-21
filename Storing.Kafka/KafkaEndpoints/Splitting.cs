
namespace Storing.Kafka;

partial class KafkaFuncs
{
  const char EndpointSeparator = ',';

  public static IEnumerable<string> SplitKafkaEndpoints(string? endpoints)
      => string.IsNullOrWhiteSpace(endpoints)
        ? []
        : endpoints.Split(EndpointSeparator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
}