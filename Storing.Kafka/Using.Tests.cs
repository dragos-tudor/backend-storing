#pragma warning disable CA2000

global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Shouldly;

namespace Storing.Kafka;

[TestClass]
public sealed partial class KafkaTests
{
  static KafkaOptions options = new KafkaOptions
  {
    EndPoints = ["kafka:9092"],
    User = GetKafkaUserName()!,
    Password = GetKafkaPassword()!,
    SecurityProtocol = SecurityProtocol.SaslPlaintext,
    SaslMechanism = SaslMechanism.ScramSha512,
    GroupId = "storing-kafka-tests-group",
    ClientId = "storing-kafka-tests",
    EnableAutoCommit = false,
    AutoOffsetReset = AutoOffsetReset.Earliest,
    ConnectTimeout = TimeSpan.FromSeconds(10),
    ConsumeTimeout = TimeSpan.FromMilliseconds(500),
  };
  static CancellationToken cancellationToken = default!;

  [AssemblyInitialize]
  public static void InitializeKafka(TestContext testContext)
  {
    var timeoutCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(1));
    var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(timeoutCancellationTokenSource.Token, testContext.CancellationToken);
    cancellationToken = cancellationTokenSource.Token;
  }

  internal static string CreateTopicName(string prefix) => $"{prefix}-{Guid.NewGuid():N}";

  static async Task WaitForTopicStateAsync(
    IAdminClient client,
    string topicName,
    TimeSpan? timeout = default,
    CancellationToken token = default)
  {
    while (!token.IsCancellationRequested)
    {
      if (await TopicExistsAsync(client, topicName, TimeSpan.FromSeconds(2), token))
        return;

      await Task.Delay(200, token);
    }
    Assert.Fail($"Topic state did not converge for '{topicName}'.");
  }

  static async Task<ConsumeResult<string, string>?> ConsumeMessageAsync(
    IConsumer<string, string> consumer,
    TimeSpan? timeout = default,
    int maxPolls = 20,
    CancellationToken token = default)
  {
    for (var index = 0; index < maxPolls && !token.IsCancellationRequested; index++)
    {
      var message = ConsumeOnce(consumer, timeout ?? TimeSpan.FromMilliseconds(500), token);

      if (message is { IsPartitionEOF: false })
        return message;
    }

    return default;
  }

  static async Task WaitForConsumerAssignmentAsync(
    IConsumer<string, string> consumer,
    TimeSpan? timeout = default,
    CancellationToken token = default)
  {
    while (!token.IsCancellationRequested)
    {
      _ = ConsumeOnce(consumer, TimeSpan.FromMilliseconds(100), token);

      if (consumer.Assignment.Count > 0)
        return;

      await Task.Delay(100, token);
    }

    Assert.Fail("Consumer assignment was not established before timeout.");
  }
}