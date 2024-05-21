
namespace Storing.SqlServer;

partial class SqlServerTests
{
  [TestMethod]
  public void entity__add_entity__entity_added()
  {
    var author = new Author{ };

    using var dbContext = CreateEntitiesContext();
    AddEntity(dbContext, author);

    Assert.AreEqual(EntityState.Added, dbContext.Entry(author).State);
  }

  [TestMethod]
  public void entity_with_empty_many_to_many_collection__add_entity_with_collection_item__collection_link_and_item_added()
  {
    var book = new Book { BookId = 0 };
    var author = new Author { Books = [book] };

    using var dbContext = CreateEntitiesContext();
    AddEntity(dbContext, author);

    Assert.AreEqual(EntityState.Added, dbContext.EntityEntry("AuthorsAuthorId", author.AuthorId)?.State);
    Assert.AreEqual(EntityState.Added, dbContext.Entry(book).State);
  }

  [TestMethod]
  public void entity_with_many_to_many_collection_item__add_entity_with_collection_item__collection_link_added_and_collection_item_unchanged()
  {
    var book = new Book { BookId = 1 };
    var author = new Author { Books = [book] };

    using var dbContext = CreateEntitiesContext();
    AddEntity(dbContext, author);

    Assert.AreEqual(EntityState.Added, dbContext.EntityEntry("BooksBookId", book.BookId)?.State);
    Assert.AreEqual(EntityState.Unchanged, dbContext.Entry(book).State);
  }
}