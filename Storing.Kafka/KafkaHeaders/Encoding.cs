namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static byte[]? EncodeKafkaHeaderValue(string? value) =>
    value is null ? null : Encoding.UTF8.GetBytes(value);

  public static string? DecodeKafkaHeaderValue(byte[]? value) =>
    value is null ? null : Encoding.UTF8.GetString(value);
}