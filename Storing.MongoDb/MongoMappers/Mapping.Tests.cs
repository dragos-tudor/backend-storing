using static Storing.MongoDb.MongoMappers;
#pragma warning disable CA1812

namespace Storing.MongoDb;
sealed record A { }
sealed record B { }

[TestClass]
public partial class MongoMappersTests
{
  [TestMethod]
  public void class_type__map_regular__registred_regular_type ()
  {
    var actual = BsonClassMap.RegisterClassMap<A>(MapClassType);

    Assert.IsTrue(actual.IgnoreExtraElements);
    Assert.IsTrue(actual.IsFrozen);
  }

  [TestMethod]
  public void class_type__map_discriminator__registred_discriminator_type ()
  {
    var actual = BsonClassMap.RegisterClassMap<B>(MapDiscriminatorType);

    Assert.AreEqual(nameof(B), actual.Discriminator);
    Assert.IsTrue(actual.DiscriminatorIsRequired);
    Assert.IsTrue(actual.IgnoreExtraElements);
    Assert.IsTrue(actual.IsFrozen);
  }
}