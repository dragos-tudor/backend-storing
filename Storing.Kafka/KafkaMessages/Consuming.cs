namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static void SubscribeToTopic<TKey, TValue>(IConsumer<TKey, TValue> consumer, string topicName)
    => consumer.Subscribe(topicName);

  public static void SubscribeToTopics<TKey, TValue>(IConsumer<TKey, TValue> consumer, IEnumerable<string> topicNames)
    => consumer.Subscribe(topicNames);

  public static ConsumeResult<TKey, TValue>? ConsumeOnce<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    TimeSpan? timeout = default,
    CancellationToken cancellationToken = default)
  {
    cancellationToken.ThrowIfCancellationRequested();
    return consumer.Consume(timeout ?? TimeSpan.FromSeconds(1));
  }

  public static IReadOnlyCollection<ConsumeResult<TKey, TValue>> ConsumeBatch<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    int maxCount,
    TimeSpan? timeout = default,
    CancellationToken cancellationToken = default)
  {
    cancellationToken.ThrowIfCancellationRequested();
    var results = new List<ConsumeResult<TKey, TValue>>(Math.Max(1, maxCount));

    while (results.Count < maxCount && !cancellationToken.IsCancellationRequested)
    {
      var consumeResult = ConsumeOnce(consumer, timeout, cancellationToken);

      if (consumeResult is null)
        break;

      if (!consumeResult.IsPartitionEOF)
        results.Add(consumeResult);
    }

    return results;
  }

  public static void CommitConsumedMessage<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    ConsumeResult<TKey, TValue> consumeResult)
    => consumer.Commit(consumeResult);

  public static async Task RunConsumerLoopAsync<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    Func<ConsumeResult<TKey, TValue>, CancellationToken, Task<bool>> handleMessage,
    TimeSpan? timeout = default,
    bool commitOnSuccess = true,
    CancellationToken cancellationToken = default)
  {
    try
    {
      while (!cancellationToken.IsCancellationRequested)
      {
        var consumeResult = ConsumeOnce(consumer, timeout, cancellationToken);

        if (consumeResult is null || consumeResult.IsPartitionEOF)
          continue;

        var handled = await handleMessage(consumeResult, cancellationToken);

        if (handled && commitOnSuccess)
          CommitConsumedMessage(consumer, consumeResult);
      }
    }
    catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
    {
    }
  }
}