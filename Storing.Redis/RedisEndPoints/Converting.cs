
namespace Storing.Redis;

partial class RedisFuncs
{
  static IEnumerable<EndPoint> ConvertToRedisEndPoints(IEnumerable<string> endpoints) =>
    endpoints.Select(EndPointCollection.TryParse).Where(endpoint => endpoint != null)!;
}