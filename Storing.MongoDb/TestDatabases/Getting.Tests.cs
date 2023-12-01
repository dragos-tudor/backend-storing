
namespace Storing.MongoDb;

static partial class TestDatabases
{
  internal const string dbCollection = "documents";

  internal static async Task<IMongoDatabase> GetMongoDatabase () =>
    (await mongoClient.Value).GetDatabase("storing");

}