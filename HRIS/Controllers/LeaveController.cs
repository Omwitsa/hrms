using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRIS.Controllers
{
    public class LeaveController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
