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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace _5.Controllers
{
    public class OrdersController : Controller
    {
        private readonly PublishingLabContext _context;
        private readonly int pageSize = 15;  

        public OrdersController(PublishingLabContext context)
        {
            _context = context;
        }

        [SetToSession("Operation")]
        public IActionResult Index(FilterOrderViewModel order, SortState sortOrder, int page = 1)
        {
            if (order.BookName == null & order.CustomerName == null)
            {
                var sessionOrder = HttpContext.Session.Get("Order");
                if (sessionOrder != null)
                    order = Transformations.DictionaryToObject<FilterOrderViewModel>(sessionOrder);
            }

            IQueryable<Order> publishingLabContext = _context.Orders;
            publishingLabContext = Sort_Search(publishingLabContext, sortOrder, order.CustomerName ?? "", order.BookName ?? "");

            var count = publishingLabContext.Count();
            publishingLabContext = publishingLabContext.Skip((page - 1) * pageSize).Take(pageSize);

            OrdersViewModel orders = new OrdersViewModel
            {
                Orders = publishingLabContext,
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterOrderViewModel = order
            };
            return View(orders);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Book)
                .SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName");
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderName,StartDate,FinishDate,Exemplairs,TotalSum,CustomerId,BookId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", order.CustomerId);
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookName", order.BookId);
            return View(order);
        }

        // GET: Operations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", order.CustomerId);
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookName", order.BookId);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderName,StartDate,FinishDate,Exemplairs,TotalSum,CustomerId,BookId")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", order.CustomerId);
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookName", order.BookId);
            return View(order);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Book)
                .SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.OrderId == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
        private IQueryable<Order> Sort_Search(IQueryable<Order> orders, SortState sortOrder, string searchCustomerName, string searchBookName)
        {
            switch (sortOrder)
            {
                case SortState.CustomerNameAsc:
                    orders = orders.OrderBy(s => s.Customer.Customername);
                    break;
                case SortState.CustomerNameDesc:
                    orders = orders.OrderByDescending(s => s.Customer.Customername);
                    break;
                case SortState.BookNameAsc:
                    orders = orders.OrderBy(s => s.Book.Name);
                    break;
                case SortState.BookNameDesc:
                    orders = orders.OrderByDescending(s => s.Book.Name);
                    break;
            }
            orders = orders.Include(o => o.Customer).Include(o => o.Book)
                .Where(o => o.Customer.Customername.Contains(searchCustomerName ?? "")
                & o.Book.Name.Contains(searchBookName ?? ""));

            return orders;
        }
    }
}
