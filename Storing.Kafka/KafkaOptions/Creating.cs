namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static KafkaOptions CreateKafkaOptions(
    IEnumerable<string> endPoints,
    string? user = default,
    string? password = default,
    string? defaultTopic = default,
    string? groupId = default,
    string? clientId = default,
    SecurityProtocol securityProtocol = SecurityProtocol.SaslPlaintext,
    SaslMechanism saslMechanism = SaslMechanism.ScramSha512,
    AutoOffsetReset autoOffsetReset = AutoOffsetReset.Earliest,
    bool enableAutoCommit = false,
    TimeSpan? connectTimeout = default,
    TimeSpan? operationTimeout = default,
    int maxRetryAttempts = 5,
    TimeSpan? retryBaseDelay = default,
    double retryBackoffFactor = 2d,
    TimeSpan? maxRetryDelay = default,
    string deadLetterTopicSuffix = "-dlq")
    => new()
    {
      EndPoints = endPoints,
      User = user ?? string.Empty,
      Password = password ?? string.Empty,
      DefaultTopic = defaultTopic ?? string.Empty,
      GroupId = groupId ?? "storing-kafka-group",
      ClientId = clientId ?? "storing-kafka-client",
      SecurityProtocol = securityProtocol,
      SaslMechanism = saslMechanism,
      AutoOffsetReset = autoOffsetReset,
      EnableAutoCommit = enableAutoCommit,
      ConnectTimeout = connectTimeout ?? TimeSpan.FromSeconds(15),
      OperationTimeout = operationTimeout ?? TimeSpan.FromSeconds(1),
      MaxRetryAttempts = maxRetryAttempts,
      RetryBaseDelay = retryBaseDelay ?? TimeSpan.FromSeconds(1),
      RetryBackoffFactor = retryBackoffFactor,
      MaxRetryDelay = maxRetryDelay ?? TimeSpan.FromMinutes(1),
      DeadLetterTopicSuffix = deadLetterTopicSuffix,
    };

  public static KafkaOptions CreateKafkaOptionsFromEnvironment(
    string bootstrapServersName = "KAFKA_BOOTSTRAP_SERVERS",
    string userName = "KAFKA_USERNAME",
    string passwordName = "KAFKA_PASSWORD",
    string defaultTopicName = "KAFKA_TOPIC",
    string groupIdName = "KAFKA_GROUP_ID",
    string clientIdName = "KAFKA_CLIENT_ID",
    string securityProtocolName = "KAFKA_SECURITY_PROTOCOL",
    string saslMechanismName = "KAFKA_SASL_MECHANISM",
    string autoOffsetResetName = "KAFKA_AUTO_OFFSET_RESET",
    string enableAutoCommitName = "KAFKA_ENABLE_AUTO_COMMIT",
    string connectTimeoutSecondsName = "KAFKA_CONNECT_TIMEOUT_SECONDS",
    string operationTimeoutMillisecondsName = "KAFKA_OPERATION_TIMEOUT_MS",
    string maxRetryAttemptsName = "KAFKA_MAX_RETRY_ATTEMPTS",
    string retryBaseDelayMillisecondsName = "KAFKA_RETRY_BASE_DELAY_MS",
    string retryBackoffFactorName = "KAFKA_RETRY_BACKOFF_FACTOR",
    string maxRetryDelayMillisecondsName = "KAFKA_MAX_RETRY_DELAY_MS",
    string deadLetterTopicSuffixName = "KAFKA_DLQ_SUFFIX")
  {
    var endpoints = SplitKafkaEndpoints(Environment.GetEnvironmentVariable(bootstrapServersName));
    var user = Environment.GetEnvironmentVariable(userName);
    var password = Environment.GetEnvironmentVariable(passwordName);
    var defaultTopic = Environment.GetEnvironmentVariable(defaultTopicName);
    var groupId = Environment.GetEnvironmentVariable(groupIdName);
    var clientId = Environment.GetEnvironmentVariable(clientIdName);

    var securityProtocol = ParseKafkaEnum(Environment.GetEnvironmentVariable(securityProtocolName), SecurityProtocol.SaslPlaintext);
    var saslMechanism = ParseKafkaEnum(Environment.GetEnvironmentVariable(saslMechanismName), SaslMechanism.ScramSha512);
    var autoOffsetReset = ParseKafkaEnum(Environment.GetEnvironmentVariable(autoOffsetResetName), AutoOffsetReset.Earliest);

    var enableAutoCommit = ParseKafkaBool(Environment.GetEnvironmentVariable(enableAutoCommitName), false);
    var connectTimeout = TimeSpan.FromSeconds(ParseKafkaInt(Environment.GetEnvironmentVariable(connectTimeoutSecondsName), 15));
    var operationTimeout = TimeSpan.FromMilliseconds(ParseKafkaInt(Environment.GetEnvironmentVariable(operationTimeoutMillisecondsName), 1000));
    var maxRetryAttempts = ParseKafkaInt(Environment.GetEnvironmentVariable(maxRetryAttemptsName), 5);
    var retryBaseDelay = TimeSpan.FromMilliseconds(ParseKafkaInt(Environment.GetEnvironmentVariable(retryBaseDelayMillisecondsName), 1000));
    var retryBackoffFactor = ParseKafkaDouble(Environment.GetEnvironmentVariable(retryBackoffFactorName), 2d);
    var maxRetryDelay = TimeSpan.FromMilliseconds(ParseKafkaInt(Environment.GetEnvironmentVariable(maxRetryDelayMillisecondsName), 60000));
    var deadLetterTopicSuffix = Environment.GetEnvironmentVariable(deadLetterTopicSuffixName) ?? "-dlq";

    return CreateKafkaOptions(
      endpoints,
      user,
      password,
      defaultTopic,
      groupId,
      clientId,
      securityProtocol,
      saslMechanism,
      autoOffsetReset,
      enableAutoCommit,
      connectTimeout,
      operationTimeout,
      maxRetryAttempts,
      retryBaseDelay,
      retryBackoffFactor,
      maxRetryDelay,
      deadLetterTopicSuffix);
  }
}