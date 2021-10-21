using System;
using System.Collections.Generic;

#nullable disable

namespace _4
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
