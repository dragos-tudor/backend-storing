
namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static Task<DeliveryResult<TKey, TValue>> PublishDeadLetterAsync<TKey, TValue>(
    IProducer<TKey, TValue> producer,
    string deadLetterTopic,
    ConsumeResult<TKey, TValue> consumeResult,
    string reason,
    CancellationToken cancellationToken = default)
  =>
    producer.ProduceAsync(deadLetterTopic, CreateDeadLetter(consumeResult, reason), cancellationToken);
}