namespace Storing.Kafka;

public record KafkaOptions : DatabaseOptions<string>
{
  public string ClientId { get; init; } = "storing-kafka-client";
  public string GroupId { get; init; } = "storing-kafka-group";
  public string DefaultTopic { get; init; } = string.Empty;
  public SecurityProtocol SecurityProtocol { get; init; } = SecurityProtocol.SaslPlaintext;
  public SaslMechanism SaslMechanism { get; init; } = SaslMechanism.ScramSha512;
  public AutoOffsetReset AutoOffsetReset { get; init; } = AutoOffsetReset.Earliest;
  public bool EnableAutoCommit { get; init; }
  public int DefaultNumPartitions { get; init; } = 1;
  public short DefaultReplicationFactor { get; init; } = 1;
  public string DeadLetterTopicSuffix { get; init; } = "-dlq";
  public int MaxRetryAttempts { get; init; } = 5;
  public TimeSpan RetryBaseDelay { get; init; } = TimeSpan.FromSeconds(1);
  public double RetryBackoffFactor { get; init; } = 2d;
  public TimeSpan MaxRetryDelay { get; init; } = TimeSpan.FromMinutes(1);
  public TimeSpan ConsumeTimeout { get; init; } = TimeSpan.FromSeconds(1);
}