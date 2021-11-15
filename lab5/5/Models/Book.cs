using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _5.Models
{
    public partial class Book
    {
        public Book()
        {
            Orders = new HashSet<Order>();
        }
        [Display(Name = "Код книги")]
        public int BookId { get; set; }
        [Display(Name = "Название книги")]
        public string Name { get; set; }
        [Display(Name = "Тираж")]
        public int? Total { get; set; }
        [Display(Name = "Дата выхода из печати")]
        public DateTime? Exitdate { get; set; }
        [Display(Name = "Себестоимость")]
        public decimal? Basecost { get; set; }
        [Display(Name = "Розничная цена")]
        public decimal? Finishcost { get; set; }
        [Display(Name = "Гонорара автора")]
        public decimal? Salary { get; set; }
        [Display(Name = "Автор")]
        public int? AuthorId { get; set; }

        public virtual Author Author { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
