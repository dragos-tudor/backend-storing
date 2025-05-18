
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Docker.Extensions.DockerFuncs;

namespace Storing.MongoDb;

[TestClass]
public partial class MongoDbTests
{
  static readonly IMongoDatabase Database = GetMongoDatabase(CreateMongoClient("mongodb://storing-mongo:27017"), "storing");

  [AssemblyInitialize]
  public static void InitializeMongoServer (TestContext _)
  {
    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(3));
    var cancellationToken = cancellationTokenSource.Token;

    RunSynchronously(() =>
      InitializeMongoServer(
        "mongo:latest", "storing-mongo",
        "storing", ["documents", "queries"],
        "storing-network", 27017,
        cancellationToken));
  }
}
