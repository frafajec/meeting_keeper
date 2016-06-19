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

        // GET: Dashboards/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Dashboard dashboard = _context.Dashboard.Single(m => m.id == id);
            if (dashboard == null)
            {
                return HttpNotFound();
            }

            return View(dashboard);
        }

        // GET: Dashboards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboards/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Dashboard dashboard)
        {
            if (ModelState.IsValid)
            {
                _context.Dashboard.Add(dashboard);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dashboard);
        }

        // GET: Dashboards/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Dashboard dashboard = _context.Dashboard.Single(m => m.id == id);
            if (dashboard == null)
            {
                return HttpNotFound();
            }
            return View(dashboard);
        }

        // POST: Dashboards/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Dashboard dashboard)
        {
            if (ModelState.IsValid)
            {
                _context.Update(dashboard);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dashboard);
        }

        // GET: Dashboards/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Dashboard dashboard = _context.Dashboard.Single(m => m.id == id);
            if (dashboard == null)
            {
                return HttpNotFound();
            }

            return View(dashboard);
        }

        // POST: Dashboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Dashboard dashboard = _context.Dashboard.Single(m => m.id == id);
            _context.Dashboard.Remove(dashboard);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
