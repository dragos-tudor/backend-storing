namespace Storing.Kafka;

public sealed partial class KafkaTests
{
    [TestMethod]
    public async Task topic__verify_topic_exists__returns_true_for_existing_topic()
    {
        using var client = CreateKafkaAdminClient(options);
        var topicName = CreateTopicName("verify-topic");

        await CreateTopicAsync(client, topicName, options, cancellationToken);
        await WaitForTopicStateAsync(client, topicName, token: cancellationToken);

        var exists = await TopicExistsAsync(client, topicName, TimeSpan.FromSeconds(5), cancellationToken);
        exists.ShouldBeTrue();

        await DeleteTopicAsync(client, topicName, cancellationToken);
    }
}