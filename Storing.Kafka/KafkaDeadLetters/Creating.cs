
namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static Message<TKey, TValue> CreateDeadLetter<TKey, TValue>(
    ConsumeResult<TKey, TValue> consumeResult,
    string reason)
  =>
    new()
    {
      Key = consumeResult.Message.Key,
      Value = consumeResult.Message.Value,
      Timestamp = consumeResult.Message.Timestamp,
      Headers = SetDeadLetterHeaders([], consumeResult, reason),
    };
}