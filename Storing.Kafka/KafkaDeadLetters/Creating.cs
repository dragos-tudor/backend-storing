
namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static Message<TKey, TValue> CreateDeadLetter<TKey, TValue>(
    string reason,
    Message<TKey, TValue> message,
    TopicPartitionOffset topicPartitionOffset)
  =>
    new()
    {
      Key = message.Key,
      Value = message.Value,
      Timestamp = message.Timestamp,
      Headers = SetDeadLetterHeaders(reason, message, topicPartitionOffset)
    };
}