using System;
using System.Collections.Generic;

#nullable disable

namespace _4
{
    public partial class OrdersView
    {
        public DateTime? Deadline { get; set; }
        public int OrderId { get; set; }
        public decimal? Sum { get; set; }
        public int CustomerId { get; set; }
    }
}
