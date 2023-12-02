using static Storing.SqlServer.TestContexts;

namespace Storing.SqlServer;

public partial class SqlQueriesTests {

  [Fact]
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

    using var dbContext = await CreateQueriesContext();
    await dbContext.Database.ExecuteSqlAsync($"DELETE FROM Filters");
    await dbContext.AddRangeAsync(filters);
    await dbContext.SaveChangesAsync();
    var query = dbContext.Filters;

    Func<int?, Expression<Func<Filter, bool>>> filterInt = val => x => x.Int >= val;
    Assert.Equal([ 1, 2 ], await query.Filter(1, filterInt).Select(x => x.Int).ToListAsync());
    Assert.Equal([ 2 ], await query.Filter(2, filterInt).Select(x => x.Int).ToListAsync());
    Assert.Equal([], await query.Filter(3, filterInt).Select(x => x.Int).ToListAsync());
    Assert.Equal([ 0, 1, 2 ], await query.Filter(null, filterInt).Select(x => x.Int).ToListAsync());

    Func<string?, Expression<Func<Filter, bool>>> filterString = val => x => x.Text.Contains(val!);
    Assert.Equal([ "ab", "abc" ], await query.Filter("ab", filterString).Select(x => x.Text).ToListAsync());
    Assert.Equal([ "abc" ], await query.Filter("abc", filterString).Select(x => x.Text).ToListAsync());
    Assert.Equal([], await query.Filter("abcd", filterString).Select(x => x.Text).ToListAsync());
    Assert.Equal([ "a", "ab", "abc" ], await query.Filter(null, filterString).Select(x => x.Text).ToListAsync());

    Func<bool?, Expression<Func<Filter, bool>>> filterBool = val => x => x.Bool == val!;
    Assert.Equal(new bool? [] { true }, await query.Filter(true, filterBool).Select(x => x.Bool).ToListAsync());
    Assert.Equal(new bool? [] { false }, await query.Filter(false, filterBool).Select(x => x.Bool).ToListAsync());
    Assert.Equal(new bool? [] { true, false, null }, await query.Filter(null, filterBool).Select(x => x.Bool).ToListAsync());
    Assert.Equal(new bool? [] { null }, await query.Where(x => x.Bool == null).Select(x => x.Bool).ToListAsync());

    Func<DateTime?, Expression<Func<Filter, bool>>> filterDate = val => x => x.Date == val!;
    Assert.Equal(new DateTime? [] { dates[0] }, await query.Filter(dates[0], filterDate).Select(x => x.Date).ToListAsync());
    Assert.Equal(new DateTime? [] { dates[1] }, await query.Filter(dates[1], filterDate).Select(x => x.Date).ToListAsync());
    Assert.Equal(new DateTime? [] { dates[0], dates[1], null }, await query.Filter(null, filterDate).Select(x => x.Date).ToListAsync());
    Assert.Equal(new DateTime? [] { null }, await query.Where(x => x.Date == null).Select(x => x.Date).ToListAsync());
    }


}