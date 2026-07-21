
namespace Storing.Kafka;

partial class KafkaFuncs
{
  static Headers CloneKafkaHeaders(Headers? headers) =>
    (headers ?? []).Aggregate(new Headers(),
      (result, header) => SetKafkaHeaderValue(result, header.Key, header.GetValueBytes()));
}