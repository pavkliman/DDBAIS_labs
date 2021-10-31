using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _5.Models;
using _5.ViewModels.Users;

namespace _5.ViewModels
{
    public class PublishingViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public FilterOrderViewModel FilterOrderViewModel { get; set; }
        public IEnumerable<Contract> Contracts { get; set; }
        public FilterContractViewModel FilterContractViewModel { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public FilterBookViewModel FilterBookViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
