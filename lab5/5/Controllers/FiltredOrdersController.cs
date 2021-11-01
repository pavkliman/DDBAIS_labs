using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _5.Data;
using _5.Models;
using _5.Infrastructure;
using _5.Infrastructure.Filters;
using _5.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace _5.Controllers
{
    public class FiltredOrdersController : Controller
    {

        private readonly PublishingLabContext _context;
        private FilterOrderViewModel _operation = new FilterOrderViewModel
        {
            CustomerName = "",
            BookName = ""
        };

        public FiltredOrdersController(PublishingLabContext context)
        {
            _context = context;
        }

        [SetToSession("SortState")]
        public IActionResult Index(SortState sortOrder)
        {
            var sessionOperation = HttpContext.Session.Get("Operation");
            var sessionSortState = HttpContext.Session.Get("SortState");
            if (sessionOperation != null)
                _operation = Transformations.DictionaryToObject<FilterOrderViewModel>(sessionOperation);
            if ((sessionSortState != null))
                if ((sessionSortState.Count > 0) & (sortOrder == SortState.No)) sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState["sortOrder"]);
            return View();
        }
    }
}
