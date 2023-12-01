using static Storing.SqlServer.TestContexts;

namespace Storing.SqlServer;

partial class SqlQueriesTests {

  [Fact]
  public async Task all_items__page__items_page ()
  {
    var groupId = Guid.NewGuid();
    var pages = new [] {
      new Page { Text = "0", GroupId = groupId },
      new Page { Text = "1", GroupId = groupId },
      new Page { Text = "2", GroupId = groupId },
      new Page { Text = "3", GroupId = groupId },
    };

    using var dbContext = await CreateQueriesContext();
    await dbContext.AddRangeAsync(pages);
    await dbContext.SaveChangesAsync();
    var query = dbContext.Pages;

    Expression<Func<Page, bool>> filter = x => x.GroupId == groupId;
    Assert.Equal([ "0", "1" ], await query.Where(filter).Page(2, 0).Select(x => x.Text).ToListAsync());
    Assert.Equal([ "2", "3" ], await query.Where(filter).Page(2, 1).Select(x => x.Text).ToListAsync());
    Assert.Equal([ "3" ], await query.Where(filter).Page(3, 1).Select(x => x.Text).ToListAsync());
    Assert.Equal([], await query.Where(filter).Page(2, 2).Select(x => x.Text).ToListAsync());
    Assert.Equal([ "0", "1" ], await query.Where(filter).Page(2).Select(x => x.Text).ToListAsync());
  }

}