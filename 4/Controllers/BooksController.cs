using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _4.Data;
using Microsoft.EntityFrameworkCore;

namespace _4.Controllers
{
    public class BooksController : Controller
    {
        private publishing_houseContext db;

        public BooksController(publishing_houseContext context)
        {
            db = context;
        }

        [ResponseCache(CacheProfileName = "CacheProfile")]
        public IActionResult Index()
        {
            return View(db.Books.Include(s => s.Author).Take(20).ToList());
        }
    }
}
