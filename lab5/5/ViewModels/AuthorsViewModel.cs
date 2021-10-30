using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using _5.Models;

namespace _5.ViewModels
{
    public class AuthorsViewModel
    {
        public IEnumerable<Author> Authors { get; set; }
        [Display(Name = "Имя автора")]
        public string AuthorName { get; set; }

        public PageViewModel PageViewModel { get; set; }
        // Порядок сортировки
        public SortViewModel SortViewModel { get; set; }
    }
}
