namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static ConsumeResult<TKey, TValue> ConsumeOnce<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    CancellationToken cancellationToken = default) =>
    consumer.Consume(cancellationToken);

  public static IReadOnlyCollection<ConsumeResult<TKey, TValue>> ConsumeBatch<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    int maxCount,
    CancellationToken cancellationToken = default)
  {
    var results = new List<ConsumeResult<TKey, TValue>>(maxCount);

    while (results.Count < maxCount && !cancellationToken.IsCancellationRequested)
    {
      var result = ConsumeOnce(consumer, cancellationToken);

      if (result is null) break;
      if (result.IsPartitionEOF is false) results.Add(result);
    }

    return results;
  }
}