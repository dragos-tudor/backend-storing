## Backend storing library.
- convenient functions for SQLServer, MongoDb and Redis databases.
- functional-style library [OOP-free].
- podman-inside-of-podman.

### Usage [sql server]
```cs
  using static Storing.SqlServer.SqlServerFuncs;
  ...

  // from json, secrets, args
  // var connString = CreateSqlConnectionString(configuration.GetSection(nameof(SqlServerOptions)).Get<SqlServerOptions>());
  var connString = CreateSqlConnectionString("127.0.0.1:1433", "username", "P@ssw0rd!", "Library");
  var dbContextOptions = CreateSqlContextOptions<LibraryContext>(connString);

  // add to entity M2M collection
  var book = new Book { BookId = 1 };
  var author = new Author { Books = [book] };

  using var dbContext = new LibraryContext(dbContextOptions);
  AddEntity(dbContext, author);

  Assert.AreEqual(EntityState.Added, dbContext.EntityEntry("BooksBookId", book.BookId)?.State);
  Assert.AreEqual(EntityState.Unchanged, dbContext.Entry(book).State);



  // update modified entity properties [partial update]
  var author = new Author{ AuthorId = Guid.NewGuid(), AuthorName = "name", BirthDate = DateTime.Now };

  using var dbContext = CreateLibraryContext(dbContextOptions);
  UpdateEntity(dbContext, author, (author) => {
    author.AuthorName = "updated";
    author.BirthDate = author.BirthDate;
  });

  Assert.IsTrue(dbContext.Entry(author).Property(s => s.AuthorName).IsModified);
  Assert.IsFalse(dbContext.Entry(author).Property(s => s.BirthDate).IsModified);



  // update modified entity M2M collection [partial update]
  var book = new Book { BookId = 0 };
  var author = new Author{ AuthorId = Guid.NewGuid() };

  using var dbContext = CreateLibraryContext(dbContextOptions);
  UpdateEntity(dbContext, author, (author) => author.Books = [ book ]);

  Assert.AreEqual(EntityState.Added, dbContext.EntityEntry("AuthorsAuthorId", author.AuthorId)?.State);
  Assert.AreEqual(EntityState.Added, dbContext.Entry(book)?.State);



  // delete entity with M2M collection
  var book = new Book { BookId = 1 };
  var author = new Author { AuthorId = Guid.NewGuid(), Books = new [] { book } };

  using var dbContext = CreateLibraryContext(dbContextOptions);
  DeleteEntity(dbContext, author);

  Assert.AreEqual(EntityState.Deleted, dbContext.EntityEntry("BooksBookId", book.BookId)?.State);
  Assert.AreEqual(EntityState.Unchanged, dbContext.Entry(book)?.State);


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

  using var dbContext = CreateLibraryContext(dbContextOptions);
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

  CollectionAssert.AreEqual((string[])["Domnisoara Christina", "La tiganci", "Maitreyi"], bookNames);
```

### Usage [mongodb]
```cs
  using static Storing.MongoDb.MongoDbFuncs;
  ...

  // from json, secrets, args
  // var clientSettings = CreateMongoClientSettings(configuration.GetSection(nameof(MongoOptions)).Get<MongoOptions>());
  var clientSettings = CreateMongoClientSettings(["127.0.0.1:27017"]);
  using var mongoClient = CreateMongoClient(clientSettings);
  var mongoDb = GetMongoDatabase(mongoClient, "Test");

  // modify book document props
  var id = Guid.NewGuid().ToString();
  var books = GetCollection<Book>(mongoDb, "books");
  var original = new Book { Id = id, BookName = "a", ReleaseYear = 2023 };
  var modified = new { BookName = "b", ReleaseYear = 2024 };

  await InsertDocument(books, original);
  await UpdateDocument(books, original, CombineDefinitions(
    SetFieldDefinition<Book, string>(nameof(Book.BookName), modified.BookName),
    SetFieldDefinition<Book, int>(nameof(Book.ReleaseYear), modified.ReleaseYear)
  ));

  var actual = await books
    .AsQueryable()
    .FirstAsync(x => x.Id == id);
  Assert.AreEqual("b", actual.BookName);
  Assert.AreEqual(2024, actual.ReleaseYear);



  // grant additionally roles to user
  var userName = Guid.NewGuid().ToString();
  await CreateUser(mongoDb, CreateCreateUserCommand(userName, "pass", ["read"]));
  await GrantRolesToUser(mongoDb, CreateGrantRolesToUserCommand(userName, ["readWrite"]));

  var actual = await FindUser(mongoDb, CreateFindUserCommand(userName));
  CollectionAssert.AreEqual((string[])["read", "readWrite"], GetUserRoles(userName, actual).OrderBy(x => x).ToList());


  // work with discriminated documents on same collection
  [BsonDiscriminator(discriminator: nameof(Discriminated), Required = true)]
  record Discriminated { public string Id { get; set; } = string.Empty; }

  record NonDiscriminated { public string Id { get; set; } = string.Empty; }

  var discriminatedColl = GetCollection<Discriminated>(mongoDb, "test");
  var nonDiscriminatedColl = GetCollection<NonDiscriminated>(mongoDb, "test");
  var discriminated = new [] {
    new Discriminated { Id = Guid.NewGuid().ToString() },
    new Discriminated { Id = Guid.NewGuid().ToString() }
  };
  var nonDiscriminated = new [] {
    new NonDiscriminated { Id = Guid.NewGuid().ToString() },
  };

  await InsertDocuments(discriminatedColl, discriminated);
  await InsertDocuments(nonDiscriminatedColl, nonDiscriminated);

  var actual = await discriminatedColl.AsDiscriminable().CountAsync();
  Assert.AreEqual(2, actual);
```

### Usage [redis]
```cs
  using Microsoft.Extensions.Caching.Distributed;
  using StackExchange.Redis;
  using static Storing.Redis.RedisFuncs;
  ...

// from json, secrets, args
  // var configOptions = CreateRedisConfigurationOptions(configuration.GetSection(nameof(RedisOptions)).Get<RedisOptions>());
  var configOptions = CreateRedisConfigurationOptions(["127.0.0.1:6379"]);
  using var redisCliient = CreateRedisClient(configOptions);
  var redisDb = GetRedisDatabase(redisClient);

  // absolute cache entry expiration
  var key = "key1";
  var text = "some text";

  var futureExpiration = TimeSpan.FromSeconds(1);
  var cacheEntryOptions = new DistributedCacheEntryOptions().SetAbsoluteExpiration(futureExpiration);

  await SetStringCacheAsync(redisDb, key, text, cacheEntryOptions);
  Assert.AreEqual(text, await GetStringCacheAsync(redisDb, key));

  await Task.Delay(TimeSpan.FromSeconds(1.5));
  Assert.IsNull(await GetStringCacheAsync(redisDb, key));
```

### Remarks
- sql server entity functions are unit-testable!
- all integration tests use internal podman containers created when dev container is started.
- options normalization. create SqlServer, MongoDb, Redis options from configurations (json, secrets, args).
- similar setup:
  - create SqlServer connection string => create db context options =>  get sql database (create DbContext).
  - create MongoDb client settings => create MongoDb client => get MongoDb database.
  - create Redis config options => create Redis client => get Redis database.
- wip podman needs to be restarted from time to time by cleaning /run/libpod and /run/podman/containers folders (solved).
