using static Storing.MongoDb.MongoIdentities;

namespace Storing.MongoDb;

public static partial class MongoCollections {

  public static IMongoCollection<T> GetCollection<T> (
    IMongoDatabase db,
    string collName) where T: Id =>
      db.GetCollection<T>(collName);

}