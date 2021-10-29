using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _5.Models
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
