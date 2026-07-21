
namespace Storing.Kafka;

partial class KafkaFuncs
{
  internal const string DeadLetterReasonHeaderName = "x-deadletter-reason";
  internal const string OriginalOffsetHeaderName = "x-original-offset";
  internal const string OriginalPartitionHeaderName = "x-original-partition";
  internal const string OriginalTopicHeaderName = "x-original-topic";

  public static Headers SetDeadLetterHeaders<TKey, TValue>(
    string reason,
    Message<TKey, TValue> message,
    TopicPartitionOffset topicPartitionOffset)
  {
    Headers headers = CloneKafkaHeaders(message.Headers);
    SetKafkaHeaderString(headers, DeadLetterReasonHeaderName, reason);
    SetKafkaHeaderString(headers, OriginalOffsetHeaderName, topicPartitionOffset.Offset.Value.ToString(CultureInfo.InvariantCulture));
    SetKafkaHeaderString(headers, OriginalPartitionHeaderName, topicPartitionOffset.Partition.Value.ToString(CultureInfo.InvariantCulture));
    SetKafkaHeaderString(headers, OriginalTopicHeaderName, topicPartitionOffset.Topic);
    return headers;
  }
}