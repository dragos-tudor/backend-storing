#pragma warning disable CA1849

namespace Storing.Kafka;

public sealed partial class KafkaTests
{
  [TestMethod]
  public async Task topic__delete_topic__topic_deleted()
  {
    using var client = CreateKafkaAdminClient(options);
    var topicName = CreateTopicName("delete-topic");

    await CreateTopicAsync(client, topicName, options, cancellationToken);
    await WaitForTopicStateAsync(client, topicName, token: cancellationToken);
    await DeleteTopicAsync(client, topicName, cancellationToken);

    var exists = TopicExists(client, topicName, TimeSpan.FromSeconds(5), cancellationToken);
    exists.ShouldBeFalse();
  }
}