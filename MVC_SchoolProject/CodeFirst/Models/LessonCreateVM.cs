using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Models
{
    public class LessonCreateVM
    {
        [Required(ErrorMessage ="Lütfen isim kısmını doldurunuz!")]
        public string Name { get; set; }
    }
}
