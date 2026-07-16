namespace Storing.Kafka;

public sealed partial class KafkaTests
{
    [TestMethod]
    public void retry__increment_retry_attempt__increases_retry_header_value()
    {
        var headers = CreateKafkaHeaders([
          new KeyValuePair<string, string?>(RetryAttemptHeaderName, "2"),
    ]);

        var updated = IncrementRetryAttempt(headers);
        var retryAttempt = GetRetryAttempt(updated);

        retryAttempt.ShouldBe(3);
    }

    [TestMethod]
    public void retry__get_retry_delay__caps_retry_delay_to_max()
    {
        var delay = GetRetryDelay(8, TimeSpan.FromSeconds(1), 2d, TimeSpan.FromSeconds(10));

        delay.ShouldBe(TimeSpan.FromSeconds(10));
    }
}