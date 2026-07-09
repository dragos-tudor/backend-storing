namespace Storing.Kafka;

public sealed partial class KafkaTests
{
  [TestMethod]
  public async Task handling__handle_consumed_message__returns_succeeded_when_handler_is_true()
  {
    var consumeResult = new ConsumeResult<string, string>
    {
      Topic = "orders",
      Message = new Message<string, string>
      {
        Key = "order-1",
        Value = "created",
        Headers = new Headers(),
      },
    };

    var retried = 0;
    var deadLettered = 0;

    var result = await HandleConsumedMessageAsync(
      consumeResult,
      (_, _) => Task.FromResult(true),
      (_, _) =>
      {
        retried++;
        return Task.CompletedTask;
      },
      (_, _, _) =>
      {
        deadLettered++;
        return Task.CompletedTask;
      },
      cancellationToken: cancellationToken);

    result.ShouldBe(KafkaMessageHandlingResult.Succeeded);
    retried.ShouldBe(0);
    deadLettered.ShouldBe(0);
  }

  [TestMethod]
  public async Task handling__handle_consumed_message__routes_to_retry_when_retry_available()
  {
    var consumeResult = new ConsumeResult<string, string>
    {
      Topic = "orders",
      Message = new Message<string, string>
      {
        Key = "order-1",
        Value = "created",
        Headers = CreateKafkaHeaders([
          new KeyValuePair<string, string?>(RetryAttemptHeaderName, "1"),
        ]),
      },
    };

    var retried = 0;
    var deadLettered = 0;

    var result = await HandleConsumedMessageAsync(
      consumeResult,
      (_, _) => Task.FromResult(false),
      (_, _) =>
      {
        retried++;
        return Task.CompletedTask;
      },
      (_, _, _) =>
      {
        deadLettered++;
        return Task.CompletedTask;
      },
      maxRetryAttempts: 3,
      cancellationToken: cancellationToken);

    result.ShouldBe(KafkaMessageHandlingResult.Retried);
    retried.ShouldBe(1);
    deadLettered.ShouldBe(0);
  }

  [TestMethod]
  public async Task handling__handle_consumed_message__routes_to_deadletter_when_retry_exhausted()
  {
    var consumeResult = new ConsumeResult<string, string>
    {
      Topic = "orders",
      Message = new Message<string, string>
      {
        Key = "order-1",
        Value = "created",
        Headers = CreateKafkaHeaders([
          new KeyValuePair<string, string?>(RetryAttemptHeaderName, "3"),
        ]),
      },
    };

    var retried = 0;
    var deadLettered = 0;

    var result = await HandleConsumedMessageAsync(
      consumeResult,
      (_, _) => Task.FromResult(false),
      (_, _) =>
      {
        retried++;
        return Task.CompletedTask;
      },
      (_, _, _) =>
      {
        deadLettered++;
        return Task.CompletedTask;
      },
      maxRetryAttempts: 3,
      cancellationToken: cancellationToken);

    result.ShouldBe(KafkaMessageHandlingResult.DeadLettered);
    retried.ShouldBe(0);
    deadLettered.ShouldBe(1);
  }

  [TestMethod]
  public async Task handling__handle_consumed_message__routes_to_deadletter_when_handler_throws_and_retry_exhausted()
  {
    var consumeResult = new ConsumeResult<string, string>
    {
      Topic = "orders",
      Message = new Message<string, string>
      {
        Key = "order-1",
        Value = "created",
        Headers = CreateKafkaHeaders([
          new KeyValuePair<string, string?>(RetryAttemptHeaderName, "9"),
        ]),
      },
    };

    string? deadLetterReason = default;

    var result = await HandleConsumedMessageAsync(
      consumeResult,
      (_, _) => throw new InvalidOperationException("handler exploded"),
      (_, _) => Task.CompletedTask,
      (_, reason, _) =>
      {
        deadLetterReason = reason;
        return Task.CompletedTask;
      },
      maxRetryAttempts: 3,
      cancellationToken: cancellationToken);

    result.ShouldBe(KafkaMessageHandlingResult.DeadLettered);
    deadLetterReason.ShouldBe("handler exploded");
  }

  [TestMethod]
  public void handling__resolve_handling_result__returns_retry_when_retry_is_available()
  {
    var headers = CreateKafkaHeaders([
      new KeyValuePair<string, string?>(RetryAttemptHeaderName, "1"),
    ]);

    var actual = ResolveHandlingResult(headers, maxRetryAttempts: 3);

    actual.ShouldBe(KafkaMessageHandlingResult.Retried);
  }

  [TestMethod]
  public void handling__resolve_handling_result__returns_deadletter_when_retry_is_exhausted()
  {
    var headers = CreateKafkaHeaders([
      new KeyValuePair<string, string?>(RetryAttemptHeaderName, "3"),
    ]);

    var actual = ResolveHandlingResult(headers, maxRetryAttempts: 3);

    actual.ShouldBe(KafkaMessageHandlingResult.DeadLettered);
  }

  [TestMethod]
  public async Task handling__process_consume_batch__returns_only_successful_messages()
  {
    var batch = (ConsumeResult<string, string>[])
    [
      new()
      {
        Topic = "orders",
        Message = new Message<string, string> { Key = "1", Value = "ok" },
      },
      new()
      {
        Topic = "orders",
        Message = new Message<string, string> { Key = "2", Value = "fail" },
      },
    ];

    var handled = await ProcessConsumeBatchAsync(
      consumer: null!,
      consumedMessages: batch,
      handleMessage: (message, _) => Task.FromResult(message.Message.Value == "ok"),
      commitEachOnSuccess: false,
      stopOnFailure: true,
      cancellationToken: cancellationToken);

    handled.Count.ShouldBe(1);
    handled.First().Message.Key.ShouldBe("1");
  }
}