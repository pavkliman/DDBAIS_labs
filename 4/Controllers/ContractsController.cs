using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _4.Data;

namespace _4.Controllers
{
    public class ContractsController : Controller
    {
        private publishing_houseContext db;

        public ContractsController(publishing_houseContext context)
        {
            db = context;
        }

        [ResponseCache(CacheProfileName = "CasheProfile")]
        public IActionResult Index()
        {
            return View(db.Contracts.Include(t => t.Author).Take(20).ToList());
        }
    }
}
