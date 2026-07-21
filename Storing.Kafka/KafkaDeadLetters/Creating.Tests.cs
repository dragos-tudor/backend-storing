namespace Storing.Kafka;

partial class KafkaTests
{
  [TestMethod]
  public void deadletter__create_deadletter__deadletter_includes_reason_header()
  {
    var message = CreateKafkaMessage("key", "payload", []);
    var topicPartitionOffset = new TopicPartitionOffset("", 0, 0);
    var deadLetter = CreateKafkaDeadLetter("handler_failed", message, topicPartitionOffset);

    GetKafkaHeaderString(deadLetter.Headers, DeadLetterReasonHeaderName).ShouldBe("handler_failed");
  }

  [TestMethod]
  public void deadletter__create_deadletter__deadletter_includes_metadata_headers()
  {
    var message = CreateKafkaMessage("key", "payload", []);
    var topicPartitionOffset = new TopicPartitionOffset("orders", 2, 25);
    var deadLetter = CreateKafkaDeadLetter("handler_failed", message, topicPartitionOffset);

    GetKafkaHeaderString(deadLetter.Headers, OriginalTopicHeaderName).ShouldBe("orders");
    GetKafkaHeaderString(deadLetter.Headers, OriginalPartitionHeaderName).ShouldBe("2");
    GetKafkaHeaderString(deadLetter.Headers, OriginalOffsetHeaderName).ShouldBe("25");
  }

  [TestMethod]
  public void deadletter__create_deadletter__deadletter_has_original_message_headers()
  {
    var message = CreateKafkaMessage("key", "payload", SetKafkaHeaderString([], "original-header", "original-value"));
    var topicPartitionOffset = new TopicPartitionOffset("", 0, 0);
    var deadLetter = CreateKafkaDeadLetter("handler_failed", message, topicPartitionOffset);

    GetKafkaHeaderString(deadLetter.Headers, "original-header").ShouldBe("original-value");
  }
}