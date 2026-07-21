
namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static IEnumerable<string> SplitKafkaEndpoints(string? endpoints)
      => string.IsNullOrWhiteSpace(endpoints)
        ? ["localhost:9092"]
        : endpoints.Split([',', ';'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
}