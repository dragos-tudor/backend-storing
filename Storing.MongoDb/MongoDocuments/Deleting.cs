
namespace Storing.MongoDb;

partial class MongoFuncs
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
}