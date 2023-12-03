## Backend storing library.
- convenient functions for SQLServer, MongoDb and Redis databases.
- functional-style library [OOP-free].
- using docker API for testing containers [docker componse free].
- docker-outside-of-docker.

### Usage [sql server]
```cs

  var connString = CreateSqlConnection("Library", "username", "P@ssw0rd!", "127.0.0.1:1433");
  var dbContextOptions = CreateSqlContextOptions<LibraryContext>(connString);

  // add to entity M2M collection
  var book = new Book { BookId = 1 };
  var author = new Author { Books = new [] { book } };

  using var dbContext = CreateLibraryContext(dbContextOptions);
  AddEntity(dbContext, author);

  Assert.Equal(EntityState.Added, dbContext.EntityEntry("BooksBookId", book.BookId)?.State);
  Assert.Equal(EntityState.Unchanged, dbContext.Entry(book).State);



  // update modified entity properties [partial update]
  var author = new Author{ AuthorId = Guid.NewGuid(), AuthorName = "name", BirthDate = DateTime.Now };

  using var dbContext = CreateLibraryContext(dbContextOptions);
  UpdateEntity(dbContext, author, (author) => {
    author.AuthorName = "updated";
    author.BirthDate = author.BirthDate;
  });

  Assert.True(dbContext.Entry(author).Property(s => s.AuthorName).IsModified);
  Assert.False(dbContext.Entry(author).Property(s => s.BirthDate).IsModified);



  // update modified entity M2M collection [partial update]
  var book = new Book { BookId = 0 };
  var author = new Author{ AuthorId = Guid.NewGuid() };

  using var dbContext = CreateLibraryContext(dbContextOptions);
  UpdateEntity(dbContext, author, (author) => author.Books = [ book ]);

  Assert.Equal(EntityState.Added, dbContext.EntityEntry("AuthorsAuthorId", author.AuthorId)?.State);
  Assert.Equal(EntityState.Added, dbContext.Entry(book)?.State);



  // delete entity with M2M collection
  var book = new Book { BookId = 1 };
  var author = new Author { AuthorId = Guid.NewGuid(), Books = new [] { book } };

  using var dbContext = CreateLibraryContext(dbContextOptions);
  DeleteEntity(dbContext, author);

  Assert.Equal(EntityState.Deleted, dbContext.EntityEntry("BooksBookId", book.BookId)?.State);
  Assert.Equal(EntityState.Unchanged, dbContext.Entry(book)?.State);

  // querying with nullable props
  public record BookQueryDto
  {
    public string? AuthorName {get; init;}
    public DateTime? ReleaseDateGreater {get; init;}
    public bool? AscendingBookName {get; init;}
    public short? PageLimit {get; init;}
    public short? PageNo {get; init;}
  }

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

  using var dbContext = CreateLibrariesContext();
  dbContext.Authors.AddRange(authors);
  dbContext.Books.AddRange(books);
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

  Assert.Contains(["Domnisoara Christina", "La tiganci", "Maitreyi"], bookNames);
```

### Remarks
- sql server entity functions are unit-testable.
- all integration tests use dynamically created [non-ephemeral] database docker containers [[Docker.DotNet](https://github.com/dotnet/dotnet-docker) package instead of [testcontainers-dotnet](https://github.com/testcontainers/testcontainers-dotnet)].