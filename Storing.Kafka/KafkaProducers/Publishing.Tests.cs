#pragma warning disable CA1031

namespace Storing.Kafka;

public sealed partial class KafkaTests
{
  record TestMessage(int Id, string Value);

  [TestMethod]
  public async Task producer__publish_message_async__message_persisted()
  {
    using var producer = CreateKafkaProducer<string, string>(options);
    var result = await PublishMessageAsync(producer, publishTopicName, "key", "value", null!, cancellationToken);

    result.Status.ShouldBe(PersistenceStatus.Persisted);
    result.Message.Key.ShouldBe("key");
    result.Message.Value.ShouldBe("value");
  }

  [TestMethod]
  public async Task producer__publish_message_async_with_serializer__message_persisted()
  {
    using var producer = CreateKafkaProducer<string, byte[]>(options);
    var payload = new TestMessage(1, "test");
    var result = await PublishMessageAsync(producer, publishTopicName, "key", payload, null!, v => SerializeJson(v), cancellationToken);

    result.Status.ShouldBe(PersistenceStatus.Persisted);
    DeserializeJson<TestMessage>(result.Value).ShouldBe(payload);
  }

  [TestMethod]
  public async Task producer__publish_message_sync__message_persisted()
  {
    using var producer = CreateKafkaProducer<string, string>(options);
    var tcs = new TaskCompletionSource<DeliveryResult<string, string>>();

    var message = PublishMessage(producer, publishTopicName, "key", "value", null!, report =>
    {
      try {
        tcs.SetResult(report);
      }
      catch (Exception ex)
      {
        tcs.SetException(ex);
      }
    });

    producer.Flush(cancellationToken);
    var result = await tcs.Task;
    result.Status.ShouldBe(PersistenceStatus.Persisted);
    result.Key.ShouldBe("key");
    result.Value.ShouldBe("value");
  }

  [TestMethod]
  public async Task producer__publish_message_sync_with_serializer__message_persisted()
  {
    using var producer = CreateKafkaProducer<string, byte[]>(options);
    var tcs = new TaskCompletionSource<DeliveryResult<string, byte[]>>();
    var payload = new TestMessage(1, "test");

    var message = PublishMessage(producer, publishTopicName, "key", payload, null!, v => SerializeJson(v), report =>
    {
      try {
        tcs.SetResult(report);
      }
      catch (Exception ex)
      {
        tcs.SetException(ex);
      }
    });

    producer.Flush(cancellationToken);
    var result = await tcs.Task;
    result.Status.ShouldBe(PersistenceStatus.Persisted);
    result.Key.ShouldBe("key");
    DeserializeJson<TestMessage>(result.Value).ShouldBe(payload);
  }
}