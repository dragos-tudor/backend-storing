#pragma warning disable CA2000
#pragma warning disable CA2025

global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static System.Threading.CancellationTokenSource;
global using Shouldly;

namespace Storing.Kafka;

[TestClass]
public partial class KafkaTests
{
  static KafkaOptions options = new KafkaOptions
  {
    EndPoints = ["kafka"],
    User = GetKafkaUserName()!,
    Password = GetKafkaPassword()!,
    SecurityProtocol = SecurityProtocol.SaslPlaintext,
    SaslMechanism = SaslMechanism.ScramSha512,
    GroupId = "storing-kafka-tests-group",
    ClientId = "storing-kafka-tests",
    EnableAutoCommit = false,
    AutoOffsetReset = AutoOffsetReset.Earliest,
    ConnectTimeout = TimeSpan.FromSeconds(10),
    OperationTimeout = TimeSpan.FromSeconds(1),
  };
  static CancellationToken cancellationToken = default!;
  static string publishTopicName = GetKafkaTopicName("storing-kafka-tests-publish");


  [AssemblyInitialize]
  public static void InitializeKafka(TestContext testContext)
  {
    var timeoutCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(1));
    var cancellationTokenSource = CreateLinkedTokenSource(timeoutCancellationTokenSource.Token, testContext.CancellationToken);
    cancellationToken = cancellationTokenSource.Token;

    using var adminClient = CreateKafkaAdminClient(options);
    if (!ExistsTopic(adminClient, publishTopicName, options))
      CreateTopicAsync(adminClient, publishTopicName, options, cancellationToken).GetAwaiter().GetResult();
  }

  [AssemblyCleanup]
  public static void CleanupKafka()
  {
    using var adminClient = CreateKafkaAdminClient(options);
    if (ExistsTopic(adminClient, publishTopicName, options))
      DeleteTopicAsync(adminClient, publishTopicName, options, cancellationToken).GetAwaiter().GetResult();
  }

  static string GetKafkaTopicName(string topicName) => $"{topicName}-{Guid.NewGuid():N}";
}