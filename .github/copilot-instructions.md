
# Coding instructions

**Project Setup**
- **Target Framework:** net10.0.
- **Nullability:** `Nullable` enabled; prefer non-nullable references and annotate nullable types explicitly.
- **Implicit usings:** disabled; each project supplies a Using.cs for global usings.
- **Analyzers & Build:** `AnalysisMode=All`, `TreatWarningsAsErrors=true`, `RunAnalyzersDuringBuild=true`. Treat code-analysis warnings as errors; be conservative with `#pragma` usage.
- **Central package management:** `ManagePackageVersionsCentrally=true` using Directory.Packages.props.
- **Testing:** Tests live adjacent to feature code in `*.Tests.cs` files (e.g., Filtering.Tests.cs).

**Repository Structure**
- **Project-per-storage:** Storing.ElasticSearch, Storing.MongoDb, Storing.Redis, Storing.SqlServer.
- **Functional folders:** Groups related logic within projects (e.g., `SqlQueries/`, `MongoCollections/`, `RedisDatabases/`).
- **Partial static modules:** Use `static partial class <Name>Funcs` (e.g., `SqlServerFuncs`, `MongoDbFuncs`) split across many files for modular functions.
- **Global usings:** Centralize commonly used usings in a Using.cs per project.

**Naming Conventions**
- **Types:** PascalCase (e.g., `DatabaseOptions`, `SqlEntity`).
- **Static helper classes:** PascalCase + `Funcs` suffix (e.g., `SqlServerFuncs`, `MongoDbFuncs`, `RedisFuncs`, `ElasticSearchFuncs`).
- **Methods:** PascalCase; async methods suffixed with `Async` (e.g., `FilterAsync`).
- **Generic parameters:** `T...` style (e.g., `T1`, `T2`, `TResult`).
- **Parameters & locals:** camelCase (e.g., `source`, `value`, `cancellationToken`).
- **Test names:** human-readable with double underscores `subject__action__expected` (e.g., `source__filter__results_returned`).
- **Fields/constants:** PascalCase; avoid leading underscores for private fields.

**File & Function Granularity**
- Prefer many small files containing single, focused functions or small related groups within feature folders.
- Use `partial` classes to keep related helpers grouped logically across files under a single static class.

**Coding Style**
- Use `var` when the right-hand type is obvious.
- Prefer expression-bodied members for trivial functions.
- Use `record` for immutable data/options containers (e.g., DatabaseOptions.cs).
- Use `init` properties for immutable initialization.
- Use target-typed `new()` where readable.
- Keep functions small and single-purpose.
- Propagate `CancellationToken` in asynchronous APIs and default to `default`.
- Be explicit about nullable types: `T?` for nullable returns/params.

**Design Patterns & Architectural Principles**
- **Functional composition via static funcs:** Logic lives in small pure-ish static functions grouped under `*Funcs` partial classes.
- **Dependency injection by delegates:** Pass behavior as delegate parameters to decouple logic from infrastructure (e.g., `Func<T2, Expression<Func<T1, bool>>> expression`).
- **Adapter per persistence:** Storage-specific implementations for different backends (SQL, Mongo, Redis, ElasticSearch) following the same functional patterns.

**Functional Programming Principles**
- Favor small pure functions that return data rather than mutate state.
- Compose behavior with higher-order functions (pass delegates).
- Localize side effects to specific storage adapters; keep logic side-effect-light.

**Typical Signatures & Delegates**
- Example function extension:
  - `public static IQueryable<T1> Filter<T1, T2>(this IQueryable<T1> source, T2 value, Func<T2, Expression<Func<T1, bool>>> expression) => ...`
- Example async operation:
  - `public static async Task<TResult> ExecuteAsync<T, TResult>(T client, CancellationToken cancellationToken = default)`

**Validation & Error Flows**
- Reserve exceptions for unexpected/unrecoverable runtime issues.
- Return results or allow callers to handle missing data via nullable returns or options.

**Testing Conventions**
- Tests use `MSTest` and `Shouldly` for assertions.
- Use `CancellationToken.None` or `default` explicitly in test calls.
- Mirror production folder structure in tests.

**Formatting & Analyzer Usage**
- Keep code consistent with `AnalysisMode=All` and `PublicApiAnalyzers`.
- Treat analyzer results as primary style enforcement.

**Examples**
- Static partial class member:
  - `partial class SqlServerFuncs { public static IQueryable<T1> Filter<T1, T2>(...) => ... }`
- Options record:
  - `public abstract record DatabaseOptions<T> { ... }`

**Recommended Practices**
- Identify and extract all well-defined, major structures/features into their folders. Bring them to surface instead to encapsulate into functions.
- Organize structure into folders like `Options`, `Credentials`, `Endpoints`, `Documents`, `Databases`, `Clients`.
- Keep files small and focused; prefer many small partial files over large monoliths.
- Always include Using.cs for project-wide usings.

**Anti-patterns**
- Avoid large mutable services and stateful classes.
- Avoid hidden dependencies; prefer explicit function parameters or delegates.
- Do not mix storage-specific logic (e.g., Mongo logic inside SQL project).

**How to add new features**
1. Identify the project (e.g., Storing.Redis).
2. Add small partial functions under the appropriate `*Funcs` static partial class (e.g., `RedisFuncs`).
3. Place files in a focused folder describing the concern.
4. Add tests adjacent to the code in `*.Tests.cs` files.

**References (repo hints)**
- Central configuration: Directory.Build.props.
- Package versions: Directory.Packages.props.
- Project patterns: Using.cs, Filtering.cs.