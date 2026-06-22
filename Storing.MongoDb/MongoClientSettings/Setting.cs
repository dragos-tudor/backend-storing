
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  static MongoClientSettings SetMongoClientSettings (
    MongoClientSettings settings,
    Action<MongoClientSettings>? configSettings = default)
  {
    configSettings?.Invoke(settings);
    return settings;
  }
}