﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _5.Controllers
{
    public class FiltredContractsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
