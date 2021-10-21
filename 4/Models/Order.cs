using System;
using System.Collections.Generic;

#nullable disable

namespace _4
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public string Ordername { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Finishdate { get; set; }
        public int? Exemplairs { get; set; }
        public decimal? Totalsum { get; set; }
        public int? CustomerId { get; set; }
        public int? BookId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
