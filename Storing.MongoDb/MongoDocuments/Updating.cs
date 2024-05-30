
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static async Task<UpdateResult> UpdateDocument<T> (
    IMongoCollection<T> collection,
    T document,
    UpdateDefinition<T> updateDefinition,
    UpdateOptions? options = null,
    CancellationToken cancellationToken = default)
  {
      var filter = GetIdFilterDefinition<T>(document);
      return await collection.UpdateOneAsync(filter, updateDefinition, options, cancellationToken);
  }

  public static async Task<UpdateResult> UpdateDocument<T> (
    IClientSessionHandle session,
    IMongoCollection<T> collection,
    T document,
    UpdateDefinition<T> updateDefinition,
    UpdateOptions? options = null,
    CancellationToken cancellationToken = default)
  {
      var filter = GetIdFilterDefinition<T>(document);
      return await collection.UpdateOneAsync(session, filter, updateDefinition, options, cancellationToken);
  }
}