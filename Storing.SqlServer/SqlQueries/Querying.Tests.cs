
namespace Storing.SqlServer;

partial class SqlServerTests
{
  sealed record BookQueryDto
  {
    public string? AuthorName {get; init;}
    public DateTime? ReleaseDateGreater {get; init;}
    public bool? AscendingBookName {get; init;}
    public short? PageLimit {get; init;}
    public short? PageNo {get; init;}
  }

  [TestMethod]
  public async Task filter_sort_page_entitites__query_entities__query_result ()
  {
    Author[] authors = [
      new() { AuthorName = "Mircea Eliade" },
      new() { AuthorName = "George Calinescu" }];
    Book[] books = [
      new() { BookName = "Maitreyi", Authors = [authors[0]] },
      new() { BookName = "Domnisoara Christina", Authors = [authors[0]] },
      new() { BookName = "Nunta in cer", Authors = [authors[0]] },
      new() { BookName = "La tiganci", Authors = [authors[0]] },
      new() { BookName = "Bietul Ioanide", Authors = [authors[1]] },
      new() { BookName = "Scrinul negru", Authors = [authors[1]] }
    ];
    using var dbContext = CreateEntitiesContext(EntitiesConnString);
    foreach(var book in books)
      AddEntity(dbContext, book);

    await dbContext.SaveChangesAsync();

    var query = new BookQueryDto() {
      AuthorName = "Mircea Eliade",
      AscendingBookName = true,
      PageLimit = 3
    };

    var bookNames = await dbContext.Books
      .Filter(query.AuthorName, (name) => (book) => book.Authors!.Any(a => a.AuthorName == name))
      .Filter(query.ReleaseDateGreater, (date) => (book) => book.ReleaseDate > date)
      .Order(query.AscendingBookName, (book) => book.BookName)
      .Page(query.PageLimit, query.PageNo)
      .Select(book => book.BookName)
      .ToArrayAsync();

    AreEqual(["Domnisoara Christina", "La tiganci", "Maitreyi"], bookNames);
  }

}