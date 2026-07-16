namespace Storing.Kafka;

partial class KafkaFuncs
{
    public static Task<DeliveryResult<TKey, TValue>> PublishMessageAsync<TKey, TValue>(
      IProducer<TKey, TValue> producer,
      string topicName,
      TKey key,
      TValue value,
      Headers? headers = default,
      CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return producer.ProduceAsync(
          topicName,
          new Message<TKey, TValue>
          {
              Key = key,
              Value = value,
              Headers = headers,
          },
          cancellationToken);
    }

    public static Task<DeliveryResult<TKey, TValue>[]> PublishMessagesAsync<TKey, TValue>(
      IProducer<TKey, TValue> producer,
      string topicName,
      IEnumerable<Message<TKey, TValue>> messages,
      CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var deliveries = messages.Select(message => producer.ProduceAsync(topicName, message, cancellationToken));
        return Task.WhenAll(deliveries);
    }

    public static Task<DeliveryResult<TKey, TSerialized>> PublishMessageAsync<TKey, TPayload, TSerialized>(
      IProducer<TKey, TSerialized> producer,
      string topicName,
      TKey key,
      TPayload value,
      Func<TPayload, TSerialized> serializer,
      Headers? headers = default,
      CancellationToken cancellationToken = default)
      => PublishMessageAsync(producer, topicName, key, serializer(value), headers, cancellationToken);

    public static Task<DeliveryResult<string, byte[]>> PublishJsonMessageAsync<TPayload>(
      IProducer<string, byte[]> producer,
      string topicName,
      string key,
      TPayload value,
      JsonSerializerOptions? serializerOptions = default,
      Headers? headers = default,
      CancellationToken cancellationToken = default)
      => PublishMessageAsync(producer, topicName, key, SerializeJson(value, serializerOptions), headers, cancellationToken);
}