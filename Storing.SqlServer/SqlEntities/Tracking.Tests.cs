
namespace Storing.SqlServer;

partial class SqlServerTests
{
  [TestMethod]
  public void entity_with_collection_item__track_entity__collection_item_tracked ()
  {
    var course  = new Course { CourseId = 0 };
    var student = new Student { StudentId = 1, Courses = [course] };

    using var dbContext = CreateTrackingContext();
    TrackEntityCollections(dbContext, student);

    Assert.AreEqual(EntityState.Added, dbContext.Entry(course).State);
  }

  [TestMethod]
  public void entity_with_navigation_item__track_entity__navigation_item_tracked ()
  {
    var professor = new Professor { ProfessorId = 0 };
    var course  = new Course { CourseId = 1, Professor = professor };

    using var dbContext = CreateTrackingContext();
    TrackEntityNavigations(dbContext, course);

    Assert.AreEqual(EntityState.Added, dbContext.Entry(professor).State);
  }

  [TestMethod]
  public void entity_with_empty_collection_item__track_entity__not_throw_error ()
  {
    var student = new Student { StudentId = 1, Courses = default };

    using var dbContext = CreateTrackingContext();
    TrackEntityCollections(dbContext, student);
  }

  [TestMethod]
  public void entity_with_empty_navigation_item__track_entity__not_throw_error ()
  {
    var course  = new Course { CourseId = 1, Professor = default };

    using var dbContext = CreateTrackingContext();
    TrackEntityNavigations(dbContext, course);
  }

  [TestMethod]
  public void entity_with_non_mapped_navigation_item__track_entity__not_throw_error ()
  {
    var professor  = new Professor { ProfessorId = 1, NonEntity = new object() };

    using var dbContext = CreateTrackingContext();
    TrackEntityNavigations(dbContext, professor);
  }

  [TestMethod]
  public void entity_with_non_mapped_collection_item__track_entity__not_throw_error ()
  {
    var professor  = new Professor { ProfessorId = 1, NonEntities = [new object()] };

    using var dbContext = CreateTrackingContext();
    TrackEntityNavigations(dbContext, professor);
  }
}