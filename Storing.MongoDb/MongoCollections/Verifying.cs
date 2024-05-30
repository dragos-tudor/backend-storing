
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static bool ExistsMongoCollections (IMongoDatabase database) => database.ListCollectionNames().Any();
}