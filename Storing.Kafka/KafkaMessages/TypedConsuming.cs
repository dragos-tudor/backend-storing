namespace Storing.Kafka;

partial class KafkaFuncs
{
    public static bool TryDeserializeConsumedValueJson<TPayload>(
      ConsumeResult<string, byte[]> consumeResult,
      out TPayload? payload,
      JsonSerializerOptions? serializerOptions = default)
    {
        try
        {
            payload = DeserializeJson<TPayload>(consumeResult.Message.Value, serializerOptions);
            return payload is not null;
        }
        catch (JsonException)
        {
            payload = default;
            return false;
        }
    }

    public static bool TryDeserializeConsumedValueJson<TPayload>(
      ConsumeResult<string, string> consumeResult,
      out TPayload? payload,
      JsonSerializerOptions? serializerOptions = default)
    {
        try
        {
            payload = DeserializeJson<TPayload>(consumeResult.Message.Value, serializerOptions);
            return payload is not null;
        }
        catch (JsonException)
        {
            payload = default;
            return false;
        }
    }

    public static async Task RunJsonConsumerLoopAsync<TPayload>(
      IConsumer<string, byte[]> consumer,
      Func<ConsumeResult<string, byte[]>, TPayload, CancellationToken, Task<bool>> handleMessage,
      Func<ConsumeResult<string, byte[]>, CancellationToken, Task>? onDeserializeFailure = default,
      JsonSerializerOptions? serializerOptions = default,
      TimeSpan? timeout = default,
      bool commitOnSuccess = true,
      CancellationToken cancellationToken = default)
      => await RunConsumerLoopAsync(
        consumer,
        async (consumeResult, token) =>
        {
            if (!TryDeserializeConsumedValueJson<TPayload>(consumeResult, out var payload, serializerOptions) || payload is null)
            {
                if (onDeserializeFailure is not null)
                    await onDeserializeFailure(consumeResult, token);

                return false;
            }

            return await handleMessage(consumeResult, payload, token);
        },
        timeout,
        commitOnSuccess,
        cancellationToken);

    public static async Task RunJsonConsumerLoopAsync<TPayload>(
      IConsumer<string, string> consumer,
      Func<ConsumeResult<string, string>, TPayload, CancellationToken, Task<bool>> handleMessage,
      Func<ConsumeResult<string, string>, CancellationToken, Task>? onDeserializeFailure = default,
      JsonSerializerOptions? serializerOptions = default,
      TimeSpan? timeout = default,
      bool commitOnSuccess = true,
      CancellationToken cancellationToken = default)
      => await RunConsumerLoopAsync(
        consumer,
        async (consumeResult, token) =>
        {
            if (!TryDeserializeConsumedValueJson<TPayload>(consumeResult, out var payload, serializerOptions) || payload is null)
            {
                if (onDeserializeFailure is not null)
                    await onDeserializeFailure(consumeResult, token);

                return false;
            }

            return await handleMessage(consumeResult, payload, token);
        },
        timeout,
        commitOnSuccess,
        cancellationToken);
}