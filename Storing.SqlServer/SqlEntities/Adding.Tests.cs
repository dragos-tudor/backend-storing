using static Storing.SqlServer.SqlEntities;
using static Storing.SqlServer.TestContexts;

namespace Storing.SqlServer;

public partial class SqlEntitiesTests {

  [Fact]
  internal async Task entity__add_entity__entity_added()
  {
    var author = new Author{ };

    using var dbContext = await CreateEntitiesContext();
    AddEntity(dbContext, author);

    Assert.Equal(EntityState.Added, dbContext.Entry(author).State);
  }

  [Fact]
  internal async Task entity_with_empty_many_to_many_collection__add_entity_with_collection_item__collection_link_and_item_added()
  {
    var book = new Book { BookId = 0 };
    var author = new Author { Books = new [] { book } };

    using var dbContext = await CreateEntitiesContext();
    AddEntity(dbContext, author);

    Assert.Equal(EntityState.Added, dbContext.EntityEntry("AuthorsAuthorId", author.AuthorId)?.State);
    Assert.Equal(EntityState.Added, dbContext.Entry(book).State);
  }

  [Fact]
  internal async Task entity_with_many_to_many_collection_item__add_entity_with_collection_item__collection_link_added_and_collection_item_unchanged()
  {
    var book = new Book { BookId = 1 };
    var author = new Author { Books = new [] { book } };

    using var dbContext = await CreateEntitiesContext();
    AddEntity(dbContext, author);

    Assert.Equal(EntityState.Added, dbContext.EntityEntry("BooksBookId", book.BookId)?.State);
    Assert.Equal(EntityState.Unchanged, dbContext.Entry(book).State);
  }

}