namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static ConsumerConfig CreateKafkaConsumerConfig(
    IEnumerable<string> endpoints,
    string groupId,
    string? user = default,
    string? password = default,
    SecurityProtocol securityProtocol = SecurityProtocol.SaslPlaintext,
    SaslMechanism saslMechanism = SaslMechanism.ScramSha512,
    AutoOffsetReset autoOffsetReset = AutoOffsetReset.Earliest,
    bool enableAutoCommit = false,
    string? clientId = default,
    TimeSpan? connectTimeout = default,
    Action<ConsumerConfig>? configBuilder = default)
  {
    var config = new ConsumerConfig
    {
      BootstrapServers = JoinKafkaEndpoints(endpoints),
      GroupId = groupId,
      SecurityProtocol = securityProtocol,
      SaslMechanism = saslMechanism,
      SaslUsername = user,
      SaslPassword = password,
      AutoOffsetReset = autoOffsetReset,
      EnableAutoCommit = enableAutoCommit,
      EnableAutoOffsetStore = false,
      ClientId = clientId,
      SocketTimeoutMs = (int)(connectTimeout ?? TimeSpan.FromSeconds(15)).TotalMilliseconds,
    };

    configBuilder?.Invoke(config);
    return config;
  }

  public static ConsumerConfig CreateKafkaConsumerConfig(
    KafkaOptions options,
    Action<ConsumerConfig>? configBuilder = default) =>
    CreateKafkaConsumerConfig(
      options.EndPoints,
      options.GroupId,
      options.User,
      options.Password,
      options.SecurityProtocol,
      options.SaslMechanism,
      options.AutoOffsetReset,
      options.EnableAutoCommit,
      options.ClientId,
      options.ConnectTimeout,
      configBuilder);
}