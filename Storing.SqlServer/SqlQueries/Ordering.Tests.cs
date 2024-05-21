
namespace Storing.SqlServer;

partial class SqlServerTests
{
  [TestMethod]
  public async Task unoredered_items__order__ordered_items ()
  {
    var orders = new [] {
      new Order { Text = "1" },
      new Order { Text = "0" },
      new Order { Text = "3" },
      new Order { Text = "2" },
    };

    using var dbContext = CreateQueriesContext();
    await dbContext.AddRangeAsync(orders);
    await dbContext.SaveChangesAsync();
    var query = dbContext.Orders;

    Expression<Func<Order, string>> exp = x => x.Text;
    AreEqual([ "0", "1", "2", "3" ], await query.Order(true, exp).Select(exp).ToListAsync());
    AreEqual([ "3", "2", "1", "0" ], await query.Order(false, exp).Select(exp).ToListAsync());
    AreEqual([ "1", "0", "3", "2" ], await query.Order(null, exp).Select(exp).ToListAsync());
  }
}