namespace Storing.Kafka;

public sealed partial class KafkaTests
{
  [TestMethod]
  public async Task message__publish_message__message_consumed()
  {
    using var client = CreateKafkaAdminClient(options);
    var topicName = CreateTopicName("publish-message");
    await CreateTopicAsync(client, topicName, options, cancellationToken);
    await WaitForTopicStateAsync(client, topicName, token: cancellationToken);

    var consumerOptions = options with { GroupId = $"{options.GroupId}-publish-{Guid.NewGuid():N}" };

    using var producer = CreateKafkaProducer<string, string>(options);
    using var consumer = CreateKafkaConsumer<string, string>(consumerOptions);

    SubscribeToTopic(consumer, topicName);
    await WaitForConsumerAssignmentAsync(consumer, token: cancellationToken);

    await PublishMessageAsync(producer, topicName, "order-1", "created", cancellationToken: cancellationToken);
    var consumed = await ConsumeMessageAsync(consumer, TimeSpan.FromMilliseconds(500), 30, cancellationToken);

    consumed.ShouldNotBeNull();
    consumed!.Message.Key.ShouldBe("order-1");
    consumed.Message.Value.ShouldBe("created");

    CommitConsumedMessage(consumer, consumed);
    await DeleteTopicAsync(client, topicName, cancellationToken);
  }
}