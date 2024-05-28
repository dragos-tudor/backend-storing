
namespace Storing.MongoDb;

[BsonDiscriminator(nameof(Filter), Required = true)]
sealed record Filter
{
  public string Id { get; set; } = default!;
  public int @int;
  public string text = string.Empty;
  public bool? @bool;
  public DateTime? date;
}

partial class MongoDbTests
{
  [TestMethod]
  public async Task document__filter__filtered_document ()
  {
    var db = GetMongoDatabase();
    var coll = GetMongoCollection<Filter>(db);
    var query = coll.AsDiscriminable();
    var dates = new [] { new DateTime(2022, 07, 01), new DateTime(2022, 07, 02) };
    await InsertDocuments(coll, [
      new Filter { Id = Guid.NewGuid().ToString(), @int = 0, text = "a", @bool = true, date = dates[0] },
      new Filter { Id = Guid.NewGuid().ToString(), @int = 1, text = "ab", @bool = false, date = dates[1] },
      new Filter { Id = Guid.NewGuid().ToString(), @int = 2, text = "abc" }
    ]);

    Func<int?, Expression<Func<Filter, bool>>> filterInt = val => x => x.@int >= val;
    AreEqual([1, 2], await query.Filter(1, filterInt).Select(x => x.@int).ToListAsync());
    AreEqual([2], await query.Filter(2, filterInt).Select(x => x.@int).ToListAsync());
    AreEqual([], await query.Filter(3, filterInt).Select(x => x.@int).ToListAsync());
    AreEqual([0, 1, 2], await query.Filter(null, filterInt).Select(x => x.@int).ToListAsync());

    Func<string?, Expression<Func<Filter, bool>>> filterString = val => x => x.text.Contains(val!);
    AreEqual([ "ab", "abc" ], await query.Filter("ab", filterString).Select(x => x.text).ToListAsync());
    AreEqual([ "abc" ], await query.Filter("abc", filterString).Select(x => x.text).ToListAsync());
    AreEqual([ ], await query.Filter("abcd", filterString).Select(x => x.text).ToListAsync());
    AreEqual([ "a", "ab", "abc" ], await query.Filter(null, filterString).Select(x => x.text).ToListAsync());

    Func<bool?, Expression<Func<Filter, bool>>> filterBool = val => x => x.@bool == val!;
    AreEqual([ true ], await query.Filter(true, filterBool).Select(x => x.@bool).ToListAsync());
    AreEqual([ false ], await query.Filter(false, filterBool).Select(x => x.@bool).ToListAsync());
    AreEqual([ true, false, null ], await query.Filter(null, filterBool).Select(x => x.@bool).ToListAsync());
    AreEqual([ null ], await query.Where(x => x.@bool == null).Select(x => x.@bool).ToListAsync());

    Func<DateTime?, Expression<Func<Filter, bool>>> filterDate = val => x => x.date == val!;
    AreEqual([ dates[0] ], await query.Filter(dates[0], filterDate).Select(x => x.date).ToListAsync());
    AreEqual([ dates[1] ], await query.Filter(dates[1], filterDate).Select(x => x.date).ToListAsync());
    AreEqual([ dates[0], dates[1], null ], await query.Filter(null, filterDate).Select(x => x.date).ToListAsync());
    AreEqual([ null ], await query.Where(x => x.date == null).Select(x => x.date).ToListAsync());
  }
}