using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _5.ViewModels
{
    public class FilterBookViewModel
    {
        public int BookId { get; set; }
        [Display(Name = "Название книги")]
        public string BookName { get; set; }
        [Display(Name = "Тираж")]
        public int Total { get; set; }
        [Display(Name = "Дата выхода из печати")]
        [DataType(DataType.Date)]
        public DateTime ExitDate { get; set; }
        [Display(Name = "Себестоимость")]
        public decimal BaseCost { get; set; }
        [Display(Name = "Розничная цена")]
        public decimal FinishCost { get; set; }
        [Display(Name = "Гонорар автора")]
        public decimal Salary { get; set; }
        [Display(Name = "имя автора")]
        public string AuthorName { get; set; }
    }
}
