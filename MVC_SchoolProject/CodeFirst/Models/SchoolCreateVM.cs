using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Models
{
    public class SchoolCreateVM
    {
        [Required(ErrorMessage ="Lütfen isim kısmını doldurun!")]
        public string Name { get; set; }
    }
}
