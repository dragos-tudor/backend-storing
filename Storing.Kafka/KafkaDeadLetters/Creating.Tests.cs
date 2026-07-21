namespace Storing.Kafka;

partial class KafkaTests
{
  [TestMethod]
  public void deadletter__create_deadletter__deadletter_includes_metadata_headers()
  {
    var message = CreateKafkaJsonMessage("key", "payload", new ());
    var topicPartitionOffset = new TopicPartitionOffset("orders", 2, 25);
    var deadLetter = CreateDeadLetter("handler_failed", message, topicPartitionOffset);

    GetKafkaHeaderString(deadLetter.Headers, DeadLetterReasonHeaderName).ShouldBe("handler_failed");
    GetKafkaHeaderString(deadLetter.Headers, OriginalTopicHeaderName).ShouldBe("orders");
    GetKafkaHeaderString(deadLetter.Headers, OriginalPartitionHeaderName).ShouldBe("2");
    GetKafkaHeaderString(deadLetter.Headers, OriginalOffsetHeaderName).ShouldBe("25");
  }
}