
namespace Storing.SqlServer;

partial class SqlServerTests
{
  [TestMethod]
  public async Task all_items__page__items_page ()
  {
    var pages = new [] {
      new Page { Text = "0" },
      new Page { Text = "1" },
      new Page { Text = "2" },
      new Page { Text = "3" },
    };

    using var dbContext = CreateQueriesContext();
    await dbContext.AddRangeAsync(pages);
    await dbContext.SaveChangesAsync();
    var query = dbContext.Pages;

    (await query.Page(2, 0).Select(x => x.Text).ToListAsync()).ShouldBe([ "0", "1" ]);
    (await query.Page(2, 1).Select(x => x.Text).ToListAsync()).ShouldBe([ "2", "3" ]);
    (await query.Page(3, 1).Select(x => x.Text).ToListAsync()).ShouldBe([ "3" ]);
    (await query.Page(2, 2).Select(x => x.Text).ToListAsync()).ShouldBe([]);
    (await query.Page(2).Select(x => x.Text).ToListAsync()).ShouldBe([ "0", "1" ]);
    (await query.Page(null, null).Select(x => x.Text).ToListAsync()).ShouldBe([ "0", "1", "2", "3" ]);
  }
}