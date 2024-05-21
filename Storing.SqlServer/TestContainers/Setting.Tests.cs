
using Docker.DotNet.Models;

namespace Storing.SqlServer;

partial class SqlServerTests
{
  static Action<CreateContainerParameters> SetCreateContainerParameters(string adminPassword) =>  (@params) =>
  {
    @params.Env = ["ACCEPT_EULA=Y", $"SA_PASSWORD={adminPassword}"];
  };
}