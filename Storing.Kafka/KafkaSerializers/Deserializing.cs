namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static T? DeserializeJson<T>(byte[]? value, JsonSerializerOptions? serializerOptions = default)
    => value is null || value.Length == 0
      ? default
      : JsonSerializer.Deserialize<T>(value, serializerOptions);

  public static T? DeserializeJson<T>(string? value, JsonSerializerOptions? serializerOptions = default)
    => string.IsNullOrWhiteSpace(value)
      ? default
      : JsonSerializer.Deserialize<T>(value, serializerOptions);

}