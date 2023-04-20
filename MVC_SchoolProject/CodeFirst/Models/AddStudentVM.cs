using CodeFirst.Entities.Concrete;
using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Models
{
    public class AddStudentVM
    {
        [Required(ErrorMessage ="Lütfen name alanını doldurunuz!")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Lütfen class alanını doldurunuz!")]
        public string Class { get; set; }
        public int SchoolId { get; set; }
        public int LessonId { get; set; }
        public List<School> Schools { get; set; }
        public List<Lesson> Lessons { get; set; }
        public int Id { get; set; }
    }
}
