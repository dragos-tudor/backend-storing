
namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static Message<TKey, byte[]> CreateKafkaDeadLetter<TKey>(
    string reason,
    Message<TKey, byte[]> message,
    TopicPartitionOffset topicPartitionOffset)
  =>
    CreateKafkaMessage(
      message.Key,
      message.Value,
      SetDeadLetterHeaders(reason, message, topicPartitionOffset),
      message.Timestamp.UtcDateTime
    );
}