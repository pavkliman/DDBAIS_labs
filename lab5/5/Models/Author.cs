using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5.Models
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
            Contracts = new HashSet<Contract>();
        }
        [Display(Name = "Код автора")]
        public int AuthorId { get; set; }
        [Display(Name = "Имя автора")]
        public string Fio { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
