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
  public void connection_string__with_regular_credentials__contains_encoded_credentials ()
  {
    var actual = GetMongoConnectionString("example.com", 27017, "regular-user", "p@ss:word");

    actual.ShouldBe("mongodb://regular-user:p%40ss%3Aword@example.com:27017");
  }

  [TestMethod]
  public void connection_string__with_admin_credentials__contains_admin_credentials ()
  {
    var actual = GetMongoConnectionString(new MongoOptions
    {
      ServerName = "example.com",
      ServerPort = 27017,
      AdminName = "admin-user",
      AdminPassword = "secret"
    });

    actual.ShouldBe("mongodb://admin-user:secret@example.com:27017");
  }

  [TestMethod]
  public void connection_string__with_regular_credentials__prefers_regular_over_admin ()
  {
    var actual = GetMongoConnectionString(new MongoOptions
    {
      ServerName = "example.com",
      ServerPort = 27017,
      AdminName = "admin-user",
      AdminPassword = "secret",
      UserName = "regular-user",
      UserPassword = "regular-secret"
    });

    actual.ShouldBe("mongodb://regular-user:regular-secret@example.com:27017");
  }
}
