
namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static Message<TKey, byte[]> CreateKafkaMessage<TKey, TPayload>(
    TKey key,
    TPayload value,
    Headers headers,
    int schemaVersion = 1,
    string? traceId = default,
    Func<TPayload, byte[]>? serializer = default)
  =>
    new()
    {
      Key = key,
      Value = serializer?.Invoke(value) ?? [],
      Headers = SetKafkaMessageHeaders<TPayload>(headers, schemaVersion, traceId)
    };

  public static Message<TKey, byte[]> CreateKafkaJsonMessage<TKey, TPayload>(
    TKey key,
    TPayload value,
    Headers headers,
    int schemaVersion = 1,
    string? traceId = default,
    JsonSerializerOptions? serializerOptions = default)
  =>
    CreateKafkaMessage(
      key,
      value,
      headers,
      schemaVersion,
      traceId,
      v => SerializeJson(v, serializerOptions)
    );
}