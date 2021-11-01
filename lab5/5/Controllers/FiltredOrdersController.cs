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
        private FilterOrderViewModel _order = new FilterOrderViewModel
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
            var sessionOrders = HttpContext.Session.Get("Orders");
            var sessionSortState = HttpContext.Session.Get("SortState");
            if (sessionOrders != null)
                _order = Transformations.DictionaryToObject<FilterOrderViewModel>(sessionOrders);
            if ((sessionSortState != null))
                if ((sessionSortState.Count > 0) & (sortOrder == SortState.No)) sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState["sortOrder"]);

            IQueryable<Order> publishingLabContext = _context.Orders;
            publishingLabContext = Sort_Search(publishingLabContext, sortOrder, _order.BookName ?? "", _order.CustomerName ?? "");


            OrdersViewModel orders = new OrdersViewModel
            {
                Orders = publishingLabContext,
                SortViewModel = new SortViewModel(sortOrder),
                FilterOrderViewModel = _order
            };
            return View(orders);
        }
        [HttpPost]
        [SetToSession("Operation")] 
        public IActionResult Index(FilterOrderViewModel order)
        {
            var sessionSortState = HttpContext.Session.Get("SortState");
            var sortOrder = new SortState();
            if (sessionSortState.Count > 0)
                sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState["sortOrder"]);

            IQueryable<Order> publishingLabContext = _context.Orders;
            publishingLabContext = Sort_Search(publishingLabContext, sortOrder, _order.BookName ?? "", _order.CustomerName ?? "");

            OrdersViewModel orders = new OrdersViewModel
            {
                Orders = publishingLabContext,
                SortViewModel = new SortViewModel(sortOrder),
                FilterOrderViewModel = order
            };

            return View(orders);
        }
        private IQueryable<Order> Sort_Search(IQueryable<Order> orders, SortState sortOrder, string searchBookName, string searchCustomerName)
        {
            switch (sortOrder)
            {
                case SortState.BookNameAsc:
                    orders = orders.OrderBy(s => s.Book.Name);
                    break;
                case SortState.BookNameDesc:
                    orders = orders.OrderByDescending(s => s.Book.Name);
                    break;
                case SortState.CustomerNameAsc:
                    orders = orders.OrderBy(s => s.Customer.Customername);
                    break;
                case SortState.CustomerNameDesc:
                    orders = orders.OrderByDescending(s => s.Customer.Customername);
                    break;
            }
            orders = orders.Include(o => o.Book).Include(o => o.Customer)
                .Where(o => o.Customer.Customername.Contains(searchCustomerName ?? "")
                & o.Book.Name.Contains(searchBookName ?? ""));

            return orders;
        }
    }
}
