namespace Storing.MongoDb;

public static partial class MongoUsers
{
  public static IEnumerable<string> GetUserRoles (
    string userName,
    UserInfoResult userInfoResult) =>
      userInfoResult.Users
        .Where(user => user.User == userName)
        .SelectMany(user => user.Roles.Select(role => role.Role));
}