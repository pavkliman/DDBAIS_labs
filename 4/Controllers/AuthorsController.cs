using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _4.Data;

namespace _4.Controllers
{
    public class AuthorsController : Controller
    {
        private publishing_houseContext db;

        public AuthorsController(publishing_houseContext context)
        {
            db = context;
        }

        [ResponseCache(CacheProfileName = "CacheProfile")]
        public IActionResult Index()
        {
            return View(db.Authors.Take(20).ToList());
        }
    }
}
