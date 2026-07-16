namespace Storing.Kafka;

partial class KafkaFuncs
{
    public static Task DeleteTopicAsync(
      IAdminClient client,
      string topicName,
      CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return client.DeleteTopicsAsync([topicName]);
    }
}