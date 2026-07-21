namespace Storing.Kafka;

public sealed partial class KafkaTests
{
  sealed record EventPayload(string Id, string Type);

  [TestMethod]
  public void json__serialize_deserialize__returns_original_payload()
  {
    var payload = new EventPayload("evt-1", "order.created");

    var binary = SerializeJson(payload);
    var actual = DeserializeJson<EventPayload>(binary);

    actual.ShouldBe(payload);
  }
}