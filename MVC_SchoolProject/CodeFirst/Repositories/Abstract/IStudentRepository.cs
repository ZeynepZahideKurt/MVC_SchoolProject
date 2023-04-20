using CodeFirst.Entities.Concrete;

namespace CodeFirst.Repositories.Abstract
{
    public interface IStudentRepository : IRepository<Student>
    {
        List<Student> GetAllStudentsIncludeLessonAndSchool();
        Student GetStudentByIdIncludeLessonAndSchool(int id);
    }
}
