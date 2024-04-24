
namespace Storing.MongoDb;

public sealed class CreateUserCommand(BsonDocument document) : BsonCommand<BsonDocument>(document) { }

public sealed class CreateUserForDatabasesCommand(BsonDocument document) : BsonCommand<BsonDocument>(document) { }

public sealed class DropUserCommand(BsonDocument document) : BsonCommand<BsonDocument>(document) { }

public sealed class DropAllUsersCommand(BsonDocument document) : BsonCommand<BsonDocument>(document) { }

public sealed class FindUserCommand(BsonDocument document) : BsonCommand<UserInfoResult>(document) { }

public sealed class GrantRolesToUserCommand(BsonDocument document) : BsonCommand<BsonDocument>(document) { }

public sealed class RevokeRolesFromUserCommand(BsonDocument document) : BsonCommand<BsonDocument>(document) { }