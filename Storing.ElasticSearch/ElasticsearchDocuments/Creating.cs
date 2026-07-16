
namespace Storing.ElasticSearch;

partial class ElasticSearchFuncs
{
    public static string CreateDocumentId() => Guid.NewGuid().ToString();
}