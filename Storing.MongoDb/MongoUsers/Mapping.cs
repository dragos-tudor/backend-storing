
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static void MapUserClassTypes () {
    BsonClassMap.RegisterClassMap<UserInfo>(MapClassType);
    BsonClassMap.RegisterClassMap<UserRole>(MapClassType);
    BsonClassMap.RegisterClassMap<UserInfoResult>(MapClassType);
  }
}