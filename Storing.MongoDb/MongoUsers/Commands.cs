using static Storing.MongoDb.MongoCommands;

namespace Storing.MongoDb;

public static partial class MongoUsers {

  public class CreateUserCommand : BsonCommand<BsonDocument> {
    public CreateUserCommand(BsonDocument document): base(document) {}
  }

  public sealed class CreateUserForDatabasesCommand : BsonCommand<BsonDocument> {
    public CreateUserForDatabasesCommand(BsonDocument document): base(document) {}
  }

  public sealed class DropUserCommand : BsonCommand<BsonDocument> {
    public DropUserCommand(BsonDocument document): base(document) {}
  }

  public sealed class DropAllUsersCommand : BsonCommand<BsonDocument> {
    public DropAllUsersCommand(BsonDocument document): base(document) {}
  }

  public sealed class FindUserCommand : BsonCommand<UserInfoResult> {
    public FindUserCommand(BsonDocument document): base(document) {}
  }

  public sealed class GrantRolesToUserCommand : BsonCommand<BsonDocument> {
    public GrantRolesToUserCommand(BsonDocument document): base(document) {}
  }

  public sealed class RevokeRolesFromUserCommand : BsonCommand<BsonDocument> {
    public RevokeRolesFromUserCommand(BsonDocument document): base(document) {}
  }
}