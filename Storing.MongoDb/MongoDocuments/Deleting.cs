
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static async Task<DeleteResult> DeleteDocument<T> (
    IMongoCollection<T> collection,
    T document,
    DeleteOptions? options = null,
    CancellationToken cancellationToken = default)
  {
    var filter = GetIdFilterDefinition(document);
    return await collection.DeleteOneAsync(filter, options, cancellationToken);
  }

  public static async Task<DeleteResult> DeleteDocument<T> (
    IClientSessionHandle session,
    IMongoCollection<T> collection,
    T document,
    DeleteOptions? options = null,
    CancellationToken cancellationToken = default)
  {
    var filter = GetIdFilterDefinition(document);
    return await collection.DeleteOneAsync(session, filter, options, cancellationToken);
  }
}