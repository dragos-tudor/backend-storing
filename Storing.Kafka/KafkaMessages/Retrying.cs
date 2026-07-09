namespace Storing.Kafka;

partial class KafkaFuncs
{
  public const string RetryAttemptHeaderName = "x-retry-attempt";

  public static int GetRetryAttempt(Headers? headers, string headerName = RetryAttemptHeaderName)
  {
    var value = GetKafkaHeaderString(headers, headerName);
    return int.TryParse(value, out var retryAttempt) ? retryAttempt : 0;
  }

  public static bool CanRetry(Headers? headers, int maxAttempts, string headerName = RetryAttemptHeaderName)
    => GetRetryAttempt(headers, headerName) < maxAttempts;

  public static Headers IncrementRetryAttempt(Headers? headers, string headerName = RetryAttemptHeaderName)
  {
    var retryAttempt = GetRetryAttempt(headers, headerName) + 1;
    return SetKafkaHeaderString(headers, headerName, retryAttempt.ToString());
  }

  public static TimeSpan GetRetryDelay(
    int retryAttempt,
    TimeSpan? baseDelay = default,
    double backoffFactor = 2d,
    TimeSpan? maxDelay = default)
  {
    var normalizedAttempt = Math.Max(0, retryAttempt);
    var delayMs = (baseDelay ?? TimeSpan.FromSeconds(1)).TotalMilliseconds * Math.Pow(backoffFactor, normalizedAttempt);
    var delay = TimeSpan.FromMilliseconds(delayMs);
    var maximumDelay = maxDelay ?? TimeSpan.FromMinutes(1);
    return delay <= maximumDelay ? delay : maximumDelay;
  }

  public static Task<DeliveryResult<TKey, TValue>> PublishRetryMessageAsync<TKey, TValue>(
    IProducer<TKey, TValue> producer,
    string topicName,
    ConsumeResult<TKey, TValue> consumeResult,
    CancellationToken cancellationToken = default)
  {
    var retryHeaders = IncrementRetryAttempt(consumeResult.Message.Headers);

    return producer.ProduceAsync(
      topicName,
      new Message<TKey, TValue>
      {
        Key = consumeResult.Message.Key,
        Value = consumeResult.Message.Value,
        Timestamp = consumeResult.Message.Timestamp,
        Headers = retryHeaders,
      },
      cancellationToken);
  }
}