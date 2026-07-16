namespace Storing.Kafka;

partial class KafkaFuncs
{
    public static string CreateKafkaBootstrapServers(IEnumerable<string> endpoints) =>
      string.Join(',', endpoints.Where(endpoint => !string.IsNullOrWhiteSpace(endpoint)));

    public static ProducerConfig CreateKafkaProducerConfig(
      IEnumerable<string> endpoints,
      string? user = default,
      string? password = default,
      SecurityProtocol securityProtocol = SecurityProtocol.SaslPlaintext,
      SaslMechanism saslMechanism = SaslMechanism.ScramSha512,
      string? clientId = default,
      TimeSpan? connectTimeout = default,
      Action<ProducerConfig>? configBuilder = default)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = CreateKafkaBootstrapServers(endpoints),
            SecurityProtocol = securityProtocol,
            SaslMechanism = saslMechanism,
            SaslUsername = user,
            SaslPassword = password,
            ClientId = clientId,
            SocketTimeoutMs = (int)(connectTimeout ?? TimeSpan.FromSeconds(15)).TotalMilliseconds,
            MessageTimeoutMs = (int)(connectTimeout ?? TimeSpan.FromSeconds(15)).TotalMilliseconds,
        };

        configBuilder?.Invoke(config);
        return config;
    }

    public static ProducerConfig CreateKafkaProducerConfig(KafkaOptions options, Action<ProducerConfig>? configBuilder = default) =>
      CreateKafkaProducerConfig(
        options.EndPoints,
        options.User,
        options.Password,
        options.SecurityProtocol,
        options.SaslMechanism,
        options.ClientId,
        options.ConnectTimeout,
        configBuilder);

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
            BootstrapServers = CreateKafkaBootstrapServers(endpoints),
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

    public static ConsumerConfig CreateKafkaConsumerConfig(KafkaOptions options, Action<ConsumerConfig>? configBuilder = default) =>
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

    public static AdminClientConfig CreateKafkaAdminConfig(
      IEnumerable<string> endpoints,
      string? user = default,
      string? password = default,
      SecurityProtocol securityProtocol = SecurityProtocol.SaslPlaintext,
      SaslMechanism saslMechanism = SaslMechanism.ScramSha512,
      string? clientId = default,
      TimeSpan? connectTimeout = default,
      Action<AdminClientConfig>? configBuilder = default)
    {
        var config = new AdminClientConfig
        {
            BootstrapServers = CreateKafkaBootstrapServers(endpoints),
            SecurityProtocol = securityProtocol,
            SaslMechanism = saslMechanism,
            SaslUsername = user,
            SaslPassword = password,
            ClientId = clientId,
            SocketTimeoutMs = (int)(connectTimeout ?? TimeSpan.FromSeconds(15)).TotalMilliseconds,
        };

        configBuilder?.Invoke(config);
        return config;
    }

    public static AdminClientConfig CreateKafkaAdminConfig(KafkaOptions options, Action<AdminClientConfig>? configBuilder = default) =>
      CreateKafkaAdminConfig(
        options.EndPoints,
        options.User,
        options.Password,
        options.SecurityProtocol,
        options.SaslMechanism,
        options.ClientId,
        options.ConnectTimeout,
        configBuilder);

    public static IProducer<TKey, TValue> CreateKafkaProducer<TKey, TValue>(
      ProducerConfig config,
      Action<ProducerBuilder<TKey, TValue>>? configBuilder = default)
    {
        var producerBuilder = new ProducerBuilder<TKey, TValue>(config);
        configBuilder?.Invoke(producerBuilder);
        return producerBuilder.Build();
    }

    public static IProducer<TKey, TValue> CreateKafkaProducer<TKey, TValue>(
      KafkaOptions options,
      Action<ProducerBuilder<TKey, TValue>>? configBuilder = default,
      Action<ProducerConfig>? configOptions = default)
      => CreateKafkaProducer<TKey, TValue>(CreateKafkaProducerConfig(options, configOptions), configBuilder);

    public static IConsumer<TKey, TValue> CreateKafkaConsumer<TKey, TValue>(
      ConsumerConfig config,
      Action<ConsumerBuilder<TKey, TValue>>? configBuilder = default)
    {
        var consumerBuilder = new ConsumerBuilder<TKey, TValue>(config);
        configBuilder?.Invoke(consumerBuilder);
        return consumerBuilder.Build();
    }

    public static IConsumer<TKey, TValue> CreateKafkaConsumer<TKey, TValue>(
      KafkaOptions options,
      Action<ConsumerBuilder<TKey, TValue>>? configBuilder = default,
      Action<ConsumerConfig>? configOptions = default)
      => CreateKafkaConsumer<TKey, TValue>(CreateKafkaConsumerConfig(options, configOptions), configBuilder);

    public static IAdminClient CreateKafkaAdminClient(
      AdminClientConfig config,
      Action<AdminClientBuilder>? configBuilder = default)
    {
        var adminBuilder = new AdminClientBuilder(config);
        configBuilder?.Invoke(adminBuilder);
        return adminBuilder.Build();
    }

    public static IAdminClient CreateKafkaAdminClient(
      KafkaOptions options,
      Action<AdminClientBuilder>? configBuilder = default,
      Action<AdminClientConfig>? configOptions = default)
      => CreateKafkaAdminClient(CreateKafkaAdminConfig(options, configOptions), configBuilder);
}