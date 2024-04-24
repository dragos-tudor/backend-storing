using static Storing.SqlServer.SqlEntities;
using static Storing.SqlServer.TestContexts;

namespace Storing.SqlServer;

public partial class SqlEntitiesTests
{
  [TestMethod]
  public async Task entity__update_entity_prop__entity_modified()
  {
    var author = new Author { AuthorId = Guid.NewGuid(), AuthorName = "name" };

    using var dbContext = await CreateEntitiesContext();
    UpdateEntity(dbContext, author, (author) =>
      author.AuthorName = "new name");

    Assert.AreEqual(EntityState.Modified, dbContext.Entry(author).State);
  }

  [TestMethod]
  public async Task entity__update_entity_prop__only_entity_prop_modified()
  {
    var author = new Author{ AuthorId = Guid.NewGuid(), AuthorName = "name", BirthDate = DateTime.Now };

    using var dbContext = await CreateEntitiesContext();
    UpdateEntity(dbContext, author, (author) => {
      author.AuthorName = "updated";
      author.BirthDate = author.BirthDate;
    });

    Assert.IsTrue(dbContext.Entry(author).Property(s => s.AuthorName).IsModified);
    Assert.IsFalse(dbContext.Entry(author).Property(s => s.BirthDate).IsModified);
  }

  [TestMethod]
  public async Task entity_without_many_to_many_collection_item__update_entity_adding_new_item__collection_link_added_and_collection_item_added()
  {
    var book = new Book { BookId = 0 };
    var author = new Author{ AuthorId = Guid.NewGuid() };

    using var dbContext = await CreateEntitiesContext();
    UpdateEntity(dbContext, author, (author) =>
      author.Books = new [] { book });

    Assert.AreEqual(EntityState.Added, dbContext.EntityEntry("AuthorsAuthorId", author.AuthorId)?.State);
    Assert.AreEqual(EntityState.Added, dbContext.Entry(book)?.State);
  }

  [TestMethod]
  public async Task entity_without_many_to_many_collection_item__update_entity_adding_existing_item__collection_link_added_and_collection_item_unchanged()
  {
    var book = new Book { BookId = 1 };
    var author = new Author{ AuthorId = Guid.NewGuid() };

    using var dbContext = await CreateEntitiesContext();
    dbContext.Attach(book);
    UpdateEntity(dbContext, author, (author) =>
      author.Books = new [] { book });

    Assert.AreEqual(EntityState.Added, dbContext.EntityEntry("AuthorsAuthorId", author.AuthorId)?.State);
    Assert.AreEqual(EntityState.Unchanged, dbContext.Entry(book)?.State);
  }

  [TestMethod]
  public async Task entity_with_many_to_many_collection_item__update_entity_removing_item__collection_link_deleted_and_collection_item_unchanged()
  {
    var book = new Book { BookId = 1 };
    var author = new Author{ AuthorId = Guid.NewGuid(), Books = [ book ] };

    using var dbContext = await CreateEntitiesContext();
    UpdateEntity(dbContext, author, (author) =>
      author.Books = []);

    Assert.AreEqual(EntityState.Deleted, dbContext.EntityEntry("BooksBookId", book.BookId)?.State);
    Assert.AreEqual(EntityState.Unchanged, dbContext.Entry(book)?.State);
  }

  [TestMethod]
  public async Task entity_with_many_to_many_collection_item__update_entity_without_changing_item__collection_link_and_collection_item_unchanged()
  {
    var book = new Book { BookId = 1 };
    var author = new Author{ AuthorId = Guid.NewGuid(), Books = [ book ] };

    using var dbContext = await CreateEntitiesContext();
    UpdateEntity(dbContext, author, (author) =>
      author.Books = [ book ]);

    Assert.AreEqual(EntityState.Unchanged, dbContext.EntityEntry("BooksBookId", book.BookId)?.State);
    Assert.AreEqual(EntityState.Unchanged, dbContext.Entry(book)?.State);
  }
}