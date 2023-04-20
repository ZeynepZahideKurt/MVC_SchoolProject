using CodeFirst.AppDbContext;
using CodeFirst.Entities.Concrete;
using CodeFirst.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Repositories.Concrete
{
    public class StudentRepository : GenericRepository<Student> ,IStudentRepository
    {
        private readonly ApplicationDbContext db;
        public StudentRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public List<Student> GetAllStudentsIncludeLessonAndSchool()
        {
           return db.Students.Include(a=>a.School).Include(b=>b.Lessons).ToList();
        }

        public Student GetStudentByIdIncludeLessonAndSchool(int id)
        {
            return db.Students.Include(a => a.School).Include(b => b.Lessons).Where(c => c.Id == id).FirstOrDefault();
        }
    }
}
