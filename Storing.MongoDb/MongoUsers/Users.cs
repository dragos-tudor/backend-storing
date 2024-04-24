#pragma warning disable CA1819

namespace Storing.MongoDb;

public record UserInfo : Id<string>
{
  [BsonElement("userId")]
  public Guid UserId { get; set; }
  [BsonElement("user")]
  public string User { get; set; } = string.Empty;
  [BsonElement("db")]
  public string Db { get; set; }= string.Empty;
  [BsonElement("roles")]
  public UserRole[] Roles { get; set; } = [];
}

public record UserRole
{
  [BsonElement("role")]
  public string Role { get; set; } = string.Empty;
  [BsonElement("db")]
  public string Db { get; set; } = string.Empty;
}

public record UserInfoResult
{
  [BsonElement("users")]
  public UserInfo[] Users { get; set; } = [];
}