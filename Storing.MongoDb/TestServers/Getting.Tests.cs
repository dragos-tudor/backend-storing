
using Docker.DotNet.Models;

namespace Storing.MongoDb;

partial class MongoDbTests
{
  static string GetServerIpAddress (NetworkSettings networkSettings) => networkSettings.IPAddress;
}