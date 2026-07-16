
namespace Storing.Kafka;

partial class KafkaFuncs
{
    public const string DeadLetterReasonHeaderName = "x-deadletter-reason";
    public const string OriginalTopicHeaderName = "x-original-topic";
    public const string OriginalPartitionHeaderName = "x-original-partition";
    public const string OriginalOffsetHeaderName = "x-original-offset";

    public static string BuildDeadLetterTopicName(string topicName, string suffix = "-dlq") => $"{topicName}{suffix}";

    public static Message<TKey, TValue> CreateDeadLetterMessage<TKey, TValue>(
      ConsumeResult<TKey, TValue> consumeResult,
      string? reason = default)
    {
        var headers = CloneKafkaHeaders(consumeResult.Message.Headers);
        headers = SetKafkaHeaderString(headers, OriginalTopicHeaderName, consumeResult.Topic);
        headers = SetKafkaHeaderString(headers, OriginalPartitionHeaderName, consumeResult.Partition.Value.ToString(CultureInfo.InvariantCulture));
        headers = SetKafkaHeaderString(headers, OriginalOffsetHeaderName, consumeResult.Offset.Value.ToString(CultureInfo.InvariantCulture));

        if (!string.IsNullOrWhiteSpace(reason))
            headers = SetKafkaHeaderString(headers, DeadLetterReasonHeaderName, reason);

        return new Message<TKey, TValue>
        {
            Key = consumeResult.Message.Key,
            Value = consumeResult.Message.Value,
            Timestamp = consumeResult.Message.Timestamp,
            Headers = headers,
        };
    }

    public static Task<DeliveryResult<TKey, TValue>> PublishDeadLetterAsync<TKey, TValue>(
      IProducer<TKey, TValue> producer,
      string deadLetterTopic,
      ConsumeResult<TKey, TValue> consumeResult,
      string? reason = default,
      CancellationToken cancellationToken = default)
      => producer.ProduceAsync(deadLetterTopic, CreateDeadLetterMessage(consumeResult, reason), cancellationToken);
}