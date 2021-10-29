using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _5.Models
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
            Contracts = new HashSet<Contract>();
        }

        public int AuthorId { get; set; }
        public string Fio { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
