#pragma warning disable CA1849

namespace Storing.Kafka;

public sealed partial class KafkaTests
{
    [TestMethod]
    public async Task topic__create_topic__topic_exists()
    {
        using var client = CreateKafkaAdminClient(options);
        var topicName = CreateTopicName("create-topic");

        await CreateTopicAsync(client, topicName, options, cancellationToken);
        await WaitForTopicStateAsync(client, topicName, token: cancellationToken);

        var exists = TopicExists(client, topicName, TimeSpan.FromSeconds(5), cancellationToken);
        exists.ShouldBeTrue();

        await DeleteTopicAsync(client, topicName, cancellationToken);
    }
}