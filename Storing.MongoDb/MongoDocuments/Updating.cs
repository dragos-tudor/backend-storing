
namespace Storing.MongoDb;

partial class MongoFuncs
{
  public static async Task<UpdateResult> UpdateDocument<T> (
    IMongoCollection<T> collection,
    T document,
    UpdateDefinition<T> updateDefinition,
    UpdateOptions? options = null,
    CancellationToken cancellationToken = default)
    where T : Id {
      var filter = GetIdFilterDefinition<T>(document);
      return await collection.UpdateOneAsync(filter, updateDefinition, options, cancellationToken);
  }
}