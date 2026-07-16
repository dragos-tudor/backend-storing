namespace Storing.Kafka;

partial class KafkaFuncs
{
    public static Task CreateTopicAsync(
      IAdminClient client,
      string topicName,
      int numberOfPartitions = 1,
      short replicationFactor = 1,
      CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var topic = new TopicSpecification
        {
            Name = topicName,
            NumPartitions = numberOfPartitions,
            ReplicationFactor = replicationFactor,
        };

        return client.CreateTopicsAsync([topic]);
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
        cancellationToken);
}