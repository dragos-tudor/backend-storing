namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static Headers SetKafkaHeaderValue(Headers headers, string headerName, byte[]? value)
  {
    headers.Add(headerName, value);
    return headers;
  }

  public static Headers SetKafkaHeaderString(Headers headers, string headerName, string? value) =>
    SetKafkaHeaderValue(headers, headerName, EncodeKafkaValue(value));
}