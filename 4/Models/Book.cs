using System;
using System.Collections.Generic;

#nullable disable

namespace _4
{
    public partial class Book
    {
        public Book()
        {
            Orders = new HashSet<Order>();
        }

        public int BookId { get; set; }
        public string Name { get; set; }
        public int? Total { get; set; }
        public DateTime? Exitdate { get; set; }
        public decimal? Basecost { get; set; }
        public decimal? Finishcost { get; set; }
        public decimal? Salary { get; set; }
        public int? AuthorId { get; set; }

        public virtual Author Author { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
