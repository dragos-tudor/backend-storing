#pragma warning disable CA2025

namespace Storing.Kafka;

public sealed partial class KafkaTests
{
  [TestMethod]
  public async Task consumer__run_consumer_loop__commits_message_after_success()
  {
    using var client = CreateKafkaAdminClient(options);
    var topicName = CreateTopicName("consume-loop");
    await CreateTopicAsync(client, topicName, options, cancellationToken);
    await WaitForTopicStateAsync(client, topicName, token: cancellationToken);

    var consumerOptions = options with { GroupId = $"{options.GroupId}-loop-{Guid.NewGuid():N}" };

    using var producer = CreateKafkaProducer<string, string>(options);
    using var consumer = CreateKafkaConsumer<string, string>(consumerOptions);
    SubscribeToTopic(consumer, topicName);
    await WaitForConsumerAssignmentAsync(consumer, token: cancellationToken);

    var handledValues = new List<string>();
    using var loopCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));

    var consumerTask = Task.Run(
      () => RunConsumerLoopAsync(
        consumer,
        async (message, token) =>
        {
          handledValues.Add(message.Message.Value);
          await Task.Yield();
          await loopCancellationTokenSource.CancelAsync();
          return true;
        },
        TimeSpan.FromMilliseconds(250),
        commitOnSuccess: true,
        cancellationToken: loopCancellationTokenSource.Token),
      cancellationToken);

    await PublishMessageAsync(producer, topicName, "order-2", "accepted", cancellationToken: cancellationToken);
    await consumerTask;

    handledValues.ShouldContain("accepted");
    await DeleteTopicAsync(client, topicName, cancellationToken);
  }
}