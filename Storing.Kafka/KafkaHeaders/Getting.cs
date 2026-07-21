namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static byte[]? GetKafkaHeaderValue(Headers headers, string headerName) =>
    headers.LastOrDefault(value => value.Key == headerName)?.GetValueBytes();

  public static string? GetKafkaHeaderString(Headers headers, string headerName) =>
    DecodeKafkaValue(GetKafkaHeaderValue(headers, headerName));
}