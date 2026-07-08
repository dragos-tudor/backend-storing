
namespace Storing.Redis;

partial class RedisFuncs
{
  public static string? GetRedisAdminUserName(string userName = "REDIS_USERNAME") => Environment.GetEnvironmentVariable(userName);

  public static string? GetRedisAdminPassword(string password = "REDIS_PASSWORD") => Environment.GetEnvironmentVariable(password);
}