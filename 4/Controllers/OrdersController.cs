using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _4.Data;

namespace _4.Controllers
{
    public class OrdersController : Controller
    {
        private publishing_houseContext db;

        public OrdersController(publishing_houseContext context)
        {
            db = context;
        }

        [ResponseCache(CacheProfileName = "CacheProfile")]
        public IActionResult Index()
        {
            return View(db.Orders.Include(n => n.Customer).Take(20).ToList());
        }
    }
}
