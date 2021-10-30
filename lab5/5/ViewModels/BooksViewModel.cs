using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using _5.Models;

namespace _5.ViewModels
{
    public class BooksViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        [Display(Name ="Название книги")]
        public string BookName { get; set; }
        [Display(Name = "Тираж")]
        public int total { get; set; }
        [Display(Name = "дата выхода из печати")]
        public DateTime ExitDate { get; set; }
        [Display(Name = "Себестоимость")]
        public decimal BaseCost { get; set; }
        [Display(Name = "Розничная цена")]
        public decimal FinishCost { get; set; }
        [Display(Name = "Гонорар автора")]
        public decimal Salary { get; set; }
        [Display(Name = "Id автора")]
        public int AuthorId { get; set; }
    }
}
