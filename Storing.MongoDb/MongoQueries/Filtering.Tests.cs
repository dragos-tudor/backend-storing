using static Storing.MongoDb.MongoIdentities;
using static Storing.MongoDb.MongoDocuments;
using static Storing.MongoDb.MongoCollections;

namespace Storing.MongoDb;

[BsonDiscriminator(nameof(Filter), Required = true)]
record Filter : Id<string> {
  public int @int;
  public string text = String.Empty;
  public bool? @bool = null;
  public DateTime? date = null;
}

public partial class MongoQueriesTests {

  [Fact]
  internal async Task document__filter__filtered_document ()
  {
    var db = await GetMongoDatabase();
    var coll = GetCollection<Filter>(db, dbCollection);
    var query = coll.AsDiscriminable();
    var dates = new [] { new DateTime(2022, 07, 01), new DateTime(2022, 07, 02) };
    await InsertDocuments(coll, new [] {
      new Filter { _id = Guid.NewGuid().ToString(), @int = 0, text = "a", @bool = true, date = dates[0] },
      new Filter { _id = Guid.NewGuid().ToString(), @int = 1, text = "ab", @bool = false, date = dates[1] },
      new Filter { _id = Guid.NewGuid().ToString(), @int = 2, text = "abc" }
    });

    Func<int?, Expression<Func<Filter, bool>>> filterInt = val => x => x.@int >= val;
    Assert.Equal(new [] { 1, 2 }, await query.Filter(1, filterInt).Select(x => x.@int).ToListAsync());
    Assert.Equal(new [] { 2 }, await query.Filter(2, filterInt).Select(x => x.@int).ToListAsync());
    Assert.Equal(new int [0], await query.Filter(3, filterInt).Select(x => x.@int).ToListAsync());
    Assert.Equal(new [] { 0, 1, 2 }, await query.Filter(null, filterInt).Select(x => x.@int).ToListAsync());

    Func<string?, Expression<Func<Filter, bool>>> filterString = val => x => x.text.Contains(val!);
    Assert.Equal(new [] { "ab", "abc" }, await query.Filter("ab", filterString).Select(x => x.text).ToListAsync());
    Assert.Equal(new [] { "abc" }, await query.Filter("abc", filterString).Select(x => x.text).ToListAsync());
    Assert.Equal(new string [0], await query.Filter("abcd", filterString).Select(x => x.text).ToListAsync());
    Assert.Equal(new string [] { "a", "ab", "abc" }, await query.Filter(null, filterString).Select(x => x.text).ToListAsync());

    Func<bool?, Expression<Func<Filter, bool>>> filterBool = val => x => x.@bool == val!;
    Assert.Equal(new bool? [] { true }, await query.Filter(true, filterBool).Select(x => x.@bool).ToListAsync());
    Assert.Equal(new bool? [] { false }, await query.Filter(false, filterBool).Select(x => x.@bool).ToListAsync());
    Assert.Equal(new bool? [] { true, false, null }, await query.Filter(null, filterBool).Select(x => x.@bool).ToListAsync());
    Assert.Equal(new bool? [] { null }, await query.Where(x => x.@bool == null).Select(x => x.@bool).ToListAsync());

    Func<DateTime?, Expression<Func<Filter, bool>>> filterDate = val => x => x.date == val!;
    Assert.Equal(new DateTime? [] { dates[0] }, await query.Filter(dates[0], filterDate).Select(x => x.date).ToListAsync());
    Assert.Equal(new DateTime? [] { dates[1] }, await query.Filter(dates[1], filterDate).Select(x => x.date).ToListAsync());
    Assert.Equal(new DateTime? [] { dates[0], dates[1], null }, await query.Filter(null, filterDate).Select(x => x.date).ToListAsync());
    Assert.Equal(new DateTime? [] { null }, await query.Where(x => x.date == null).Select(x => x.date).ToListAsync());
  }

}