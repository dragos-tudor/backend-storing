using static Storing.SqlServer.TestContexts;

namespace Storing.SqlServer;

partial class SqlQueriesTests {

  [Fact]
  public async Task all_items__page__items_page ()
  {
    var pages = new [] {
      new Page { Text = "0" },
      new Page { Text = "1" },
      new Page { Text = "2" },
      new Page { Text = "3" },
    };

    using var dbContext = await CreateQueriesContext();
    await dbContext.Database.ExecuteSqlAsync($"DELETE FROM Pages");
    await dbContext.AddRangeAsync(pages);
    await dbContext.SaveChangesAsync();
    var query = dbContext.Pages;

    Assert.Equal([ "0", "1" ], await query.Page(2, 0).Select(x => x.Text).ToListAsync());
    Assert.Equal([ "2", "3" ], await query.Page(2, 1).Select(x => x.Text).ToListAsync());
    Assert.Equal([ "3" ], await query.Page(3, 1).Select(x => x.Text).ToListAsync());
    Assert.Equal([], await query.Page(2, 2).Select(x => x.Text).ToListAsync());
    Assert.Equal([ "0", "1" ], await query.Page(2).Select(x => x.Text).ToListAsync());
    Assert.Equal([ "0", "1", "2", "3" ], await query.Page(null, null).Select(x => x.Text).ToListAsync());
  }

}