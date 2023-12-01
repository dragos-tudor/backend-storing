namespace Storing.MongoDb;

public static partial class MongoUsers {

  public static IEnumerable<string> GetUserRoles (
    string userName,
    UserInfoResult userInfoResult) =>
      userInfoResult.users
        .Where(user => user.user == userName)
        .SelectMany(user => user.roles.Select(role => role.role));

}