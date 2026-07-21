namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static Task<DeliveryResult<TKey, byte[]>> PublishMessageAsync<TKey>(
    IProducer<TKey, byte[]> producer,
    string topicName,
    Message<TKey, byte[]> message,
    CancellationToken cancellationToken = default)
  => producer.ProduceAsync(topicName, message, cancellationToken);

  public static Message<TKey, byte[]> PublishMessage<TKey>(
    IProducer<TKey, byte[]> producer,
    string topicName,
    Message<TKey, byte[]> message,
    Action<DeliveryReport<TKey, byte[]>>? deliveryHandler = default)
  {
    producer.Produce(topicName, message, deliveryHandler);
    return message;
  }

  public static IEnumerable<Message<TKey, byte[]>> PublishMessages<TKey>(
    IProducer<TKey, byte[]> producer,
    string topicName,
    IEnumerable<Message<TKey, byte[]>> messages,
    Action<DeliveryReport<TKey, byte[]>>? deliveryHandler = default)
  {
    foreach (var message in messages)
      PublishMessage(producer, topicName, message, deliveryHandler);
    return messages;
  }
}