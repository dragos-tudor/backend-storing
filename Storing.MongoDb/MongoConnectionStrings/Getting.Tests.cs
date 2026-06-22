namespace Storing.MongoDb;

partial class MongoDbTests
{
  [TestMethod]
  public void connection_string__without_credentials__returns_plain_mongo_uri ()
  {
    var actual = GetMongoConnectionString("example.com", 27017);

    actual.ShouldBe("mongodb://example.com:27017");
  }

  [TestMethod]
  public void connection_string__with_credentials__contains_encoded_credentials ()
  {
    var actual = GetMongoConnectionString("example.com", 27017, "user", "p@ss:word");

    actual.ShouldBe("mongodb://user:p%40ss%3Aword@example.com:27017");
  }

  [TestMethod]
  public void connection_string__with_credentials__contains_credentials ()
  {
    var actual = GetMongoConnectionString(new MongoOptions
    {
      ServerName = "example.com",
      ServerPort = 27017,
      UserName = "user",
      UserPassword = "p@ss:word"
    });

    actual.ShouldBe("mongodb://user:p%40ss%3Aword@example.com:27017");
  }

  [TestMethod]
  public void connection_string_replica_set__without_credentials__returns_plain_mongo_uri ()
  {
    var actual = GetMongoConnectionString(["example.com"], [27017], "rs0");

    actual.ShouldBe("mongodb://example.com:27017?replicaSet=rs0");
  }

  [TestMethod]
  public void connection_string_replica_set__with_credentials_and_one_server__contains_encoded_credentials ()
  {
    var actual = GetMongoConnectionString(["example.com"], [27017], "rs0", "user", "p@ss:word");

    actual.ShouldBe("mongodb://user:p%40ss%3Aword@example.com:27017?replicaSet=rs0");
  }

  [TestMethod]
  public void connection_string_replica_set__with_credentials_and_multiple_servers__contains_encoded_credentials ()
  {
    var actual = GetMongoConnectionString(["example.com", "example.com"], [27017, 27018], "rs0", "user", "p@ss:word");

    actual.ShouldBe("mongodb://user:p%40ss%3Aword@example.com:27017,example.com:27018?replicaSet=rs0");
  }

  [TestMethod]
  public void connection_string_replica_set__with_credentials_and_one_server__contains_credentials ()
  {
    var actual = GetMongoConnectionString(new MongoReplicaSetOptions
    {
      ServerNames = ["example.com"],
      ServerPorts = [27017],
      UserName = "user",
      UserPassword = "p@ss:word",
      ReplicaSet = "rs0"
    });

    actual.ShouldBe("mongodb://user:p%40ss%3Aword@example.com:27017?replicaSet=rs0");
  }

  [TestMethod]
  public void connection_string_replica_set__with_credentials_and_multiple_servers__contains_credentials ()
  {
    var actual = GetMongoConnectionString(new MongoReplicaSetOptions
    {
      ServerNames = ["example.com", "example.com"],
      ServerPorts = [27017, 27018],
      ReplicaSet = "rs0"
    });

    actual.ShouldBe("mongodb://example.com:27017,example.com:27018?replicaSet=rs0");
  }
}
