namespace Storing.Kafka;

partial class KafkaFuncs
{
    public static async Task RunResilientConsumerLoopAsync<TKey, TValue>(
      IConsumer<TKey, TValue> consumer,
      IProducer<TKey, TValue> producer,
      Func<ConsumeResult<TKey, TValue>, CancellationToken, Task<bool>> handleMessage,
      string? retryTopicName = default,
      string? deadLetterTopicName = default,
      int maxRetryAttempts = 5,
      TimeSpan? timeout = default,
      bool commitOnResolved = true,
      CancellationToken cancellationToken = default)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var consumeResult = ConsumeOnce(consumer, timeout, cancellationToken);

                if (consumeResult is null || consumeResult.IsPartitionEOF)
                    continue;

                var resolved = await HandleConsumedMessageAsync(
                  producer,
                  consumeResult,
                  handleMessage,
                  retryTopicName,
                  deadLetterTopicName,
                  maxRetryAttempts,
                  cancellationToken: cancellationToken);

                if (commitOnResolved && resolved is KafkaMessageHandlingResult.Succeeded or KafkaMessageHandlingResult.Retried or KafkaMessageHandlingResult.DeadLettered)
                    CommitConsumedMessage(consumer, consumeResult);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
        }
    }

    public static Task RunResilientConsumerLoopAsync<TKey, TValue>(
      IConsumer<TKey, TValue> consumer,
      IProducer<TKey, TValue> producer,
      Func<ConsumeResult<TKey, TValue>, CancellationToken, Task<bool>> handleMessage,
      KafkaOptions options,
      string? retryTopicName = default,
      string? deadLetterTopicName = default,
      bool commitOnResolved = true,
      CancellationToken cancellationToken = default)
      => RunResilientConsumerLoopAsync(
        consumer,
        producer,
        handleMessage,
        retryTopicName,
        deadLetterTopicName,
        options.MaxRetryAttempts,
        options.ConsumeTimeout,
        commitOnResolved,
        cancellationToken);
}