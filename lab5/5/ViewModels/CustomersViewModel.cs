using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _5.Models;
using System.ComponentModel.DataAnnotations;

namespace _5.ViewModels
{
    public class CustomersViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
        [Display(Name = "Имя заказчика")]
        public string CustomerName { get; set; }

        public PageViewModel PageViewModel { get; set; }
        // Порядок сортировки
        public SortViewModel SortViewModel { get; set; }
    }
}
