using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace meeting_keeper.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Subscribe()
        {
            ViewData["Message"] = "Your subscription page.";

            return View();
        }

        public IActionResult Cookie()
        {
            ViewData["Message"] = "Your cookie page.";

            return View();
        }

        public IActionResult Terms()
        {
            ViewData["Message"] = "Your terms page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
