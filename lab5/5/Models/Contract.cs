using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _5.Models
{
    public partial class Contract
    {
        [Display(Name = "Код контракта")]
        public int ContractId { get; set; }
        [Display(Name = "Дата заключения")]
        public DateTime? Date { get; set; }
        [Display(Name = "Дата расторжения")]
        public DateTime? Deadline { get; set; }
        [Display(Name = "Автор")]
        public int? AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }
}
