using static Storing.SqlServer.SqlEntities;
using static Storing.SqlServer.TestContexts;

namespace Storing.SqlServer;

public partial class SqlEntitiesTests {

  [Fact]
  internal async Task entity__delete_entity__entity_deleted()
  {
    var entity = new Author { AuthorId = Guid.NewGuid() };

    using var dbContext = await CreateEntitiesContext();
    DeleteEntity(dbContext, entity);

    Assert.Equal(EntityState.Deleted, dbContext.Entry(entity).State);
  }

  [Fact]
  internal async Task entity_with_many_to_many_collection_item__delete_entity__collection_link_deleted_and_collection_item_unchanged()
  {
    var book = new Book { BookId = 1 };
    var entity = new Author { AuthorId = Guid.NewGuid(), Books = new [] { book } };

    using var dbContext = await CreateEntitiesContext();
    DeleteEntity(dbContext, entity);

    Assert.Equal(EntityState.Deleted, dbContext.EntityEntry("BooksBookId", book.BookId)?.State);
    Assert.Equal(EntityState.Unchanged, dbContext.Entry(book)?.State);
  }

}