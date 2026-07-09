namespace Storing.Kafka;

public sealed partial class KafkaTests
{
  [TestMethod]
  public void deadletter__create_deadletter_message__includes_source_metadata_headers()
  {
    var consumed = new ConsumeResult<string, string>
    {
      Topic = "orders",
      Partition = new Partition(2),
      Offset = new Offset(25),
      Message = new Message<string, string>
      {
        Key = "k-1",
        Value = "payload",
        Headers = new Headers(),
      },
    };

    var deadLetter = CreateDeadLetterMessage(consumed, "handler_failed");

    GetKafkaHeaderString(deadLetter.Headers, OriginalTopicHeaderName).ShouldBe("orders");
    GetKafkaHeaderString(deadLetter.Headers, OriginalPartitionHeaderName).ShouldBe("2");
    GetKafkaHeaderString(deadLetter.Headers, OriginalOffsetHeaderName).ShouldBe("25");
    GetKafkaHeaderString(deadLetter.Headers, DeadLetterReasonHeaderName).ShouldBe("handler_failed");
  }
}