namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static void SubscribeToTopic<TKey, TValue>(IConsumer<TKey, TValue> consumer, string topicName)
    => consumer.Subscribe(topicName);

  public static void SubscribeToTopics<TKey, TValue>(IConsumer<TKey, TValue> consumer, IEnumerable<string> topicNames)
    => consumer.Subscribe(topicNames);
}