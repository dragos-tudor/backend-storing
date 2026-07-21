
namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static byte[] SerializeJson<T>(T value, JsonSerializerOptions? serializerOptions = default)
    => JsonSerializer.SerializeToUtf8Bytes(value, serializerOptions);

  public static string SerializeJsonString<T>(T value, JsonSerializerOptions? serializerOptions = default)
    => JsonSerializer.Serialize(value, serializerOptions);
}
