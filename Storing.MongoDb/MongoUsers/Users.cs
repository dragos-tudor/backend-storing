using static Storing.MongoDb.MongoIdentities;

namespace Storing.MongoDb;

public static partial class MongoUsers {

  public record UserInfo : Id<string> {
    public Guid userId = Guid.Empty;
    public string user = String.Empty;
    public string db = String.Empty;
    public UserRole[] roles = new UserRole[0];
  }

  public record UserRole {
    public string role = String.Empty;
    public string db = String.Empty;
  }

  public record UserInfoResult {
    public UserInfo[] users = new UserInfo[0];
  }

}