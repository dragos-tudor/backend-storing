namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static Task CreateTopicAsync(
    IAdminClient client,
    string topicName,
    int numberOfPartitions = 1,
    short replicationFactor = 1,
    TimeSpan requestTimeout = default,
    TimeSpan operationTimeout = default,
    CancellationToken cancellationToken = default)
  {
    cancellationToken.ThrowIfCancellationRequested();

    var topic = new TopicSpecification
    {
      Name = topicName,
      NumPartitions = numberOfPartitions,
      ReplicationFactor = replicationFactor,
    };
    var topicsOptions = new CreateTopicsOptions
    {
      RequestTimeout = requestTimeout,
      OperationTimeout = operationTimeout
    };

    return client.CreateTopicsAsync([topic], topicsOptions);
  }

  public static Task CreateTopicAsync(
    IAdminClient client,
    string topicName,
    KafkaOptions options,
    CancellationToken cancellationToken = default)
    => CreateTopicAsync(
      client,
      topicName,
      options.DefaultNumPartitions,
      options.DefaultReplicationFactor,
      options.ConnectTimeout,
      options.OperationTimeout,
      cancellationToken);
}