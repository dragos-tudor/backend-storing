namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static void CommitConsumedMessage<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    ConsumeResult<TKey, TValue> consumeResult)
    => consumer.Commit(consumeResult);
}