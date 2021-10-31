using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using _5.Models;
using _5.Models;

namespace _5.ViewModels
{
    public class ContractsViewModel
    {
        public IEnumerable<Contract> Contracts { get; set; }
        public FilterContractViewModel FilterContractViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
