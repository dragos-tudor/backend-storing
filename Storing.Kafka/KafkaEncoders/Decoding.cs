namespace Storing.Kafka;

partial class KafkaFuncs
{
  static string? DecodeKafkaValue(byte[]? value) =>
    value is null ? null : Encoding.UTF8.GetString(value);
}