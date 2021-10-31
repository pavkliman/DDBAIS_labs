using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _5.Models;

namespace _5.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<FilterBookViewModel> Books { get; set; }
        public IEnumerable<FilterContractViewModel> Contracts { get; set; }
        public IEnumerable<FilterOrderViewModel> Orders { get; set; }
    }
}
