using static Storing.SqlServer.SqlEntities;
using static Storing.SqlServer.TestContexts;

namespace Storing.SqlServer;

public partial class SqlEntitiesTests {

  [Fact]
  internal async Task entity_with_collection_item__track_entity__collection_item_tracked ()
  {
    var course  = new Course { CourseId = 0 };
    var student = new Student { StudentId = 1, Courses = new [] { course } };

    using var dbContext = await CreateTrackingContext();
    TrackEntityCollections(dbContext, student);

    Assert.Equal(EntityState.Added, dbContext.Entry(course).State);
  }

  [Fact]
  internal async Task entity_with_navigation_item__track_entity__navigation_item_tracked ()
  {
    var professor = new Professor { ProfessorId = 0 };
    var course  = new Course { CourseId = 1, Professor = professor };

    using var dbContext = await CreateTrackingContext();
    TrackEntityNavigations(dbContext, course);

    Assert.Equal(EntityState.Added, dbContext.Entry(professor).State);
  }

  [Fact]
  internal async Task entity_with_emoty_collection_item__track_entity__not_throw_error ()
  {
    var student = new Student { StudentId = 1, Courses = default };

    using var dbContext = await CreateTrackingContext();
    TrackEntityCollections(dbContext, student);
  }

  [Fact]
  internal async Task entity_with_empty_navigation_item__track_entity__not_throw_error ()
  {
    var course  = new Course { CourseId = 1, Professor = default };

    using var dbContext = await CreateTrackingContext();
    TrackEntityNavigations(dbContext, course);
  }

  [Fact]
  internal async Task entity_with_non_mapped_navigation_item__track_entity__not_throw_error ()
  {
    var professor  = new Professor { ProfessorId = 1, NonEntity = new object() };

    using var dbContext = await CreateTrackingContext();
    TrackEntityNavigations(dbContext, professor);
  }

  [Fact]
  internal async Task entity_with_non_mapped_collection_item__track_entity__not_throw_error ()
  {
    var professor  = new Professor { ProfessorId = 1, NonEntities = new [] { new object() } };

    using var dbContext = await CreateTrackingContext();
    TrackEntityNavigations(dbContext, professor);
  }

}