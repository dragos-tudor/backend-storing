## Backend databases library.
- convenient functions for SQLServer, MongoDb and Redis databases.
- functional-style library [OOP-free].
- using docker API for testing containers [docker componse free].
- docker-outside-of-docker.

### Usage [sql server]
```cs

  // adding to entity M2M collection
  var book = new Book { BookId = 1 };
  var author = new Author { Books = new [] { book } };

  using var dbContext = await CreateEntitiesContext();
  AddEntity(dbContext, author);

  Assert.Equal(EntityState.Added, dbContext.EntityEntry("BooksBookId", book.BookId)?.State);
  Assert.Equal(EntityState.Unchanged, dbContext.Entry(book).State);



  // update modified entity properties [partial update]
  var author = new Author{ AuthorId = Guid.NewGuid(), AuthorName = "name", BirthDate = DateTime.Now };

  using var dbContext = await CreateEntitiesContext();
  UpdateEntity(dbContext, author, (author) => {
    author.AuthorName = "updated";
    author.BirthDate = author.BirthDate;
  });

  Assert.True(dbContext.Entry(author).Property(s => s.AuthorName).IsModified);
  Assert.False(dbContext.Entry(author).Property(s => s.BirthDate).IsModified);

  // update modified entity M2M collection [partial update]
  var book = new Book { BookId = 0 };
  var author = new Author{ AuthorId = Guid.NewGuid() };

  using var dbContext = await CreateLibraryContext();
  UpdateEntity(dbContext, author, (author) => author.Books = [ book ]);

  Assert.Equal(EntityState.Added, dbContext.EntityEntry("AuthorsAuthorId", author.AuthorId)?.State);
  Assert.Equal(EntityState.Added, dbContext.Entry(book)?.State);



  // delete entity with M2M collection
  var book = new Book { BookId = 1 };
  var author = new Author { AuthorId = Guid.NewGuid(), Books = new [] { book } };

  using var dbContext = await CreateEntitiesContext();
  DeleteEntity(dbContext, author);

  Assert.Equal(EntityState.Deleted, dbContext.EntityEntry("BooksBookId", book.BookId)?.State);
  Assert.Equal(EntityState.Unchanged, dbContext.Entry(book)?.State);
```

### Remarks
- sql server entity functions could be unit-testable.
- all integration tests use dynamically [non-ephemeral] created testing containers.