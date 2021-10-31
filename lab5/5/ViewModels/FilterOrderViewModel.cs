using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _5.ViewModels
{
    public class FilterOrderViewModel
    {
        public int OrderId { get; set; }
        [Display(Name = "Имя заказчика")]
        public string OrderName { get; set; }
        [Display(Name = "Дата поступления заказа")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Дата исполнения заказа")]
        [DataType(DataType.Date)]
        public DateTime FinishDate { get; set; }
        [Display(Name = "Количество экземпляров")]
        public int Exemplairs { get; set; }
        [Display(Name = "Сумма выплат")]
        public decimal TotalSum { get; set; }
        [Display(Name = "Имя зазчика")]
        public string CustomerName { get; set; }
        [Display(Name = "Название книги")]
        public string BookName { get; set; }
    }
}
