using static Storing.MongoDb.MongoMappers;

namespace Storing.MongoDb;

public static partial class MongoUsers {

  public static void MapUserClassTypes () {
    BsonClassMap.RegisterClassMap<UserInfo>(MapClassType);
    BsonClassMap.RegisterClassMap<UserRole>(MapClassType);
    BsonClassMap.RegisterClassMap<UserInfoResult>(MapClassType);
  }

}