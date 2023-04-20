using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Entities.Abstract
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Lütfen isim kısmını doldurun!")]
        public string Name { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
