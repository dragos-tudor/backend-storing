using static Storing.MongoDb.MongoMappers;

namespace Storing.MongoDb;

record A { }
record B { }

public partial class MongoMappersTests {

  [Fact]
  internal void class_type__map_regular__registred_regular_type ()
  {
    var actual = BsonClassMap.RegisterClassMap<A>(MapClassType);

    Assert.True(actual.IgnoreExtraElements);
    Assert.True(actual.IsFrozen);
  }

  [Fact]
  internal void class_type__map_discriminator__registred_discriminator_type ()
  {
    var actual = BsonClassMap.RegisterClassMap<B>(MapDiscriminatorType);

    Assert.Equal(nameof(B), actual.Discriminator);
    Assert.True(actual.DiscriminatorIsRequired);
    Assert.True(actual.IgnoreExtraElements);
    Assert.True(actual.IsFrozen);
  }

}