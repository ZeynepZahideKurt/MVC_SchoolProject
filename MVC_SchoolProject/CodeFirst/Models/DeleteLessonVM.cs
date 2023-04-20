using CodeFirst.Entities.Concrete;

namespace CodeFirst.Models
{
    public class DeleteLessonVM
    {
        public School School { get; set; }
        public int LessonId { get; set; }
        public List<Lesson> Lessons { get; set; }
    }
}
