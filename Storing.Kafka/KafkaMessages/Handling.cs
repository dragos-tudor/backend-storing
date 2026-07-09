namespace Storing.Kafka;

public enum KafkaMessageHandlingResult
{
  Succeeded,
  Retried,
  DeadLettered,
}

partial class KafkaFuncs
{
  public static KafkaMessageHandlingResult ResolveHandlingResult(
    Headers? headers,
    int maxRetryAttempts = 5)
    => CanRetry(headers, maxRetryAttempts)
      ? KafkaMessageHandlingResult.Retried
      : KafkaMessageHandlingResult.DeadLettered;

  public static async Task<KafkaMessageHandlingResult> HandleConsumedMessageAsync<TKey, TValue>(
    ConsumeResult<TKey, TValue> consumeResult,
    Func<ConsumeResult<TKey, TValue>, CancellationToken, Task<bool>> handleMessage,
    Func<ConsumeResult<TKey, TValue>, CancellationToken, Task> publishRetry,
    Func<ConsumeResult<TKey, TValue>, string?, CancellationToken, Task> publishDeadLetter,
    int maxRetryAttempts = 5,
    string? deadLetterReason = default,
    CancellationToken cancellationToken = default)
  {
    cancellationToken.ThrowIfCancellationRequested();

    try
    {
      var handled = await handleMessage(consumeResult, cancellationToken);

      if (handled)
        return KafkaMessageHandlingResult.Succeeded;
    }
    catch (Exception exception)
    {
      deadLetterReason = deadLetterReason ?? exception.Message;
    }

    if (ResolveHandlingResult(consumeResult.Message.Headers, maxRetryAttempts) == KafkaMessageHandlingResult.Retried)
    {
      await publishRetry(consumeResult, cancellationToken);
      return KafkaMessageHandlingResult.Retried;
    }

    await publishDeadLetter(consumeResult, deadLetterReason ?? "handler_returned_false", cancellationToken);
    return KafkaMessageHandlingResult.DeadLettered;
  }

  public static async Task<KafkaMessageHandlingResult> HandleConsumedMessageAsync<TKey, TValue>(
    IProducer<TKey, TValue> producer,
    ConsumeResult<TKey, TValue> consumeResult,
    Func<ConsumeResult<TKey, TValue>, CancellationToken, Task<bool>> handleMessage,
    string? retryTopicName = default,
    string? deadLetterTopicName = default,
    int maxRetryAttempts = 5,
    string? deadLetterReason = default,
    CancellationToken cancellationToken = default)
    => await HandleConsumedMessageAsync(
      consumeResult,
      handleMessage,
      (message, token) => PublishRetryMessageAsync(producer, retryTopicName ?? message.Topic, message, token),
      (message, reason, token) => PublishDeadLetterAsync(
        producer,
        deadLetterTopicName ?? BuildDeadLetterTopicName(message.Topic),
        message,
        reason,
        token),
      maxRetryAttempts,
      deadLetterReason,
      cancellationToken);

  public static async Task<IReadOnlyCollection<ConsumeResult<TKey, TValue>>> ProcessConsumeBatchAsync<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    IReadOnlyCollection<ConsumeResult<TKey, TValue>> consumedMessages,
    Func<ConsumeResult<TKey, TValue>, CancellationToken, Task<bool>> handleMessage,
    bool commitEachOnSuccess = true,
    bool stopOnFailure = false,
    CancellationToken cancellationToken = default)
  {
    cancellationToken.ThrowIfCancellationRequested();

    var handledMessages = new List<ConsumeResult<TKey, TValue>>();

    foreach (var consumeResult in consumedMessages)
    {
      var handled = await handleMessage(consumeResult, cancellationToken);

      if (handled)
      {
        handledMessages.Add(consumeResult);

        if (commitEachOnSuccess)
          CommitConsumedMessage(consumer, consumeResult);

        continue;
      }

      if (stopOnFailure)
        break;
    }

    return handledMessages;
  }

  public static Task<IReadOnlyCollection<ConsumeResult<TKey, TValue>>> ProcessConsumeBatchAsync<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    int maxCount,
    Func<ConsumeResult<TKey, TValue>, CancellationToken, Task<bool>> handleMessage,
    TimeSpan? timeout = default,
    bool commitEachOnSuccess = true,
    bool stopOnFailure = false,
    CancellationToken cancellationToken = default)
    => ProcessConsumeBatchAsync(
      consumer,
      ConsumeBatch(consumer, maxCount, timeout, cancellationToken),
      handleMessage,
      commitEachOnSuccess,
      stopOnFailure,
      cancellationToken);
}