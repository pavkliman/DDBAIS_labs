using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _5.ViewModels
{
    public class FilterContractViewModel
    {
        public int ContractId { get; set; }
        [Display(Name = "Дата заключения")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name = "Дата расторжения")]
        [DataType(DataType.Date)]
        public DateTime DeadLine { get; set; }
        [Display(Name = "Имя автора")]
        public string AuthorName { get; set; }
    }
}
