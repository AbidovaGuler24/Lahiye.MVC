﻿using Microsoft.AspNetCore.Mvc;

namespace Lahiye.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class Dashboard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
