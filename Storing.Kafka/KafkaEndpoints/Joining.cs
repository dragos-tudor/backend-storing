
namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static string JoinKafkaEndpoints(IEnumerable<string> endpoints) =>
    string.Join(',', endpoints.Where(endpoint => !string.IsNullOrWhiteSpace(endpoint)));
}