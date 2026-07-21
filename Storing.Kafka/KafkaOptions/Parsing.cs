
namespace Storing.Kafka;

partial class KafkaFuncs
{
  static TEnum ParseKafkaEnum<TEnum>(string? value, TEnum fallback)
    where TEnum : struct, Enum
    => Enum.TryParse<TEnum>(value, true, out var parsed) ? parsed : fallback;

  static int ParseKafkaInt(string? value, int fallback)
    => int.TryParse(value, out var parsed) ? parsed : fallback;

  static double ParseKafkaDouble(string? value, double fallback)
    => double.TryParse(value, out var parsed) ? parsed : fallback;

  static bool ParseKafkaBool(string? value, bool fallback)
    => bool.TryParse(value, out var parsed) ? parsed : fallback;
}