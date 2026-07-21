
namespace Storing.Kafka;

partial class KafkaFuncs
{
  static string GetKafkaSchemaType<TPayload>() =>
    typeof(TPayload).Name;

  public static string? GetKafkaSchemaTypeHeader(Headers headers) =>
    GetKafkaHeaderString(headers, SchemaTypeHeaderName);

  public static int GetKafkaSchemaVersionHeader(Headers headers) =>
    int.Parse(GetKafkaHeaderString(headers, SchemaVersionHeaderName) ?? "1", CultureInfo.InvariantCulture);

  public static string? GetKafkaTraceIdHeader(Headers headers) =>
    GetKafkaHeaderString(headers, TraceIdHeaderName);
}