using _5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using _5.Data;
using _5.ViewModels;

namespace _5.Controllers
{
    public class HomeController : Controller
    {
        private readonly PublishingLabContext _db;
        public HomeController(PublishingLabContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var customers = _db.Customers.Take(15).ToList();
            var authors = _db.Authors.Take(15).ToList();
            List<FilterBookViewModel> books = _db.Books
                .OrderByDescending(d => d.Exitdate)
                .Select(t => new FilterBookViewModel
                {
                    BookId = t.BookId,
                    BookName = t.Name,
                    Total = (int)t.Total,
                    ExitDate = (DateTime)t.Exitdate,
                    BaseCost = (decimal)t.Basecost,
                    FinishCost = (decimal)t.Finishcost,
                    Salary = (decimal)t.Salary,
                    AuthorName = t.Author.Fio
                })
                .Take(15)
                .ToList();
            List<FilterContractViewModel> contracts = _db.Contracts
                .OrderByDescending(d => d.Date)
                .Select(t => new FilterContractViewModel 
                {
                    ContractId = t.ContractId, 
                    Date = (DateTime)t.Date,
                    DeadLine = (DateTime)t.Deadline,
                    AuthorName = t.Author.Fio
                })
                .Take(15)
                .ToList();
            List<FilterOrderViewModel> orders = _db.Orders
                .OrderByDescending(d => d.Startdate)
                .Select(t => new FilterOrderViewModel
                {
                    OrderId = t.OrderId,
                    OrderName = t.Ordername,
                    StartDate = (DateTime)t.Startdate,
                    FinishDate = (DateTime)t.Finishdate,
                    Exemplairs = (int)t.Exemplairs,
                    TotalSum = (decimal)t.Totalsum,
                    CustomerName = t.Customer.Customername,
                    BookName = t.Book.Name
                })
                .Take(15)
                .ToList();
            HomeViewModel homeViewModel = new HomeViewModel { Authors = authors, Customers = customers,
                Books = books, Contracts = contracts};
            return View(homeViewModel);
        }
    }
}
