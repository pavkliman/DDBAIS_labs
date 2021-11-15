using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _5.Models
{
    public partial class Order
    {
        [Display(Name = "Код заказа")]
        public int OrderId { get; set; }
        [Display(Name = "Наименование заказа")]
        public string Ordername { get; set; }
        [Display(Name = "Дата получения")]
        public DateTime? Startdate { get; set; }
        [Display(Name = "Дата исполнения")]
        public DateTime? Finishdate { get; set; }
        [Display(Name = "Число экземпляров")]
        public int? Exemplairs { get; set; }
        [Display(Name = "Сумма заказа")]
        public decimal? Totalsum { get; set; }
        [Display(Name = "Заказчик")]
        public int? CustomerId { get; set; }
        [Display(Name = "Книга")]
        public int? BookId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
