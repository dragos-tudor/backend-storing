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
    Console.WriteLine(Encoding.UTF8.GetString(result.Message.Value));
    DeserializeJson<TestMessage>(result.Value).ShouldBe(payload);
  }

  [TestMethod]
  public async Task producer__publish_message_sync__message_persisted()
  {
    using var producer = CreateKafkaProducer<string, string>(options);
    var tcs = new TaskCompletionSource<string>();

    var message = PublishMessage(producer, publishTopicName, "key", "value", null!, report =>
    {
      try {
        report.Status.ShouldBe(PersistenceStatus.Persisted);
        report.Key.ShouldBe("key");
        report.Value.ShouldBe("value");
        tcs.SetResult(default!);
      }
      catch (Exception ex)
      {
        tcs.SetException(ex);
      }
    });

    producer.Flush(cancellationToken);
    await tcs.Task;
  }

  [TestMethod]
  public async Task producer__publish_message_sync_with_serializer__message_persisted()
  {
    using var producer = CreateKafkaProducer<string, byte[]>(options);
    var tcs = new TaskCompletionSource<string>();
    var payload = new TestMessage(1, "test");

    var message = PublishMessage(producer, publishTopicName, "key", payload, null!, v => SerializeJson(v), report =>
    {
      try {
        report.Status.ShouldBe(PersistenceStatus.Persisted);
        report.Key.ShouldBe("key");
        DeserializeJson<TestMessage>(report.Value).ShouldBe(payload);
        tcs.SetResult(default!);
      }
      catch (Exception ex)
      {
        tcs.SetException(ex);
      }
    });

    producer.Flush(cancellationToken);
    await tcs.Task;
  }
}