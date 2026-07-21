
namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static Message<TKey, byte[]> CreateKafkaMessage<TKey>(
    TKey key,
    byte[] value,
    Headers headers,
    DateTime date = default)
  =>
    new()
    {
      Key = key,
      Value = value,
      Headers = headers,
      Timestamp = GetKafkaMessageTimestamp(date)
    };

  public static Message<TKey, byte[]> CreateKafkaMessage<TKey, TPayload>(
    TKey key,
    TPayload value,
    Headers headers,
    DateTime date = default,
    int schemaVersion = 1,
    string? traceId = default,
    Func<TPayload, byte[]>? serializer = default)
  =>
    CreateKafkaMessage(
      key,
      serializer?.Invoke(value) ?? SerializeJson(value),
      SetKafkaMessageHeaders<TPayload>(headers, schemaVersion, traceId),
      date
    );
}