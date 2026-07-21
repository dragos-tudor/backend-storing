namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static Task<DeliveryResult<TKey, TValue>> PublishMessageAsync<TKey, TValue>(
    IProducer<TKey, TValue> producer,
    string topicName,
    TKey key,
    TValue value,
    Headers headers,
    CancellationToken cancellationToken = default)
  {
    var message = new Message<TKey, TValue>
    {
      Key = key,
      Value = value,
      Headers = headers,
    };
    return producer.ProduceAsync(topicName, message, cancellationToken);
  }

  public static Task<DeliveryResult<TKey, TSerialized>> PublishMessageAsync<TKey, TPayload, TSerialized>(
    IProducer<TKey, TSerialized> producer,
    string topicName,
    TKey key,
    TPayload value,
    Headers headers,
    Func<TPayload, TSerialized> serializer,
    CancellationToken cancellationToken = default)
    => PublishMessageAsync(producer, topicName, key, serializer(value), headers, cancellationToken);


  public static Message<TKey, TValue> PublishMessage<TKey, TValue>(
    IProducer<TKey, TValue> producer,
    string topicName,
    TKey key,
    TValue value,
    Headers headers,
    Action<DeliveryReport<TKey, TValue>>? deliveryHandler = default)
  {
    var message = new Message<TKey, TValue>
    {
      Key = key,
      Value = value,
      Headers = headers
    };
    producer.Produce(topicName, message, deliveryHandler);
    return message;
  }

  public static Message<TKey, TSerialized> PublishMessage<TKey, TPayload, TSerialized>(
    IProducer<TKey, TSerialized> producer,
    string topicName,
    TKey key,
    TPayload value,
    Headers headers,
    Func<TPayload, TSerialized> serializer,
    Action<DeliveryReport<TKey, TSerialized>>? deliveryHandler = default)
    => PublishMessage(producer, topicName, key, serializer(value), headers, deliveryHandler);
}