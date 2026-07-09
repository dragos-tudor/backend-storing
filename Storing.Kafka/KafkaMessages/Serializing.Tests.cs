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

  [TestMethod]
  public void kafka_headers__set_header_string__overwrites_existing_value()
  {
    var headers = CreateKafkaHeaders([
      new KeyValuePair<string, string?>("x-trace-id", "one"),
    ]);

    var updated = SetKafkaHeaderString(headers, "x-trace-id", "two");
    var actual = GetKafkaHeaderString(updated, "x-trace-id");

    actual.ShouldBe("two");
  }
}