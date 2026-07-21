
namespace Storing.Kafka;

partial class KafkaFuncs
{
  const string SchemaTypeHeaderName = "x-schema-type";
  const string SchemaVersionHeaderName = "x-schema-version";
  const string TraceIdHeaderName = "x-traceId";

  public static Headers SetKafkaSchemaTypeHeader(Headers headers, string schemaType) =>
    SetKafkaHeaderString(headers, SchemaTypeHeaderName, schemaType);

  public static Headers SetKafkaSchemaVersionHeader(Headers headers, int schemaVersion) =>
    SetKafkaHeaderString(headers, SchemaVersionHeaderName, schemaVersion.ToString(CultureInfo.InvariantCulture));

  public static Headers SetKafkaTraceIdHeader(Headers headers, string traceId) =>
    SetKafkaHeaderString(headers, TraceIdHeaderName, traceId);

  public static Headers SetKafkaMessageHeaders(
    Headers headers,
    string schemaType,
    int schemaVersion,
    string? traceId) =>
    SetKafkaTraceIdHeader(
      SetKafkaSchemaVersionHeader(
        SetKafkaSchemaTypeHeader(headers, schemaType),
        schemaVersion),
      traceId ?? "");

  public static Headers SetKafkaMessageHeaders<TSchema>(
    Headers headers,
    int schemaVersion,
    string? traceId) =>
    SetKafkaMessageHeaders(headers, GetKafkaSchemaType<TSchema>(), schemaVersion, traceId);

}