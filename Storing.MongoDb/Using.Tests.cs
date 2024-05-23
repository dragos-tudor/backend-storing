
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Docker.Extensions.DockerFuncs;

namespace Storing.MongoDb;

[TestClass]
public partial class MongoDbTests
{
  const string MongoCollectionName = "documents";
  const string MongoDatabaseName = "storing";

  [AssemblyInitialize]
  public static void InitializeMongeServer(TestContext _)
  {
    StartMongoServer();
    CleanMongoDatabase(MongoDbClient, MongoDatabaseName, MongoCollectionName);
  }
}