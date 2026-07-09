namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static bool TopicExists(
    IAdminClient client,
    string topicName,
    TimeSpan? timeout = default,
    CancellationToken cancellationToken = default)
  {
    cancellationToken.ThrowIfCancellationRequested();
    var metadata = client.GetMetadata(topicName, timeout ?? TimeSpan.FromSeconds(10));

    return metadata.Topics.Any(topic => topic.Topic == topicName && topic.Error.Code == ErrorCode.NoError);
  }

  public static Task<bool> TopicExistsAsync(
    IAdminClient client,
    string topicName,
    TimeSpan? timeout = default,
    CancellationToken cancellationToken = default)
    => Task.FromResult(TopicExists(client, topicName, timeout, cancellationToken));
}