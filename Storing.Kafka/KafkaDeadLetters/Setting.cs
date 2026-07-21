
namespace Storing.Kafka;

partial class KafkaFuncs
{
  public const string DeadLetterReasonHeaderName = "x-deadletter-reason";
  public const string OriginalOffsetHeaderName = "x-original-offset";
  public const string OriginalPartitionHeaderName = "x-original-partition";
  public const string OriginalTopicHeaderName = "x-original-topic";

  public static Headers SetDeadLetterHeaders<TKey, TValue>(
    Headers headers,
    ConsumeResult<TKey, TValue> consumeResult,
    string reason)
  {
    SetKafkaHeaderString(headers, DeadLetterReasonHeaderName, reason);
    SetKafkaHeaderString(headers, OriginalOffsetHeaderName, consumeResult.Offset.Value.ToString(CultureInfo.InvariantCulture));
    SetKafkaHeaderString(headers, OriginalPartitionHeaderName, consumeResult.Partition.Value.ToString(CultureInfo.InvariantCulture));
    SetKafkaHeaderString(headers, OriginalTopicHeaderName, consumeResult.Topic);
    return SetKafkaAdditionalHeaders(consumeResult.Message.Headers ?? [], headers);
  }
}