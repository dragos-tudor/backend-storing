
namespace Storing.Kafka;

public sealed partial class KafkaTests
{
  [TestMethod]
  public async Task topic__delete_topic__topic_deleted()
  {
    using var client = CreateKafkaAdminClient(options);
    var topicName = GetKafkaTopicName("delete-topic");

    await CreateTopicAsync(client, topicName, options, cancellationToken);

    var initial = ExistsTopic(client, topicName, options);
    initial.ShouldBeTrue();

    await DeleteTopicAsync(client, topicName, options, cancellationToken);

    var exists = ExistsTopic(client, topicName, options);
    exists.ShouldBeFalse();
  }
}