using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _5.Models;

namespace _5.ViewModels
{
    public class OrdersViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public FilterOrderViewModel FilterOrderViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
