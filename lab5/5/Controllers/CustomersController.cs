using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _5.Data;
using _5.Models;
using _5.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace _5.Controllers
{
    public class CustomersController : Controller
    {
        private readonly int pageSize = 10;

        private readonly PublishingLabContext _context;

        public CustomersController(PublishingLabContext context)
        {
            _context = context;
        }
        public IActionResult Index(string CustomerName, SortState sortOrder, int page = 1)
        {
            IQueryable<Customer> publishingLabContext = _context.Customers;
            publishingLabContext = Sort_Search(publishingLabContext, sortOrder, CustomerName ?? "");

            var count = publishingLabContext.Count();
            publishingLabContext = publishingLabContext.Skip((page - 1) * pageSize).Take(pageSize);

            CustomersViewModel customers = new CustomersViewModel
            {
                Customers = publishingLabContext,
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                CustomerName = CustomerName
            };
            return View(customers);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .SingleOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CustomerName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,CustomerName")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuelExists(customer.CustomerId))
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
            return View(customer);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .SingleOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.CustomerId == id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool FuelExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
        private IQueryable<Customer> Sort_Search(IQueryable<Customer> customers, SortState sortOrder, string CustomerName)
        {
            switch (sortOrder)
            {
                case SortState.CustomerNameAsc:
                    customers = customers.OrderBy(s => s.CustomerId);
                    break;
                case SortState.CustomerNameDesc:
                    customers = customers.OrderByDescending(s => s.CustomerId);
                    break;
            }
            customers = customers.Where(o => o.Customername.Contains(CustomerName ?? ""));
            return customers;
        }
    }
}
