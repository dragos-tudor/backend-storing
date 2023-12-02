using Xunit;
using NSubstitute;
using static Docker.Extensions.Messages;

namespace Docker.Extensions;

public sealed partial class MessagesTests
{
  [Fact]
  public void match_throttle_threshold__log_message__message_logged()
  {
    var writter = Substitute.For<Action<string>>();
    var formatter = delegate (JSONMessage message) { return message.Status; };
    LogMessage(new JSONMessage(){ Status = "a" }, 0, 10, formatter, writter);

    writter.Received().Invoke("a");
  }

  [Fact]
  public void unmatch_throttle_threshold__log_message__message_not_logged()
  {
    var writer = Substitute.For<Action<string>>();
    LogMessage(new JSONMessage(), 1, 10, default!, writer);
    LogMessage(new JSONMessage(), 2, 10, default!, writer);

    writer.DidNotReceiveWithAnyArgs();
  }

  [Fact]
  public void toggle_match_throttle_threshold__log_messages__toggle_message_logging()
  {
    var writter = Substitute.For<Action<string>>();
    var formatter = delegate (JSONMessage message) { return message.Status; };
    var logMessage = LogMessages(10, formatter, writter);
    logMessage(new JSONMessage() { Status = "a" });

    writter.Received().Invoke("a");
    writter.ClearReceivedCalls();

    logMessage(new JSONMessage() { Status = "a" });
    writter.DidNotReceiveWithAnyArgs();
  }

}