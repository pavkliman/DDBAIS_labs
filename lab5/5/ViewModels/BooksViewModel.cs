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
       public FilterBookViewModel FilterBookViewModel { get; set; }
       public PageViewModel PageViewModel { get; set; }
       public SortViewModel SortViewModel { get; set; }
    }
}
