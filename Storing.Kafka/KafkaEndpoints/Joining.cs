
namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static string JoinKafkaEndpoints(IEnumerable<string> endpoints) =>
    string.Join(EndpointSeparator, endpoints.Where(endpoint => !string.IsNullOrWhiteSpace(endpoint)));
}