using System;
using System.Collections.Generic;

#nullable disable

namespace _4
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string Customername { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
