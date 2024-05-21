
using Docker.DotNet.Models;

namespace Storing.Redis;

partial class RedisTests
{
  static string GetServerIpAddress (NetworkSettings networkSettings) => networkSettings.IPAddress;
}