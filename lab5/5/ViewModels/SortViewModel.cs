using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _5.ViewModels
{
    public enum SortState
    {
        No,
        AuthorNameAsc,
        AuthornameDesc,
        BookNameAsc,
        BookNameDesc,
        CustomerNameAsc,
        CustomerNameDesc,
        OrderExemplairsAsc,
        OrderExemplairsDesc
    }
    public class SortViewModel
    {
        public SortState AuthorNameSort { get; set; }
        public SortState BookNameSort { get; set; }
        public SortState CustomerNameSort { get; set; }
        public SortState OrderExemplairsSort { get; set; }

        public SortState CurrentState { get; set; }

        public SortViewModel(SortState sortOrder)
        {
            AuthorNameSort = sortOrder == SortState.AuthorNameAsc ? SortState.AuthornameDesc : SortState.AuthorNameAsc;
            BookNameSort = sortOrder == SortState.BookNameAsc ? SortState.BookNameDesc : SortState.BookNameAsc;
            CustomerNameSort = sortOrder == SortState.CustomerNameAsc ? SortState.CustomerNameDesc : SortState.CustomerNameAsc;
            OrderExemplairsSort = sortOrder == SortState.OrderExemplairsAsc ? SortState.OrderExemplairsDesc : SortState.OrderExemplairsAsc;
            CurrentState = sortOrder;
        }
    }
}
