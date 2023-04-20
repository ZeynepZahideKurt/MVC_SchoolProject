using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Models
{
    public class SchoolUpdateVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen isim kısmını doldurun!")]
        public string Name { get; set; }
    }
}
