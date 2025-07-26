
namespace Storing.SqlServer;

partial class SqlServerTests
{
  [TestMethod]
  public async Task items__filter__filtered_items ()
  {
    var dates = new [] {
      new DateTime(2022, 07, 01),
      new DateTime(2022, 07, 02)
    };
    var filters = new [] {
      new Filter { Int = 0, Text = "a", Bool = true, Date = dates[0] },
      new Filter { Int = 1, Text = "ab", Bool = false, Date = dates[1] },
      new Filter { Int = 2, Text = "abc" }
    };

    using var dbContext = CreateQueriesContext();
    await dbContext.AddRangeAsync(filters);
    await dbContext.SaveChangesAsync();
    var query = dbContext.Filters;

    Func<int?, Expression<Func<Filter, bool>>> filterInt = val => x => x.Int >= val;
    (await query.Filter(1, filterInt).Select(x => x.Int).ToListAsync()).ShouldBe([1, 2]);
    (await query.Filter(2, filterInt).Select(x => x.Int).ToListAsync()).ShouldBe([ 2 ]);
    (await query.Filter(3, filterInt).Select(x => x.Int).ToListAsync()).ShouldBe([]);
    (await query.Filter(null, filterInt).Select(x => x.Int).ToListAsync()).ShouldBe([ 0, 1, 2 ]);

    Func<string?, Expression<Func<Filter, bool>>> filterString = val => x => x.Text.Contains(val!);
    (await query.Filter("ab", filterString).Select(x => x.Text).ToListAsync()).ShouldBe([ "ab", "abc" ]);
    (await query.Filter("abc", filterString).Select(x => x.Text).ToListAsync()).ShouldBe([ "abc" ]);
    (await query.Filter("abcd", filterString).Select(x => x.Text).ToListAsync()).ShouldBe([]);
    (await query.Filter(null, filterString).Select(x => x.Text).ToListAsync()).ShouldBe([ "a", "ab", "abc" ]);

    Func<bool?, Expression<Func<Filter, bool>>> filterBool = val => x => x.Bool == val!;
    (await query.Filter(true, filterBool).Select(x => x.Bool).ToListAsync()).ShouldBe([ true ]);
    (await query.Filter(false, filterBool).Select(x => x.Bool).ToListAsync()).ShouldBe([ false ]);
    (await query.Filter(null, filterBool).Select(x => x.Bool).ToListAsync()).ShouldBe([ true, false, null ]);
    (await query.Where(x => x.Bool == null).Select(x => x.Bool).ToListAsync()).ShouldBe([ null ]);

    Func<DateTime?, Expression<Func<Filter, bool>>> filterDate = val => x => x.Date == val!;
    (await query.Filter(dates[0], filterDate).Select(x => x.Date).ToListAsync()).ShouldBe([ dates[0] ]);
    (await query.Filter(dates[1], filterDate).Select(x => x.Date).ToListAsync()).ShouldBe([ dates[1] ]);
    (await query.Filter(null, filterDate).Select(x => x.Date).ToListAsync()).ShouldBe([ dates[0], dates[1], null ]);
    (await query.Where(x => x.Date == null).Select(x => x.Date).ToListAsync()).ShouldBe([ null ]);
  }
}