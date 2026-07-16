#pragma warning disable CA1812

namespace Storing.Kafka;

public sealed partial class KafkaTests
{
    sealed record OrderEvent(string Id, string EventType);

    [TestMethod]
    public void typed_consuming__deserialize_string_json__returns_payload()
    {
        var consumeResult = new ConsumeResult<string, string>
        {
            Topic = "orders",
            Message = new Message<string, string>
            {
                Key = "order-1",
                Value = "{\"Id\":\"order-1\",\"EventType\":\"created\"}",
            },
        };

        var parsed = TryDeserializeConsumedValueJson<OrderEvent>(consumeResult, out var payload);

        parsed.ShouldBeTrue();
        payload.ShouldNotBeNull();
        payload!.Id.ShouldBe("order-1");
    }

    [TestMethod]
    public void typed_consuming__deserialize_invalid_json__returns_false()
    {
        var consumeResult = new ConsumeResult<string, string>
        {
            Topic = "orders",
            Message = new Message<string, string>
            {
                Key = "order-1",
                Value = "{invalid-json}",
            },
        };

        var parsed = TryDeserializeConsumedValueJson<OrderEvent>(consumeResult, out var payload);

        parsed.ShouldBeFalse();
        payload.ShouldBeNull();
    }
}