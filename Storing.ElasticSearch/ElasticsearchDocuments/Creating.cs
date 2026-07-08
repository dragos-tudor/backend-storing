
namespace Storing.ElasticSearch;

partial class ElasticSearchFuncs
{
  public static string GetOrCreateDocumentId(string? docId = default) => docId ?? Guid.NewGuid().ToString();
}