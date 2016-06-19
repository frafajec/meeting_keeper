using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using meeting_keeper.Models;

namespace meeting_keeper.Controllers
{
    public class DashboardsController : Controller
    {
        private ApplicationDbContext _context;

        public DashboardsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Dashboards
        public IActionResult Index()
        {
            //return View(_context.Dashboard.ToList());
            return View();
        }
        
    }
}
