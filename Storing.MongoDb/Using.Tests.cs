
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Shouldly;

namespace Storing.MongoDb;

[TestClass]
public partial class MongoDbTests
{
  static readonly IMongoDatabase Database = GetMongoDatabase(CreateMongoClient("mongodb://127.0.0.1:27017"), "storing");

  [AssemblyInitialize]
  public static void InitializeMongoServer(TestContext _)
  {
    CleanMongoDatabase(Database, "documents", "queries");
  }
}
