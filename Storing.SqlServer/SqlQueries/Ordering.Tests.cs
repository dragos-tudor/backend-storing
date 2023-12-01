using static Storing.SqlServer.TestContexts;

namespace Storing.SqlServer;

partial class SqlQueriesTests {

  [Fact]
  public async Task unoredered_items__order__ordered_items ()
  {
    var groupId = Guid.NewGuid();
    var orders = new [] {
      new Order { Text = "1", GroupId = groupId },
      new Order { Text = "0", GroupId = groupId },
      new Order { Text = "3", GroupId = groupId },
      new Order { Text = "2", GroupId = groupId },
    };

    using var dbContext = await CreateQueriesContext();
    await dbContext.AddRangeAsync(orders);
    await dbContext.SaveChangesAsync();
    var query = dbContext.Orders;

    Expression<Func<Order, string>> exp = x => x.Text;
    Expression<Func<Order, bool>> filter = x => x.GroupId == groupId;
    Assert.Equal([ "0", "1", "2", "3" ], await query.Where(filter).Order(true, exp).Select(exp).ToListAsync());
    Assert.Equal([ "3", "2", "1", "0" ], await query.Where(filter).Order(false, exp).Select(exp).ToListAsync());
    Assert.Equal([ "1", "0", "3", "2" ], await query.Where(filter).Order(null, exp).Select(exp).ToListAsync());
  }

}