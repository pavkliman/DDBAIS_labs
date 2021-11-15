using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _5.Models
{
    public partial class  Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        [Display(Name = "Код заказчика")]
        public int CustomerId { get; set; }
        [Display(Name = "Код заказа")]
        public string Customername { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
